<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultatieNoua.aspx.cs" Inherits="CabStomatologic.ConsultatieNoua" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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

    <title>Adauga consultatie noua</title>

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
                        <h2 class="NonPrintable">Adaugare consultatie noua
                        </h2>
                        <h2 class="Printable">Denis - Dent: Consultatie</h2>
                        <p class="lead"></p>
                        <br />
                        <div class="NonPrintable">
                            <asp:Label ID="LabelPrincipalErr" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>
                        </div>

                        <div style="background-color: white; border-radius: 12px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 20%">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume pacient"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numePacientTB" runat="server" Height="27px" Width="250px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label2" runat="server" Text="CNP"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="cnpTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Data nasterii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataNasteriiTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
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
                                                    <asp:Label ID="Label3" runat="server" Text="Domiciliu:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label5" runat="server" Text="Judet"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="judetTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Oras"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="orasTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label7" runat="server" Text="Strada"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="stradaTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label8" runat="server" Text="Numar"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarTB" runat="server" Height="27px" Width="102px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label9" runat="server" Text="Bloc"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="blocTB" runat="server" Height="27px" Width="102px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Scara"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="scaraTB" runat="server" Height="27px" Width="102px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label11" runat="server" Text="Numar apartament"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="nrApartamentTB" runat="server" Height="27px" Width="102px" Enabled="False"></asp:TextBox>
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
                                                    <asp:Label ID="Label12" runat="server" Text="Data"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label13" runat="server" Text="Ora"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px">
                                                    <asp:TextBox ID="oraTB" runat="server" Height="27px" Width="102px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                        <table class="Printable" style="position:center; width:100%;">
                                            <tr>
                                                <td style="text-align:right; width:20%;">
                                                    <asp:Label ID="Label30" runat="server" Text="Diagnostic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="diagnosticPrintTB" runat="server" TextMode="MultiLine" Enabled="false" Width="550px" OnPreRender="diagnosticPrintTB_PreRender" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label31" runat="server" Text="Interventie"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left;padding:5px;">
                                                    <asp:TextBox ID="interventiePrintTB" runat="server" TextMode="MultiLine" Enabled="false" Width="550" OnPreRender="interventiePrintTB_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label32" runat="server" Text="Planul de tratament + Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="planTratamentObservatiiPrintTB" runat="server" TextMode="MultiLine" Enabled="false" Width="400px" OnPreRender="planTratamentObservatiiPrintTB_PreRender"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                        <br class="NonPrintable" />
                                        <div class="NonPrintable" style="display: inline-block;">
                                            <table class="NonPrintable" style="position: center; width: 100%;">
                                                <tr>
                                                    <td style="text-align: left; padding: 5px;" colspan="16">
                                                        <asp:Label ID="Label14" runat="server" Text="Selectati dintele la care sa lucrat."></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte18IBtn" runat="server" ImageUrl="~/Poze dinti/18.png" Style="height: 100px; width: auto;" OnClick="dinte18IBtn_Click" />
                                                        <br />
                                                        18
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte17IBtn" runat="server" ImageUrl="~/Poze dinti/17.png" Style="height: 100px; width: auto;" OnClick="dinte17IBtn_Click" />
                                                        <br />
                                                        17
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte16IBtn" runat="server" ImageUrl="~/Poze dinti/16.png" Style="height: 100px; width: auto;" OnClick="dinte16IBtn_Click" />
                                                        <br />
                                                        16
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte15IBtn" runat="server" ImageUrl="~/Poze dinti/15.png" Style="height: 100px; width: auto;" OnClick="dinte15IBtn_Click" />
                                                        <br />
                                                        15
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte14IBtn" runat="server" ImageUrl="~/Poze dinti/14.png" Style="height: 100px; width: auto;" OnClick="dinte14IBtn_Click" />
                                                        <br />
                                                        14
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte13IBtn" runat="server" ImageUrl="~/Poze dinti/13.png" Style="height: 100px; width: auto;" OnClick="dinte13IBtn_Click" />
                                                        <br />
                                                        13
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte12IBtn" runat="server" ImageUrl="~/Poze dinti/12.png" Style="height: 100px; width: auto;" OnClick="dinte12IBtn_Click" />
                                                        <br />
                                                        12
                                                    </td>
                                                    <td style="text-align: center; padding: 5px; border-right: solid; border-color: silver; padding-right: 10px;">
                                                        <asp:ImageButton ID="dinte11IBtn" runat="server" ImageUrl="~/Poze dinti/11.png" Style="height: 100px; width: auto;" OnClick="dinte11IBtn_Click" />
                                                        <br />
                                                        11
                                                    </td>
                                                    <td style="text-align: center; padding: 5px; padding-left: 10px;">
                                                        <asp:ImageButton ID="dinte21IBtn" runat="server" ImageUrl="~/Poze dinti/21.png" Style="height: 100px; width: auto;" OnClick="dinte21IBtn_Click" />
                                                        <br />
                                                        21
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte22IBtn" runat="server" ImageUrl="~/Poze dinti/22.png" Style="height: 100px; width: auto;" OnClick="dinte22IBtn_Click" />
                                                        <br />
                                                        22
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte23IBtn" runat="server" ImageUrl="~/Poze dinti/23.png" Style="height: 100px; width: auto;" OnClick="dinte23IBtn_Click" />
                                                        <br />
                                                        23
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte24IBtn" runat="server" ImageUrl="~/Poze dinti/24.png" Style="height: 100px; width: auto;" OnClick="dinte24IBtn_Click" />
                                                        <br />
                                                        24
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte25IBtn" runat="server" ImageUrl="~/Poze dinti/25.png" Style="height: 100px; width: auto;" OnClick="dinte25IBtn_Click" />
                                                        <br />
                                                        25
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte26IBtn" runat="server" ImageUrl="~/Poze dinti/26.png" Style="height: 100px; width: auto;" OnClick="dinte26IBtn_Click" />
                                                        <br />
                                                        26
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte27IBtn" runat="server" ImageUrl="~/Poze dinti/27.png" Style="height: 100px; width: auto;" OnClick="dinte27IBtn_Click" />
                                                        <br />
                                                        27
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte28IBtn" runat="server" ImageUrl="~/Poze dinti/28.png" Style="height: 100px; width: auto;" OnClick="dinte28IBtn_Click" />
                                                        <br />
                                                        28
                                                    </td>
                                                </tr>
                                                <tr style="border-top: solid; border-color: silver;">
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte48IBtn" runat="server" ImageUrl="~/Poze dinti/48.png" Style="height: 100px; width: auto;" OnClick="dinte48IBtn_Click" />
                                                        <br />
                                                        48
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte47IBtn" runat="server" ImageUrl="~/Poze dinti/47.png" Style="height: 100px; width: auto;" OnClick="dinte47IBtn_Click" />
                                                        <br />
                                                        47
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte46IBtn" runat="server" ImageUrl="~/Poze dinti/46.png" Style="height: 100px; width: auto;" OnClick="dinte46IBtn_Click" />
                                                        <br />
                                                        46
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte45IBtn" runat="server" ImageUrl="~/Poze dinti/45.png" Style="height: 100px; width: auto;" OnClick="dinte45IBtn_Click" />
                                                        <br />
                                                        45
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte44IBtn" runat="server" ImageUrl="~/Poze dinti/44.png" Style="height: 100px; width: auto;" OnClick="dinte44IBtn_Click" />
                                                        <br />
                                                        44
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte43IBtn" runat="server" ImageUrl="~/Poze dinti/43.png" Style="height: 100px; width: auto;" OnClick="dinte43IBtn_Click" />
                                                        <br />
                                                        43
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte42IBtn" runat="server" ImageUrl="~/Poze dinti/42.png" Style="height: 100px; width: auto;" OnClick="dinte42IBtn_Click" />
                                                        <br />
                                                        42
                                                    </td>
                                                    <td style="text-align: center; padding: 5px; border-right: solid; border-color: silver; padding-right: 10px;">
                                                        <asp:ImageButton ID="dinte41IBtn" runat="server" ImageUrl="~/Poze dinti/41.png" Style="height: 100px; width: auto;" OnClick="dinte41IBtn_Click" />
                                                        <br />
                                                        41
                                                    </td>
                                                    <td style="text-align: center; padding: 5px; padding-left: 10px;">
                                                        <asp:ImageButton ID="dinte31IBtn" runat="server" ImageUrl="~/Poze dinti/31.png" Style="height: 100px; width: auto;" OnClick="dinte31IBtn_Click" />
                                                        <br />
                                                        31
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte32IBtn" runat="server" ImageUrl="~/Poze dinti/32.png" Style="height: 100px; width: auto;" OnClick="dinte32IBtn_Click" />
                                                        <br />
                                                        32
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte33IBtn" runat="server" ImageUrl="~/Poze dinti/33.png" Style="height: 100px; width: auto;" OnClick="dinte33IBtn_Click" />
                                                        <br />
                                                        33
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte34IBtn" runat="server" ImageUrl="~/Poze dinti/34.png" Style="height: 100px; width: auto;" OnClick="dinte34IBtn_Click" />
                                                        <br />
                                                        34
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte35IBtn" runat="server" ImageUrl="~/Poze dinti/35.png" Style="height: 100px; width: auto;" OnClick="dinte35IBtn_Click" />
                                                        <br />
                                                        35
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte36IBtn" runat="server" ImageUrl="~/Poze dinti/36.png" Style="height: 100px; width: auto;" OnClick="dinte36IBtn_Click" />
                                                        <br />
                                                        36
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte37IBtn" runat="server" ImageUrl="~/Poze dinti/37.png" Style="height: 100px; width: auto;" OnClick="dinte37IBtn_Click" />
                                                        <br />
                                                        37
                                                    </td>
                                                    <td style="text-align: center; padding: 5px;">
                                                        <asp:ImageButton ID="dinte38IBtn" runat="server" ImageUrl="~/Poze dinti/38.png" Style="height: 100px; width: auto;" OnClick="dinte38IBtn_Click" />
                                                        <br />
                                                        38
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <br class="NonPrintable" />
                                        <br class="NonPrintable" />
                                        <table style="position: center; width: 100%;">
                                            <tr class="NonPrintable">
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label15" runat="server" Text="Dintele selectat"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dinteTB" runat="server" Height="27px" Width="204px" Enabled="False"></asp:TextBox>
                                                    <asp:Button ID="stergeDinteBtn" runat="server" Text="Sterge" OnClick="stergeDinteBtn_Click" />
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelDinteErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
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
                                            <tr class="NonPrintable">
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label23" runat="server" Text="Diagnostic:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="diagnosticTB" runat="server" TextMode="MultiLine" Width="550px" Height="100px" Enabled="False" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelDiagnosticBoxErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label20" runat="server" Text="Clasa diagnostice"></asp:Label>&nbsp;  
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="clasaDiagnosticeDDL" Width="350px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="clasaDiagnosticeDDL_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelClasaDiagnosticeErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label21" runat="server" Text="Diagnostic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="diagnosticDDL" runat="server" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="diagnosticDDL_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelDiagnosticErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label22" runat="server" Text="Cod diagnostic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="codDiagnosticTB" runat="server" Height="27px" Width="102px" Enabled="false"></asp:TextBox>&emsp;&emsp;&emsp;&emsp;
                                                    <asp:Button ID="adaugaDiagnosticBtn" runat="server" Text="Adauga diagnostic" OnClick="adaugaDiagnosticBtn_Click" />&emsp;
                                                    <asp:Label ID="LabelAdaugaDiagnosticErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
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
                                            <tr class="NonPrintable">
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label24" runat="server" Text="Interventie:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="interventieTB" runat="server" TextMode="MultiLine" Width="550px" Height="100px" Enabled="False" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelInterventieBoxErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label16" runat="server" Text="Clasa interventii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="clasaInterventiiDDL" runat="server" Width="350px" OnSelectedIndexChanged="clasaInterventiiDDL_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem>Selectati...</asp:ListItem>
                                                        <asp:ListItem>Consultatie</asp:ListItem>
                                                        <asp:ListItem>Albire dentara si tratamente speciale</asp:ListItem>
                                                        <asp:ListItem>Protetica</asp:ListItem>
                                                        <asp:ListItem>Chirurgie</asp:ListItem>
                                                        <asp:ListItem>Implant dentar</asp:ListItem>
                                                        <asp:ListItem>Bonturi protetice</asp:ListItem>
                                                        <asp:ListItem>Proteze speciale pe implant</asp:ListItem>
                                                        <asp:ListItem>Proteze</asp:ListItem>
                                                        <asp:ListItem>Radiologie</asp:ListItem>
                                                        <asp:ListItem>Ablatii/cimentari/amprente</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelClasaIErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label17" runat="server" Text="Interventia efectuata"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="interventiaDDL" runat="server" Width="600px" OnSelectedIndexChanged="interventiaDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:Label ID="LabelInterventieErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label18" runat="server" Text="Cost interventie"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="costInterventieTB" runat="server" Width="102px" Height="27px" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label19" runat="server" Text="LEI"></asp:Label>&emsp;&emsp;&emsp;
                                                    <asp:Button ID="adaugaInterventieBtn" runat="server" Text="Adauga interventie" OnClick="adaugaInterventieBtn_Click" />&emsp;
                                                    <asp:Label ID="LabelAdaugaInterventieErr" runat="server" Text="" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
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
                                        </table>
                                        <table style="position: center; width: 100%;">
                                            <tr class="NonPrintable">
                                                <td style="text-align: right; width:20%;">
                                                    <asp:Label ID="Label28" runat="server" Text="Planul de tratament + Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="planTratamentObservatiiTB" runat="server" TextMode="MultiLine" Height="70px" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label25" runat="server" Text="Cost total"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px">
                                                    <asp:TextBox ID="costTotalTB" runat="server" Width="102px" Height="27px" Enabled="False" Style="text-align: right;"></asp:TextBox>
                                                    <asp:Label ID="Label26" runat="server" Text="LEI"></asp:Label>
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label27" runat="server" Text="Nume medic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeMedicTB" runat="server" Enabled="false" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelMedicErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:CheckBox ID="printPreviewCB" runat="server" />&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text="Vizualizare format pentru printare."></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Label ID="LabelRezulatAdaugare" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Button ID="adaugareBtn" runat="server" Text="Adauga consultatie noua" CssClass="buton1" OnClick="adaugareBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click" />
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
