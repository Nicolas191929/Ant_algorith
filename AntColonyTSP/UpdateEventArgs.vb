Public Class UpdateEventArgs
    Inherits EventArgs

    Private ReadOnly m_CurrentIteration As Integer
    Private ReadOnly m_SuccessfulIterations As Integer
    Private ReadOnly m_Failures As Integer
    Private ReadOnly m_CurrentBestValue As Double
    Private ReadOnly m_LastValue As Double
    Private ReadOnly m_BestTour As IEnumerable(Of City)

    Public ReadOnly Property CurrentIteration() As Integer
        Get
            Return m_CurrentIteration
        End Get
    End Property

    Public ReadOnly Property SuccessfulIterations() As Integer
        Get
            Return m_SuccessfulIterations
        End Get
    End Property

    Public ReadOnly Property Failures() As Integer
        Get
            Return m_Failures
        End Get
    End Property

    Public ReadOnly Property CurrentBestValue() As Double
        Get
            Return m_CurrentBestValue
        End Get
    End Property

    Public ReadOnly Property LastValue() As Double
        Get
            Return m_LastValue
        End Get
    End Property

    Public ReadOnly Property BestTour() As IEnumerable(Of City)
        Get
            Return m_BestTour
        End Get
    End Property

    Public Sub New( _
        ByVal current_iteration As Integer, _
        ByVal successful_iterations As Integer, _
        ByVal failures As Integer, _
        ByVal current_best_value As Double, _
        ByVal last_value As Double, _
        ByVal best_tour As IEnumerable(Of City) _
    )
        m_CurrentIteration = current_iteration
        m_SuccessfulIterations = successful_iterations
        m_Failures = failures
        m_CurrentBestValue = current_best_value
        m_LastValue = last_value
        m_BestTour = best_tour
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format( _
            "Iteration: {1}{0}w/o change: {2}{0}failed: {3}{0}value: {4:f1}{0}last: {5:f1}", _
            ControlChars.Tab, CurrentIteration, SuccessfulIterations, Failures, CurrentBestValue, LastValue _
        )
    End Function

End Class
