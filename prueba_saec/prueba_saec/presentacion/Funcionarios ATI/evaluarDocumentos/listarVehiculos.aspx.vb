Public Class listarVehiculos1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        'cargarMenu()
        Dim vehiculos = New clsVehiculo()
        Dim idCarpeta As Integer = 113
        Dim idArea As Integer = 2
        Dim TablaVehiculos As DataTable = vehiculos.listarVehiculosParaEvaluar(idCarpeta, idArea)
        'Session("rutEmpresa") = vehiculos.obtenerRutEmpresa(rutContratista).Rows(0).Item(0)
        gridListarVehiculosParaEvaluar.DataSource = TablaVehiculos
        gridListarVehiculosParaEvaluar.DataBind()

    End Sub

    Protected Sub btnIrVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarVehiculosParaEvaluar.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim patente As String = gridListarVehiculosParaEvaluar.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarVehiculosParaEvaluar.Rows(pos).Cells(2).Text
            Dim idVehiculo As Integer = gridListarVehiculosParaEvaluar.Rows(pos).Cells(3).Text
            Session("patente") = patente
            Session("idVehiculo") = idVehiculo

            Response.Redirect("evaluarDocumentosVehiculo.aspx")

        End If

    End Sub

End Class