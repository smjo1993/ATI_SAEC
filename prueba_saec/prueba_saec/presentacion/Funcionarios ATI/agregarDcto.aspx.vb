Imports System.Linq
Public Class agregarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If
        validarUsuario()
        cargarMenu()
        cargarAreas()
    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
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

    Protected Sub btnCrearDocumento_Click(sender As Object, e As EventArgs) Handles btnCrearDocumento.Click

        Dim nuevoDocumento As New clsDocumento

        Dim log As New clsLog

        Dim insercion As New Boolean

        If (txtNombreDocumento.Text.Trim() = "" Or dropTipoDocumento.Items.Equals("")) Then
            'lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else

            For Each item In chkListaAreas.Items

                If item.Selected Then

                    insercion = nuevoDocumento.insertarDocumento(txtNombreDocumento.Text,
                                                             dropTipoDocumento.SelectedItem.Value,
                                                            item.Value)
                    log.insertarRegistro("Se ha creado el Requerimiento Documental: " + txtNombreDocumento.Text.Trim() + ", Área: " + item.ToString(), Session("usuario").getRut)

                End If

            Next item

            'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            Response.Redirect("verListaDctos.aspx")

        End If


    End Sub
End Class