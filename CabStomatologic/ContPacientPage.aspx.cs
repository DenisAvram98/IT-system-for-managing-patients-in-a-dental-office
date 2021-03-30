using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class ContPacientPage : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string numeP;
        string cnp;
        string cnpPacientLogat = "";

        protected void IncarcareDatePacientLogat ()
        {
            string stmt = "select Nume, Prenume, Telefon, Email from ContPacienti where CNP='" + cnpPacientLogat + "'";
            SqlCommand sc = new SqlCommand(stmt, con);
            SqlDataReader dr = sc.ExecuteReader();
            if (dr.Read())
            {
                numeTB.Text = dr["Nume"].ToString().Trim();
                prenumeTB.Text = dr["Prenume"].ToString().Trim();
                telefonTB.Text = dr["Telefon"].ToString().Trim();
                emailTB.Text = dr["Email"].ToString().Trim();
            }
            dr.Close();
            sc.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["numePacient"] as string))
                    {
                        numeP = Request.QueryString["numePacient"];
                        cnp = Request.QueryString["cnpPacient"];
                        LabelIesirePacient.Text = "Iesire, " + numeP.ToString().Trim();
                        Label9.Text = "";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Application["numePacient"] as string))
                        {
                            LabelIesirePacient.Text = "Iesire, " + (string)Application["numePacient"];
                            Label9.Text = "";
                        }
                        else
                        {
                            throw new NullReferenceException("Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.");
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    Label9.ForeColor = Color.Red;
                    Label9.Text = ex.Message;
                }
                cnpPacientLogat = (string)Application["cnpPacient"];

                string stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CISimple";
                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "CISimple");
                GridView1.DataSource = ds.Tables["CISimple"];
                GridView1.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIAlbireDentaraTratamenteSpeciale";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIAlbireDentaraTratamenteSpeciale");
                GridView2.DataSource = ds.Tables["CIAlbireDentaraTratamenteSpeciale"];
                GridView2.DataBind();

                stmt = "select nume as [Nume interventie], categorie as [Categorie], pret as [Pret in Lei] from CIProtetica";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIProtetica");
                GridView3.DataSource = ds.Tables["CIProtetica"];
                GridView3.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIChirurgie";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIChirurgie");
                GridView10.DataSource = ds.Tables["CIChirurgie"];
                GridView10.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIImplantDentar";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIImplantDentar");
                GridView4.DataSource = ds.Tables["CIImplantDentar"];
                GridView4.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIBonturiProtetice";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIBonturiProtetice");
                GridView5.DataSource = ds.Tables["CIBonturiProtetice"];
                GridView5.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIProtezeSpecialeImplant";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIProtezeSpecialeImplant");
                GridView6.DataSource = ds.Tables["CIProtezeSpecialeImplant"];
                GridView6.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIProteze";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIProteze");
                GridView7.DataSource = ds.Tables["CIProteze"];
                GridView7.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIRadiologie";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIRadiologie");
                GridView8.DataSource = ds.Tables["CIRadiologie"];
                GridView8.DataBind();

                stmt = "select nume as [Nume interventie], pret as [Pret in Lei] from CIAblatiiCimentariAmprente";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "CIAblatiiCimentariAmprente");
                GridView9.DataSource = ds.Tables["CIAblatiiCimentariAmprente"];
                GridView9.DataBind();

                stmt = "select nume, prenume from Medici";
                da = new SqlDataAdapter(stmt, con);
                da.Fill(ds, "Medici");
                DataRow[] medici = ds.Tables["Medici"].Select();

                if (!Page.IsPostBack) //ca sa faca upload numai o data cand se ruleaza pagina si nu de fiecare data cand se da refresh
                {
                    mediciDDL.Items.Clear();
                    mediciDDL.Items.Add("Selectati...");
                    foreach (DataRow row in medici)
                    {
                        mediciDDL.Items.Add(row["nume"].ToString() + " " + row["prenume"].ToString());
                    }
                }

                da.Dispose();
                ds.Dispose();
                Label10.Text = "";
                Label11.Text = "";
                Label12.Text = "";
                Label13.Text = "";
                Label14.Text = "";
                Label15.Text = "";
                Label16.Text = "";
                Label17.Text = "";
                Label37.Text = "";

                dataTB.Attributes.Add("readonly", "readonly");

                if (!IsPostBack)
                {
                    IncarcareDatePacientLogat();
                }
            }
            catch (Exception ex)
            {
                Label9.ForeColor = Color.Red;
                Label9.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label10.ForeColor = Color.Red;
                Label10.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label11.ForeColor = Color.Red;
                Label11.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label12.ForeColor = Color.Red;
                Label12.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label13.ForeColor = Color.Red;
                Label13.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label14.ForeColor = Color.Red;
                Label14.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label15.ForeColor = Color.Red;
                Label15.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label16.ForeColor = Color.Red;
                Label16.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label17.ForeColor = Color.Red;
                Label17.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label37.ForeColor = Color.Red;
                Label37.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void solicitatiProgramareBtn_Click(object sender, EventArgs e)
        {
            Regex test = new Regex("^[a-zA-Z ]+$");
            Regex testNum = new Regex("^[0-9]+$");
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    if (numeTB.Text != "")
                    {
                        if (test.IsMatch(numeTB.Text))
                        {
                            if (prenumeTB.Text != "")
                            {
                                if (test.IsMatch(prenumeTB.Text))
                                {
                                    if (telefonTB.Text != "")
                                    {
                                        if (testNum.IsMatch(telefonTB.Text))
                                        {
                                            if (emailTB.Text.Trim() == "" || testEmail.IsMatch(emailTB.Text.Trim()))
                                            {
                                                if (mediciDDL.SelectedIndex >= 1) //la 0 avem Selectati
                                                {
                                                    if (motivProgramareDDL.SelectedIndex >= 1) //la 0 avem selectati
                                                    {
                                                        if (dataTB.Text != "")
                                                        {
                                                            DateTime data = Convert.ToDateTime(dataTB.Text);
                                                            if (data > DateTime.Today.Date)
                                                            {
                                                                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
                                                                {
                                                                    if (oraDDL.SelectedIndex >= 1) //la 0 avem selectati
                                                                    {
                                                                        string stmt = "select Data, Ora from Programari";
                                                                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                                                        DataSet ds = new DataSet();
                                                                        da.Fill(ds, "Programari");
                                                                        DataRow[] programri = ds.Tables["Programari"].Select();

                                                                        string ora = oraDDL.SelectedValue.ToString();
                                                                        ora = ora + ":00";
                                                                        Boolean check = false;
                                                                        foreach (DataRow row in programri)
                                                                        {
                                                                            string incerc = row["data"].ToString(); //data returnata ii si cu timp si de aia am facut asta
                                                                            DateTime incercData = Convert.ToDateTime(incerc);
                                                                            if (incercData.ToString("MM/dd/yyyy") == data.ToString("MM/dd/yyyy") && row["Ora"].ToString() == ora)
                                                                            {
                                                                                Label36.ForeColor = Color.Red;
                                                                                Label36.Text = "Data si ora selectata nu sunt disponibile!";
                                                                                Label34.ForeColor = Color.Red;
                                                                                Label34.Text = "Incercati sa alegeti o alta ora pentru programarea dumneavoastra.<br>Daca nici o ora nu este disponibila pentru data selectata, va rugam sa incercati pentru o data diferita.";
                                                                                Label28.Text = "";
                                                                                Label29.Text = "";
                                                                                Label30.Text = "";
                                                                                Label31.Text = "";
                                                                                Label32.Text = "";
                                                                                Label33.Text = "";
                                                                                LabelEmailErr.Text = "";
                                                                                check = true;
                                                                                break;
                                                                            }
                                                                        }
                                                                        if (check == false)
                                                                        {
                                                                            //daca trece de foreach
                                                                            string numeM = mediciDDL.SelectedValue.ToString();
                                                                            string[] f = numeM.Split(' ');
                                                                            string nM = f[0].ToString();
                                                                            string prenumeM = f[1].ToString();
                                                                            int idM = 0;
                                                                            if (f.Length == 3)
                                                                            {
                                                                                string prenume2M = f[2].ToString();
                                                                                prenumeM = prenumeM + " " + prenume2M;
                                                                                stmt = "select IdMedic from Medici where Nume='" + nM + "' and Prenume='" + prenumeM + "'";
                                                                                SqlCommand sc3 = new SqlCommand(stmt, con);
                                                                                SqlDataReader reader2 = sc3.ExecuteReader();
                                                                                while (reader2.Read())
                                                                                {
                                                                                    idM = Int32.Parse(reader2["IdMedic"].ToString());
                                                                                }
                                                                                reader2.Close();
                                                                                sc3.Dispose();
                                                                            }
                                                                            else if (f.Length == 2)
                                                                            {
                                                                                stmt = "select IdMedic from Medici where Nume='" + nM + "' and Prenume='" + prenumeM + "'";
                                                                                SqlCommand sc2 = new SqlCommand(stmt, con);
                                                                                SqlDataReader reader = sc2.ExecuteReader();
                                                                                while (reader.Read())
                                                                                {
                                                                                    idM = Int32.Parse(reader["IdMedic"].ToString());
                                                                                }
                                                                                reader.Close();
                                                                                sc2.Dispose();
                                                                            }

                                                                            stmt = "insert into Programari ([Nume], [Prenume], [Telefon], [Email], [IdMedic], [NumeMedic], [MotivProgramare], [Data], [Ora], [Mesaj], [StatusProgramare], [CNP]) values (@n,@p,@t,@e,@idM,@nM,@mP,@d,@o,@m,@sP,@cnp)";
                                                                            SqlCommand sc = new SqlCommand(stmt, con);
                                                                            sc.Parameters.AddWithValue("@n", numeTB.Text);
                                                                            sc.Parameters.AddWithValue("@p", prenumeTB.Text);
                                                                            sc.Parameters.AddWithValue("@t", telefonTB.Text);
                                                                            sc.Parameters.AddWithValue("@e", emailTB.Text);
                                                                            sc.Parameters.AddWithValue("@idM", idM);
                                                                            sc.Parameters.AddWithValue("@nM", numeM);
                                                                            sc.Parameters.AddWithValue("@mP", motivProgramareDDL.SelectedValue.ToString());
                                                                            sc.Parameters.AddWithValue("@d", data.ToString("MM/dd/yyyy"));
                                                                            sc.Parameters.AddWithValue("@o", ora);
                                                                            sc.Parameters.AddWithValue("@m", mesajTB.Text);
                                                                            sc.Parameters.AddWithValue("@sP", "Neconfirmata");
                                                                            sc.Parameters.AddWithValue("@cnp", cnpPacientLogat);
                                                                            sc.ExecuteNonQuery();

                                                                            Label36.ForeColor = Color.Green;
                                                                            Label36.Text = "Programarea a fost solocitata cu succes.<br>Statusul programari il puteti verifica accesand pagina Programari din meinu.";
                                                                            Label28.Text = "";
                                                                            Label29.Text = "";
                                                                            Label30.Text = "";
                                                                            Label31.Text = "";
                                                                            Label32.Text = "";
                                                                            Label33.Text = "";
                                                                            Label34.Text = "";
                                                                            Label9.Text = "";
                                                                            LabelEmailErr.Text = "";

                                                                            sc.Dispose();
                                                                            ds.Dispose();
                                                                            da.Dispose();
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        Label34.ForeColor = Color.Red;
                                                                        Label34.Text = "Nu ati selectat ora dorita.";
                                                                        Label33.Text = "";
                                                                        Label28.Text = "";
                                                                        Label29.Text = "";
                                                                        Label30.Text = "";
                                                                        Label31.Text = "";
                                                                        Label32.Text = "";
                                                                        Label36.Text = "";
                                                                        Label9.Text = "";
                                                                        LabelEmailErr.Text = "";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Label33.ForeColor = Color.Red;
                                                                    Label33.Text = "Sambata si duminica cabinetul stomatologic Denis - Dent nu lucreaza.";
                                                                    Label28.Text = "";
                                                                    Label29.Text = "";
                                                                    Label30.Text = "";
                                                                    Label31.Text = "";
                                                                    Label32.Text = "";
                                                                    Label34.Text = "";
                                                                    Label36.Text = "";
                                                                    Label9.Text = "";
                                                                    LabelEmailErr.Text = "";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Label33.ForeColor = Color.Red;
                                                                Label33.Text = "Data aleasa este indisponibila. Nu se poate realiza programarea pentru data de azi sau zilele trecute deja.";
                                                                Label28.Text = "";
                                                                Label29.Text = "";
                                                                Label30.Text = "";
                                                                Label31.Text = "";
                                                                Label32.Text = "";
                                                                Label34.Text = "";
                                                                Label36.Text = "";
                                                                Label9.Text = "";
                                                                LabelEmailErr.Text = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Label33.ForeColor = Color.Red;
                                                            Label33.Text = "Nu ati ales data dorita.";
                                                            Label32.Text = "";
                                                            Label28.Text = "";
                                                            Label29.Text = "";
                                                            Label30.Text = "";
                                                            Label31.Text = "";
                                                            Label34.Text = "";
                                                            Label36.Text = "";
                                                            Label9.Text = "";
                                                            LabelEmailErr.Text = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Label32.ForeColor = Color.Red;
                                                        Label32.Text = "Nu ati selectat motivul programari.";
                                                        Label31.Text = "";
                                                        Label28.Text = "";
                                                        Label29.Text = "";
                                                        Label33.Text = "";
                                                        Label34.Text = "";
                                                        Label36.Text = "";
                                                        Label9.Text = "";
                                                        LabelEmailErr.Text = "";
                                                    }
                                                }
                                                else
                                                {
                                                    Label31.ForeColor = Color.Red;
                                                    Label31.Text = "Nu ati selectat medicul stomatolog dorit.";
                                                    Label30.Text = "";
                                                    Label28.Text = "";
                                                    Label29.Text = "";
                                                    Label32.Text = "";
                                                    Label33.Text = "";
                                                    Label34.Text = "";
                                                    Label36.Text = "";
                                                    Label9.Text = "";
                                                    LabelEmailErr.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                LabelEmailErr.ForeColor = Color.Red;
                                                LabelEmailErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                                                Label31.Text = "";
                                                Label30.Text = "";
                                                Label28.Text = "";
                                                Label29.Text = "";
                                                Label32.Text = "";
                                                Label33.Text = "";
                                                Label34.Text = "";
                                                Label36.Text = "";
                                                Label9.Text = "";
                                            }
                                        }
                                        else
                                        {
                                            Label30.ForeColor = Color.Red;
                                            Label30.Text = "Numarul de telefon poate sa contina numai cifre.";
                                            Label28.Text = "";
                                            Label29.Text = "";
                                            Label31.Text = "";
                                            Label32.Text = "";
                                            Label33.Text = "";
                                            Label34.Text = "";
                                            Label36.Text = "";
                                            Label9.Text = "";
                                            LabelEmailErr.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        Label30.ForeColor = Color.Red;
                                        Label30.Text = "Nu ati introdus numarul de telefon.";
                                        Label29.Text = "";
                                        Label28.Text = "";
                                        Label31.Text = "";
                                        Label32.Text = "";
                                        Label33.Text = "";
                                        Label34.Text = "";
                                        Label36.Text = "";
                                        Label9.Text = "";
                                        LabelEmailErr.Text = "";
                                    }
                                }
                                else
                                {
                                    Label29.ForeColor = Color.Red;
                                    Label29.Text = "Prenumele nu are voie sa contina altceva in afara de litere.";
                                    Label28.Text = "";
                                    Label30.Text = "";
                                    Label31.Text = "";
                                    Label32.Text = "";
                                    Label33.Text = "";
                                    Label34.Text = "";
                                    Label36.Text = "";
                                    Label9.Text = "";
                                    LabelEmailErr.Text = "";
                                }
                            }
                            else
                            {
                                Label29.ForeColor = Color.Red;
                                Label29.Text = "Nu ati introdus prenumele.";
                                Label28.Text = "";
                                Label30.Text = "";
                                Label31.Text = "";
                                Label32.Text = "";
                                Label33.Text = "";
                                Label34.Text = "";
                                Label36.Text = "";
                                Label9.Text = "";
                                LabelEmailErr.Text = "";
                            }
                        }
                        else
                        {
                            Label28.ForeColor = Color.Red;
                            Label28.Text = "Numele nu are voie sa contina altceva in afara de litere.";
                            Label36.Text = "";
                            Label29.Text = "";
                            Label30.Text = "";
                            Label31.Text = "";
                            Label32.Text = "";
                            Label33.Text = "";
                            Label34.Text = "";
                            Label9.Text = "";
                            LabelEmailErr.Text = "";
                        }
                    }
                    else
                    {
                        Label28.ForeColor = Color.Red;
                        Label28.Text = "Nu ati introdus numele.";
                        Label29.Text = "";
                        Label30.Text = "";
                        Label31.Text = "";
                        Label32.Text = "";
                        Label33.Text = "";
                        Label34.Text = "";
                        Label36.Text = "";
                        Label9.Text = "";
                        LabelEmailErr.Text = "";
                    }
                }
                else
                {
                    Label36.ForeColor = Color.Red;
                    Label36.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    Label28.Text = "";
                    Label29.Text = "";
                    Label30.Text = "";
                    Label31.Text = "";
                    Label32.Text = "";
                    Label33.Text = "";
                    Label34.Text = "";
                    Label9.Text = "";
                    LabelEmailErr.Text = "";
                }
            }
            catch (FormatException fe)
            {
                Label33.ForeColor = Color.Red;
                Label33.Text = "Data introdusa/aleasa are formatl gresit: " + fe.Message;
                Label28.Text = "";
                Label29.Text = "";
                Label30.Text = "";
                Label31.Text = "";
                Label32.Text = "";
                Label34.Text = "";
                Label36.Text = "";
                Label9.Text = "";
                LabelEmailErr.Text = "";
            }
            catch (Exception ex)
            {
                Label36.ForeColor = Color.Red;
                Label36.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label28.Text = "";
                Label29.Text = "";
                Label30.Text = "";
                Label31.Text = "";
                Label32.Text = "";
                Label33.Text = "";
                Label34.Text = "";
                Label9.Text = "";
                LabelEmailErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            Label28.Text = "";
            Label29.Text = "";
            Label30.Text = "";
            Label31.Text = "";
            Label32.Text = "";
            Label33.Text = "";
            Label34.Text = "";
            mediciDDL.SelectedIndex = 0;
            motivProgramareDDL.SelectedIndex = 0;
            dataTB.Text = "";
            oraDDL.SelectedIndex = 0;
            mesajTB.Text = "";
            LabelEmailErr.Text = "";

            try
            {
                con = new SqlConnection(conString);
                con.Open();
                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    IncarcareDatePacientLogat();
                    Label36.ForeColor = Color.Green;
                    Label36.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
                    Label9.ForeColor = Color.Green;
                    Label9.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
                    Label28.Text = "";
                    Label29.Text = "";
                    Label30.Text = "";
                    Label31.Text = "";
                    Label32.Text = "";
                    Label33.Text = "";
                    Label34.Text = "";
                    LabelEmailErr.Text = "";
                }
                else
                {
                    Label36.ForeColor = Color.Red;
                    Label36.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    Label9.ForeColor = Color.Red;
                    Label9.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    Label28.Text = "";
                    Label29.Text = "";
                    Label30.Text = "";
                    Label31.Text = "";
                    Label32.Text = "";
                    Label33.Text = "";
                    Label34.Text = "";
                    LabelEmailErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                Label36.ForeColor = Color.Red;
                Label36.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label9.ForeColor = Color.Red;
                Label9.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                Label28.Text = "";
                Label29.Text = "";
                Label30.Text = "";
                Label31.Text = "";
                Label32.Text = "";
                Label33.Text = "";
                Label34.Text = "";
                LabelEmailErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}