<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MainContainer = New System.Windows.Forms.SplitContainer
        Me.MapPicture = New System.Windows.Forms.PictureBox
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.StartButton = New System.Windows.Forms.Button
        Me.StopButton = New System.Windows.Forms.Button
        Me.ClearButton = New System.Windows.Forms.Button
        Me.ShowLabelsCheck = New System.Windows.Forms.CheckBox
        Me.ShowDebugCheck = New System.Windows.Forms.CheckBox
        Me.LoadBackgroundButton = New System.Windows.Forms.Button
        Me.OpenPictureDialog = New System.Windows.Forms.OpenFileDialog
        Me.MainContainer.Panel1.SuspendLayout()
        Me.MainContainer.Panel2.SuspendLayout()
        Me.MainContainer.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainContainer
        '
        Me.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.MainContainer.IsSplitterFixed = True
        Me.MainContainer.Location = New System.Drawing.Point(0, 0)
        Me.MainContainer.Name = "MainContainer"
        Me.MainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'MainContainer.Panel1
        '
        Me.MainContainer.Panel1.Controls.Add(Me.MapPicture)
        '
        'MainContainer.Panel2
        '
        Me.MainContainer.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.MainContainer.Size = New System.Drawing.Size(812, 571)
        Me.MainContainer.SplitterDistance = 545
        Me.MainContainer.SplitterWidth = 1
        Me.MainContainer.TabIndex = 0
        '
        'MapPicture
        '
        Me.MapPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MapPicture.Location = New System.Drawing.Point(0, 0)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(812, 545)
        Me.MapPicture.TabIndex = 0
        Me.MapPicture.TabStop = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.StartButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.StopButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.ClearButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.LoadBackgroundButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.ShowLabelsCheck)
        Me.FlowLayoutPanel1.Controls.Add(Me.ShowDebugCheck)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(812, 25)
        Me.FlowLayoutPanel1.TabIndex = 3
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(0, 0)
        Me.StartButton.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(80, 25)
        Me.StartButton.TabIndex = 0
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.Location = New System.Drawing.Point(85, 0)
        Me.StopButton.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(80, 25)
        Me.StopButton.TabIndex = 1
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'ClearButton
        '
        Me.ClearButton.Location = New System.Drawing.Point(170, 0)
        Me.ClearButton.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(80, 25)
        Me.ClearButton.TabIndex = 4
        Me.ClearButton.Text = "Clear"
        Me.ClearButton.UseVisualStyleBackColor = True
        '
        'ShowLabelsCheck
        '
        Me.ShowLabelsCheck.AutoSize = True
        Me.ShowLabelsCheck.Location = New System.Drawing.Point(340, 5)
        Me.ShowLabelsCheck.Margin = New System.Windows.Forms.Padding(0, 5, 5, 0)
        Me.ShowLabelsCheck.Name = "ShowLabelsCheck"
        Me.ShowLabelsCheck.Size = New System.Drawing.Size(92, 19)
        Me.ShowLabelsCheck.TabIndex = 2
        Me.ShowLabelsCheck.Text = "Show labels"
        Me.ShowLabelsCheck.UseVisualStyleBackColor = True
        '
        'ShowDebugCheck
        '
        Me.ShowDebugCheck.AutoSize = True
        Me.ShowDebugCheck.Location = New System.Drawing.Point(437, 5)
        Me.ShowDebugCheck.Margin = New System.Windows.Forms.Padding(0, 5, 5, 0)
        Me.ShowDebugCheck.Name = "ShowDebugCheck"
        Me.ShowDebugCheck.Size = New System.Drawing.Size(137, 19)
        Me.ShowDebugCheck.TabIndex = 3
        Me.ShowDebugCheck.Text = "Show debug window"
        Me.ShowDebugCheck.UseVisualStyleBackColor = True
        '
        'LoadBackgroundButton
        '
        Me.LoadBackgroundButton.Location = New System.Drawing.Point(255, 0)
        Me.LoadBackgroundButton.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.LoadBackgroundButton.Name = "LoadBackgroundButton"
        Me.LoadBackgroundButton.Size = New System.Drawing.Size(80, 25)
        Me.LoadBackgroundButton.TabIndex = 5
        Me.LoadBackgroundButton.Text = "Picture …"
        Me.LoadBackgroundButton.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 571)
        Me.Controls.Add(Me.MainContainer)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "MainForm"
        Me.Text = "Travelling Salesman"
        Me.MainContainer.Panel1.ResumeLayout(False)
        Me.MainContainer.Panel2.ResumeLayout(False)
        Me.MainContainer.ResumeLayout(False)
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents MapPicture As System.Windows.Forms.PictureBox
    Friend WithEvents StopButton As System.Windows.Forms.Button
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents ShowLabelsCheck As System.Windows.Forms.CheckBox
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ShowDebugCheck As System.Windows.Forms.CheckBox
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents LoadBackgroundButton As System.Windows.Forms.Button
    Friend WithEvents OpenPictureDialog As System.Windows.Forms.OpenFileDialog

End Class
