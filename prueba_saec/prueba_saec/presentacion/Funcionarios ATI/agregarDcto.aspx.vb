Imports System.Linq
Public Class agregarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            cargarAreas()
            cargarDocumentos()

        End If

    End Sub

    Public Sub cargarAreas()

        Dim checked(8) As Integer

        Dim areas As DataTable = obtenerTablaAreas()

        chkListaAreas.Items.Clear()

        For Each celda As DataRow In areas.Rows

            Dim item As New ListItem()

            item.Text = celda("descripcion").ToString()
            item.Value = celda("id")

            chkListaAreas.Items.Add(item)

        Next


    End Sub

    Public Sub cargarDocumentos()

        Dim documentos As DataTable = obtenerDocumentos()

        'dropTipoDocumento.Items.Clear()
        'dropTipoDocumento.Items.Add("")

        'For Each celda As DataRow In documentos.Rows

        '    Dim itemDrop As New ListItem

        '    itemDrop.Text = celda("tipo").ToString()

        '    itemDrop.Value = celda("tipo").ToString()

        '    dropTipoDocumento.Items.Add(itemDrop)

        'Next

    End Sub

    Public Function obtenerTablaAreas() As DataTable
        Dim Areas = New clsArea()
        Return Areas.obtenerNombre()
    End Function

    Public Function obtenerDocumentos() As DataTable
        Dim Documentos = New clsDocumento()
        Return Documentos.obtenerDocumento()
    End Function

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("../login.aspx")
    End Sub

    Protected Sub btnCrearDocumento_Click(sender As Object, e As EventArgs) Handles btnCrearDocumento.Click

        Dim nuevoDocumento As New clsDocumento

        Dim insercion As New Boolean

        'For Each item In chkListaAreas.Items



        '    If item.Selected Then

        '        insercion = nuevoDocumento.insertarDocumento(txtNombreDocumento.Text,
        '                                                     dropTipoDocumento.SelectedItem.Value,
        '                                                     chkListaAreas.SelectedItem.Value)

        '    End If

        'Next


        If (txtNombreDocumento.Text.Trim() = "" Or dropTipoDocumento.Items.Equals("")) Then
            lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else

            For Each item In chkListaAreas.Items

                If item.Selected Then

                    insercion = nuevoDocumento.insertarDocumento(txtNombreDocumento.Text,
                                                             dropTipoDocumento.SelectedItem.Value,
                                                             item.Value)

                End If

            Next item

            'dropTipoDocumento.
            'chkListaAreas.
            'txtNombreDocumento.Text = ""

            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

        End If


    End Sub
End Class