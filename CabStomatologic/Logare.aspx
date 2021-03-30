<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logare.aspx.cs" Inherits="CabStomatologic.Logare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Denis - Dent</title>

    <!-- Bootstrap core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

</head>

<body>
    <form id="form1" runat="server">
        <div>
            <!-- Navigation 
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark static-top">
                <div class="container">
                    <a class="navbar-brand" href="#">Start Bootstrap</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarResponsive">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item active">
                                <a class="nav-link" href="#">Home
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">About</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Services</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Contact</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            -->

            <!-- Page Content -->
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <img src="vendor/bootstrap/Asset 2.png" style="padding-top: 20pt;"/>
                        <h1>Bine ati venit!</h1>
                        <p class="lead">Va rugam sa alegeti un cont potrivit voua.</p>
                        <ul class="list-unstyled">
                            <li>
                                <asp:Button ID="contPacientBtn" runat="server" Text="CONT PACIENT" class="buton" OnClick="contPacientBtn_Click" title="Pentru programari, catalog interventi, click aici." />
                                &emsp;
                                <asp:Button ID="contReceptieBtn" runat="server" Text="CONT RECEPTIE" CssClass="buton" OnClick="contReceptieBtn_Click"/>
                                &emsp;
                                <asp:Button ID="contMedicBtn" runat="server" Text="CONT MEDIC" class="buton" OnClick="contMedicBtn_Click" />
                            </li>
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
