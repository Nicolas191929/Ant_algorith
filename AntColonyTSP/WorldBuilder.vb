Public Class WorldBuilder

    Private ReadOnly m_Cities As New Dictionary(Of String, City)()
    Private ReadOnly m_Roads As New List(Of Road)()

    Friend ReadOnly Property Cities() As IEnumerable(Of City)
        Get
            Return m_Cities.Values
        End Get
    End Property

    Friend ReadOnly Property Roads() As IEnumerable(Of Road)
        Get
            Return m_Roads
        End Get
    End Property

    Public Function AddCity(ByVal name As String) As City
        Dim city As New City(name)
        m_Cities.Add(name, city)
        Return city
    End Function

    Public Sub AddCity(ByVal city As City)
        m_Cities.Add(city.Name, city)
    End Sub

    Public Sub AddCities(ByVal names As IEnumerable(Of String))
        For Each name In names
            AddCity(name)
        Next
    End Sub

    Public Sub AddCities(ByVal ParamArray names As String())
        For Each name In names
            AddCity(name)
        Next
    End Sub

    Public Sub AddCities(ByVal cities As IEnumerable(Of City))
        For Each city In cities
            AddCity(city)
        Next
    End Sub

    Public Sub AddCities(ByVal ParamArray cities As City())
        For Each city In cities
            AddCity(city)
        Next
    End Sub

    Public Function AddRoad(ByVal distance As Double, ByVal from As City, ByVal [to] As City) As Road
        Dim road As New Road(distance)
        from.AddRoad(road, [to])
        [to].AddRoad(road, from)
        m_Roads.Add(road)
        Return road
    End Function

    Public Function AddRoad(ByVal distance As Double, ByVal from As String, ByVal [to] As String) As Road
        Dim from_city As City = Nothing

        If Not m_Cities.TryGetValue(from, from_city) Then
            from_city = AddCity(from)
        End If

        Dim to_city As City = Nothing
        If Not m_Cities.TryGetValue([to], to_city) Then
            to_city = AddCity([to])
        End If

        Return AddRoad(distance, from_city, to_city)
    End Function

End Class
