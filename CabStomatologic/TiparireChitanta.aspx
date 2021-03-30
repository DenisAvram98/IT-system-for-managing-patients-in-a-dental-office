<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TiparireChitanta.aspx.cs" Inherits="CabStomatologic.TiparireChitantaaspx" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <style type="text/css">
        .Printable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: none;
            }


            .Printable {
                display: block;
            }
        }
    </style>

    <title>Tiparire chitanta</title>

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
                        <h2 class="NonPrintable">Tiparire chitanta</h2>
                        <h2 class="Printable">Denis - Dent: Chitanta</h2>
                        <p class="lead"></p>
                        <br />
                        <div class="NonPrintable">
                            <asp:Label ID="LabelPrincipalErr" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>
                        </div>

                        <div style="background-color: white; border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume pacient"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numePacientTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 1%"></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label2" runat="server" Text="CNP"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="cnpTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label3" runat="server" Text="Data"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataTB" runat="server" Enabled="false" Height="27px" Width="204px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Ora"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="oraTB" runat="server" Enabled="false" Height="27px" Width="102px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label5" runat="server" Text="Numar consultatie"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="nrConsultatieDDL" runat="server" Width="204px" OnSelectedIndexChanged="nrConsultatieDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelNrConsultatieErr" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                                <td>
                                                    <hr />
                                                </td>
                                                <td>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label6" runat="server" Text="Informatii constultatie:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label7" runat="server" Text="Data"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataConsultatieTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label8" runat="server" Text="Ora"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="oraConsultatieTB" runat="server" Enabled="false" Height="27px" Width="102px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label9" runat="server" Text="Intervetni efectuate"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="interventiTB" runat="server" Enabled="false" TextMode="MultiLine" Width="550px" OnPreRender="interventiTB_PreRender" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label10" runat="server" Text="Cost total"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="costTotalTB" runat="server" Height="27px" Width="102px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label12" runat="server" Text="LEI"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label18" runat="server" Text="Nume medic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeMedicTB" runat="server" Height="27px" Enabled="false" Width="250px"></asp:TextBox>
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
                                                <td>
                                                    <hr />
                                                </td>
                                                <td>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label11" runat="server" Text="Achitat pana azi"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="sumaAchitataBazaDTB" runat="server" Height="27px" Width="102px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label13" runat="server" Text="LEI"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label14" runat="server" Text="Ramas de achitat"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="sumaRamasaTB" runat="server" Height="27px" Width="102px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label15" runat="server" Text="LEI"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label16" runat="server" Text="Achitat azi"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="achitatAziTB" runat="server" Height="27px" Width="102px" AutoPostBack="true" OnTextChanged="achitatAziTB_TextChanged" TextMode="Number" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label17" runat="server" Text="LEI"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px">
                                                    <asp:Label ID="LabelAchitatAziErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px" colspan="5">
                                                    <asp:Label ID="LabelRezultatTiparireErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Button ID="tiparireChitantaBtn" runat="server" Text="Tipareste chitanta" CssClass="buton1" OnClick="tiparireChitantaBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: left;">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ConsultatiiNeachitate.aspx" ForeColor="Silver" Font-Size="Large">Consultatii neachitate</asp:HyperLink>
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
