Option Explicit On
#Region "BIMPVS"
Imports System.Windows.Forms
Imports Autodesk.Navisworks.Api.Plugins
Imports Autodesk.Navisworks.Api

<PluginAttribute( _
                "NWPlugin.BIMPVS", _
                 "NTUT", _
                 ExtendedToolTip:="BIM-based Panorama Viewer System", _
                 ToolTip:="NWPlugin.BIMPVS by Eric", _
                 DisplayName:="BIMPVS", _
                 Options:=PluginOptions.None)> _
<AddInPluginAttribute(AddInLocation.AddIn, Icon:="icon7.ico", LargeIcon:="icon7.ico")> _
Public Class BIMPVS
    Inherits AddInPlugin

    Public formMF As BIMPVSform

    Public Overrides Function Execute(ByVal ParamArray parameters() As String) As Integer
        Dim doc As Document = Autodesk.Navisworks.Api.Application.ActiveDocument

        Try
            Dim pr As PluginRecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("BIMPVSform.NTUT")

            If formMF IsNot Nothing Then
                formMF.Visible = Not (formMF.Visible)
            Else
                formMF = New BIMPVSform
                formMF.vbBIMFMS = Me
                formMF.Show()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType().Name)
        End Try

        Return 0
    End Function

End Class
#End Region