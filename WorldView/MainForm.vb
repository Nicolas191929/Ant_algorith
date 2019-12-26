Imports PtPair = System.Collections.Generic.KeyValuePair(Of System.Drawing.Point, System.Drawing.Point)

Public Class MainForm

    Private m_Map As Map
    Private m_TspThread As Threading.Thread

    Private Sub MapPicture_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MapPicture.MouseUp
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                If m_Map.FindCity(e.Location) Is Nothing Then
                    m_Map.AddCity(e.Location)
                End If
            Case Windows.Forms.MouseButtons.Right
                m_Map.RemoveCity(e.Location)
        End Select
    End Sub

    Private Sub StartButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StartButton.Click
        StopTsp()

        If m_Map.CityCount < 4 Then
            MsgBox("At least 4 cities are needed.", MsgBoxStyle.Information, "Error")
            Return
        End If

        StopButton.Enabled = True
        StartButton.Enabled = False
        m_TspThread = New Threading.Thread(AddressOf StartTsp)
        m_TspThread.IsBackground = True
        m_TspThread.Start()
    End Sub

    Private Sub StopButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StopButton.Click
        StopTsp()
    End Sub

    Private Sub ClearButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ClearButton.Click
        StopTsp()
        m_Map.Clear()
    End Sub

    Private Sub ShowLabelsCheck_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ShowLabelsCheck.CheckedChanged
        m_Map.ShowLabels = ShowLabelsCheck.Checked
    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        OpenPictureDialog.Filter = SupportedPictureFilters()
        StopButton.Enabled = False
        m_Map = New Map(MapPicture)
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        StopTsp()
    End Sub

    Private Sub ShowDebugCheck_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ShowDebugCheck.CheckedChanged
        DebugWindow.Instance.Visible = ShowDebugCheck.Checked
    End Sub

    Private Sub LoadBackgroundButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoadBackgroundButton.Click
        If LoadBackgroundButton.Text = "Clear pic" Then
            m_Map.BackgroundPicture = Nothing
            LoadBackgroundButton.Text = "Picture …"
        Else
            If OpenPictureDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                m_Map.BackgroundPicture = Bitmap.FromFile(OpenPictureDialog.FileName)
                LoadBackgroundButton.Text = "Clear pic"
            End If
        End If
    End Sub

    Private Sub StartTsp()
        Invoke(New Action(AddressOf DebugWindow.Instance.Clear))

        Dim w = m_Map.ConstructTsp()
        AddHandler w.Update, AddressOf World_Update
        Dim best_tour = w.FindTour()
        Invoke(New Action(Of IEnumerable(Of AntColonyTSP.City))(AddressOf m_Map.DrawBestTour), best_tour)
        Invoke(New Action(AddressOf StopTsp))
    End Sub

    Private Sub StopTsp()
        StopButton.Enabled = False
        If m_TspThread IsNot Nothing AndAlso m_TspThread.IsAlive Then
            m_TspThread.Abort()
        End If
        StartButton.Enabled = True
    End Sub

    Private Sub World_Update(ByVal sender As World, ByVal e As UpdateEventArgs)
        If InvokeRequired Then
            Invoke(New Action(Of World, UpdateEventArgs)(AddressOf World_Update), sender, e)
            Threading.Thread.Sleep(100)
            Return
        End If

        m_Map.Redraw(sender, e)
        DebugWindow.Instance.AddItem(e.ToString())
    End Sub

End Class
