﻿Public Class evaluarDocumentosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If

        Dim vehiculo = New clsVehiculo()
        Dim idCarpeta As Integer = 113
        Dim idArea As Integer = 2
        Dim idVehiculo As Integer = Session("idVehiculo")
        Dim tablaDocumentosVehiculo = vehiculo.listarDocumentosVehiculoParaRevisar(idCarpeta, idArea, idVehiculo)
        gridListarDocumentosVehiculo.DataSource = tablaDocumentosVehiculo
        gridListarDocumentosVehiculo.DataBind()
        lblVehiculo.Text = Session("patente")

    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Protected Sub btnVerDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosVehiculo.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim ruta As String = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text
        Dim nombreArchivo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(1).Text
        Dim extension As String = ExtraerExtension(ruta, ".")

        If (e.CommandName = "ver") Then

            If extension = "pdf" Then

                'Se codifica la ruta del archivo para pasarlo por URl
                Dim rutaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(ruta)
                Dim rutaCodificada As String = System.Convert.ToBase64String(rutaBase64)
                'Response.Clear()
                'Response.ContentType = "application/pdf"
                Response.Write("<script type='text/javascript'>detailedresults=window.open('verDocumento.aspx?r=" + rutaCodificada + "');</script>")
                'Response.WriteFile(ruta)

            Else

                Response.Clear()
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", ruta))
                Response.WriteFile(ruta)
                Response.End()

            End If

        End If

    End Sub

    'funcion que obtiene la extension del archivo
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function

End Class