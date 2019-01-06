Imports System.Windows.Forms
Imports System.Drawing
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports Autodesk.Navisworks.Api
Imports Autodesk.Navisworks.Api.Plugins
Imports Autodesk.Navisworks.Api.Interop.ComApi
Imports Autodesk.Navisworks.Api.Controls
Imports ComApi = Autodesk.Navisworks.Api.Interop.ComApi
Imports ComApiBridge = Autodesk.Navisworks.Api.ComApi
Imports System.Drawing.Imaging
Imports System.Math
Imports System.IO


Public Class BIMPVSform
    Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub

    Public vbBIMFMS As BIMPVS
 
    ' 控制Form起始位置為右上角
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim scr = Screen.FromPoint(Me.Location)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Top)
        MyBase.OnLoad(e)
    End Sub
    Private Sub btnCopyurl_Click(sender As Object, e As EventArgs) Handles btnCopyurl.Click
        Clipboard.SetText(TextUrl.Text)
    End Sub
    Private Sub btnOpenurl_Click(sender As Object, e As EventArgs) Handles btnOpenurl.Click
        Dim PanoUrl_Line As String = TextUrl.Text
        Process.Start(PanoUrl_Line)
    End Sub
    Private Sub btnLocate_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        ' get current viewpoint
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        ' get copy viewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        ' focal distance
        Dim oFocal As Double = 100
        ' position
        Dim oPos As Point3D = oCurrVCopy.Position
        ' get current view direction
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        ' current target point (Look At)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z + oViewDir.Z * oFocal)
        ' meter to feet
        Const MeterToFeet As Double = 3.280839895
        ' current position
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        ' new point at
        Dim newPointAt As Point3D = New Point3D(PfX.Text * MeterToFeet, PfY.Text * MeterToFeet, PfZ.Text * MeterToFeet)
        ' set to new target
        oCurrVCopy.PointAt(newPointAt)
        ' set which direction is up: in this case it is Z+
        oCurrVCopy.AlignUp(New Vector3D(0, 0, 1))
        ' update current viewpoint
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub btnPano_Click(sender As Object, e As EventArgs) Handles btnPano.Click
        fov90()
        catch_PosTarget()

        Dim Po(2, 2), Pf(2, 2), Pb(2, 2), Pr(2, 2), Pl(2, 2), Pu(2, 2), Pd(2, 2), dis(2, 2), X1, Y1, Z1, Roll As Double
        Const ToDegrees As Double = 180 / System.Math.PI
        ' 轉換徑度為角度
        Po(0, 0) = PoX.Text
        Po(0, 1) = PoY.Text
        Po(0, 2) = PoZ.Text
        Pf(0, 0) = PfX.Text
        Pf(0, 1) = PfY.Text
        Pf(0, 2) = PfZ.Text
        dis = subtractArray(Pf, Po)

        X1 = Pf(0, 0) - Po(0, 0)
        Y1 = Pf(0, 1) - Po(0, 1)
        Z1 = Pf(0, 2) - Po(0, 2)

        If X1 <> 0 Or Y1 <> 0 Then

            Roll = Z1 / (Sqrt((X1) ^ 2 + (Y1) ^ 2))
        Else
            Roll = 0
        End If
        Roll = System.Math.Atan(Roll) * ToDegrees
        '[ Roll = Roll正切角*ToDegrees ]

        Pu = LookUDBIMmodel(90)
        Pd = LookUDBIMmodel(-90)
        Pb = LookUDBIMmodel(180)
        Pl = addArray(multiplyArray(subtractArray(Pu, Po), axisAngle(Po, Pf, 90)), Po)
        Pr = addArray(multiplyArray(subtractArray(Pu, Po), axisAngle(Po, Pf, -90)), Po)

        PupX.Text = Pu(0, 0).ToString("0.000")
        PupY.Text = Pu(0, 1).ToString("0.000")
        PupZ.Text = Pu(0, 2).ToString("0.000")
        PbackX.Text = Pb(0, 0).ToString("0.000")
        PbackY.Text = Pb(0, 1).ToString("0.000")
        PbackZ.Text = Pb(0, 2).ToString("0.000")
        PdownX.Text = Pd(0, 0).ToString("0.000")
        PdownY.Text = Pd(0, 1).ToString("0.000")
        PdownZ.Text = Pd(0, 2).ToString("0.000")
        PleftX.Text = Pl(0, 0).ToString("0.000")
        PleftY.Text = Pl(0, 1).ToString("0.000")
        PleftZ.Text = Pl(0, 2).ToString("0.000")
        PrightX.Text = Pr(0, 0).ToString("0.000")
        PrightY.Text = Pr(0, 1).ToString("0.000")
        PrightZ.Text = Pr(0, 2).ToString("0.000")

        tbRollRight.Text = Roll
        tbRollLeft.Text = -Roll
        tbRollUp.Text = u
        tbRollDown.Text = d
        tbRollBack.Text = 0

        Look_front()
        Render_front()

        Look_back()
        Render_back()

        Look_front()
        Look_up()
        Render_Up()

        Look_front()
        Look_down()
        Render_Down()

        Look_front()
        Look_left()
        Render_Left()

        Look_front()
        Look_right()
        Render_Right()

        Look_front()

        BackgroundWorker1.RunWorkerAsync()





    End Sub
    Private Sub Render_front()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_f.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Render_back()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_b.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Render_Up()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_u.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Render_Down()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_d.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Render_Left()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_l.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Render_Right()
        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State

        Dim oView As InwOpAnonView = oState.CurrentView
        oView.ViewPoint.Lighting = nwELighting.eLighting_USER_LIGHTING
        oView.ViewPoint.RenderStyle = nwERenderStyle.eRenderStyle_SHADED_RENDER

        Dim img As Bitmap = TryCast(ImageUtilities.ConvertFromIPicture(oState.CreatePicture(oView, Nothing, 1920, 1920)), Image)
        Dim encoderParameters As New EncoderParameters(1)
        encoderParameters.Param(0) = New EncoderParameter(Encoder.Quality, 75L)
        img.Save("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_r.jpg", GetNewEncoder(ImageFormat.Jpeg), encoderParameters)
        img.Dispose()
    End Sub
    Private Sub Look_front()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        ' get current viewpoint
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        ' get copy viewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        ' focal distance
        Dim oFocal As Double = 100
        ' position
        Dim oPos As Point3D = oCurrVCopy.Position
        ' get current view direction
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        ' current target point (Look At)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        ' meter to feet
        Const MeterToFeet As Double = 3.280839895
        ' current position
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        ' new point at
        Dim newPointAt As Point3D = New Point3D(PfX.Text * MeterToFeet, PfY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        ' set to new target
        oCurrVCopy.PointAt(newPointAt)
        ' set which direction is up: in this case it is Z+
        oCurrVCopy.AlignUp(New Vector3D(0, 0, 1))
        ' update current viewpoint
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub Look_back()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        Dim oFocal As Double = 100
        Dim oPos As Point3D = oCurrVCopy.Position
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        Const MeterToFeet As Double = 3.280839895
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        Dim newPointAt As Point3D = New Point3D(PbackX.Text * MeterToFeet, PbackY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        oCurrVCopy.PointAt(newPointAt)
        oCurrVCopy.AlignUp(New Vector3D(0, 0, 1))
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub Look_up()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        Dim oFocal As Double = 100
        Dim oPos As Point3D = oCurrVCopy.Position
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        Const MeterToFeet As Double = 3.280839895
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        Dim newPointAt As Point3D = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PupZ.Text * MeterToFeet)
        oCurrVCopy.PointAt(newPointAt)
        SetCameraRoll(tbRollUp.Text)
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)

    End Sub
    Private Sub Look_down()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        Dim oFocal As Double = 100
        Dim oPos As Point3D = oCurrVCopy.Position
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        Const MeterToFeet As Double = 3.280839895
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        Dim newPointAt As Point3D = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PdownZ.Text * MeterToFeet)
        oCurrVCopy.PointAt(newPointAt)
        SetCameraRoll(tbRollDown.Text)
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub Look_left()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        Dim oFocal As Double = 100
        Dim oPos As Point3D = oCurrVCopy.Position
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        Const MeterToFeet As Double = 3.280839895
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        Dim newPointAt As Point3D = New Point3D(PleftX.Text * MeterToFeet, PleftY.Text * MeterToFeet, PleftZ.Text * MeterToFeet)
        oCurrVCopy.PointAt(newPointAt)
        SetCameraRoll(tbRollLeft.Text)
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub Look_right()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        Dim oFocal As Double = 100
        Dim oPos As Point3D = oCurrVCopy.Position
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z)
        Const MeterToFeet As Double = 3.280839895
        oCurrVCopy.Position = New Point3D(PoX.Text * MeterToFeet, PoY.Text * MeterToFeet, PoZ.Text * MeterToFeet)
        Dim newPointAt As Point3D = New Point3D(PrightX.Text * MeterToFeet, PrightY.Text * MeterToFeet, PrightZ.Text * MeterToFeet)
        oCurrVCopy.PointAt(newPointAt)
        SetCameraRoll(tbRollRight.Text)
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Public Shared Function GetCentrePointOfSelection() As Point3D
        Dim centreOfBox As Point3D = Nothing
        'check that selection is valid
        If Not Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.IsEmpty Then
            'Get bounding box of the selection
            Dim bb3d As BoundingBox3D = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.SelectedItems.BoundingBox()
            'get the centre point
            centreOfBox = bb3d.Center
        End If
        'return the vector
        Return centreOfBox
    End Function
    ' set up vector
    Private Overloads Sub changeUp()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        ' make a copy of current viewpoint
        Dim oCurrVCopy As Viewpoint = oDoc.CurrentViewpoint.CreateCopy
        ' a new up vector. e.g. Y +
        Dim oNewUpVec As Vector3D = New Vector3D(0, 1, 0)
        oCurrVCopy.AlignUp(oNewUpVec)
        ' update current viewpoint
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Function MultiplyRotation3D(ByVal r2 As Rotation3D, ByVal r1 As Rotation3D) As Rotation3D
        Dim oRot As Rotation3D = New Rotation3D(((r2.D * r1.A) + ((r2.A * r1.D) _
                        + ((r2.B * r1.C) _
                        - (r2.C * r1.B)))), ((r2.D * r1.B) _
                        + ((r2.B * r1.D) _
                        + ((r2.C * r1.A) _
                        - (r2.A * r1.C)))), ((r2.D * r1.C) _
                        + ((r2.C * r1.D) _
                        + ((r2.A * r1.B) _
                        - (r2.B * r1.A)))), ((r2.D * r1.D) _
                        - ((r2.A * r1.A) _
                        - ((r2.B * r1.B) _
                        - (r2.C * r1.C)))))
        oRot.Normalize()
        Return oRot
    End Function
    Private Function getViewDir(ByVal oVP As Viewpoint) As Vector3D
        Dim oRot As Rotation3D = oVP.Rotation
        ' calculate view direction
        Dim oNegtiveZ As Rotation3D = New Rotation3D(0, 0, -1, 0)
        Dim otempRot As Rotation3D = MultiplyRotation3D(oNegtiveZ, oRot.Invert)
        Dim oViewDirRot As Rotation3D = MultiplyRotation3D(oRot, otempRot)
        ' get view direction
        Dim oViewDir As Vector3D = New Vector3D(oViewDirRot.A, oViewDirRot.B, oViewDirRot.C)
        oViewDir.Normalize()
        Return New Vector3D(oViewDir.X, oViewDir.Y, oViewDir.Z)
    End Function
    ' == Calculate RotationMatrix ==
    Function addArray(ByVal A(,) As Double, ByVal B(,) As Double) As Double(,)
        Dim ans(3, 3) As Double
        Dim i, j As Integer
        For i = 0 To 2
            For j = 0 To 2
                ans(i, j) = A(i, j) + B(i, j)
            Next
        Next
        Return ans
    End Function
    Function subtractArray(ByVal A(,) As Double, ByVal B(,) As Double) As Double(,)
        Dim ans(3, 3) As Double
        Dim i, j As Integer
        For i = 0 To 2
            For j = 0 To 2
                ans(i, j) = A(i, j) - B(i, j)
            Next
        Next
        Return ans
    End Function
    Function multiplyArray(ByVal A(,) As Double, ByVal B(,) As Double) As Double(,)
        Dim ans(3, 3) As Double
        Dim i, j, k As Integer
        For i = 0 To 2
            For j = 0 To 2
                For k = 0 To 2
                    ans(i, j) = ans(i, j) + A(i, k) * B(k, j)
                Next
            Next
        Next
        Return ans
    End Function
    Function axisAngle(ByVal Po(,) As Double, ByVal Pf(,) As Double, ByVal angleA As Double) As Double(,) '軸角
        Dim unitV(2, 2), ans(2, 2), r As Double
        Dim sinA, cosA, x, y, z As Double
        '計算單位向量
        unitV = subtractArray(Pf, Po)
        r = Math.Sqrt(unitV(0, 0) ^ 2 + unitV(0, 1) ^ 2 + unitV(0, 2) ^ 2)
        unitV(0, 0) = unitV(0, 0) / r
        unitV(0, 1) = unitV(0, 1) / r
        unitV(0, 2) = unitV(0, 2) / r

        sinA = Math.Sin(Math.PI * angleA / 180)
        cosA = Math.Cos(Math.PI * angleA / 180)
        x = unitV(0, 0)
        y = unitV(0, 1)
        z = unitV(0, 2)

        ans(0, 0) = cosA + (1 - cosA) * x ^ 2
        ans(0, 1) = (1 - cosA) * x * y - sinA * z
        ans(0, 2) = (1 - cosA) * x * z + sinA * y
        ans(1, 0) = (1 - cosA) * y * x + sinA * z
        ans(1, 1) = cosA + (1 - cosA) * y ^ 2
        ans(1, 2) = (1 - cosA) * y * z - sinA * x
        ans(2, 0) = (1 - cosA) * z * x - sinA * y
        ans(2, 1) = (1 - cosA) * z * y + sinA * x
        ans(2, 2) = cosA + (1 - cosA) * z ^ 2
        Return ans
    End Function
    Dim u, d As Integer
    Function LookUDBIMmodel(ByVal ang As Double) As Double(,)
        Dim ans(2, 2) As Double
        Dim Tx, Ty, Tz, Px, Py, Pz, X1, Y1, Z1, r, A, B As Double
        Const ToDegrees As Double = 180 / System.Math.PI
        Px = PoX.Text
        Py = PoY.Text
        Pz = PoZ.Text
        Tx = PfX.Text
        Ty = PfY.Text
        Tz = PfZ.Text

        r = Math.Sqrt((Tx - Px) ^ 2 + (Ty - Py) ^ 2 + (Tz - Pz) ^ 2)

        X1 = Tx - Px
        Y1 = Ty - Py
        Z1 = Tz - Pz

        If X1 <> 0 Then
            A = Y1 / X1
            B = Z1 / (Sqrt((X1) ^ 2 + (Y1) ^ 2))
        Else
            A = 0
            B = 0
        End If

        B = System.Math.Atan(B) * ToDegrees

        If X1 > 0 And Y1 = 0 Then
            A = 0
        ElseIf X1 > 0 And Y1 > 0 Then
            A = System.Math.Atan(A) * ToDegrees
        ElseIf X1 = 0 And Y1 > 0 Then
            A = 90
        ElseIf X1 < 0 And Y1 > 0 Then
            A = 180 + System.Math.Atan(A) * ToDegrees
        ElseIf X1 < 0 And Y1 = 0 Then
            A = 180
        ElseIf X1 < 0 And Y1 < 0 Then
            A = 180 + System.Math.Atan(A) * ToDegrees
        ElseIf X1 = 0 And Y1 < 0 Then
            A = 270
        ElseIf Y1 < 0 And X1 > 0 Then
            A = 360 + System.Math.Atan(A) * ToDegrees
        End If

        B = B + ang
        If ang <> 180 Then
            If B > 90 Then
                u = 180
                d = 0
            ElseIf B < -90 Then
                u = 0
                d = 180
            End If
        End If

        ans(0, 0) = Px + r * Cos(A * Math.PI / 180) * Cos(B * Math.PI / 180)
        ans(0, 1) = Py + r * Sin(A * Math.PI / 180) * Cos(B * Math.PI / 180)
        ans(0, 2) = Pz + r * Sin(B * Math.PI / 180)
        Return ans
        ' == RotationMatrix Finish ==
    End Function
    Public Function GetNewEncoder(format As ImageFormat) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = imageCodecInfo.GetImageDecoders()
        For Each imageCodecInfo As ImageCodecInfo In From imageCodecInfo1 In codecs Where (imageCodecInfo1.FormatID = format.Guid)
            Return imageCodecInfo
        Next
        Return Nothing
    End Function
    Public NotInheritable Class ImageUtilities
        Private Sub New()
        End Sub
        Public Shared Function ConvertFromIPicture(image As Object) As Object
            Return ImageOLEConverter.Instance.ConvertFromIPicture(image)
        End Function

        Private Class ImageOLEConverter
            Inherits AxHost
            Public Shared ReadOnly Instance As New ImageOLEConverter()

            Private Sub New()
                MyBase.New(Guid.Empty.ToString())
            End Sub

            Public Function ConvertFromIPicture(image As Object) As Object
                Return AxHost.GetPictureFromIPicture(image)
            End Function
        End Class
    End Class
    Public MustInherit Class RenderPlugin

    End Class
    Const Radian As Double = System.Math.PI / 180
    Private Sub SetCameraRoll(ByVal angle As Double)
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Const Radian As Double = System.Math.PI / 180
        Dim oCurrVCopy As Viewpoint = oDoc.CurrentViewpoint.CreateCopy
        ' position
        Dim oPos As Point3D = oCurrVCopy.Position
        ' get current view direction
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        ' focal distance
        Dim oFocal As Double = 100
        ' current target point (Look At)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z + oViewDir.Z * oFocal)

        Dim r As Double = Sqrt((oTarget.X - oPos.X) ^ 2 + (oTarget.Y - oPos.Y) ^ 2 + (oTarget.Z - oPos.Z) ^ 2)
        Dim unitX As Double = (oTarget.X - oPos.X) / r
        Dim unitY As Double = (oTarget.Y - oPos.Y) / r
        Dim unitZ As Double = (oTarget.Z - oPos.Z) / r

        Dim oState As ComApi.InwOpState
        oState = ComApiBridge.ComApiBridge.State
        oState.BeginEdit("Rotate Camera")
        Dim cam As InwNvCamera = oState.CurrentView.ViewPoint.Camera
        ' Here, we create a new Quaternion from the camera rotation.
        Dim orig As New Quaternion(cam.Rotation)
        ' And create a Quaternion to represent the rotation we want to
        ' apply to the camera.
        ' Quaternion delta = Quaternion.FromAngleAxis(0, 0, 1, angle);
        Dim delta As Quaternion = Quaternion.FromAngleAxis(unitX, unitY, unitZ, angle * Radian)
        ' And we simply multiple the original rotation with the new one.
        Dim new_rot As Quaternion = Quaternion.Multiply(orig, delta)
        ' And update the rotation of the camera.
        cam.Rotation = new_rot.ToRotation()
        oState.EndEdit()
    End Sub
    Private Sub catch_PosTarget()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        ' meter to feet
        Const MeterToFeet As Double = 3.280839895
        ' get current viewpoint
        Dim oCurVP As Viewpoint = oDoc.CurrentViewpoint
        ' get copy viewpoint
        Dim oCurrVCopy As Viewpoint = oCurVP.CreateCopy()
        ' focal distance
        Dim oFocal As Double = 100
        ' position
        Dim oPos As Point3D = oCurrVCopy.Position
        ' get current view direction
        Dim oViewDir As Vector3D = getViewDir(oCurrVCopy)
        ' current target point (Look At)
        Dim oTarget As Point3D = New Point3D(oPos.X + oViewDir.X * oFocal, oPos.Y + oViewDir.Y * oFocal, oPos.Z + oViewDir.Z * oFocal)

        PoX.Text = oPos.X / MeterToFeet
        PoY.Text = oPos.Y / MeterToFeet
        PoZ.Text = oPos.Z / MeterToFeet
        PfX.Text = oTarget.X / MeterToFeet
        PfY.Text = oTarget.Y / MeterToFeet
        PfZ.Text = oTarget.Z / MeterToFeet
    End Sub
    Private Sub CommonPan()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurrVCopy As Viewpoint = oDoc.CurrentViewpoint.CreateCopy()
        If oCurrVCopy.Tool <> Tool.CommonPan Then
            oCurrVCopy.Tool = Tool.CommonPan
        End If
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub
    Private Sub fov90()
        Dim oDoc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument
        Dim oCurrVCopy As Viewpoint = oDoc.CurrentViewpoint.CreateCopy
        Dim x As Double = 2 * Math.Atan(1 / oCurrVCopy.AspectRatio)
        oCurrVCopy.HeightField = x
        oDoc.CurrentViewpoint.CopyFrom(oCurrVCopy)
    End Sub

    Private Sub BIMPVSform_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        vbBIMFMS.formMF = Nothing
    End Sub
    ' =====================================Form1Upload===================================================
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ' upload the attachment
        For i As Integer = 0 To 5
            Dim fileupload As New FileUploadForm
            Dim fileInfo As FileInfo
            Select Case i
                Case 0
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_b.jpg")
                Case 1
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_d.jpg")
                Case 2
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_l.jpg")
                Case 3
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_r.jpg")
                Case 4
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_f.jpg")
                Case 5
                    fileInfo = New FileInfo("C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\BIMPVS\Panorama\pano_u.jpg")
            End Select
            fileupload.FileUrl = fileInfo.FullName
            fileupload.FileName = fileInfo.Name
            fileupload.FileSize = fileInfo.Length
            fileupload.UploadType = "PanoImage"
            fileupload.ShowDialog()
            'BackgroundWorker1.ReportProgress(i + 1, CInt(i + 1) & "/" & ofdMain.FileNames.Length)
            i = i
        Next
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'do not update the text if a cancellation request is pending
        If e.UserState IsNot Nothing And Not BackgroundWorker1.CancellationPending Then
            Label1.Text = e.UserState.ToString()
        End If
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Dim ws As New WebReference.BIMPVS_WebServices
        Dim PanoUrl As String = ws.CreatePano
        TextUrl.Text = PanoUrl
    End Sub
    ' =====================================Form1Finish===================================================

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub dgvPanoData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

End Class