<DebuggerDisplay("City {Name}")> _
Public Class City

    Public Const Radius As Integer = 6

    Private ReadOnly m_Location As Point
    Private ReadOnly m_Name As String
    Private m_TspCity As AntColonyTSP.City

    Public ReadOnly Property Location() As Point
        Get
            Return m_Location
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

    Public Property TspCity() As AntColonyTSP.City
        Get
            Return m_TspCity
        End Get
        Set(ByVal value As AntColonyTSP.City)
            m_TspCity = value
        End Set
    End Property

    Public Sub New(ByVal location As Point, ByVal name As String, ByVal tsp_city As AntColonyTSP.City)
        m_Location = location
        m_Name = name
        m_TspCity = tsp_city
    End Sub

    Public Shared Function Distance(ByVal pt1 As Point, ByVal pt2 As Point) As Double
        Return Math.Sqrt((pt1.X - pt2.X) ^ 2 + (pt1.Y - pt2.Y) ^ 2)
    End Function

    Public Shared Function Distance(ByVal line As KeyValuePair(Of Point, Point)) As Double
        Return Distance(line.Key, line.Value)
    End Function

End Class
