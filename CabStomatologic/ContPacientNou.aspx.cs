using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class ContPacientNou : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            parolaTB.Attributes.Add("value", parolaTB.Text.Trim());
            confirmareParolaTB.Attributes.Add("value", confirmareParolaTB.Text.Trim());
        }

        Regex testNum = new Regex("^[0-9]+$");
        static string dataNasterii = "";
        static string sexBd = "";
        static Boolean testCNP = false;
        protected void cnpTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cnpTB.Text.Trim() != "")
                {
                    if (testNum.IsMatch(cnpTB.Text.Trim()))
                    {
                        if (cnpTB.Text.Trim().Length == 13)
                        {
                            if (AdaugaPacientNou.verificCNP(cnpTB.Text.Trim()) == true) //verificam daca CNP-ul este adevarat
                            {
                                string a1 = null, a2 = null, l1 = null, l2 = null, z1 = null, z2 = null, final = null;
                                string cnp = cnpTB.Text.Trim();
                                string sex = cnp.Substring(0, 1);
                                if (sex == "1" || sex == "2") //nascuti intre 1 ianuarie 1900 si 31 decembrie 1999
                                {
                                    a1 = cnp.Substring(1, 1);
                                    a2 = cnp.Substring(2, 1);
                                    l1 = cnp.Substring(3, 1);
                                    l2 = cnp.Substring(4, 1);
                                    z1 = cnp.Substring(5, 1);
                                    z2 = cnp.Substring(6, 1);
                                    if (l1 == "0" && z1 == "0")
                                    {
                                        final = l2 + "/" + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "0" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l2 + "/" + z1 + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "1" && z1 == "0")
                                    {
                                        final = l1 + l2 + "/" + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "1" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l1 + l2 + "/" + z1 + z2 + "/19" + a1 + a2;
                                    }
                                    dataNasterii = final;
                                    if (sex == "1")
                                    {
                                        //sexDDL.SelectedIndex = 1;
                                        sexBd = "Masculin";
                                    }
                                    else if (sex == "2")
                                    {
                                        //sexDDL.SelectedIndex = 2;
                                        sexBd = "Feminin";
                                    }

                                    LabelCnpErr.Text = "";
                                    testCNP = true;
                                }
                                else if (sex == "5" || sex == "6") //nascuti intre 1 ianuarie 2000 si 31 decembrie 2099
                                {
                                    a1 = cnp.Substring(1, 1);
                                    a2 = cnp.Substring(2, 1);
                                    l1 = cnp.Substring(3, 1);
                                    l2 = cnp.Substring(4, 1);
                                    z1 = cnp.Substring(5, 1);
                                    z2 = cnp.Substring(6, 1);
                                    if (l1 == "0" && z1 == "0")
                                    {
                                        final = l2 + "/" + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "0" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l2 + "/" + z1 + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "1" && z1 == "0")
                                    {
                                        final = l1 + l2 + "/" + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "1" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l1 + l2 + "/" + z1 + z2 + "/20" + a1 + a2;
                                    }

                                    dataNasterii = final;
                                    if (sex == "5")
                                    {
                                        //sexDDL.SelectedIndex = 1;
                                        sexBd = "Masculin";
                                    }
                                    else if (sex == "6")
                                    {
                                        //sexDDL.SelectedIndex = 2;
                                        sexBd = "Feminin";
                                    }

                                    LabelCnpErr.Text = "";
                                    testCNP = true;
                                }
                                else //pentru rezidenti si ani 1800 si 1899
                                {
                                    dataNasterii = "";
                                    if (sex == "3" || sex == "7")
                                    {
                                        //sexDDL.SelectedIndex = 1;
                                        sexBd = "Masculin";
                                    }
                                    else if (sex == "4" || sex == "8")
                                    {
                                        //sexDDL.SelectedIndex = 2;
                                        sexBd = "Feminin";
                                    }
                                    LabelCnpErr.Text = "";
                                    testCNP = true;
                                }
                            }
                            else
                            {
                                LabelCnpErr.ForeColor = Color.Red;
                                LabelCnpErr.Text = "CNP-ul introdus este inexistent.";
                                testCNP = false;
                                dataNasterii = "";
                                sexBd = "";
                            }
                        }
                        else
                        {
                            LabelCnpErr.ForeColor = Color.Red;
                            LabelCnpErr.Text = "CNP-ul trebuie sa contina 13 cifre.";
                            testCNP = false;
                            dataNasterii = "";
                            sexBd = "";
                        }
                    }
                    else
                    {
                        LabelCnpErr.ForeColor = Color.Red;
                        LabelCnpErr.Text = "CNP-ul nu are voie sa contina litere.";
                        testCNP = false;
                        dataNasterii = "";
                        sexBd = "";
                    }
                }
                else
                {
                    LabelCnpErr.ForeColor = Color.Red;
                    LabelCnpErr.Text = "Nu ati introuds CNP-ul.";
                    testCNP = false;
                    dataNasterii = "";
                    sexBd = "";
                }
            }
            catch (FormatException ex)
            {
                LabelCnpErr.ForeColor = Color.Red;
                LabelCnpErr.Text = "Nu se poate calcula data nasterii din CNP-ul introdus: " + ex.Message;
            }
        }

        static Boolean testPW = false;
        protected void parolaTB_TextChanged(object sender, EventArgs e)
        {
            if (parolaTB.Text.Trim() != "")
            {
                if (parolaTB.Text.Trim().Length >= 8 && parolaTB.Text.Trim().Length <= 15)
                {
                    testPW = true;
                    LabelParolaErr.Text = "";
                    LabelConfirmareParolaErr.Text = "";
                }
                else
                {
                    LabelParolaErr.ForeColor = Color.Red;
                    LabelParolaErr.Text = "Parola poate sa contina minim 8 caractere si maxim 15 caractere.";
                    LabelConfirmareParolaErr.Text = "";
                    testPW = false;
                }
            }
            else
            {
                LabelParolaErr.ForeColor = Color.Red;
                LabelParolaErr.Text = "Nu ati introdus parola.";
                LabelConfirmareParolaErr.Text = "";
                testPW = false;
            }
        }

        static Boolean testConfirmarePW = false;
        protected void confirmareParolaTB_TextChanged(object sender, EventArgs e)
        {
            if (parolaTB.Text.Trim() != "" && testPW == true)
            {
                if (confirmareParolaTB.Text.Trim() != "")
                {
                    if (confirmareParolaTB.Text.Trim().Length >= 8 && confirmareParolaTB.Text.Trim().Length <= 15 && confirmareParolaTB.Text.Trim() == parolaTB.Text.Trim())
                    {
                        testConfirmarePW = true;
                        LabelConfirmareParolaErr.Text = "";
                        LabelParolaErr.Text = "";
                    }
                    else
                    {
                        LabelConfirmareParolaErr.ForeColor = Color.Red;
                        LabelConfirmareParolaErr.Text = "Parola introdusa nu coincide cu parola introdusa mai sus.";
                        LabelParolaErr.Text = "";
                        testConfirmarePW = false;
                    }
                }
                else
                {
                    LabelConfirmareParolaErr.ForeColor = Color.Red;
                    LabelConfirmareParolaErr.Text = "Nu ati confirmat parola introdusa mai sus.";
                    LabelParolaErr.Text = "";
                    testConfirmarePW = false;
                }
            }
            else
            {
                LabelParolaErr.ForeColor = Color.Red;
                LabelParolaErr.Text = "Nu ati introdus parola sau parola introdusa nu este valida.";
                LabelConfirmareParolaErr.Text = "";
                testConfirmarePW = false;
            }
        }

        Regex test = new Regex("^[a-zA-Z ]+$");
        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void inregistrareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (numeTB.Text.Trim() != "")
                {
                    if (test.IsMatch(numeTB.Text.Trim()))
                    {
                        if (prenumeTB.Text.Trim() != "")
                        {
                            if (test.IsMatch(prenumeTB.Text.Trim()))
                            {
                                if (testCNP == true && cnpTB.Text.Trim() != "" && testNum.IsMatch(cnpTB.Text.Trim()) && AdaugaPacientNou.verificCNP(cnpTB.Text.Trim()) == true && cnpTB.Text.Trim().Length == 13)
                                {
                                    if (telefonTB.Text.Trim() != "")
                                    {
                                        if (testNum.IsMatch(telefonTB.Text.Trim()))
                                        {
                                            if (emailTB.Text.Trim() == "" || testEmail.IsMatch(emailTB.Text.Trim()))
                                            {
                                                if (testPW == true && parolaTB.Text.Trim() != "" && parolaTB.Text.Trim().Length >= 8 && parolaTB.Text.Trim().Length <= 15)
                                                {
                                                    if (testConfirmarePW == true && confirmareParolaTB.Text.Trim() != "" && confirmareParolaTB.Text.Trim().Length >= 8 && confirmareParolaTB.Text.Trim().Length <= 15 && confirmareParolaTB.Text.Trim() == parolaTB.Text.Trim())
                                                    {
                                                        string stmt = "select CNP from ContPacienti where CNP='"+cnpTB.Text.Trim()+"'";
                                                        SqlCommand sc = new SqlCommand(stmt, con);
                                                        SqlDataReader dr = sc.ExecuteReader();
                                                        if (dr.HasRows)
                                                        {
                                                            LabelInregistrareErr.ForeColor = Color.Red;
                                                            LabelInregistrareErr.Text = "CNP-ului " + cnpTB.Text.Trim() + " ii este asociat un cont si nu se poate reutiliza pentru a crea alt cont.";
                                                            LabelPrincipalErr.Text = "";
                                                            LabelCnpErr.Text = "";
                                                            LabelConfirmareParolaErr.Text = "";
                                                            LabelEmailErr.Text = "";
                                                            LabelNumeErr.Text = "";
                                                            LabelParolaErr.Text = "";
                                                            LabelPrenumeErr.Text = "";
                                                            LabelTelefonErr.Text = "";
                                                        }
                                                        else
                                                        {
                                                            dr.Close();
                                                            sc.Dispose();

                                                            stmt = "insert into ContPacienti ([CNP],[Nume],[Prenume],[Sex],[DataNasterii],[Telefon],[Email],[Parola]) values (@cnp,@n,@p,@s,@dN,@t,@e,@pW)";
                                                            sc = new SqlCommand(stmt, con);
                                                            sc.Parameters.AddWithValue("@cnp", cnpTB.Text.Trim());
                                                            sc.Parameters.AddWithValue("@n", numeTB.Text.Trim());
                                                            sc.Parameters.AddWithValue("@p", prenumeTB.Text.Trim());
                                                            sc.Parameters.AddWithValue("@s", sexBd);
                                                            sc.Parameters.AddWithValue("@dN", dataNasterii);
                                                            sc.Parameters.AddWithValue("@t", telefonTB.Text.Trim());
                                                            sc.Parameters.AddWithValue("@e", emailTB.Text.Trim());
                                                            sc.Parameters.AddWithValue("@pW", confirmareParolaTB.Text.Trim());
                                                            sc.ExecuteNonQuery();

                                                            LabelInregistrareErr.ForeColor = Color.Green;
                                                            LabelInregistrareErr.Text = "Inregistrarea a fost efectuata cu succes.<br>In 5 secunde veti fi redirectionati pe pagina de Autentificare.";
                                                            LabelPrincipalErr.Text = "";
                                                            LabelCnpErr.Text = "";
                                                            LabelConfirmareParolaErr.Text = "";
                                                            LabelEmailErr.Text = "";
                                                            LabelNumeErr.Text = "";
                                                            LabelParolaErr.Text = "";
                                                            LabelPrenumeErr.Text = "";
                                                            LabelTelefonErr.Text = "";

                                                            sc.Dispose();

                                                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('ContPacientAutentificare.aspx') }, 5000);", true); //in 5 secunde se va redirectiona la pagina principala
                                                        }
                                                        dr.Close();
                                                        sc.Dispose();
                                                    }
                                                    else
                                                    {
                                                        LabelConfirmareParolaErr.ForeColor = Color.Red;
                                                        LabelConfirmareParolaErr.Text = "Nu ati confirmat parola introdusa mai sus.";
                                                        LabelInregistrareErr.Text = "";
                                                        LabelPrincipalErr.Text = "";
                                                        LabelCnpErr.Text = "";
                                                        LabelEmailErr.Text = "";
                                                        LabelNumeErr.Text = "";
                                                        LabelParolaErr.Text = "";
                                                        LabelPrenumeErr.Text = "";
                                                        LabelTelefonErr.Text = "";
                                                    }
                                                }
                                                else
                                                {
                                                    LabelParolaErr.ForeColor = Color.Red;
                                                    LabelParolaErr.Text = "Nu ati introdus parola.";
                                                    LabelInregistrareErr.Text = "";
                                                    LabelPrincipalErr.Text = "";
                                                    LabelCnpErr.Text = "";
                                                    LabelEmailErr.Text = "";
                                                    LabelNumeErr.Text = "";
                                                    LabelPrenumeErr.Text = "";
                                                    LabelTelefonErr.Text = "";
                                                    LabelConfirmareParolaErr.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                LabelEmailErr.ForeColor = Color.Red;
                                                LabelEmailErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                                                LabelParolaErr.Text = "";
                                                LabelInregistrareErr.Text = "";
                                                LabelPrincipalErr.Text = "";
                                                LabelCnpErr.Text = "";
                                                LabelNumeErr.Text = "";
                                                LabelPrenumeErr.Text = "";
                                                LabelTelefonErr.Text = "";
                                                LabelConfirmareParolaErr.Text = "";
                                            }
                                        }
                                        else
                                        {
                                            LabelTelefonErr.ForeColor = Color.Red;
                                            LabelTelefonErr.Text= "Numarul de telefon poate sa contina numai cifre.";
                                            LabelEmailErr.Text = "";
                                            LabelParolaErr.Text = "";
                                            LabelInregistrareErr.Text = "";
                                            LabelPrincipalErr.Text = "";
                                            LabelCnpErr.Text = "";
                                            LabelNumeErr.Text = "";
                                            LabelPrenumeErr.Text = "";
                                            LabelConfirmareParolaErr.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        LabelTelefonErr.ForeColor = Color.Red;
                                        LabelTelefonErr.Text = "Nu ati introdus numarul de telefon.";
                                        LabelEmailErr.Text = "";
                                        LabelParolaErr.Text = "";
                                        LabelInregistrareErr.Text = "";
                                        LabelPrincipalErr.Text = "";
                                        LabelCnpErr.Text = "";
                                        LabelNumeErr.Text = "";
                                        LabelPrenumeErr.Text = "";
                                        LabelConfirmareParolaErr.Text = "";
                                    }
                                }
                                else
                                {
                                    LabelCnpErr.ForeColor = Color.Red;
                                    LabelCnpErr.Text = "Nu exista CNP-ul introdus. CNP-ul este gresit.";
                                    LabelTelefonErr.Text = "";
                                    LabelEmailErr.Text = "";
                                    LabelParolaErr.Text = "";
                                    LabelInregistrareErr.Text = "";
                                    LabelPrincipalErr.Text = "";
                                    LabelNumeErr.Text = "";
                                    LabelPrenumeErr.Text = "";
                                    LabelConfirmareParolaErr.Text = "";
                                }
                            }
                            else
                            {
                                LabelPrenumeErr.ForeColor = Color.Red;
                                LabelPrenumeErr.Text = "Prenumele nu are voie sa contina altceva in afara de litere si spatii libere.";
                                LabelCnpErr.Text = "";
                                LabelTelefonErr.Text = "";
                                LabelEmailErr.Text = "";
                                LabelParolaErr.Text = "";
                                LabelInregistrareErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                LabelNumeErr.Text = "";
                                LabelConfirmareParolaErr.Text = "";
                            }
                        }
                        else
                        {
                            LabelPrenumeErr.ForeColor = Color.Red;
                            LabelPrenumeErr.Text = "Nu ati introdus prenumele.";
                            LabelCnpErr.Text = "";
                            LabelTelefonErr.Text = "";
                            LabelEmailErr.Text = "";
                            LabelParolaErr.Text = "";
                            LabelInregistrareErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelNumeErr.Text = "";
                            LabelConfirmareParolaErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelNumeErr.ForeColor = Color.Red;
                        LabelNumeErr.Text= "Numele nu are voie sa contina altceva in afara de litere.";
                        LabelPrenumeErr.Text = "";
                        LabelCnpErr.Text = "";
                        LabelTelefonErr.Text = "";
                        LabelEmailErr.Text = "";
                        LabelParolaErr.Text = "";
                        LabelInregistrareErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelConfirmareParolaErr.Text = "";
                    }
                }
                else
                {
                    LabelNumeErr.ForeColor = Color.Red;
                    LabelNumeErr.Text = "Nu ati introdus numele.";
                    LabelPrenumeErr.Text = "";
                    LabelCnpErr.Text = "";
                    LabelTelefonErr.Text = "";
                    LabelEmailErr.Text = "";
                    LabelParolaErr.Text = "";
                    LabelInregistrareErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelConfirmareParolaErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelInregistrareErr.ForeColor = Color.Red;
                LabelInregistrareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            LabelPrincipalErr.ForeColor = Color.Green;
            LabelPrincipalErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelInregistrareErr.ForeColor = Color.Green;
            LabelInregistrareErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelNumeErr.Text = "";
            LabelPrenumeErr.Text = "";
            LabelCnpErr.Text = "";
            LabelTelefonErr.Text = "";
            LabelEmailErr.Text = "";
            LabelParolaErr.Text = "";
            LabelConfirmareParolaErr.Text = "";
            numeTB.Text = "";
            prenumeTB.Text = "";
            cnpTB.Text = "";
            telefonTB.Text = "";
            emailTB.Text = "";
            parolaTB.Attributes.Add("value", "");
            confirmareParolaTB.Attributes.Add("value", "");
        }
    }
}