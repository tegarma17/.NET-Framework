Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim cn As MySqlConnection
    Dim ds As New DataSet
    Dim dr As MySqlDataReader
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim cmd As MySqlCommand


    Sub Koneksi()
        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;user=root;password=;database=barang;allow user variables=true"
        cn.Open()

    End Sub
    Sub tampil()

        Try
            Koneksi()
            da = New MySqlDataAdapter("SELECT * FROM barang", cn)
            ds = New DataSet
            da.Fill(ds, "barang")
            DataGridView1.DataSource = ds.Tables("barang")
            DataGridView1.ReadOnly = True
        Catch ex As Exception
            MsgBox("Menampilkan data gagal")
        End Try
    End Sub

    Sub KosongData()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Harap isi")
        Else
            Koneksi()
            Dim simpan As String = "insert into barang values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
            cmd = New MySqlCommand(simpan, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Sukses")
            tampil()
            KosongData()

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tampil()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Koneksi()
        Dim edit As String = "update barang set nama_barang='" & TextBox2.Text & "',total='" & TextBox3.Text & "', harga='" & TextBox4.Text & "'where id_barang='" & TextBox1.Text & "'"
        cmd = New MySqlCommand(edit, cn)
        cmd.ExecuteNonQuery()
        MsgBox("DATA UPDATE")
        tampil()
        KosongData()

    End Sub

    Private Sub TextBox1_Keypress1(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            Koneksi()
            cmd = New MySqlCommand("select * from barang where id_barang='" & TextBox1.Text & "'", cn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                MsgBox("ID BARANG TIDAK ADA!, Silahkan Input lagi", "AHA")
                TextBox1.Focus()
            Else
                TextBox2.Text = dr.Item("nama_barang")
                TextBox3.Text = dr.Item("total")
                TextBox4.Text = dr.Item("harga")
                TextBox2.Focus()
            End If

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("silahkan pilih data yang akan di hapus")
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Koneksi()
                Dim hapus As String = "delete from barang where id_barang='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, cn)
                cmd.ExecuteNonQuery()
                tampil()
                KosongData()

            End If
        End If
    End Sub
End Class
