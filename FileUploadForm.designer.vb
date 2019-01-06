<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileUploadForm
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意:  以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(16, 90)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(427, 23)
        Me.ProgressBar1.TabIndex = 19
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(14, 116)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(36, 12)
        Me.lblSize.TabIndex = 23
        Me.lblSize.Text = "lblSize"
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(14, 68)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(39, 12)
        Me.lblState.TabIndex = 22
        Me.lblState.Text = "lblState"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Location = New System.Drawing.Point(14, 41)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(45, 12)
        Me.lblSpeed.TabIndex = 21
        Me.lblSpeed.Text = "lblSpeed"
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(14, 13)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(41, 12)
        Me.lblTime.TabIndex = 20
        Me.lblTime.Text = "lblTime"
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(375, 64)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 20)
        Me.btnCancel.TabIndex = 24
        Me.btnCancel.Text = "取消"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FileUploadForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 135)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblTime)
        Me.Name = "FileUploadForm"
        Me.Text = "檔案上傳進度"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
