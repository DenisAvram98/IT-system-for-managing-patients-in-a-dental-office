﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContMedicPage.aspx.cs" Inherits="CabStomatologic.ContMedicPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autentificare medic</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

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
                                <a class="nav-link" href="ContMedicPage.aspx">Autentificare medic
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
                        <h2>Autentificare medic stomatolog</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="" ForeColor="red" CssClass="lead"></asp:Label>
                        <div class="lead" style="overflow-x: auto;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <div style="background-color: white; border-radius: 12px; overflow-x: auto; display: inline-block; padding: 20px;">
                                    <table style="position: center; width: 100%">
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label1" runat="server" Text="Utilizator"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:DropDownList ID="mediciDDL" Width="350px" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 5px" colspan="2">
                                                <asp:Label ID="Label5" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label2" runat="server" Text="Parola"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="parolaTB" Width="350px" Height="27px" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 5px;" colspan="2">
                                                <asp:Label ID="Label6" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <table style="position: center; width: 100%;">
                                        <tr>
                                            <td style="position: center; padding: 5px;">
                                                <asp:Label ID="Label4" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="position: center; padding: 5px;">
                                                <asp:Button ID="autentificareBtn" runat="server" Text="Autentificare" CssClass="buton1" OnClick="autentificareBtn_Click" />&emsp;&emsp;
                                                <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="position: center; padding: 5px; text-align: center;">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:Label ID="Label7" runat="server" Text="Ati uitat parola?" ForeColor="#33ccff" CssClass="collapsePanelHeader"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="position: center; padding: 5px; text-align: center;">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <table style="position: center; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right; width: 35%;">
                                                                <asp:Label ID="Label8" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                                            </td>
                                                            <td style="text-align: left; padding: 5px;">
                                                                <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="350px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: center; padding: 5px">
                                                                <asp:Button ID="trimiteEmailBtn" runat="server" Text="Trimite" CssClass="buton1" OnClick="trimiteEmailBtn_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <table style="position: center; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; padding: 5px;">
                                                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                                    CollapseControlID="Label7" Collapsed="true" ExpandControlID="Label7" TargetControlID="Panel3" TextLabelID="Label7" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>

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
