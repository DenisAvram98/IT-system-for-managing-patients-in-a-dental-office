using System;
using System.Collections.Generic;
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
    public partial class ContReceptieAutentificare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            receptieTB.Attributes.Add("readonly", "readonly");
            receptieTB.Text = "Receptie";
        }

        protected void autentificareBtn_Click(object sender, EventArgs e)
        {
            if (receptieTB.Text.Trim()!="")
            {
                if (receptieTB.Text.Trim()=="Receptie")
                {
                    if (parolaTB.Text.Trim()!="")
                    {
                        if (parolaTB.Text.Trim()=="Receptie")
                        {
                            if (receptieTB.Text.Trim()==parolaTB.Text.Trim())
                            {
                                LabelParolaErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                LabelAutentificareErr.Text = "";

                                string url;
                                Application["utilizatorR"] = receptieTB.Text.Trim();
                                url = "PaginaPrincipalaReceptie.aspx?utilizatorR=" + receptieTB.Text.Trim();
                                Response.Redirect(url);
                            }
                        }
                        else
                        {
                            LabelParolaErr.ForeColor = Color.Red;
                            LabelParolaErr.Text = "Parola introdusa este gresita!";
                            LabelPrincipalErr.Text = "";
                            LabelAutentificareErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelParolaErr.ForeColor = Color.Red;
                        LabelParolaErr.Text = "Nu ati introdus parola.";
                        LabelAutentificareErr.Text = "";
                        LabelPrincipalErr.Text = "";
                    }
                }
                else
                {
                    LabelAutentificareErr.ForeColor = Color.Red;
                    LabelAutentificareErr.Text = "Sa modificat utilizatorul. Utilizatorul curent nu are acces.";
                    LabelParolaErr.Text = "";
                    LabelPrincipalErr.Text = "";
                }
            }
            else
            {
                LabelAutentificareErr.ForeColor = Color.Red;
                LabelAutentificareErr.Text = "Sa modificat utilizatorul. Utilizatorul curent nu are acces.";
                LabelParolaErr.Text = "";
                LabelPrincipalErr.Text = "";
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            LabelAutentificareErr.ForeColor = Color.Green;
            LabelAutentificareErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelParolaErr.Text = "";
            LabelPrincipalErr.Text = "";
            LabelTrimiteEmailErr.Text = "";
            receptieTB.Text = "Receptie";
            parolaTB.Attributes.Add("value", "");
            emailTB.Text = "";
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void trimiteEmailBtn_Click(object sender, EventArgs e)
        {
            if (emailTB.Text.Trim()!="")
            {
                if (testEmail.IsMatch(emailTB.Text.Trim()))
                {
                    if (emailTB.Text.Trim()=="denis_avram98@yahoo.com")
                    {
                        string parola = "Receptie";
                        MailAddress to = new MailAddress(emailTB.Text.Trim());
                        MailAddress from = new MailAddress("denisavram121@gmail.com", "Denis - Dent");
                        MailMessage message = new MailMessage(from, to);
                        message.Subject = "Cerere parola";
                        message.Body = "Utilizator: Receptie" + "\nParola d-voastra: " + parola;
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
                        }
                        catch (SmtpException ex)
                        {
                            LabelTrimiteEmailErr.ForeColor = Color.Red;
                            LabelTrimiteEmailErr.Text = "Nu se poate realiza conexiunea la serverul SMTP: " + ex.Message;
                        }
                    }
                    else
                    {
                        LabelTrimiteEmailErr.ForeColor = Color.Red;
                        LabelTrimiteEmailErr.Text = "Adresa e-mail nu corespunde pentru contul de receptie.";
                    }
                }
                else
                {
                    LabelTrimiteEmailErr.ForeColor = Color.Red;
                    LabelTrimiteEmailErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                }
            }
            else
            {
                LabelTrimiteEmailErr.ForeColor = Color.Red;
                LabelTrimiteEmailErr.Text = "Nu ati introdus adresa e-mail.";
            }
        }
    }
}