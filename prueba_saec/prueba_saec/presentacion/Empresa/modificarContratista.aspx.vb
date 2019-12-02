Public Class modificarContratista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            BtnAceptar.Visible = False
            Dim rutContratista As String = Session("rutEmpresa")
            Dim contratista As New clsContratista
            Dim dt As New DataTable
            dt = contratista.obtenerContratista(rutContratista)
            Dim row As DataRow
            row = dt.Rows(0)
            TxtNombre.Text = row("nombre").ToString
            'TxtLogin.Text = row("login").ToString
            'TxtClave.Text = row("clave").ToString
            TxtRut.Text = row("rut").ToString
            TxtFono.Text = row("fono").ToString
            TxtCorreo.Text = row("correo").ToString
            'TxtClave.Attributes("type") = "password"
            'cargarEstado(row("estado").ToString)
            bloquearCampos()
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

    Public Sub bloquearCampos()
        TxtNombre.ReadOnly = True
        TxtRut.ReadOnly = True
        'TxtLogin.ReadOnly = True
        'TxtClave.ReadOnly = True
        TxtFono.ReadOnly = True
        TxtCorreo.ReadOnly = True
        'DropEstado.Enabled = False
        'DropEstado.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
    End Sub

    Public Sub desbloquearCampos()
        TxtNombre.ReadOnly = False
        TxtFono.ReadOnly = False
        TxtCorreo.ReadOnly = False
        'DropEstado.Enabled = True
    End Sub

    'Public Sub cargarEstado(estado As String)
    '    DropEstado.Items.Clear()
    '    If estado = "A" Then
    '        Dim item As New ListItem()
    '        item.Text = "Activo"
    '        item.Value = "A"
    '        DropEstado.Items.Add(item)
    '        Dim item2 As New ListItem()
    '        item2.Text = "Inactivo"
    '        item2.Value = "I"
    '        DropEstado.Items.Add(item2)
    '    End If
    '    If estado = "I" Then
    '        Dim item As New ListItem()
    '        item.Text = "Inactivo"
    '        item.Value = "I"
    '        DropEstado.Items.Add(item)
    '        Dim item2 As New ListItem()
    '        item2.Text = "Activo"
    '        item2.Value = "A"
    '        DropEstado.Items.Add(item2)
    '    End If

    'End Sub

    'Public Sub cargarOtrosEstados()
    '    Dim item As New ListItem()
    '    If DropEstado.Text = "Activo" Then
    '        item.Text = "Inactivo"
    '        DropEstado.Items.Add(item)
    '    End If
    '    If DropEstado.Text = "Inactivo" Then
    '        item.Text = "Activo"
    '        DropEstado.Items.Add(item)
    '    End If

    'End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim contratista As New clsContratista
        Dim log As New clsLog
        Dim accion As Boolean
        Dim registro As Boolean
        LblAdvertencia.Text = ""
        If (TxtNombre.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "") Then
            LblAdvertencia.Text = "Ha ocurrido un error. Uno de los campos se encuentra en blanco."
        Else
            accion = contratista.actualizarContratista(TxtNombre.Text.Trim(), TxtRut.Text.Trim(), TxtFono.Text.Trim(), TxtCorreo.Text.Trim())
            If accion = False Then
                LblAdvertencia.Text = "Ha ocurrido un error en la conexión. Favor inténtelo nuevamente."
            Else
                LblAdvertencia.Text = "Se ha modificado al Contratista con éxito."
                registro = log.insertarRegistro("Se ha modificado al contratista de rut: " + TxtRut.Text.Trim(), Session("usuario").getRut)
                bloquearCampos()
                BtnAceptar.Visible = False
                btnModificar.Visible = True
                'DropEncargados.Items.Clear()
                'cargarEncargadoEmpresa(TxtRut.Text)
            End If
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        LblAdvertencia.Text = ""
        'DropEstado.Items.Clear()
        'cargarEncargadoEmpresa(TxtRut.Text)
        desbloquearCampos()
        'cargarOtrosContratistasDisponibles()
        btnModificar.Visible = False
        BtnAceptar.Visible = True
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
        Response.Redirect("verContratistas.aspx")
    End Sub

End Class