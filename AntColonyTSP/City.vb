Imports System.Diagnostics

<DebuggerDisplay("{Name}")> _
Public Class City

    Private ReadOnly m_Name As String
    Private ReadOnly m_Roads As Dictionary(Of City, Road)

    Public ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

    Public ReadOnly Property NeighbourCities() As IEnumerable(Of City)
        Get
            Return m_Roads.Keys
        End Get
    End Property

    Public ReadOnly Property Roads(ByVal [to] As City) As Road
        Get
            Dim ret As Road = Nothing
            m_Roads.TryGetValue([to], ret)
            Return ret
        End Get
    End Property

    Public Sub New(ByVal name As String)
        m_Name = name
        m_Roads = New Dictionary(Of City, Road)()
    End Sub

    Friend Sub AddRoad(ByVal road As Road, ByVal other_city As City)
        m_Roads.Add(other_city, road)
    End Sub
End Class
