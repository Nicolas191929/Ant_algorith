<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebugWindow
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
        Me.DebugList = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'DebugList
        '
        Me.DebugList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DebugList.FormattingEnabled = True
        Me.DebugList.Location = New System.Drawing.Point(0, 0)
        Me.DebugList.Name = "DebugList"
        Me.DebugList.Size = New System.Drawing.Size(531, 199)
        Me.DebugList.TabIndex = 0
        '
        'DebugWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 206)
        Me.ControlBox = False
        Me.Controls.Add(Me.DebugList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "DebugWindow"
        Me.ShowInTaskbar = False
        Me.Text = "Debug Output"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents DebugList As System.Windows.Forms.ListBox
End Class
