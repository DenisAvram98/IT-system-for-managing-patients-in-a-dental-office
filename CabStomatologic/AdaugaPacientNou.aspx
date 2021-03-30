<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdaugaPacientNou.aspx.cs" Inherits="CabStomatologic.AdaugaPacientNou" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Adauga pacient nou</title>

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

            <div class="container">
                <div class="row text-center">
                    <div class="col-lg-12 text-center">
                        <h2>Adaugare pacient nou
                        </h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="Label22" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>
                        <div style="background-color: white; border-radius: 12px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px;">
                                        <asp:Label ID="Label21" runat="server" Text="Campurile marcate cu * sunt obligatorii" ForeColor="Silver"></asp:Label>
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%">
                                                    <asp:Label ID="Label1" runat="server" Text="Nume *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td style="width: 1%;"></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label5" runat="server" Text="Nume mama (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeMamaTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeMErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label2" runat="server" Text="Prenume *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="prenumeTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Nume tata (Nume Prenume)"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTataTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelPrenumeErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeTErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label3" runat="server" Text="CNP *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="cnpTB" runat="server" Height="27px" Width="204px" OnTextChanged="cnpTB_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label7" runat="server" Text="Data nasterii *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="dataNasteriiTB" runat="server" Height="27px" Width="204px"></asp:TextBox>
                                                    <asp:Button ID="dataNasteriiBtn" runat="server" Text="..." title="Alegeti data." />
                                                    <ajaxToolkit:CalendarExtender ID="dataNasteriiCalendarExtender" runat="server"
                                                        Enabled="true" PopupButtonID="dataNasteriiBtn" TargetControlID="dataNasteriiTB" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelCnpErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelDataNErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label4" runat="server" Text="Sex *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="sexDDL" runat="server" Width="204px">
                                                        <asp:ListItem>Selectati...</asp:ListItem>
                                                        <asp:ListItem>Masculin</asp:ListItem>
                                                        <asp:ListItem>Feminin</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelSexErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label8" runat="server" Text="Locul nasterii (Localitatea) *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="locNastereTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelLocNastereErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label17" runat="server" Text="Telefon *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="telefonTB" runat="server" Height="27px" Width="250px" TextMode="Phone"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: right; padding: 5px;">
                                                    <asp:Label ID="Label18" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelTelefonErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelEmailErr" runat="server" Text=""></asp:Label>
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
                                                    <asp:Label ID="Label9" runat="server" Text="Domiciliu:" ForeColor="Silver"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label10" runat="server" Text="Judet *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="judetDDL" runat="server" Width="204px" OnSelectedIndexChanged="judetDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelJudetErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label11" runat="server" Text="Oras *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:DropDownList ID="orasDDL" runat="server" Width="204px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelOrasErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label12" runat="server" Text="Strada *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="stradaTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelStradaErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label13" runat="server" Text="Numar"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numarTB" runat="server" Height="27px" Width="102px" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelNumarErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label14" runat="server" Text="Bloc"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="blocTB" runat="server" Height="27px" Width="102px" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelBlocErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label15" runat="server" Text="Scara"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="scaraTB" runat="server" Height="27px" Width="102px" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelScaraErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label16" runat="server" Text="Numar apartament"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="nrApartamentTB" runat="server" Height="27px" Width="102px" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelNumarApartamentErr" runat="server" Text=""></asp:Label>
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
                                                    <asp:TextBox ID="medicFamilieTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Label ID="LabelMedicFErr" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label20" runat="server" Text="Observatii"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align: left; padding: 5px;" colspan="4">
                                                    <asp:TextBox ID="observatiiTB" runat="server" Width="400px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Label ID="LabelRezultatAdaugare" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="5">
                                                    <asp:Button ID="adaugareBtn" runat="server" Text="Adauga pacient nou" CssClass="buton1" OnClick="adaugareBtn_Click" />&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Aulare" CssClass="buton1" OnClick="anulareBtn_Click" />
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
