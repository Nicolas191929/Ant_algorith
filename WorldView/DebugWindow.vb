Public Class DebugWindow

    Public Shared ReadOnly Property Instance() As DebugWindow
        Get
            Static _instance As New DebugWindow()
            Return _instance
        End Get
    End Property

    Public Sub AddItem(ByVal text As String)
        If DebugList.Items.Count > 1000 Then
            DebugList.Items.RemoveAt(0)
        End If
        DebugList.Items.Add(text)
        DebugList.SetSelected(DebugList.Items.Count - 1, True)
    End Sub

    Public Sub Clear()
        DebugList.Items.Clear()
    End Sub

    Private Sub DebugWindow_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DebugWindow_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        DebugList.UseCustomTabOffsets = True
    End Sub
End Class