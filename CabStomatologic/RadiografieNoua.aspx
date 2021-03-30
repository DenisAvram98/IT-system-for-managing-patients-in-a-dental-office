<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadiografieNoua.aspx.cs" Inherits="CabStomatologic.RadiografieNoua" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Adauga radiografie noua</title>

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
                        <h2>Adaugare radiografie noua</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>

                        <div style="background-color: white; border-radius: 12px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume pacient"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" >
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
                                                <td style="text-align: left; padding: 5px;" class="auto-style6">
                                                    <asp:TextBox ID="dataTB" runat="server" Height="27px" Width="204px"></asp:TextBox>
                                                    <asp:Button ID="dataBtn" runat="server" Text="..." title="Alegeti data." />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="true" PopupButtonID="dataBtn" TargetControlID="dataTB" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label4" runat="server" Text="Imagine"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <!--<asp:Image ID="RadiografieI" runat="server" Height="70px" Width="80px" />-->
                                                    <asp:ImageButton ID="pozaRadIBtn" runat="server" Height="70px" Width="80px" AlternateText=" " Visible="false"/>&nbsp;
                                                    <asp:Label ID="LabelImagineMare" runat="server" Text="Click pe imagine, pentru a o vedea mai mare." ForeColor="Green" Visible="false"></asp:Label>
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                        <img id="largeImg" src="" runat="server" style="max-width:800px;"/>
                                                        <div style="text-align: center; padding:5px;">
                                                            <asp:Button ID="closeBtn" runat="server" Text="Inchide" />
                                                        </div>
                                                    </asp:Panel>
                                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                                        PopupControlID="Panel2" TargetControlID="pozaRadIBtn" CancelControlID="closeBtn" BackgroundCssClass="modalBackground">
                                                        <Animations>
                                                            <OnShowing>
                                                                <FadeIn Duration=".3" Fps="30" />
                                                            </OnShowing>
                                                            <OnShown>
                                                                <FadeIn Duration=".3" Fps="30" />
                                                            </OnShown>
                                                            <OnHiding>
                                                                <FadeOut Duration=".3" Fps="30" />
                                                            </OnHiding>
                                                            <OnHidden>
                                                                <FadeOut Duration=".0" Fps="30" />
                                                            </OnHidden>
                                                        </Animations>
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align:left; padding:5px">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" >
                                                    <asp:Label ID="LabelImagineErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" >
                                                    <asp:Button ID="incarcaRadBtn" runat="server" Text="Incarca radiografia" OnClick="incarcaRadBtn_Click" />&nbsp;
                                                    <asp:Label ID="LabelNumeRadiografie" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="Label5" runat="server" Text="Diagnostic:" ForeColor="Silver"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px" colspan="4">
                                                    <asp:TextBox ID="diagnosticTB" runat="server" Enabled="false" TextMode="MultiLine" Width="650px" Height="100px" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" >
                                                    <asp:Label ID="LabelDiagnosticBoxErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label6" runat="server" Text="Clasa diagnostice"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:DropDownList ID="clasaDiagnosticeDDL" runat="server" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="clasaDiagnosticeDDL_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px;" >
                                                    <asp:Label ID="LabelClasaDErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label7" runat="server" Text="Diagnostic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:DropDownList ID="diagnosticDDL" runat="server" Width="600px" AutoPostBack="true" OnSelectedIndexChanged="diagnosticDDL_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left; padding: 5px" >
                                                    <asp:Label ID="LabelDiagnosticErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label8" runat="server" Text="Cod diagnostic"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="codDiagnosticTB" runat="server" Enabled="false" Height="27px" Width="102px"></asp:TextBox>&emsp;&emsp;&emsp;&emsp;
                                                    <asp:Button ID="adaugaDiagnosticBtn" runat="server" Text="Adauga diagnsotic" OnClick="adaugaDiagnosticBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label9" runat="server" Text="Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px" colspan="4">
                                                    <asp:TextBox ID="observatiiTB" runat="server" TextMode="MultiLine" Height="70px" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:CheckBox ID="reintoarcerePPCB" runat="server" />&nbsp;
                                                    <asp:Label ID="Label10" runat="server" Text="Intoarcere automata la Pagina Principala. (dupa adaugarea radiografiei)"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Label ID="LabelRezultatAdaugareErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Button ID="adaugareBtn" runat="server" Text="Adauga radiografie noua" CssClass="buton1" OnClick="adaugareBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left;">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/VizualizareRadiografii.aspx" ForeColor="Silver" Font-Size="Large">Vizualizare radiografii</asp:HyperLink>
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
