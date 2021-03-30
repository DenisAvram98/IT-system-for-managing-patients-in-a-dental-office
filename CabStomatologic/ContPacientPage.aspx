<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ContPacientPage.aspx.cs" Inherits="CabStomatologic.ContPacientPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Pagina Principala</title>

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
                                <a class="nav-link" href="ContPacientPage.aspx">Pagina Principala
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="StatusProgramari.aspx">Programari</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="ContPacientAutentificare.aspx">
                                    <asp:Label ID="LabelIesirePacient" runat="server" Text="Iesire, "></asp:Label></a>
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
                        <h2>Catalog interventii</h2>
                        <p class="lead"></p>
                        <br />
                        <asp:Label ID="Label9" runat="server" Text="" ForeColor="Red" CssClass="lead"></asp:Label>
                        <div>
                            <div style="height: 400px; overflow-y: auto; display: inline-block; border-radius: 12px;" class="lead">
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                    <AlternatingRowStyle BackColor="#66CCFF" />
                                    <HeaderStyle BackColor="#33CCFF" />
                                </asp:GridView>
                            </div>

                            <br />
                            <br />
                            <div class="lead">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label1" runat="server" Text="Albrire dentara si tratamente speciale">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server">
                                    <div style="height: 250px; overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label10" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                    CollapseControlID="Panel1" Collapsed="true" ExpandControlID="Panel1" TargetControlID="Panel2" />

                                <br />
                                <asp:Panel ID="Panel3" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label2" runat="server" Text="Protetica">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server">
                                    <div style="height: 250px; overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label11" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView3" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                                    CollapseControlID="Panel3" Collapsed="true" ExpandControlID="Panel3" TargetControlID="Panel4" />

                                <br />
                                <asp:Panel ID="Panel18" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color:white; border-radius:12px;">
                                        <asp:Label ID="Label35" runat="server" Text="Chirurgie"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel19" runat="server">
                                    <div style="height:250px; overflow-y: auto; display:inline-block; border-radius:12px;">
                                        <asp:Label ID="Label37" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView10" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="false" Font-Overline="false" Font-Strikeout="false" Font-Underline="false">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="server" 
                                    CollapseControlID="Panel18" Collapsed="true" ExpandControlID="Panel18" TargetControlID="Panel19"/>

                                <br />
                                <asp:Panel ID="Panel5" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label3" runat="server" Text="Implant dentar">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel6" runat="server">
                                    <div style="overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label12" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView4" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                                    CollapseControlID="Panel5" Collapsed="true" ExpandControlID="Panel5" TargetControlID="Panel6" />

                                <br />
                                <asp:Panel ID="Panel7" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label4" runat="server" Text="Bonturi protetice">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel8" runat="server">
                                    <div style="height: 250px; overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label13" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView5" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                                    CollapseControlID="Panel7" Collapsed="true" ExpandControlID="Panel7" TargetControlID="Panel8" />

                                <br />
                                <asp:Panel ID="Panel9" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label5" runat="server" Text="Proteze speciale pe implant">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel10" runat="server">
                                    <div style="overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label14" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView6" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server"
                                    CollapseControlID="Panel9" Collapsed="true" ExpandControlID="Panel9" TargetControlID="Panel10" />

                                <br />
                                <asp:Panel ID="Panel11" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label6" runat="server" Text="Proteze">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel12" runat="server">
                                    <div style="height: 250px; overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label15" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView7" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server"
                                    CollapseControlID="Panel11" Collapsed="true" ExpandControlID="Panel11" TargetControlID="Panel12" />

                                <br />
                                <asp:Panel ID="Panel13" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label7" runat="server" Text="Radiologie">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel14" runat="server">
                                    <div style="overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label16" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView8" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server"
                                    CollapseControlID="Panel13" Collapsed="true" ExpandControlID="Panel13" TargetControlID="Panel14" />

                                <br />
                                <asp:Panel ID="Panel15" runat="server" CssClass="collapsePanelHeader">
                                    <div style="background-color: white; border-radius: 12px;">
                                        <asp:Label ID="Label8" runat="server" Text="Ablatii/cimentari/amprente">
                                        </asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel16" runat="server">
                                    <div style="height: 250px; overflow-y: auto; display: inline-block; border-radius: 12px;">
                                        <asp:Label ID="Label17" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView9" runat="server" CellPadding="4" HorizontalAlign="Center" BackColor="White" GridLines="None" Font-Bold="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                            <HeaderStyle BackColor="#33CCFF" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" runat="server"
                                    CollapseControlID="Panel15" Collapsed="true" ExpandControlID="Panel15" TargetControlID="Panel16" />
                            </div>
                        </div>

                        <br />
                        <br />
                        <br />
                        <h2>Formular programare</h2>
                        <br />
                        <div class="lead">
                            <asp:Panel ID="Panel17" runat="server" HorizontalAlign="Center">
                                <div style="background-color: white; border-radius: 12px; overflow-x: auto;">
                                    <asp:Label ID="Label27" runat="server" Text="Campurile marcate cu * sunt obligatorii" ForeColor="#c0c0c0"></asp:Label>
                                    <table style="position: center; width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label18" runat="server" Text="Nume *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="numeTB" runat="server" Height="27px" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label28" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label19" runat="server" Text="Prenume *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="prenumeTB" runat="server" Height="27px" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label29" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label20" runat="server" Text="Telefon *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="telefonTB" runat="server" TextMode="Phone" Height="27px" Width="204px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label30" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label21" runat="server" Text="Adresa e-mail"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="emailTB" runat="server" Height="27px" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="LabelEmailErr" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label22" runat="server" Text="Medic *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:DropDownList ID="mediciDDL" runat="server" Width="350px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label31" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label23" runat="server" Text="Motiv pentru programare *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:DropDownList ID="motivProgramareDDL" runat="server" Width="350px">
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
                                                    <asp:ListItem>Altele</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label32" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label24" runat="server" Text="Data *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="dataTB" runat="server" Height="27px" Width="204px" ReadOnly="False"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="dataProgramareCalendarExtender" runat="server"
                                                    Enabled="true" PopupButtonID="dataProgramareBtn" TargetControlID="dataTB" />
                                                <asp:Button ID="dataProgramareBtn" runat="server" Text="..." title="Alegeti data." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label33" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label25" runat="server" Text="Ora *"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:DropDownList ID="oraDDL" runat="server" Width="204px">
                                                    <asp:ListItem>Selectati...</asp:ListItem>
                                                    <asp:ListItem>09:00</asp:ListItem>
                                                    <asp:ListItem>10:00</asp:ListItem>
                                                    <asp:ListItem>11:00</asp:ListItem>
                                                    <asp:ListItem>12:00</asp:ListItem>
                                                    <asp:ListItem>13:00</asp:ListItem>
                                                    <asp:ListItem>14:00</asp:ListItem>
                                                    <asp:ListItem>15:00</asp:ListItem>
                                                    <asp:ListItem>16:00</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; padding:5px;" colspan="2">
                                                <asp:Label ID="Label34" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <asp:Label ID="Label26" runat="server" Text="Mesaj"></asp:Label>&nbsp;
                                            </td>
                                            <td style="text-align: left; padding: 5px;">
                                                <asp:TextBox ID="mesajTB" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="position:center; width:100%;">
                                        <tr>
                                            <td style="text-align:center; padding: 5px;">
                                                <asp:Label ID="Label36" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 5px;">
                                                <asp:Button ID="solicitatiProgramareBtn" runat="server" Text="Solicitati programarea" CssClass="buton1" OnClick="solicitatiProgramareBtn_Click" /> &emsp;&emsp;
                                                <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton1" OnClick="anulareBtn_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
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
