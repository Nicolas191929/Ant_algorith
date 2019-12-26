Public Class Ant

    Private Shared ReadOnly m_Random As New Random()
    Private m_Pheromones As Double
    Private ReadOnly m_VisitedCities As List(Of City)
    Private m_CurrentPosition As City
    Private m_TourValue As Double

    Private Shared ReadOnly Property Random() As Random
        Get
            Return m_Random
        End Get
    End Property

    Public ReadOnly Property VisitedCities() As IList(Of City)
        Get
            Return m_VisitedCities
        End Get
    End Property

    Public Property CurrentPosition() As City
        Get
            Return m_CurrentPosition
        End Get
        Private Set(ByVal value As City)
            m_CurrentPosition = value
        End Set
    End Property

    Public Property Pheromones() As Double
        Get
            Return m_Pheromones
        End Get
        Set(ByVal value As Double)
            m_Pheromones = value
        End Set
    End Property

    Public Property TourValue() As Double
        Get
            Return m_TourValue
        End Get
        Private Set(ByVal value As Double)
            m_TourValue = value
        End Set
    End Property

    Public Sub New(ByVal start_position As City, ByVal pheromones As Double)
        m_CurrentPosition = start_position
        m_Pheromones = pheromones
        m_VisitedCities = New List(Of City)()
        VisitedCities.Add(start_position)
    End Sub

    Public Function SearchTour() As Boolean
        TourValue = 0.0

        Do While TravelOn()
        Loop

        Dim closing_road = CurrentPosition.Roads(VisitedCities.First())
        If closing_road IsNot Nothing Then
            TourValue += closing_road.Distance
            Return True
        End If

        Return False
    End Function

    Private Function TravelOn() As Boolean
        Dim next_city = GetNextCity()
        If next_city Is Nothing Then
            Return False
        End If

        CurrentPosition = next_city
        TourValue += VisitedCities.Last().Roads(CurrentPosition).Distance
        VisitedCities.Add(CurrentPosition)
        Return True
    End Function

    Private Function GetNextCity() As City
        Dim city_weights As New Dictionary(Of City, Double)()
        Dim sum_of_weights = 0.0

        For Each city In CurrentPosition.NeighbourCities
            If Not VisitedCities.Contains(city) Then
                Dim weight = CurrentPosition.Roads(city).WeighedValue
                city_weights.Add(city, weight)
                sum_of_weights += weight
            End If
        Next

        Dim rnd = Random.NextDouble()
        Dim sum = 0.0

        For Each pair In city_weights
            sum += pair.Value / sum_of_weights
            If sum >= rnd Then
                Return pair.Key
            End If
        Next

        Return Nothing
    End Function
End Class
