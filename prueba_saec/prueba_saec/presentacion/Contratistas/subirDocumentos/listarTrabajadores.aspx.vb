Public Class listarTrabajadores
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinTrabajadores.Visible = False
        validarContratista()
        If IsPostBack Then
            Return
        End If
        cargarMenu()

        Dim trabajadores = New clsTrabajador()
        Dim rutContratista As String = Session("contratistaEntrante").getRut
        Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadores(rutContratista)
        Session("rutEmpresa") = trabajadores.obtenerRutEmpresa(rutContratista).Rows(0).Item(0)

        If TablaTrabajadores Is Nothing Then
            sinTrabajadores.Visible = True
        Else
            If TablaTrabajadores.Rows.Count > 0 Then
                gridListarTrabajadores.DataSource = TablaTrabajadores
                gridListarTrabajadores.DataBind()
            Else
                sinTrabajadores.Visible = True
            End If
        End If

    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,2", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If

    End Sub
    Protected Sub btnIrTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarTrabajadores.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutTrabajador As String = gridListarTrabajadores.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarTrabajadores.Rows(pos).Cells(2).Text
            Dim idTrabajador As Integer = gridListarTrabajadores.Rows(pos).Cells(3).Text
            Dim trabajador = New clsTrabajador()
            Dim rutContratista As String = Session("contratistaEntrante").getRut
            Session("rutTrabajador") = rutTrabajador
            Session("idTrabajador") = idTrabajador
            Session("rutContratista") = rutContratista
            Session("documentosTrabajador") = trabajador.listarDocumentosTrabajador(idTrabajador, rutContratista)
            Response.Redirect("SubirDocumentosTrabajador.aspx")

        End If


        If (e.CommandName = "eliminar") Then

            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista   
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idTrabajador As Integer = gridListarTrabajadores.Rows(pos).Cells(3).Text
            Dim trabajador = New clsTrabajador()
            trabajador.eliminarTrabajador(idTrabajador)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

    End Sub

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

End Class