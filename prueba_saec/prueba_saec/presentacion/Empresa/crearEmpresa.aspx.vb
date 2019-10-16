Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows
Public Class crearEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarDatos()

    End Sub

    Public Sub cargarDatos()
        Dim contratista As New clsContratista
        Dim listaContratista As DataTable = contratista.listarContratistasHabilitados()
        dropContratistas.Items.Clear()
        dropContratistas.Items.Add("")
        For Each row As DataRow In listaContratista.Rows
            Dim item As New ListItem()
            item.Text = row("nombre").ToString()
            item.Value = row("rut").ToString()
            dropContratistas.Items.Add(item)
        Next

    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim empresa As New clsEmpresa
        Dim insercion As New Boolean
        If (TxtRazonSocial.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtGiro.Text.Trim() = "" Or TxtDireccion.Text.Trim() = "" Or TxtCiudad.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCelular.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "" Or dropContratistas.SelectedItem.Text.Trim() = "") Then
            lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else
            insercion = empresa.insertarEmpresa(TxtRazonSocial.Text.Trim(), TxtRut.Text.Trim(), TxtGiro.Text.Trim(), TxtDireccion.Text.Trim(), TxtCiudad.Text.Trim(), dropContratistas.SelectedItem.Text.Trim(), TxtFono.Text.Trim(), TxtCelular.Text.Trim(), TxtCorreo.Text.Trim(), dropContratistas.SelectedItem.Value.ToString.Trim())
        End If
    End Sub
End Class