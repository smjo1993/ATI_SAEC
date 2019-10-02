Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows
Public Class crearNuevoDocumento
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim itemCheckbox As String = ""
        'Dim areas As DataTable = obtenerAreas()

        'For Each fila As DataRow In areas.Rows

        '    itemCheckbox = itemCheckbox & "<asp:ListItem>" + fila("descripcion") + "</asp:ListItem>"

        '    lblItemCheckbox.Text = itemCheckbox

        'Next
        'obtenerAreas()

        If Not IsPostBack Then
            Me.obtenerAreas()
        End If



    End Sub

    'Public Function obtenerAreas() As DataTable
    '    Dim Area = New clsArea()
    '    Return Area.obtenerNombre()
    'End Function

    'Public Function obtenerTipoDocumento() As DataTable
    '    Dim Documento = New clsDocumento()
    '    Return Documento.obtenerTipo()

    'End Function

    Private Sub obtenerAreas()
        Dim chkListaAreas = New CheckBoxList

        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager _
                .ConnectionStrings("constr").ConnectionString()
            Using cmd As New SqlCommand()
                cmd.CommandText = "select * from dbo.TB_SAEC_Area"
                cmd.Connection = conn

                conn.Open()

                Using sdr As SqlDataReader = cmd.ExecuteReader()

                    While sdr.Read()
                        Dim item As New ListItem()
                        item.Text = sdr("descripcion").ToString()
                        'item.Value = sdr("HobbyId").ToString()
                        item.Selected = Convert.ToBoolean(sdr("IsSelected"))
                        chkListaAreas.Items.Add(item)
                    End While

                End Using

                conn.Close()

            End Using
        End Using
    End Sub

End Class