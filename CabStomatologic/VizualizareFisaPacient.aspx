<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VizualizareFisaPacient.aspx.cs" Inherits="CabStomatologic.VizualizareFisaPacient" MaintainScrollPositionOnPostback="true" %>

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

    <title>Vizualizare / Editare fisa pacient</title>

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
                        <h2 class="NonPrintable">Vizualizare / Editare fisa pacient</h2>
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
                                        <table style="position: center; width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 16%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editNumeIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editNumeIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveNumeIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveNumeIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelNumeIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelNumeIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Nume mama (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeMamaTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editNumeMIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editNumeMIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveNumeMIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveNumeMIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelNumeMIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelNumeMIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeMSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label3" runat="server" Text="Prenume"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="prenumeTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editPrenumeIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editPrenumeIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="savePrenumeIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="savePrenumeIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelPrenumeIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelPrenumeIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Nume tata (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTataTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editNumeTIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editNumeTIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveNumeTIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveNumeTIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelNumeTIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelNumeTIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelPrenumeSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeTSaveErr" runat="server" Text=""></asp:Label>
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
                                                    <asp:ImageButton ID="editTelefonIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editTelefonIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveTelefonIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveTelefonIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelTelefonIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelTelefonIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label10" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editEmailIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editEmailIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveEmailIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveEmailIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelEmailIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelEmailIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelTelefonSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelEmailSaveErr" runat="server" Text=""></asp:Label>
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
                                                    <asp:DropDownList ID="judeteDDL" runat="server" Visible="false" Width="204px" OnSelectedIndexChanged="judeteDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:ImageButton ID="editJudetIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editJudetIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveJudetIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveJudetIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelJudetIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelJudetIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelJudetSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label13" runat="server" Text="Oras"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="orasTB" runat="server" Height="27px" Width="204px" Enabled="false"></asp:TextBox>
                                                    <asp:DropDownList ID="oraseDDL" runat="server" Width="204px" Visible="false"></asp:DropDownList>
                                                    <asp:ImageButton ID="editOrasIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editOrasIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveOrasIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveOrasIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelOrasIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelOrasIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelOrasSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label14" runat="server" Text="Strada"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="stradaTB" runat="server" Height="27px" Width="250px" Enabled="false"></asp:TextBox>
                                                    <asp:ImageButton ID="editStradaIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editStradaIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveStradaIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveStradaIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelStradaIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelStradaIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelStradaSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label15" runat="server" Text="Numar"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                    <asp:ImageButton ID="editNumarIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editNumarIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveNumarIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveNumarIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelNumarIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelNumarIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumarSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label16" runat="server" Text="Bloc"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="blocTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                    <asp:ImageButton ID="editBlocIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editBlocIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveBlocIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveBlocIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelBlocIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelBlocIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelBlocSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label17" runat="server" Text="Scara"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="scaraTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                    <asp:ImageButton ID="editScaraIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editScaraIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveScaraIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveScaraIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelScaraIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelScaraIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelScaraSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label18" runat="server" Text="Numar apartament"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarApartamentTB" runat="server" Height="27px" Width="104px" Enabled="false" TextMode="Number"></asp:TextBox>
                                                    <asp:ImageButton ID="editNumarApartamentIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editNumarApartamentIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveNumarApartamentIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveNumarApartamentIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelNumarApartamentIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelNumarApartamentIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumarApartamentSaveErr" runat="server" Text=""></asp:Label>
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
                                                    <asp:ImageButton ID="editMedicFamIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editMedicFamIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveMedicFamIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveMedicFamIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelMedicFamIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelMedicFamIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelMedicFamSaveErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label20" runat="server" Text="Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="observatiiTB" runat="server" TextMode="MultiLine" Width="400px" Enabled="false" OnPreRender="observatiiTB_PreRender"></asp:TextBox>
                                                    <asp:ImageButton ID="editObsIBtn" title="Editare." runat="server" ImageUrl="~/Icons/Edit.png" OnClick="editObsIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="saveObsIBtn" title="Salvare." runat="server" ImageUrl="~/Icons/save.JPG" Visible="false" OnClick="saveObsIBtn_Click" CssClass="NonPrintable" />
                                                    <asp:ImageButton ID="cancelObsIBtn" title="Anulare." runat="server" ImageUrl="~/Icons/Delete.png" Visible="false" OnClick="cancelObsIBtn_Click" CssClass="NonPrintable" />
                                                </td>
                                            </tr>
                                            <tr class="NonPrintable">
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelObservatiiSaveErr" runat="server" Text=""></asp:Label>
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
