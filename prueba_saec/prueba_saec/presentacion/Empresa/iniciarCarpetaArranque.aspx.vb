Public Class iniciarCarpetaArranque
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        validarUsuario()
        cargarMenu()
        lblMensaje.Text = ""
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
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
        Dim rutUsuario As String = usuario.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub cargarDatos()
        Dim newListItem = New ListItem("seleccione un item", "-1")
        dropEmpresas.Items.Add(newListItem)
        Dim empresa As New clsEmpresa
        Dim listaEmpresas As DataTable = empresa.empresasSinCarpeta
        If (listaEmpresas Is Nothing) Then
        Else
            If (listaEmpresas.Rows.Count > 0) Then
                For Each celda As DataRow In listaEmpresas.Rows
                    Dim item As New ListItem()
                    item.Text = celda("razonSocial").ToString()
                    item.Value = celda("rut").ToString()
                    'item.Selected = Convert.ToBoolean(celda("IsSelected"))
                    dropEmpresas.Items.Add(item)
                Next
            End If
        End If
    End Sub
    Private Function calcularFechaExpiracion(ByVal fecha As Date) As Date
        Dim mes As Integer = fecha.Month
        Dim diferencia As Integer = 12 - mes
        fecha = fecha.AddMonths(diferencia)
        Dim dia As Integer = fecha.Day
        diferencia = 31 - dia
        fecha = fecha.AddDays(diferencia)
        Return fecha
    End Function
    Protected Sub btnCrearCarpeta_Click(sender As Object, e As EventArgs) Handles btnCrearCarpeta.Click
        Dim alerta As New clsAlertas
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            If (dropEmpresas.SelectedValue = "-1") Then
                lblMensaje.Text = alerta.alerta("ALERTA", "seleccione un item")
            Else
                Dim em As New clsEmpresa
                Dim rutEmpresa As String = dropEmpresas.SelectedValue
                Dim fechaExpiracion As Date
                Dim fechaCreacion As Date
                Dim carpetaArranque As New clsCarpetaArranque
                If (txtFecha.Text = "") Then
                    Dim descripcion As String = "Creacion de la carpeta arranque de la empresa " + dropEmpresas.SelectedItem.Text
                    fechaCreacion = Today
                    Dim mes As Integer = fechaCreacion.Month
                    If (mes = 11 Or mes = 12) Then
                        fechaExpiracion = calcularFechaExpiracion(fechaCreacion)
                        fechaExpiracion = fechaExpiracion.AddYears(1)
                    Else
                        fechaExpiracion = calcularFechaExpiracion(fechaCreacion)
                    End If

                    'fechaExpiracion = DateAdd("m", 12, Today)
                    If (carpetaArranque.insertarEmpresa(fechaExpiracion, rutEmpresa, fechaCreacion, descripcion, usuario.getRut) = True) Then
                        My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa))
                        Response.Redirect("iniciarCarpetaArranque.aspx")
                    Else
                        Response.Redirect("iniciarCarpetaArranque.aspx")
                        End If
                    Else
                        fechaCreacion = Today
                    fechaExpiracion = Convert.ToDateTime(txtFecha.Text)
                    If (DateTime.Compare(fechaCreacion, fechaExpiracion) = 0 Or DateTime.Compare(fechaCreacion, fechaExpiracion) > 0) Then
                        lblMensaje.Text = alerta.alerta("ALERTA", "fecha erronea")
                    Else
                        Dim descripcion As String = "Creacion de la carpeta arranque de la empresa " + dropEmpresas.SelectedItem.Text
                        If (carpetaArranque.insertarEmpresa(fechaExpiracion, rutEmpresa, fechaCreacion, descripcion, usuario.getRut) = True) Then
                            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa))
                            Response.Redirect("iniciarCarpetaArranque.aspx")
                        Else
                            Response.Redirect("iniciarCarpetaArranque.aspx")
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub btnCrearEmpresa_Click(sender As Object, e As EventArgs) Handles btnCrearEmpresa.Click
        Response.Redirect("crearEmpresa.aspx")
    End Sub
End Class