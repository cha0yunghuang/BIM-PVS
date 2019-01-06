<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BIMPVSform
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BIMPVSform))
        Me.btnPano = New System.Windows.Forms.Button()
        Me.PfX = New System.Windows.Forms.TextBox()
        Me.PfY = New System.Windows.Forms.TextBox()
        Me.PfZ = New System.Windows.Forms.TextBox()
        Me.PoX = New System.Windows.Forms.TextBox()
        Me.PoY = New System.Windows.Forms.TextBox()
        Me.PoZ = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label_position = New System.Windows.Forms.Label()
        Me.Label_Front = New System.Windows.Forms.Label()
        Me.PupX = New System.Windows.Forms.TextBox()
        Me.PupY = New System.Windows.Forms.TextBox()
        Me.PbackX = New System.Windows.Forms.TextBox()
        Me.PupZ = New System.Windows.Forms.TextBox()
        Me.PbackY = New System.Windows.Forms.TextBox()
        Me.PbackZ = New System.Windows.Forms.TextBox()
        Me.Label_back = New System.Windows.Forms.Label()
        Me.Label_up = New System.Windows.Forms.Label()
        Me.PleftX = New System.Windows.Forms.TextBox()
        Me.PleftY = New System.Windows.Forms.TextBox()
        Me.PdownX = New System.Windows.Forms.TextBox()
        Me.PleftZ = New System.Windows.Forms.TextBox()
        Me.PdownY = New System.Windows.Forms.TextBox()
        Me.PdownZ = New System.Windows.Forms.TextBox()
        Me.Label_down = New System.Windows.Forms.Label()
        Me.Label_left = New System.Windows.Forms.Label()
        Me.PrightX = New System.Windows.Forms.TextBox()
        Me.PrightY = New System.Windows.Forms.TextBox()
        Me.PrightZ = New System.Windows.Forms.TextBox()
        Me.Label_right = New System.Windows.Forms.Label()
        Me.tbRollUp = New System.Windows.Forms.TextBox()
        Me.tbRollLeft = New System.Windows.Forms.TextBox()
        Me.tbRollRight = New System.Windows.Forms.TextBox()
        Me.tbRollBack = New System.Windows.Forms.TextBox()
        Me.tbRollDown = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLocate = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ofdMain = New System.Windows.Forms.OpenFileDialog()
        Me.TextUrl = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCopyurl = New System.Windows.Forms.Button()
        Me.btnOpenurl = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPano
        '
        resources.ApplyResources(Me.btnPano, "btnPano")
        Me.btnPano.BackColor = System.Drawing.Color.White
        Me.btnPano.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPano.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPano.FlatAppearance.BorderSize = 0
        Me.btnPano.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnPano.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnPano.Name = "btnPano"
        Me.btnPano.UseVisualStyleBackColor = False
        '
        'PfX
        '
        resources.ApplyResources(Me.PfX, "PfX")
        Me.PfX.Name = "PfX"
        '
        'PfY
        '
        resources.ApplyResources(Me.PfY, "PfY")
        Me.PfY.Name = "PfY"
        '
        'PfZ
        '
        resources.ApplyResources(Me.PfZ, "PfZ")
        Me.PfZ.Name = "PfZ"
        '
        'PoX
        '
        resources.ApplyResources(Me.PoX, "PoX")
        Me.PoX.Name = "PoX"
        '
        'PoY
        '
        resources.ApplyResources(Me.PoY, "PoY")
        Me.PoY.Name = "PoY"
        '
        'PoZ
        '
        resources.ApplyResources(Me.PoZ, "PoZ")
        Me.PoZ.Name = "PoZ"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label_position
        '
        resources.ApplyResources(Me.Label_position, "Label_position")
        Me.Label_position.Name = "Label_position"
        '
        'Label_Front
        '
        resources.ApplyResources(Me.Label_Front, "Label_Front")
        Me.Label_Front.Name = "Label_Front"
        '
        'PupX
        '
        resources.ApplyResources(Me.PupX, "PupX")
        Me.PupX.Name = "PupX"
        '
        'PupY
        '
        resources.ApplyResources(Me.PupY, "PupY")
        Me.PupY.Name = "PupY"
        '
        'PbackX
        '
        resources.ApplyResources(Me.PbackX, "PbackX")
        Me.PbackX.Name = "PbackX"
        '
        'PupZ
        '
        resources.ApplyResources(Me.PupZ, "PupZ")
        Me.PupZ.Name = "PupZ"
        '
        'PbackY
        '
        resources.ApplyResources(Me.PbackY, "PbackY")
        Me.PbackY.Name = "PbackY"
        '
        'PbackZ
        '
        resources.ApplyResources(Me.PbackZ, "PbackZ")
        Me.PbackZ.Name = "PbackZ"
        '
        'Label_back
        '
        resources.ApplyResources(Me.Label_back, "Label_back")
        Me.Label_back.Name = "Label_back"
        '
        'Label_up
        '
        resources.ApplyResources(Me.Label_up, "Label_up")
        Me.Label_up.Name = "Label_up"
        '
        'PleftX
        '
        resources.ApplyResources(Me.PleftX, "PleftX")
        Me.PleftX.Name = "PleftX"
        '
        'PleftY
        '
        resources.ApplyResources(Me.PleftY, "PleftY")
        Me.PleftY.Name = "PleftY"
        '
        'PdownX
        '
        resources.ApplyResources(Me.PdownX, "PdownX")
        Me.PdownX.Name = "PdownX"
        '
        'PleftZ
        '
        resources.ApplyResources(Me.PleftZ, "PleftZ")
        Me.PleftZ.Name = "PleftZ"
        '
        'PdownY
        '
        resources.ApplyResources(Me.PdownY, "PdownY")
        Me.PdownY.Name = "PdownY"
        '
        'PdownZ
        '
        resources.ApplyResources(Me.PdownZ, "PdownZ")
        Me.PdownZ.Name = "PdownZ"
        '
        'Label_down
        '
        resources.ApplyResources(Me.Label_down, "Label_down")
        Me.Label_down.Name = "Label_down"
        '
        'Label_left
        '
        resources.ApplyResources(Me.Label_left, "Label_left")
        Me.Label_left.Name = "Label_left"
        '
        'PrightX
        '
        resources.ApplyResources(Me.PrightX, "PrightX")
        Me.PrightX.Name = "PrightX"
        '
        'PrightY
        '
        resources.ApplyResources(Me.PrightY, "PrightY")
        Me.PrightY.Name = "PrightY"
        '
        'PrightZ
        '
        resources.ApplyResources(Me.PrightZ, "PrightZ")
        Me.PrightZ.Name = "PrightZ"
        '
        'Label_right
        '
        resources.ApplyResources(Me.Label_right, "Label_right")
        Me.Label_right.Name = "Label_right"
        '
        'tbRollUp
        '
        resources.ApplyResources(Me.tbRollUp, "tbRollUp")
        Me.tbRollUp.Name = "tbRollUp"
        '
        'tbRollLeft
        '
        resources.ApplyResources(Me.tbRollLeft, "tbRollLeft")
        Me.tbRollLeft.Name = "tbRollLeft"
        '
        'tbRollRight
        '
        resources.ApplyResources(Me.tbRollRight, "tbRollRight")
        Me.tbRollRight.Name = "tbRollRight"
        '
        'tbRollBack
        '
        resources.ApplyResources(Me.tbRollBack, "tbRollBack")
        Me.tbRollBack.Name = "tbRollBack"
        '
        'tbRollDown
        '
        resources.ApplyResources(Me.tbRollDown, "tbRollDown")
        Me.tbRollDown.Name = "tbRollDown"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'btnLocate
        '
        resources.ApplyResources(Me.btnLocate, "btnLocate")
        Me.btnLocate.BackColor = System.Drawing.Color.White
        Me.btnLocate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLocate.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnLocate.FlatAppearance.BorderSize = 0
        Me.btnLocate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnLocate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnLocate.Name = "btnLocate"
        Me.btnLocate.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label_right)
        Me.Panel1.Controls.Add(Me.Label_left)
        Me.Panel1.Controls.Add(Me.Label_up)
        Me.Panel1.Controls.Add(Me.btnLocate)
        Me.Panel1.Controls.Add(Me.Label_Front)
        Me.Panel1.Controls.Add(Me.Label_down)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label_back)
        Me.Panel1.Controls.Add(Me.Label_position)
        Me.Panel1.Controls.Add(Me.tbRollDown)
        Me.Panel1.Controls.Add(Me.PdownZ)
        Me.Panel1.Controls.Add(Me.tbRollBack)
        Me.Panel1.Controls.Add(Me.PbackZ)
        Me.Panel1.Controls.Add(Me.PdownY)
        Me.Panel1.Controls.Add(Me.PoZ)
        Me.Panel1.Controls.Add(Me.tbRollRight)
        Me.Panel1.Controls.Add(Me.PrightZ)
        Me.Panel1.Controls.Add(Me.tbRollLeft)
        Me.Panel1.Controls.Add(Me.PbackY)
        Me.Panel1.Controls.Add(Me.PleftZ)
        Me.Panel1.Controls.Add(Me.tbRollUp)
        Me.Panel1.Controls.Add(Me.PoY)
        Me.Panel1.Controls.Add(Me.PupZ)
        Me.Panel1.Controls.Add(Me.PdownX)
        Me.Panel1.Controls.Add(Me.PrightY)
        Me.Panel1.Controls.Add(Me.PfZ)
        Me.Panel1.Controls.Add(Me.PleftY)
        Me.Panel1.Controls.Add(Me.PbackX)
        Me.Panel1.Controls.Add(Me.PrightX)
        Me.Panel1.Controls.Add(Me.PupY)
        Me.Panel1.Controls.Add(Me.PleftX)
        Me.Panel1.Controls.Add(Me.PoX)
        Me.Panel1.Controls.Add(Me.PupX)
        Me.Panel1.Controls.Add(Me.PfY)
        Me.Panel1.Controls.Add(Me.PfX)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        '
        'BackgroundWorker1
        '
        '
        'ofdMain
        '
        Me.ofdMain.FileName = "OpenFileDialog1"
        Me.ofdMain.Multiselect = True
        '
        'TextUrl
        '
        resources.ApplyResources(Me.TextUrl, "TextUrl")
        Me.TextUrl.Name = "TextUrl"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'btnCopyurl
        '
        Me.btnCopyurl.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.btnCopyurl, "btnCopyurl")
        Me.btnCopyurl.Name = "btnCopyurl"
        Me.btnCopyurl.UseVisualStyleBackColor = True
        '
        'btnOpenurl
        '
        resources.ApplyResources(Me.btnOpenurl, "btnOpenurl")
        Me.btnOpenurl.Name = "btnOpenurl"
        Me.btnOpenurl.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'BIMPVSform
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCopyurl)
        Me.Controls.Add(Me.btnOpenurl)
        Me.Controls.Add(Me.TextUrl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnPano)
        Me.Controls.Add(Me.PictureBox2)
        Me.MaximizeBox = False
        Me.Name = "BIMPVSform"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PfX As System.Windows.Forms.TextBox
    Friend WithEvents PfY As System.Windows.Forms.TextBox
    Friend WithEvents PfZ As System.Windows.Forms.TextBox
    Friend WithEvents PoX As System.Windows.Forms.TextBox
    Friend WithEvents PoY As System.Windows.Forms.TextBox
    Friend WithEvents PoZ As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label_position As System.Windows.Forms.Label
    Friend WithEvents Label_Front As System.Windows.Forms.Label
    Friend WithEvents PupX As System.Windows.Forms.TextBox
    Friend WithEvents PupY As System.Windows.Forms.TextBox
    Friend WithEvents PbackX As System.Windows.Forms.TextBox
    Friend WithEvents PupZ As System.Windows.Forms.TextBox
    Friend WithEvents PbackY As System.Windows.Forms.TextBox
    Friend WithEvents PbackZ As System.Windows.Forms.TextBox
    Friend WithEvents Label_back As System.Windows.Forms.Label
    Friend WithEvents Label_up As System.Windows.Forms.Label
    Friend WithEvents PleftX As System.Windows.Forms.TextBox
    Friend WithEvents PleftY As System.Windows.Forms.TextBox
    Friend WithEvents PdownX As System.Windows.Forms.TextBox
    Friend WithEvents PleftZ As System.Windows.Forms.TextBox
    Friend WithEvents PdownY As System.Windows.Forms.TextBox
    Friend WithEvents PdownZ As System.Windows.Forms.TextBox
    Friend WithEvents Label_down As System.Windows.Forms.Label
    Friend WithEvents Label_left As System.Windows.Forms.Label
    Friend WithEvents PrightX As System.Windows.Forms.TextBox
    Friend WithEvents PrightY As System.Windows.Forms.TextBox
    Friend WithEvents PrightZ As System.Windows.Forms.TextBox
    Friend WithEvents Label_right As System.Windows.Forms.Label
    Friend WithEvents tbRollUp As System.Windows.Forms.TextBox
    Friend WithEvents tbRollLeft As System.Windows.Forms.TextBox
    Friend WithEvents tbRollRight As System.Windows.Forms.TextBox
    Friend WithEvents tbRollBack As System.Windows.Forms.TextBox
    Friend WithEvents tbRollDown As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents btnPano As System.Windows.Forms.Button
    Public WithEvents btnLocate As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ofdMain As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TextUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents btnCopyurl As System.Windows.Forms.Button
    Public WithEvents btnOpenurl As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
