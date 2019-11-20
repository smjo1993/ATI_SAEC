Imports System.Linq
Public Class agregarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarAreas()

        End If

    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.rutUsuario
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Public Sub cargarAreas()

        Dim areas As DataTable = obtenerTablaAreas()

        chkListaAreas.Items.Clear()

        For Each celda As DataRow In areas.Rows

            Dim item As New ListItem()

            item.Text = celda("nombre").ToString()
            item.Value = celda("id")

            chkListaAreas.Items.Add(item)


        Next

    End Sub

    Public Function obtenerTablaAreas() As DataTable
        Dim Areas = New clsArea()
        Return Areas.obtenerNombre()
    End Function

    Public Function obtenerDocumentos() As DataTable
        Dim Documentos = New clsDocumento()
        Return Documentos.obtenerDocumento()
    End Function

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("../login.aspx")
    End Sub

    Protected Sub btnCrearDocumento_Click(sender As Object, e As EventArgs) Handles btnCrearDocumento.Click

        Dim nuevoDocumento As New clsDocumento

        Dim insercion As New Boolean

        If (txtNombreDocumento.Text.Trim() = "" Or dropTipoDocumento.Items.Equals("")) Then
            lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else

            For Each item In chkListaAreas.Items

                If item.Selected Then

                    insercion = nuevoDocumento.insertarDocumento(txtNombreDocumento.Text,
                                                             dropTipoDocumento.SelectedItem.Value,
                                                             item.Value)

                End If

            Next item

            ClientScript.RegisterStartupScript(Me.GetType(), "Popup", "desplegarModal();", True)

            'Dim title As String = "Greetings"
            'Dim body As String = "Welcome to ASPSnippets.com"

            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup", "ShowPopup('" & Title & "', '" & body & "');", True)

            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

        End If


    End Sub
End Class