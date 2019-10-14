Public Class iniciarCarpetaArranque
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMensaje.Text = ""
        If Not Page.IsPostBack Then
            lblMensaje.Text = ""
            cargarDatos()
        End If
    End Sub

    Protected Sub cargarDatos()
        Dim empresa As New clsEmpresa
        Dim listaEmpresas As DataTable = empresa.obtenerEmpresas
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

    Protected Sub btnCrearCarpeta_Click(sender As Object, e As EventArgs) Handles btnCrearCarpeta.Click
        Dim alerta As New clsAlertas

        Dim empresa As String = dropEmpresas.SelectedValue

        Dim fechaExpiracion As String = txtFecha.Text

        Dim fecha As Date

        If (txtFecha.Text = "") Then

            fecha = DateAdd("m", 12, Today)

            fechaExpiracion = Format$(fecha, "yyyy/MM/dd").ToString()

            lblMensaje.Text = alerta.alerta("ALERTA", fechaExpiracion + " " + empresa)

        Else

            Dim hoy As Date = Today

            fecha = Convert.ToDateTime(txtFecha.Text)

            If (DateTime.Compare(hoy, fecha) = 0 Or DateTime.Compare(hoy, fecha) > 0) Then
                lblMensaje.Text = alerta.alerta("ALERTA", "fecha erronea")
            End If

            'lblMensaje.Text = alerta.alerta("ALERTA", hoy + " " + empresa)

        End If


    End Sub
    Protected Sub btnCrearEmpresa_Click(sender As Object, e As EventArgs) Handles btnCrearEmpresa.Click
        Response.Redirect("crearEmpresa.aspx")
    End Sub
End Class