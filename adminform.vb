Imports System.Data.OleDb

Public Class adminform

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If RoleComboBox.Items.Count = 0 Then
            MessageBox.Show("Roles not loaded. Please try again.")
            Return
        End If

        Dim selectedRole As String = If(RoleComboBox.SelectedItem IsNot Nothing, RoleComboBox.SelectedItem.ToString(), "")

        If selectedRole = "Admin" Then
            RegisterUser("admin-usrname&pass", "[admin username]", "[admin password]")
        ElseIf selectedRole = "Student" Then
            RegisterUser("USERNAME AND PAS", "[Username]", "[Password]")
        Else
            MessageBox.Show("Please select a role before registering.")
        End If
    End Sub

    Private Sub RegisterUser(tableName As String, usernameColumn As String, passwordColumn As String)
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()
        Dim confirmPassword As String = TextBox3.Text.Trim()

        If password <> confirmPassword Then
            MessageBox.Show("Passwords do not match. Please enter matching passwords.")
            Return
        End If

        If CreatingUser(tableName, username, password, usernameColumn, passwordColumn) Then
            MessageBox.Show("{tableName} account created successfully!")
        Else
            MessageBox.Show("{tableName} account creation failed. Please try again.")
        End If
    End Sub

    Private Function CreatingUser(tableName As String, username As String, password As String, usernameColumn As String, passwordColumn As String) As Boolean
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\username&pass(access)\username&pass.accdb;Persist Security Info=True"
        Dim query As String = "INSERT INTO [{tableName}] ([{usernameColumn}], [{passwordColumn}]) VALUES (@Username, @Password)"

        Using connection As New OleDbConnection(connectionString)
            Using command As New OleDbCommand(query, connection)
                command.Parameters.AddWithValue("@Username", username)
                command.Parameters.AddWithValue("@Password", password)

                connection.Open()
                Return command.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function


    Private Sub adminform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoleComboBox.Items.Clear()
        RoleComboBox.Items.Add("Admin")
        RoleComboBox.Items.Add("Student")
    End Sub

End Class
