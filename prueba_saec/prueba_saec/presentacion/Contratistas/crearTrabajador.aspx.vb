﻿Public Class crearTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarMenu()

    End Sub

    Protected Sub cargarMenu()
        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnRealizarRegistro.Click
        Dim trabajador As New clsTrabajador
        Dim empresa As New clsEmpresa
        Dim insercion As New Boolean
        Dim log As New clsLog
        Dim registro As Boolean
        Dim nombreEmpresa As String
        nombreEmpresa = empresa.obtenerEmpresa(Session("rutEmpresa")).Rows(0).Item(0).ToString
        insercion = trabajador.insertarTrabajador(TxtRut.Text.Trim(), TxtNombre.Text.Trim(), TxtFono.Text.Trim(), TxtCorreo.Text.Trim(), Session("contratistaEntrante").getRut)
        registro = log.insertarRegistro("Se ha creado al trabajador de rut: " + TxtRut.Text.Trim() + " en la C.A de la empresa " + nombreEmpresa, Session("contratistaEntrante").getRut)
        Response.Redirect("../Contratistas/subirDocumentos/listarTrabajadores.aspx")
    End Sub


End Class