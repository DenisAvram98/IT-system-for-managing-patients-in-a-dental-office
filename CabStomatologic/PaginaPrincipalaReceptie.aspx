<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaPrincipalaReceptie.aspx.cs" Inherits="CabStomatologic.PaginaPrincipalaReceptie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Pagina Principala</title>

    <!-- Bootstrap core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <!-- Navigation -->
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark static-top">
                <div class="container">
                    <a class="navbar-brand_2" href="#" style="text-decoration: none;">
                        <img src="vendor/bootstrap/Asset 16.png" />
                        Denis - Dent
                    </a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarResponsive">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item active">
                                <a class="nav-link" href="PaginaPrincipalaReceptie.aspx">Pagina Principala
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="ConfirmareProgramari.aspx">Programari</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="ContReceptieAutentificare.aspx">
                                    <asp:Label ID="LabelIesireReceptie" runat="server" Text="Iesire"></asp:Label></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#"></a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <!-- Page Content -->
            <div class="container">
                <div class="row text-center">
                    <div class="col-lg-12 text-center">
                        <h2>Receptie</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" CssClass="lead"></asp:Label>
                        <div style="background-color: white; border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 35%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Cautati pacientul dupa CNP"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px; width: 25%;">
                                                    <asp:TextBox ID="cnpCautatTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Button ID="cautaPacientCNPBtn" runat="server" Text="Cauta" CssClass="buton1" OnClick="cautaPacientCNPBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 35%;">
                                                    <asp:Label ID="Label2" runat="server" Text="Sau dupa nume"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px; width: 25%;">
                                                    <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Button ID="cautaNumeBtn" runat="server" Text="Cauta" CssClass="buton1" OnClick="cautaNumeBtn_Click" />
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Button ID="anulareCautareBtn" runat="server" Text="Anulare cautare" CssClass="buton1" OnClick="anulareCautareBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="4">
                                                    <asp:Label ID="LabelCautareErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div id="dvScroll" style="padding-left: 20px; padding-right: 20px; height: 250px; width: 800px; overflow: auto; display: inline-block;">
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="Both" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                                <AlternatingRowStyle BackColor="#66CCFF" />
                                                <HeaderStyle BackColor="#33CCFF" />
                                                <SelectedRowStyle BorderColor="Green" BorderStyle="Solid" BorderWidth="5px" Font-Underline="True" />
                                            </asp:GridView>
                                        </div>
                                        <input type="hidden" id="div_position" name="div_position" />
                                        <script type="text/javascript">
                                            window.onload = function () {
                                                var div = document.getElementById("dvScroll");
                                                var div_position = document.getElementById("div_position");
                                                var position = parseInt('<%=Request.Form["div_position"] %>');
                                                if (isNaN(position)) {
                                                    position = 0;
                                                }
                                                div.scrollTop = position;
                                                div.onscroll = function () {
                                                    div_position.value = div.scrollTop;
                                                };
                                            };
                                        </script>

                                        <table style="position: center; width: 100%">
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelActivare" runat="server" Text="Selectati pacientul pentru a modifica prezenta si activa butoanele de mai jos." ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 25%;">
                                                    <asp:Label ID="LabelPrezent" runat="server" Text="Prezent" Visible="false"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="prezentDDL" runat="server" Width="204px" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="prezentDDL_SelectedIndexChanged">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Da</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelPrezentErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <table style="position:center; width:100%;">
                                            <tr>
                                                <td style="text-align:center; padding:5px;">
                                                    <asp:Button ID="adaugaPacientNouBtn" runat="server" Text="Adauga pacient nou" CssClass="buton1" OnClick="adaugaPacientNouBtn_Click"/>
                                                </td>
                                                <td style="text-align:center; padding:5px;">
                                                    <asp:Button ID="vizEditFisaPacientBtn" runat="server" CssClass="buton1" Text="Vizualizare / Editare fisa pacient" Enabled="false" Font-Bold="False" ForeColor="Silver" OnClick="vizEditFisaPacientBtn_Click"/>
                                                </td>
                                                <td style="text-align:center; padding:5px;">
                                                    <asp:Button ID="consultatiiNeachitateBtn" runat="server" Text="Consultatii neachitate" CssClass="buton1" OnClick="consultatiiNeachitateBtn_Click"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr />
                                                </td>
                                                <td>
                                                    <hr />
                                                </td>
                                                <td>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align:center; padding:5px;">
                                                    <asp:Button ID="tiparireChitantaBtn" runat="server" Text="Tiparire chitanta" CssClass="buton1" Enabled="false" Font-Bold="False" ForeColor="Silver" OnClick="tiparireChitantaBtn_Click"/>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <br />
                        <ul class="list-unstyled">
                            <li></li>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Bootstrap core JavaScript -->
            <script src="vendor/jquery/jquery.slim.min.js"></script>
            <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
        </div>
    </form>
</body>
</html>
