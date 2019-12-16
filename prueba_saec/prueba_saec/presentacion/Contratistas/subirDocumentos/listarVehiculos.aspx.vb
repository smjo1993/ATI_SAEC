Public Class listarVehiculos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinVehiculos.Visible = False
        validarContratista()
        If IsPostBack Then
            Return
        End If

        cargarMenu()
        Dim vehiculos = New clsVehiculo()
        Dim rutContratista As String = Session("contratistaEntrante").getRut
        Dim TablaVehiculos As DataTable = vehiculos.listarVehiculos(rutContratista)
        Session("rutEmpresa") = vehiculos.obtenerRutEmpresa(rutContratista).Rows(0).Item(0)

        If TablaVehiculos Is Nothing Then
            sinVehiculos.Visible = True
        Else
            If TablaVehiculos.Rows.Count > 0 Then

                gridListarVehiculos.DataSource = TablaVehiculos
                gridListarVehiculos.DataBind()
            Else
                sinVehiculos.Visible = True
            End If
        End If


    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,4", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If

    End Sub

    Protected Sub btnIrVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarVehiculos.RowCommand

        If (e.CommandName = "ir") Then

            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim patente As String = gridListarVehiculos.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarVehiculos.Rows(pos).Cells(2).Text
            Dim idVehiculo As Integer = gridListarVehiculos.Rows(pos).Cells(3).Text
            Dim rutContratista As String = Session("contratistaEntrante").getRut
            Session("patente") = patente
            Session("idVehiculo") = idVehiculo
            Session("rutContratista") = rutContratista
            Response.Redirect("SubirDocumentosVehiculo.aspx")

        End If

        If (e.CommandName = "eliminar") Then

            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista   
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idVehiculo As Integer = gridListarVehiculos.Rows(pos).Cells(3).Text
            Dim vehiculo = New clsVehiculo()
            vehiculo.eliminarVehiculo(idVehiculo)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

    End Sub


    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

End Class