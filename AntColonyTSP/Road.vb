Imports System.Diagnostics

<DebuggerDisplay("Distance: {Distance}, Pheromone level: {PheromoneLevel}")> _
Public Class Road

    Private Const α As Double = World.α
    Private Const β As Double = World.β
    Private m_Distance As Double
    Private m_PheromoneLevel As Double

    Public ReadOnly Property Distance() As Double
        Get
            Return m_Distance
        End Get
    End Property

    Public Property PheromoneLevel() As Double
        Get
            Return m_PheromoneLevel
        End Get
        Set(ByVal value As Double)
            m_PheromoneLevel = value
        End Set
    End Property

    Public ReadOnly Property WeighedValue() As Double
        Get
            Return Distance ^ α * PheromoneLevel ^ β
        End Get
    End Property

    Public Sub New(ByVal distance As Double)
        m_Distance = distance
    End Sub
End Class
