<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusProgramari.aspx.cs" Inherits="CabStomatologic.StatusProgramari" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Programari</title>

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
                            <li class="nav-item">
                                <a class="nav-link" href="ContPacientPage.aspx">Pagina Principala
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item active">
                                <a class="nav-link" href="StatusProgramari.aspx">Programari</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="ContPacientAutentificare.aspx">
                                    <asp:Label ID="LabelIesirePacient" runat="server" Text="Iesire, "></asp:Label></a>
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
                        <h2>Status programari</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" CssClass="lead"></asp:Label>

                        <div style="background-color: white; border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%;">
                                            <tr style="white-space:nowrap;">
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Button ID="confirmateBtn" runat="server" Text="Confirmate" CssClass="buton1" OnClick="confirmateBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="neconfirmateBtn" runat="server" Text="Neconfirmate" CssClass="buton1" OnClick="neconfirmateBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="expirateBtn" runat="server" Text="Expirate" CssClass="buton1" OnClick="expirateBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="toateBtn" runat="server" Text="Toate" CssClass="buton1" OnClick="toateBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;">
                                                    <asp:Label ID="LabelStatusProgramariErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                            HorizontalAlign="Center" BackColor="White" GridLines="Both"
                                            Font-Bold="false" Font-Underline="false" Font-Strikeout="false"
                                            Font-Overline="false" OnRowDataBound="GridView1_RowDataBound">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                        <br />
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
