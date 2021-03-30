<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VizualizareFisaPacientMedic.aspx.cs" Inherits="CabStomatologic.VizualizareFisaPacientMedic"  MaintainScrollPositionOnPostback="true"%>

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

    <title>Vizualizare fisa pacient</title>

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
                        <h2 class="NonPrintable">Vizualizare fisa pacient</h2>
                        <h2 class="Printable">Denis - Dent: Fisa pacient</h2>
                        <p class="lead"></p>
                        <br />
                        <div class="NonPrintable">
                            <asp:Label ID="LabelPrincipalErr" runat="server" Text="" CssClass="lead"></asp:Label>
                        </div>

                        <div style="background-color: white; border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 16%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Nume mama (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeMamaTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label3" runat="server" Text="Prenume"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="prenumeTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Nume tata (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTataTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label5" runat="server" Text="CNP"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="cnpTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Data nasterii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataNasteriiTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label7" runat="server" Text="Sex"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="sexTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label8" runat="server" Text="Locul nasterii (Localitatea)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="locNastereTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label9" runat="server" Text="Telefon"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="telefonTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
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
                                                    <asp:Label ID="Label12" runat="server" Text="Domiciliu:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label11" runat="server" Text="Judet"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="judetTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label13" runat="server" Text="Oras"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="orasTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label14" runat="server" Text="Strada"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="stradaTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label15" runat="server" Text="Numar"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label16" runat="server" Text="Bloc"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="blocTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label17" runat="server" Text="Scara"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="scaraTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label18" runat="server" Text="Numar apartament"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarApartamentTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
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
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label19" runat="server" Text="Medic familie"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="medicFamilieTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label20" runat="server" Text="Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="observatiiTB" runat="server" TextMode="MultiLine" Width="400px" Enabled="false" OnPreRender="observatiiTB_PreRender"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Label ID="LabelRezultatPrintareErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Button ID="printareBtn" runat="server" Text="Printare" CssClass="buton1" OnClick="printareBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: left;" colspan="2">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Silver" NavigateUrl="~/VizualizareConsultatii.aspx">Vizualizare consultatii</asp:HyperLink>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right;">
                                                    <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="Silver" NavigateUrl="~/VizualizareRadiografii.aspx">Vizualizare radiografii</asp:HyperLink>
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
