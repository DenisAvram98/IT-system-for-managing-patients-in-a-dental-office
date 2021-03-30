using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class ContPacientAutentificare : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Regex testNum = new Regex("^[0-9]+$");
        protected void autentificareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (cnpTB.Text.Trim() != "")
                {
                    if (testNum.IsMatch(cnpTB.Text.Trim()))
                    {
                        if (cnpTB.Text.Trim().Length == 13)
                        {
                            string stmt = "select CNP from ContPacienti where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (!dr.HasRows)
                            {
                                LabelCnpErr.ForeColor = Color.Red;
                                LabelCnpErr.Text = "CNP-ul introdus nu are asociat nici un cont.";
                                LabelParolaErr.Text = "";
                                LabelRezultatAutentificareErr.Text = "";
                                dr.Close();
                                sc.Dispose();
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                if (parolaTB.Text.Trim() != "")
                                {
                                    if (parolaTB.Text.Trim().Length >= 8 && parolaTB.Text.Trim().Length <= 15)
                                    {
                                        stmt = "select Parola, Nume, Prenume from ContPacienti where CNP='" + cnpTB.Text.Trim() + "' and Parola='" + parolaTB.Text.Trim() + "'";
                                        sc = new SqlCommand(stmt, con);
                                        dr = sc.ExecuteReader();
                                        if (!dr.HasRows)
                                        {
                                            LabelParolaErr.ForeColor = Color.Red;
                                            LabelParolaErr.Text = "Parola introdusa este gresita!";
                                            LabelPrincipalErr.Text = "";
                                            LabelRezultatAutentificareErr.Text = "";
                                            LabelCnpErr.Text = "";
                                            sc.Dispose();
                                            dr.Close();
                                        }
                                        else
                                        {
                                            string url;
                                            Application["cnpPacient"] = cnpTB.Text.Trim();
                                            string numeP = "";
                                            if (dr.Read())
                                            {
                                                numeP= dr[1].ToString().Trim() + " " + dr[2].ToString().Trim();
                                                Application["numePacient"] = numeP;
                                            }
                                            sc.Dispose();
                                            dr.Close();

                                            url = "ContPacientPage.aspx?cnpPacient=" + cnpTB.Text.Trim()+"&numePacient="+numeP;
                                            Response.Redirect(url);

                                            LabelCnpErr.Text = "";
                                            LabelParolaErr.Text = "";
                                            LabelPrincipalErr.Text = "";
                                            LabelRezultatAutentificareErr.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        LabelParolaErr.ForeColor = Color.Red;
                                        LabelParolaErr.Text = "Parola poate sa contina minim 8 caractere si maxim 15 caractere.";
                                        LabelCnpErr.Text = "";
                                        LabelPrincipalErr.Text = "";
                                        LabelRezultatAutentificareErr.Text = "";
                                    }
                                }
                                else
                                {
                                    LabelParolaErr.ForeColor = Color.Red;
                                    LabelParolaErr.Text = "Nu ati introdus parola.";
                                    LabelCnpErr.Text = "";
                                    LabelPrincipalErr.Text = "";
                                    LabelRezultatAutentificareErr.Text = "";
                                }
                            }
                        }
                        else
                        {
                            LabelCnpErr.ForeColor = Color.Red;
                            LabelCnpErr.Text = "CNP-ul trebuie sa contina 13 cifre.";
                            LabelParolaErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelRezultatAutentificareErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelCnpErr.ForeColor = Color.Red;
                        LabelCnpErr.Text = "CNP-ul nu are voie sa contina litere.";
                        LabelParolaErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelRezultatAutentificareErr.Text = "";
                    }
                }
                else
                {
                    LabelCnpErr.ForeColor = Color.Red;
                    LabelCnpErr.Text = "Nu ati introuds CNP-ul.";
                    LabelParolaErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelRezultatAutentificareErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            LabelRezultatAutentificareErr.ForeColor = Color.Green;
            LabelRezultatAutentificareErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelCnpErr.Text = "";
            LabelParolaErr.Text = "";
            LabelPrincipalErr.Text = "";
            cnpTB.Text = "";
            parolaTB.Attributes.Add("value", "");
            cnpEmailTB.Text = "";
            emailTB.Text = "";
            LabelCnpEmailErr.Text = "";
            LabelTrimiteEmailErr.Text = "";
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void trimiteEmailBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (cnpEmailTB.Text.Trim() != "")
                {
                    if (testNum.IsMatch(cnpEmailTB.Text.Trim()))
                    {
                        if (cnpEmailTB.Text.Trim().Length == 13)
                        {
                            string stmt = "select Parola from ContPacienti where CNP='" + cnpEmailTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (!dr.HasRows)
                            {
                                LabelCnpErr.ForeColor = Color.Red;
                                LabelCnpErr.Text = "CNP-ul introdus nu are asociat nici un cont.";
                                LabelParolaErr.Text = "";
                                LabelRezultatAutentificareErr.Text = "";
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                if (emailTB.Text.Trim() != "")
                                {
                                    if (testEmail.IsMatch(emailTB.Text.Trim()))
                                    {
                                        stmt = "select Parola from ContPacienti where CNP='" + cnpEmailTB.Text.Trim() + "' and Email='" + emailTB.Text.Trim() + "'";
                                        sc = new SqlCommand(stmt, con);
                                        dr = sc.ExecuteReader();
                                        if (!dr.Read())
                                        {
                                            LabelTrimiteEmailErr.ForeColor = Color.Red;
                                            LabelTrimiteEmailErr.Text = "Adresa e-mail nu corespunde cu CNP-ul " + cnpEmailTB.Text.Trim();
                                            LabelCnpEmailErr.Text = "";
                                        }
                                        else
                                        {
                                            string parola = dr[0].ToString().Trim();
                                            MailAddress to = new MailAddress(emailTB.Text.Trim());
                                            MailAddress from = new MailAddress("denisavram121@gmail.com", "Denis - Dent");
                                            MailMessage message = new MailMessage(from, to);
                                            message.Subject = "Cerere parola";
                                            message.Body = "CNP utilizator: " + cnpEmailTB.Text.Trim() + "\nParola d-voastra: " + parola;
                                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                                            {
                                                Credentials = new NetworkCredential("denisavram121@gmail.com", "uovcrgrikfmxzvws"),
                                                EnableSsl = true

                                            };

                                            try
                                            {
                                                client.Send(message);
                                                LabelTrimiteEmailErr.ForeColor = Color.Green;
                                                LabelTrimiteEmailErr.Text = "Parola dumneavoastra a fost trimisa cu succes, pe e-email-ul: " + emailTB.Text.Trim();
                                                LabelCnpEmailErr.Text = "";
                                            }
                                            catch (SmtpException ex)
                                            {
                                                LabelTrimiteEmailErr.ForeColor = Color.Red;
                                                LabelTrimiteEmailErr.Text = "Nu se poate realiza conexiunea la serverul SMTP: " + ex.Message;
                                                LabelCnpEmailErr.Text = "";
                                            }
                                        }
                                        dr.Close();
                                        sc.Dispose();
                                    }
                                    else
                                    {
                                        LabelTrimiteEmailErr.ForeColor = Color.Red;
                                        LabelTrimiteEmailErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                                        LabelCnpEmailErr.Text = "";
                                    }
                                }
                                else
                                {
                                    LabelTrimiteEmailErr.ForeColor = Color.Red;
                                    LabelTrimiteEmailErr.Text = "Nu ati introdus adresa e-mail.";
                                    LabelCnpEmailErr.Text = "";
                                }
                            }
                            dr.Close();
                            sc.Dispose();
                        }
                        else
                        {
                            LabelCnpEmailErr.ForeColor = Color.Red;
                            LabelCnpEmailErr.Text = "CNP-ul trebuie sa contina 13 cifre.";
                            LabelTrimiteEmailErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelCnpEmailErr.ForeColor = Color.Red;
                        LabelCnpEmailErr.Text = "CNP-ul nu are voie sa contina litere.";
                        LabelTrimiteEmailErr.Text = "";
                    }
                }
                else
                {
                    LabelCnpEmailErr.ForeColor = Color.Red;
                    LabelCnpEmailErr.Text = "Nu ati introuds CNP-ul.";
                    LabelTrimiteEmailErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelTrimiteEmailErr.ForeColor = Color.Red;
                LabelTrimiteEmailErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelCnpEmailErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}