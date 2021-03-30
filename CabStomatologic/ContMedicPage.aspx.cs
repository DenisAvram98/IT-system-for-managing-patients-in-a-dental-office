using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace CabStomatologic
{
    public partial class ContMedicPage : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                string stmt = "select Nume, Prenume from Medici";
                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Medici");
                DataRow[] medici = ds.Tables["Medici"].Select();
                if (!Page.IsPostBack)
                {
                    mediciDDL.Items.Clear();
                    mediciDDL.Items.Add("Selectati...");
                    foreach (DataRow row in medici)
                    {
                        mediciDDL.Items.Add(row["Nume"].ToString().Trim() + " " + row["Prenume"].ToString().Trim());
                    }
                }

                ds.Dispose();
                da.Dispose();
                Label3.Text = "";
            }
            catch (Exception ex)
            {
                Label3.ForeColor = Color.Red;
                Label3.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void autentificareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (mediciDDL.SelectedIndex>=1)
                {
                    if (parolaTB.Text.Trim()!="")
                    {
                        con = new SqlConnection(conString);
                        con.Open();

                        string medic = mediciDDL.SelectedValue.ToString().Trim();
                        string[] f = medic.Split(' ');
                        string numeM = f[0];
                        string prenumeM = f[1];
                        if (f.Length==3)
                        {
                            string prenumeM2 = f[2];
                            prenumeM = prenumeM + " " + prenumeM2;
                            string stmt = "select Nume, Prenume, Parola from Medici where Nume='" + numeM + "' and Prenume='" + prenumeM + "' and Parola='"+parolaTB.Text.Trim()+"'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (!dr.Read()) //daca parola este gresita
                            {
                                Label4.ForeColor = Color.Red;
                                Label4.Text = "Parola introdusa este gresita!";
                                Label5.Text = "";
                                Label6.Text = "";
                                dr.Close();
                                sc.Dispose();
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                string url;
                                Application["numeMedic"] = medic;
                                url = "PaginaPrincipalaMedic.aspx?numeMedic="+medic;
                                Response.Redirect(url);
                            }
                        }
                        else if (f.Length==2)
                        {
                            string stmt = "select Nume, Prenume, Parola from Medici where Nume='" + numeM + "' and Prenume='" + prenumeM + "' and Parola='" + parolaTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (!dr.Read()) //daca parola este gresita
                            {
                                Label4.Text = "Parola introdusa este gresita!";
                                Label5.Text = "";
                                Label6.Text = "";
                                dr.Close();
                                sc.Dispose();
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                string url;
                                Application["numeMedic"] = medic;
                                url = "PaginaPrincipalaMedic.aspx?numeMedic=" + medic;
                                Response.Redirect(url);
                            }
                            Label3.Text = "";
                        }
                    }
                    else
                    {
                        Label6.ForeColor = Color.Red;
                        Label6.Text = "Nu ati introdus parola.";
                        Label4.Text = "";
                        Label5.Text = "";
                    }
                }
                else
                {
                    Label5.ForeColor = Color.Red;
                    Label5.Text = "Nu ati selectat utilizatorul.";
                    Label4.Text = "";
                    Label6.Text = "";
                }
            }
            catch (Exception ex)
            {
                Label3.ForeColor = Color.Red;
                Label3.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            Label5.Text = "";
            Label6.Text = "";
            Label3.Text = "";
            Label4.ForeColor = Color.Green;
            Label4.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            mediciDDL.SelectedIndex = 0;
            parolaTB.Attributes.Add("value", "");
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void trimiteEmailBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (emailTB.Text.Trim()!="")
                {
                    if (testEmail.IsMatch(emailTB.Text.Trim()))
                    {
                        string stmt = "select Parola from Medici where Email='" + emailTB.Text.Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        SqlDataReader dr = sc.ExecuteReader();
                        if (!dr.Read())
                        {
                            Label9.ForeColor = Color.Red;
                            Label9.Text = "Adresa e-mail nu corespunde nici unui cont registrat la noi.";
                            Label5.Text = "";
                            Label6.Text = "";
                            Label4.Text = "";
                        }
                        else
                        {
                            string parola = dr[0].ToString();
                            MailAddress to = new MailAddress(emailTB.Text.Trim());
                            MailAddress from = new MailAddress("denisavram121@gmail.com", "Denis - Dent");
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "Cerere parola";
                            message.Body = "Parola d-voastra: " + parola;

                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                            {
                                Credentials = new NetworkCredential("denisavram121@gmail.com", "uovcrgrikfmxzvws"),
                                EnableSsl = true
                            };
                            try
                            {
                                client.Send(message);
                                Label9.ForeColor = Color.Green;
                                Label9.Text = "Parola dumneavoastra a fost trimisa cu succes, pe e-email-ul: " + emailTB.Text.Trim();
                            }
                            catch (SmtpException ex)
                            {
                                Label9.ForeColor = Color.Red;
                                Label9.Text = "Nu se poate realiza conexiunea la serverul SMTP: " + ex.Message;
                                Label5.Text = "";
                                Label6.Text = "";
                                Label4.Text = "";
                            }
                        }

                        dr.Close();
                        sc.Dispose();
                    }
                    else
                    {
                        Label9.ForeColor = Color.Red;
                        Label9.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                        Label5.Text = "";
                        Label6.Text = "";
                        Label4.Text = "";
                    }
                }
                else
                {
                    Label9.ForeColor = Color.Red;
                    Label9.Text = "Nu ati introdus adresa e-mail.";
                    Label5.Text = "";
                    Label6.Text = "";
                    Label4.Text = "";
                }
            }
            catch (Exception ex)
            {
                Label9.ForeColor = Color.Red;
                Label9.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label5.Text = "";
                Label6.Text = "";
                Label4.Text = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}