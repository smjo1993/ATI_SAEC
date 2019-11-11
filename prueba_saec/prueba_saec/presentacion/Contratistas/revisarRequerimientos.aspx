<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="revisarRequerimientos.aspx.vb" Inherits="prueba_saec.revisarRequerimientos" %>

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


            
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Poner algo aqui</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">

                    <table class="table table-bordered dataTable" id="tablaDoc" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">                       
                            <tr class="align-left">
                                <td class="auto-style1">
                                   
                                    <asp:GridView ID="gridDocumentos" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>
                                            
                                            <asp:BoundField DataField="nombreDoc" HeaderText="DOCUMENTOS" />
                                            <asp:BoundField DataField="nombreArea" HeaderText="ÁREA" />         
                                            <asp:TemplateField HeaderText="ACEPTAR">

                                                <ItemTemplate >

                                                    <%--<asp:CheckBox ID="chk" runat="server" CommandName="chekear" />--%>
                                                    <%--<input id="chk" type="checkbox" runat="server" />--%>
                                                    <input id="chk" type="checkbox" runat="server" />
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="AGREGAR COMENTARIO">
                                         
                                            </asp:TemplateField>

                                        </Columns>
                                        
                                    </asp:GridView>  
                                    
                                </td>
                            </tr>
                        </table>
                        
                        <input id="qwe" type="checkbox" runat="server"/>
                    </div>
                </div>
                
            </div>

        </div>
         
        <asp:Button ID="Button1" runat="server" Text="Confirmar solicitud" />
    </form>
</body>
</html>
