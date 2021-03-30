<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContPacientNou.aspx.cs" Inherits="CabStomatologic.ContPacientNou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Cont nou</title>

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
                                <a class="nav-link" href="ContPacientAutentificare.aspx">Autentificare pacient
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="Logare.aspx">Acasa</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#"></a>
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
                        <h2>Inregistrare cont nou</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" CssClass="lead"></asp:Label>

                        <div style="border-radius: 12px;">
                            <div class="lead" style="overflow: auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color: white; border-radius: 12px; overflow: auto; padding-left: 20px; padding-right: 20px; display:inline-block;">
                                        <asp:Label ID="Label1" runat="server" Text="Campurile marcate cu * sunt obligatorii" ForeColor="Silver"></asp:Label>
                                        <table style="position: center; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 40%;">
                                                    <asp:Label ID="Label2" runat="server" Text="Nume *"></asp:Label>&nbsp; 
                                                </td>
                                                <td style="text-align: left; padding: 5px;">
                                                    <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelNumeErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label3" runat="server" Text="Prenume *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="prenumeTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelPrenumeErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label4" runat="server" Text="CNP *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="cnpTB" runat="server" Height="27px" Width="204px" AutoPostBack="true" OnTextChanged="cnpTB_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center;padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelCnpErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label5" runat="server" Text="Telefon *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="telefonTB" runat="server" Height="27px" Width="204px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelTelefonErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label6" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelEmailErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label7" runat="server" Text="Parola *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="parolaTB" runat="server" Height="27px" Width="250px" placeholder="Minim 8, maxim 15 caractere" TextMode="Password" AutoPostBack="true" OnTextChanged="parolaTB_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelParolaErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label8" runat="server" Text="Confirmare parola *"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="confirmareParolaTB" runat="server" Height="27px" Width="250px" placeholder="Minim 8, maxim 15 caractere" TextMode="Password" AutoPostBack="true" OnTextChanged="confirmareParolaTB_TextChanged" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelConfirmareParolaErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelInregistrareErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Button ID="inregistrareBtn" runat="server" Text="Inregistrare" CssClass="buton1" OnClick="inregistrareBtn_Click"/>&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click"/>
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
