<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PaginaPrincipalaMedic.aspx.cs" Inherits="CabStomatologic.PaginaPrincipalaMedic" %>

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
                                <a class="nav-link" href="PaginaPrincipalaMedic.aspx">Pagina Principala
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="Programari.aspx">Programari</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="ContMedicPage.aspx">Iesire cont medic</a>
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
                        <h2>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="" ForeColor="red" CssClass="lead"></asp:Label>

                        <div style="background-color: white; border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; padding-left: 20px; padding-right: 20px; width: 800px; display: inline-block;">
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label3" runat="server" Text="Pacientii prezenti" Font-Size="Larger"></asp:Label>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Button ID="refreshBtn" runat="server" Text="Reimprospatare" CssClass="buton1" OnClick="anulareCautareBtn_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" ScrollBars="Auto">
                                    <div id="dvScroll" style="padding-left: 20px; padding-right: 20px; height: 250px; width: 800px;overflow: auto; display: inline-block;">
                                        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="Both" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" OnRowCommand="GridView1_RowCommand">
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
                                </asp:Panel>

                                <br />
                                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px; display:inline-block;">
                                        <table style="position: center; width: 100%">
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="3">
                                                    <asp:Label ID="LabelButoaneActiveDezactive" runat="server" Text="" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;"></td>
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="adaugaConsultatieBtn" runat="server" Text="Adauga consultatie noua" CssClass="buton1" Enabled="False" Font-Bold="False" ForeColor="Silver" OnClick="adaugaConsultatieBtn_Click" />
                                                </td>
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="adaugaRadiografiiBtn" runat="server" Text="Adauga radiografie noua" CssClass="buton1" Enabled="False" Font-Bold="False" ForeColor="Silver" OnClick="adaugaRadiografiiBtn_Click" />
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
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="vizFisaPacientBtn" runat="server" Text="Vizualizare fisa pacient" CssClass="buton1" Enabled="False" Font-Bold="False" ForeColor="Silver" OnClick="vizFisaPacientBtn_Click" />
                                                </td>
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="vizConsultatiPacientBtn" runat="server" Text="Vizulaizare consultatii pacient" CssClass="buton1" Enabled="False" Font-Bold="False" ForeColor="Silver" OnClick="vizConsultatiPacientBtn_Click" />
                                                </td>
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="vizRadiografiiBtn" runat="server" Text="Vizualizare radiografii" CssClass="buton1" Enabled="False" Font-Bold="False" ForeColor="Silver" OnClick="vizRadiografiiBtn_Click" />
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
