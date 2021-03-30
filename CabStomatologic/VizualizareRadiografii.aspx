<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VizualizareRadiografii.aspx.cs" Inherits="CabStomatologic.VizualizareRadiografii" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Vizualizare radiografii</title>

    <!-- Bootstrap core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 1000px;
        }
    </style>
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
                        <h2>Vizualizare radiografii</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>

                        <div style="background-color: white; border-radius: 12px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <table style="position: center; width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="Label1" runat="server" Text="Nume pacient"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="numePacientTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%;"></td>
                                            <td style="text-align: right; padding: 5px;">
                                                <asp:Label ID="Label2" runat="server" Text="CNP"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="cnpTB" runat="server" Enabled="false" Height="27px" Width="204px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label3" runat="server" Text="Imagini:" ForeColor="Silver"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;" colspan="4">
                                                <asp:Label ID="LabelRadiografiiErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align:left; padding:5px;" colspan="4">
                                                <asp:Label ID="Label9" runat="server" Text="*Click o data pe poza dorita - afiseaza mai jos <u>'Informatii radiografice'</u> despre poza respectiva." ForeColor="Silver"></asp:Label>
                                                <br />
                                                <asp:Label ID="Label10" runat="server" Text="**Click a doua oara pe <u>aceasi poza</u> - se deschide poza in format mai mare (rezolutia initiala)." ForeColor="Silver"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="containerImagini" style="text-align: left; padding: 5px;" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center"></asp:Panel>
                                                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" CssClass="modalPopup" align="center" Style="display: none;">
                                                    <img id="largeImg" src="" runat="server" style="max-width:800px;"/>
                                                    <div style="text-align: center; padding: 5px;">
                                                        <asp:Button ID="closeBtn" runat="server" Text="Inchide" />
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center"></asp:Panel>
                                                <!--HTML cod pentru V2:la click pe ibtn (ImageButton) se descnide automat Popup
                                                <asp:Panel ID="Panel22" runat="server" HorizontalAlign="Center"></asp:Panel>
                                                <asp:Panel ID="Panel33" runat="server" HorizontalAlign="Center"></asp:Panel>
                                                <asp:Panel ID="Panel44" runat="server" HorizontalAlign="Center"></asp:Panel>
                                                -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label4" runat="server" Text="Informatii radiografie:" ForeColor="Silver"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding: 5px;" colspan="4">
                                                <asp:Label ID="LabelInformatiiRErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label8" runat="server" Text="Nume imagine"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="numeImagineTB" runat="server" Enabled="false" Height="27px" Width="250px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="text-align: right; padding: 5px;">
                                                <asp:Label ID="Label5" runat="server" Text="Data"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="dataTB" runat="server" Enabled="false" Height="27px" Width="204px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label6" runat="server" Text="Diagnostic"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;" colspan="4">
                                                <asp:TextBox ID="diagnosticTB" runat="server" Enabled="false" TextMode="MultiLine" Height="200px" Width="550px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label7" runat="server" Text="Observatii"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;" colspan="4">
                                                <asp:TextBox ID="observatiiTB" runat="server" Enabled="false" TextMode="MultiLine" Height="70px" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/RadiografieNoua.aspx" ForeColor="Silver" Font-Size="Large">Adauga radiografie noua</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
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
