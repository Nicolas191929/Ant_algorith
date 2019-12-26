Public Class World

    Public Const α As Double = -1.5
    Public Const β As Double = 1.5
    Public Const NumberOfAnts As Integer = 100
    Public Const InitialPheromoneValue As Double = 1
    Public Const PheromoneDecayFactor As Double = 0.1
    Public Const MaxIterations As Integer = 20

    Public Event Update(ByVal sender As World, ByVal e As UpdateEventArgs)

    Private Shared ReadOnly m_Random As New Random()
    Private ReadOnly m_Cities As List(Of City)
    Private ReadOnly m_Roads As List(Of Road)
    Private ReadOnly m_WorstTourValue As Double
    Private ReadOnly m_WeightingFactor As Double

    Public ReadOnly Property Cities() As IList(Of City)
        Get
            Return m_Cities
        End Get
    End Property

    Public ReadOnly Property Roads() As IEnumerable(Of Road)
        Get
            Return m_Roads
        End Get
    End Property

    Public Sub New(ByVal prototype As WorldBuilder)
        Me.New(prototype.Cities, prototype.Roads)
    End Sub

    Public Sub New(ByVal cities As IEnumerable(Of City), ByVal roads As IEnumerable(Of Road))
        m_Cities = New List(Of City)(cities)
        m_Roads = New List(Of Road)(roads)
        m_WorstTourValue = Aggregate road In roads Into Sum(road.Distance)
        m_WeightingFactor = cities.Count / roads.Count
    End Sub

    Public Function FindTour() As IEnumerable(Of City)
        Dim best_tour As IList(Of City) = Nothing
        Dim best_tour_value = m_WorstTourValue
        Dim iterations = 0
        Dim iterations_without_change = 0
        Dim number_of_failures = 0
        Dim last_value As Double
        Dim num_success As Integer

        For Each road In Roads
            road.PheromoneLevel = InitialPheromoneValue
        Next

        Dim ant_pheromone_capacity = 0.2
        Dim overall_decay_value = PheromoneDecayFactor * InitialPheromoneValue * m_Roads.Count

        Do While iterations_without_change < MaxIterations
            iterations += 1
            Dim ants As New List(Of Ant)(NumberOfAnts)
            Dim ant_success As New Dictionary(Of Ant, Boolean)(NumberOfAnts)

            For i = 1 To NumberOfAnts
                ' We have to pick a random starting city in order to distribute
                ' the pheromones for the last route of a tour randomly!
                Dim rnd_index = m_Random.Next(Cities.Count)
                ants.Add(New Ant(Cities(rnd_index), ant_pheromone_capacity))
            Next

            last_value = 0
            num_success = 0

            For Each ant In ants
                Dim success = ant.SearchTour() AndAlso ant.VisitedCities.Count = m_Cities.Count
                ant_success(ant) = success

                If success Then
                    ' We use a delta to compensate mathematical instabilities in
                    ' floating point operations.
                    Dim delta = ant.TourValue - best_tour_value

                    ' We're talking about pixels here! 0.01 is small enough.
                    If Math.Abs(delta) <= 0.01 Then
                        iterations_without_change += 1
                    ElseIf delta < 0 Then
                        best_tour_value = ant.TourValue
                        best_tour = ant.VisitedCities
                        iterations_without_change = 0
                    ElseIf delta <= best_tour_value * 0.01 Then
                        ' The difference is too small to yield correct phermone
                        ' values (that is: the tour is nearly as good as the
                        ' current best one): We give up.
                        iterations_without_change += 1
                    Else
                        iterations_without_change = 0
                    End If

                    last_value += ant.TourValue
                    num_success += 1
                Else
                    iterations_without_change = 0
                    number_of_failures += 1
                End If
            Next

            last_value /= num_success

            For Each road In Roads
                ' No decay for the currently best tour!
                ' This is an extension to the algorithm to ensure its termination.

                Dim road_is_in_best_tour = False

                If best_tour IsNot Nothing AndAlso best_tour.Count > 0 Then
                    Dim first = best_tour(0)

                    For i = 1 To best_tour.Count - 1
                        If first.Roads(best_tour(i)) Is road Then
                            road_is_in_best_tour = True
                            Exit For
                        End If

                        first = best_tour(i)
                    Next

                    If best_tour(best_tour.Count - 1).Roads(best_tour(0)) Is road Then
                        road_is_in_best_tour = True
                    End If
                End If

                If Not road_is_in_best_tour Then
                    UpdatePheromoneLevel(road)
                End If
            Next

            Dim individual_pheromone_level = overall_decay_value

            For Each annotated_ant In ant_success
                If annotated_ant.Value Then
                    annotated_ant.Key.Pheromones = individual_pheromone_level
                    Dim cities = annotated_ant.Key.VisitedCities
                    Dim tour_bonus = TourPheromoneBonus(annotated_ant.Key)
                    For i = 1 To cities.Count - 1
                        Dim road = cities(i - 1).Roads(cities(i))
                        road.PheromoneLevel += tour_bonus
                    Next

                    Dim last_road = cities(cities.Count - 1).Roads(cities(0))
                    last_road.PheromoneLevel += tour_bonus
                End If
            Next

            RaiseUpdate( _
                New UpdateEventArgs( _
                    iterations, iterations_without_change, number_of_failures, _
                    best_tour_value, last_value, best_tour _
                ) _
            )
        Loop

        Return best_tour
    End Function

    Private Sub RaiseUpdate(ByVal args As UpdateEventArgs)
        RaiseEvent Update(Me, args)
    End Sub

    Private Sub UpdatePheromoneLevel(ByVal road As Road)
        Const RemainingPheromoneFactor As Double = 1.0 - PheromoneDecayFactor
        road.PheromoneLevel = road.PheromoneLevel * RemainingPheromoneFactor
    End Sub

    Private Function TourPheromoneBonus(ByVal ant As Ant) As Double
        ' We penalize long tours and try to get the worst possible tour
        ' to yield 0 pheromone.
        ' The square tries to "stretch" the range of possible bonuses.
        Return (ant.Pheromones * (m_WorstTourValue / ant.TourValue - 1)) ^ 2
    End Function

End Class
