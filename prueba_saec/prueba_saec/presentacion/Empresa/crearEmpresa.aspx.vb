Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows
Public Class crearEmpresa
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarMenu()
        If Not Page.IsPostBack Then
            cargarDatos()
        End If
    End Sub
    Public Sub cargarDatos()
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.listarContratistasSinempresa()
        dropContratistas.Items.Clear()
        dropContratistas.Items.Add("")
        For Each row As DataRow In dt.Rows
            Dim item As New ListItem()
            item.Text = row("nombre").ToString()
            item.Value = row("rut").ToString()
            dropContratistas.Items.Add(item)
        Next
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
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim empresa As New clsEmpresa
        Dim insercion As New Boolean
        Dim existe As DataTable
        existe = empresa.obtenerEmpresa(TxtRut.Text.Trim())
        If existe.Rows.Count > 0 Then
            lblAdvertencia.Text = "Rut de Empresa ya ingresado. Por favor verifíquelo."
        Else
            If (TxtRazonSocial.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtGiro.Text.Trim() = "" Or TxtDireccion.Text.Trim() = "" Or TxtCiudad.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCelular.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "" Or dropContratistas.SelectedItem.Text.Trim() = "") Then
                lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
            Else
                insercion = empresa.insertarEmpresa(TxtRazonSocial.Text.Trim(), TxtRut.Text.Trim(), TxtGiro.Text.Trim(), TxtDireccion.Text.Trim(), TxtCiudad.Text.Trim(), dropContratistas.SelectedItem.Text.Trim(), TxtFono.Text.Trim(), TxtCelular.Text.Trim(), TxtCorreo.Text.Trim(), dropContratistas.SelectedItem.Value.ToString.Trim())
                asignarContratista(dropContratistas.SelectedItem.Value.ToString.Trim())
                lblAdvertencia.Text = "Empresa creada con éxito."
                limpiarCampos()
                cargarDatos()
            End If
        End If



    End Sub

    Public Sub asignarContratista(rut As String)
        Dim contratista As New clsContratista
        Dim accion As New Boolean
        accion = contratista.activarContratista(rut)
    End Sub

    Public Sub limpiarCampos()
        TxtRazonSocial.Text = ""
        TxtRut.Text = ""
        TxtGiro.Text = ""
        TxtDireccion.Text = ""
        TxtCiudad.Text = ""
        TxtFono.Text = ""
        TxtCelular.Text = ""
        TxtCorreo.Text = ""
    End Sub
End Class