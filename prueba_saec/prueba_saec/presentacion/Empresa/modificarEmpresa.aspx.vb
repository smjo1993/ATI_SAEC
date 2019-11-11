Public Class modificarEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BtnAceptar.Visible = False
            Dim rutEmpresa As String = Session("rutEmpresa")
            Dim empresa As New clsEmpresa
            Dim dt As New DataTable
            dt = empresa.obtenerEmpresa(rutEmpresa)
            Dim row As DataRow
            row = dt.Rows(0)
            TxtRazonSocial.Text = row("razonSocial").ToString
            TxtRut.Text = row("rut").ToString
            TxtGiro.Text = row("giro").ToString
            TxtDireccion.Text = row("direccion").ToString
            TxtCiudad.Text = row("ciudad").ToString
            TxtFono.Text = row("fono").ToString
            TxtCelular.Text = row("celular").ToString
            TxtCorreo.Text = row("correo").ToString
            cargarEncargadoEmpresa(row("rut").ToString)
            'cargarOtrosContratistasDisponibles()
            bloquearCampos()
        End If
    End Sub

    'Public Sub cargarDropEmpresas()
    '    Dim empresa As New clsEmpresa
    '    Dim listaEmpresas As DataTable = empresa.listarEmpresas()

    '    Dim datav As New DataView
    '    datav = listaEmpresas.DefaultView
    '    datav.Sort = "razonSocial"
    '    listaEmpresas = datav.ToTable()

    '    dropEmpresas.Items.Clear()
    '    dropEmpresas.Items.Add("")
    '    For Each row As DataRow In listaEmpresas.Rows
    '        Dim item As New ListItem()
    '        item.Text = row("razonSocial").ToString()
    '        item.Value = row("rut").ToString()
    '        dropEmpresas.Items.Add(item)
    '    Next

    'End Sub

    Public Sub bloquearCampos()
        TxtRazonSocial.ReadOnly = True
        TxtRut.ReadOnly = True
        TxtGiro.ReadOnly = True
        TxtDireccion.ReadOnly = True
        TxtCiudad.ReadOnly = True
        TxtFono.ReadOnly = True
        TxtCelular.ReadOnly = True
        TxtCorreo.ReadOnly = True
        DropEncargados.Enabled = False
        DropEncargados.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
    End Sub

    Public Sub desbloquearCampos()
        TxtRazonSocial.ReadOnly = False
        TxtGiro.ReadOnly = False
        TxtDireccion.ReadOnly = False
        TxtCiudad.ReadOnly = False
        TxtFono.ReadOnly = False
        TxtCelular.ReadOnly = False
        TxtCorreo.ReadOnly = False
        DropEncargados.Enabled = True
    End Sub

    'Public Sub dropEmpresas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropEmpresas.SelectedIndexChanged
    '    If dropEmpresas.SelectedItem.Text.ToString <> "" Then
    '        Dim empresa As New clsEmpresa
    '        Dim dt As New DataTable
    '        dt = empresa.obtenerEmpresa(dropEmpresas.SelectedItem.Value.ToString())
    '        Dim row As DataRow
    '        row = dt.Rows(0)
    '        TxtRazonSocial.Text = row("razonSocial").ToString
    '        TxtRut.Text = row("rut").ToString
    '        TxtGiro.Text = row("giro").ToString
    '        TxtDireccion.Text = row("direccion").ToString
    '        TxtCiudad.Text = row("ciudad").ToString
    '        TxtFono.Text = row("fono").ToString
    '        TxtCelular.Text = row("celular").ToString
    '        TxtCorreo.Text = row("correo").ToString
    '        cargarEncargadoEmpresa(row("rut").ToString)
    '        cargarOtrosContratistasDisponibles()
    '        TxtRazonSocial.ReadOnly = False
    '        TxtGiro.ReadOnly = False
    '        TxtDireccion.ReadOnly = False
    '        TxtCiudad.ReadOnly = False
    '        TxtFono.ReadOnly = False
    '        TxtCelular.ReadOnly = False
    '        TxtCorreo.ReadOnly = False
    '    Else
    '        TxtRazonSocial.Text = ""
    '        TxtRut.Text = ""
    '        TxtGiro.Text = ""
    '        TxtDireccion.Text = ""
    '        TxtCiudad.Text = ""
    '        TxtFono.Text = ""
    '        TxtCelular.Text = ""
    '        TxtCorreo.Text = ""
    '        bloquearCampos()
    '        DropEncargados.Items.Clear()
    '    End If



    'End Sub

    Public Sub cargarEncargadoEmpresa(rut As String)
        DropEncargados.Items.Clear()
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.obtenerContratistaDeEmpresa(rut)
        Dim row As DataRow
        row = dt.Rows(0)
        Dim item As New ListItem()
        item.Text = row("nombre").ToString()
        item.Value = row("rut").ToString()
        DropEncargados.Items.Add(item)
    End Sub

    Public Function obtenerRutEncargadoEmpresa(rut As String) As String
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.obtenerContratistaDeEmpresa(rut)
        Dim row As DataRow
        row = dt.Rows(0)
        Return row("rut").ToString()
    End Function

    Public Sub cargarOtrosContratistasDisponibles()
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.listarContratistasSinempresa()
        'Ordenando la lista de Contratistas a agregar
        Dim datav As New DataView
        datav = dt.DefaultView
        datav.Sort = "nombre"
        dt = datav.ToTable()
        'Agregando los items
        For Each row As DataRow In dt.Rows
            Dim item As New ListItem()
            item.Text = row("nombre").ToString()
            item.Value = row("rut").ToString()
            DropEncargados.Items.Add(item)
        Next
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim empresa As New clsEmpresa
        Dim contratista As New clsContratista
        Dim accion As Boolean
        Dim accion2 As Boolean
        Dim accion3 As Boolean
        LblAdvertencia.Text = ""
        If (TxtRazonSocial.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtGiro.Text.Trim() = "" Or TxtDireccion.Text.Trim() = "" Or TxtCiudad.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCelular.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "") Then
            LblAdvertencia.Text = "Ha ocurrido un error. Uno de los campos se encuentra en blanco."
        Else
            accion3 = contratista.desactivarContratista(obtenerRutEncargadoEmpresa(TxtRut.Text.Trim()))
            accion = empresa.actualizarEmpresa(TxtRazonSocial.Text.Trim(), TxtRut.Text.Trim(), TxtGiro.Text.Trim(), TxtDireccion.Text.Trim(), TxtCiudad.Text.Trim(), DropEncargados.SelectedItem.Text.Trim(), TxtCorreo.Text.Trim(), TxtFono.Text.Trim(), TxtCelular.Text.Trim(), DropEncargados.SelectedItem.Value.Trim())
            accion2 = contratista.activarContratista(DropEncargados.SelectedItem.Value.Trim())

            If accion = False Or accion2 = False Or accion3 = False Then
                LblAdvertencia.Text = "Ha ocurrido un error en la conexión. Favor inténtelo nuevamente."
            Else
                LblAdvertencia.Text = "Se ha modificado la empresa con éxito."
                bloquearCampos()
                BtnAceptar.Visible = False
                btnModificar.Visible = True
                DropEncargados.Items.Clear()
                cargarEncargadoEmpresa(TxtRut.Text)
            End If
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        LblAdvertencia.Text = ""
        DropEncargados.Items.Clear()
        cargarEncargadoEmpresa(TxtRut.Text)
        desbloquearCampos()
        cargarOtrosContratistasDisponibles()
        btnModificar.Visible = False
        BtnAceptar.Visible = True
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
        Response.Redirect("verEmpresas.aspx")
    End Sub

End Class