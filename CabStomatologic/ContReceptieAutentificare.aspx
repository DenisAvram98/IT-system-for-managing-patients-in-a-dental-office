<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContReceptieAutentificare.aspx.cs" Inherits="CabStomatologic.ContReceptieAutentificare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Autentificare receptie</title>

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
                                <a class="nav-link" href="ContReceptieAutentificare.aspx">Autentificare receptie
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
                        <h2>Autentificare receptie</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="LabelPrincipalErr" runat="server" Text="" CssClass="lead"></asp:Label>

                        <div style="border-radius:12px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="lead" style="overflow:auto;">
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    <div style="background-color:white; border-radius:12px; overflow:auto; padding-right:20px; padding-left:20px; display:inline-block;">
                                        <table style="position:center; width:100%;">
                                            <tr>
                                                <td style="text-align:right; width:35%;">
                                                    <asp:Label ID="Label1" runat="server" Text="Utilizator"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="receptieTB" runat="server" Height="27px" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="Label2" runat="server" Text="Parola"></asp:Label>&nbsp;
                                                </td>
                                                <td style="text-align:left; padding:5px;">
                                                    <asp:TextBox ID="parolaTB" runat="server" Height="27px" Width="250px" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelParolaErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Label ID="LabelAutentificareErr" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; padding:5px;" colspan="2">
                                                    <asp:Button ID="autentificareBtn" runat="server" Text="Autentificare"  CssClass="buton1" OnClick="autentificareBtn_Click"/>&emsp;&emsp;
                                                    <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Style="text-align: center;">
                                                        <asp:Label ID="Label3" runat="server" Text="Ati uitat parola?" ForeColor="#33ccff" CssClass="collapsePanelHeader"></asp:Label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; padding: 5px;" colspan="2">
                                                    <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center">
                                                        <table style="position: center; width: 100%;">
                                                            <tr>
                                                                <td style="text-align: right; width: 35%;">
                                                                    <asp:Label ID="Label4" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                                </td>
                                                                <td style="text-align: left; padding: 5px;">
                                                                    <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="350px"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: left; padding: 5px;">
                                                                    <asp:Button ID="trimiteEmailBtn" runat="server" Text="Trimite" CssClass="buton1" OnClick="trimiteEmailBtn_Click"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:center;padding:5px;" colspan="2">
                                                                    <asp:Label ID="LabelTrimiteEmailErr" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" 
                                                        CollapseControlID="Label3" Collapsed="true" ExpandControlID="Label3" TargetControlID="Panel4"/>
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
