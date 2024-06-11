Imports System.Data.OleDb

Public Class Form1

    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\username&pass(access)\username&pass.accdb;Persist Security Info = True"
    Dim connection As New OleDbConnection(connString)

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Dim username As String = UsernameTextbox.Text
        Dim password As String = Pass.Text

        Dim selectedRole As String = If(RoleComboBox.SelectedItem IsNot Nothing, RoleComboBox.SelectedItem.ToString(), "")


        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) OrElse String.IsNullOrWhiteSpace(selectedRole) Then

            MessageBox.Show("Please insert your credentials.")
            Return
        End If


        If Login(username, password, selectedRole) Then
            If selectedRole = "Admin" Then
                MessageBox.Show("Login successful.")
                Dim Form2 As New Form2()
                Form2.Show()
                Me.Hide()
            ElseIf selectedRole = "Student" Then
                MessageBox.Show("Login successful.")
                Dim Form2 As New Form2()
                Form2.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username, password, or role. Please try again.")
            End If
        Else
            MessageBox.Show("Login failed. Please try again.")
        End If
    End Sub

    Private Function Login(username As String, password As String, role As String) As Boolean
        Dim selectQuery As String = ""


        If role = "Admin" Then
            selectQuery = "SELECT COUNT(*) FROM [admin-usrname&pass] WHERE [admin username] = @Username AND [admin password] = @Password"
        ElseIf role = "Student" Then
            selectQuery = "SELECT COUNT(*) FROM [USERNAME AND PAS] WHERE Username = @Username AND Password = @Password"
        End If

        If selectQuery = "" Then
            Return False
        End If

        Using cmd As New OleDbCommand(selectQuery, connection)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Password", password)
            connection.Open()

            Dim result As Integer = CInt(cmd.ExecuteScalar)
            connection.Close()
            Return result > 0
        End Using
    End Function


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RoleComboBox.Items.Clear()
        RoleComboBox.Items.Add("Admin")
        RoleComboBox.Items.Add("Student")

    End Sub

    Private Sub regbtn_Click(sender As Object, e As EventArgs) Handles regbtn.Click
2:
        adminform.Show()

        Me.Hide()
    End Sub
End Class
