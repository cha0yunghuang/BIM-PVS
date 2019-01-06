Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Public Class FileUploadForm

    Public FileUrl As String
    Public FileName As String
    Public FileSize As Integer
    Public UploadType As String

    '''<summary>
    '''將本地文件上傳到指定的伺服器(HttpWebRequest方法)
    '''</summary>
    '''<param name="address">文件上傳的伺服器</param>
    '''<param name="fileNamePath">要上傳的本地文件(全路徑)</param>
    '''<param name="saveName">文件上傳後的名稱</param>
    '''<param name="progressBar">上傳進度條</param>
    '''<returns>成功回傳1，失敗回傳0</returns>
    Private Function Upload_Request(ByVal address As String, ByVal fileNamePath As String, ByVal saveName As String, ByVal progressBar As ProgressBar) As Integer
        Dim ReturnValue As Integer = 0
        ' 要上传的文件
        Dim fs As FileStream = New FileStream(fileNamePath, FileMode.Open, FileAccess.Read)
        Dim r As BinaryReader = New BinaryReader(fs)
        '时间戳
        Dim strBoundary As String = "----------" + DateTime.Now.Ticks.ToString("x")
        Dim boundaryBytes() As Byte = Encoding.ASCII.GetBytes(vbCrLf & "--" & strBoundary & vbCrLf)
        '请求头部信息
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("--")
        sb.Append(strBoundary)
        sb.Append(vbCrLf)
        sb.Append("Content-Disposition: form-data; name=""")
        sb.Append("file")
        sb.Append("""; filename=""")
        sb.Append(saveName)
        sb.Append("""")
        sb.Append(vbCrLf)
        sb.Append("Content-Type: ")
        sb.Append("application/octet-stream")
        sb.Append(vbCrLf)
        sb.Append(vbCrLf)
        Dim strPostHeader As String = sb.ToString()
        Dim postHeaderBytes() As Byte = Encoding.UTF8.GetBytes(strPostHeader)
        '根據uri建立HttpWebRequest對象
        Dim httpReq As HttpWebRequest = CType(WebRequest.Create(New Uri(address)), HttpWebRequest)
        httpReq.Method = "POST"
        '對發送的數據不使用緩存
        httpReq.AllowWriteStreamBuffering = False
        '設置獲得響應的超過時間(3000秒)
        httpReq.Timeout = 3000000
        httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary
        Dim length As Long = fs.Length + postHeaderBytes.Length + boundaryBytes.Length
        Dim fileLength As Long = fs.Length
        httpReq.ContentLength = length
        Try
            progressBar.Maximum = Integer.MaxValue
            progressBar.Minimum = 0
            progressBar.Value = 0
            '每次上傳4K
            Dim bufferLength As Integer = 4096
            Dim buffer() As Byte = New Byte(bufferLength) {}
            '已上傳的字節數
            Dim offset As Long = 0
            '開始上傳時間
            Dim startTime As DateTime = DateTime.Now
            Dim size As Integer = r.Read(buffer, 0, bufferLength)
            Dim postStream As Stream = httpReq.GetRequestStream()
            '請求頭部消息
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length)
            While size > 0
                postStream.Write(buffer, 0, size)
                offset += size
                progressBar.Value = CType((offset * (Integer.MaxValue / length)), Integer)
                Dim span As TimeSpan = DateTime.Now - startTime
                Dim second As Double = span.TotalSeconds
                lblTime.Text = "已耗時：" + second.ToString("F2") + "秒"
                If second > 0.001 Then
                    lblSpeed.Text = "平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒"
                Else
                    lblSpeed.Text = "正在連接…"
                End If
                lblState.Text = "已上傳：" + (offset * 100.0 / length).ToString("F2") + "%"
                lblSize.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M"
                Application.DoEvents()
                size = r.Read(buffer, 0, bufferLength)
            End While
            '添加尾部的時間戳
            postStream.Write(boundaryBytes, 0, boundaryBytes.Length)
            postStream.Close()
            '獲取伺服器端的響應
            Dim webRespon As WebResponse = httpReq.GetResponse()
            Dim s As Stream = webRespon.GetResponseStream()
            Dim sr As StreamReader = New StreamReader(s)
            '讀取服務氣端返回的消息
            Dim sReturnString As String = sr.ReadLine()
            s.Close()
            sr.Close()
            If sReturnString = "Success" Then
                ReturnValue = 1
            ElseIf sReturnString = "Error" Then
                ReturnValue = 0
            End If
        Catch
            ReturnValue = 0
        Finally
            fs.Close()
            r.Close()
        End Try
        Return ReturnValue
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub FileUploadForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If UploadType = "PanoImage" Then
            Try
                Upload_Request("http://140.124.63.3/BIMPVS_Server/PanoUpload.aspx", FileUrl, FileName, ProgressBar1)
            Catch ex As Exception
                MsgBox("上傳失敗")
                MsgBox(ex.Message)
            Finally
                Me.Close()
            End Try
        End If
    End Sub

    Private Sub FileUploadForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class