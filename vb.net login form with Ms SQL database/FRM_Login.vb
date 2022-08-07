Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports BB_API_Framework

Public Class FRM_Login
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub

    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub
    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown, panel1.MouseDown, pictureBox3.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112, &HF012, 0)
    End Sub

    Private Sub panel1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        ReleaseCapture()
        SendMessage(Me.Handle, &H112, &HF012, 0)
    End Sub

    Private Sub txtuser_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtuser.Enter
        If txtuser.Text = "Username" Then
            txtuser.Text = ""
            txtuser.ForeColor = Color.LightGray
            Puser.BackColor = Color.Orange
        End If
    End Sub

    Private Sub txtuser_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtuser.Leave
        If txtuser.Text = "" Then
            txtuser.Text = "Username"
            txtuser.ForeColor = Color.Silver
            Puser.BackColor = Color.White
        Else
            Puser.BackColor = Color.Green
        End If
    End Sub

    Private Sub txtpass_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles txtpass.Enter
        If txtpass.Text = "Password" Then
            txtpass.Text = ""
            txtpass.ForeColor = Color.LightGray
            txtpass.UseSystemPasswordChar = True
            PPass.BackColor = Color.Orange
        End If
    End Sub

    Private Sub txtpass_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtpass.Leave
        If txtpass.Text = "" Then
            txtpass.Text = "Password"
            txtpass.ForeColor = Color.Silver
            PPass.BackColor = Color.White
            txtpass.UseSystemPasswordChar = False
        Else
            PPass.BackColor = Color.Green
        End If
    End Sub


    Private Sub btncerrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btncerrar.Click
        Application.Exit()
    End Sub

    Private Sub btnminimizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnminimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub


    Private Sub txtuser_KeyDown(sender As Object, e As KeyEventArgs) Handles txtuser.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtpass.Focus()
            txtpass.SelectAll()
        End If
    End Sub

    Private Sub txtpass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpass.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnlogin_Click(sender, e)
        End If
    End Sub
    Private Sub btnlogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnlogin.Click
        If txtuser.Text = "" Or txtuser.Text = "Username" Then
            MessageBox.Show("You have not filled out Username")
            txtuser.Focus()
            Exit Sub
        End If
        If txtpass.Text = "" Or txtpass.Text = "Password" Then
            MessageBox.Show("You have not filled out Password")
            txtpass.Focus()
            Exit Sub
        End If

        'If MySqlSetting.Login(txtuser.Text, EncyPassword) Then
        '    Me.DialogResult = DialogResult.OK
        'Else
        '    MessageBox.Show("User or password is incorrect.")
        'End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
        'Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub linkpass_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkpass.LinkClicked

    End Sub




    Function IsValidEmailFormat(ByVal s As String) As Boolean
        Return Regex.IsMatch(s, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
    End Function
    Public RegisterX As New DataTable
    Public Sub RegisterXB()
        RegisterX = New DataTable
        RegisterX.Columns.Add(New DataColumn("username", GetType(String)))
        RegisterX.Columns.Add(New DataColumn("password", GetType(String)))
        RegisterX.Columns.Add(New DataColumn("hKey", GetType(String)))
        RegisterX.Columns.Add(New DataColumn("uKey", GetType(String)))
        RegisterX.Columns.Add(New DataColumn("email", GetType(String)))
    End Sub
    Dim EncyPassword As String

    Dim TS_OTP_CountDown As New TimeSpan(0, 0, 45) 'ten seconds
    Dim SW_OTP_count As New Stopwatch


    Private Sub TimeOTP_Tick(sender As Object, e As EventArgs) Handles TimeOTP.Tick
        If SW_OTP_count.Elapsed <= TS_OTP_CountDown Then
            Dim toGo As TimeSpan = TS_OTP_CountDown - SW_OTP_count.Elapsed
        Else
            SW_OTP_count.Stop()
            TimeOTP.Stop()
            SW_OTP_count.Reset()
        End If
    End Sub
End Class