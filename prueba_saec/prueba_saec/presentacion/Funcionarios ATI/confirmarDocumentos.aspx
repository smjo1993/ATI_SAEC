
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="confirmarDocumentos.aspx.vb" Inherits="prueba_saec.confirmarDocumentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">
    <link href="../../css/checkbox.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">

        <div class="container-fluid">


            <%-- EMPRESA --%>
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">EMPRESA</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">


                        <table class="table table-bordered dataTable" id="tablaDoc" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">
                            <tr class="align-left">
                                <td class="auto-style1">

                                    <asp:GridView ID="confirmarEmpresa" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>
                                            
                                            <asp:BoundField DataField="nombreDoc" HeaderText="DOCUMENTOS" />
                                            <asp:BoundField DataField="nombreArea" HeaderText="ÁREA" />
                                            <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:TemplateField HeaderText="CONFIRMAR">

                                                <ItemTemplate>
                                                    <label class="switch ">
                                                        <input id="chk" type="checkbox" runat="server" />
                                                        <span class="slider round"></span>
                                                    </label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COMENTARIOS">

                                                <ItemTemplate>
                                                    <asp:Button ID="btnComentario" CssClass="button primary" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" Text="Comentarios" />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ELIMINAR">

                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnEliminarDocumento"
                                                        ImageUrl="http://files.softicons.com/download/toolbar-icons/flatastic-icons-part-1-by-custom-icon-design/png/32x32/delete.png"
                                                        CommandName="eliminar"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        runat="server"
                                                        />
                                                </ItemTemplate>

                                             </asp:TemplateField>


                                        </Columns>

                                    </asp:GridView>

                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

            </div>

            <%-- TRABAJADOR --%>
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">TRABAJADOR</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered dataTable" id="tablaDoc" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">
                            <tr class="align-left">
                                <td class="auto-style1">

                                    <asp:GridView ID="confirmarTrabajador" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>

                                            <asp:BoundField DataField="nombreDoc" HeaderText="DOCUMENTOS" />
                                            <asp:BoundField DataField="nombreArea" HeaderText="ÁREA" />
                                            <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:TemplateField HeaderText="CONFIRMAR">

                                                <ItemTemplate>
                                                    <label class="switch ">
                                                        <input id="chk" type="checkbox" runat="server" />
                                                        <span class="slider round"></span>
                                                    </label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COMENTARIOS">

                                                <ItemTemplate>
                                                    <asp:Button ID="Button2" runat="server" Text="Button" />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ELIMINAR">

                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnEliminarDocumento"
                                                        ImageUrl="http://files.softicons.com/download/toolbar-icons/flatastic-icons-part-1-by-custom-icon-design/png/32x32/delete.png"
                                                        CommandName="eliminar"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        runat="server"
                                                        />
                                                </ItemTemplate>

                                             </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

            </div>

            <%-- VEHICULO --%>
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">VEHICULO</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered dataTable" id="tablaDoc" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">
                            <tr class="align-left">
                                <td class="auto-style1">

                                    <asp:GridView ID="confirmarVehiculo" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>

                                            <asp:BoundField DataField="nombreDoc" HeaderText="DOCUMENTOS" />
                                            <asp:BoundField DataField="nombreArea" HeaderText="ÁREA" />
                                            <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>
                                            <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:TemplateField HeaderText="CONFIRMAR">

                                                <ItemTemplate>
                                                    <label class="switch ">
                                                        <input id="chk" type="checkbox" runat="server" />
                                                        <span class="slider round"></span>
                                                    </label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COMENTARIOS">

                                                <ItemTemplate>
                                                    <asp:Button ID="Button5" runat="server" Text="Button" />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="ELIMINAR">

                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnEliminarDocumento"
                                                        ImageUrl="http://files.softicons.com/download/toolbar-icons/flatastic-icons-part-1-by-custom-icon-design/png/32x32/delete.png"
                                                        CommandName="eliminar"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        runat="server"
                                                        />
                                                </ItemTemplate>

                                             </asp:TemplateField>

                                        </Columns>
                                       
                                    </asp:GridView>

                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

            </div>
            <asp:Button ID="btnConfirmarDocumentos" runat="server" Text="Confirmar solicitud" />


        </div>


    </form>
</body>
</html>
