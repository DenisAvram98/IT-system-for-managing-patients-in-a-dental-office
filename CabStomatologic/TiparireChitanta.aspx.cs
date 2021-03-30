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
    public partial class TiparireChitantaaspx : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string receptie = "";

        protected void IncarcaConsultatiaSelectata ()
        {
            using (con = new SqlConnection(conString))
            {
                con.Open();

                string stmt = "select Data, Ora, Interventii, Medic, CostTotal from Consultatii where IdConsultatie='" + nrConsultatieDDL.SelectedValue.ToString().Trim() + "'";
                SqlCommand sc = new SqlCommand(stmt, con);
                SqlDataReader dr = sc.ExecuteReader();
                if (dr.Read())
                {
                    DateTime data = Convert.ToDateTime(dr["data"].ToString().Trim());
                    DateTime ora = Convert.ToDateTime(dr["ora"].ToString().Trim());
                    dataConsultatieTB.Text = data.ToString("M/d/yyyy");
                    oraConsultatieTB.Text = ora.ToString("HH:mm");
                    string interventii = dr["Interventii"].ToString().Trim();
                    interventii = interventii.Replace(";", "\n");
                    interventii = interventii.Replace("_", "\nInterventie: ");
                    interventii = interventii.Replace("//", " - ");
                    interventii = interventii.Replace(".", " ");
                    interventiTB.Text = interventii;
                    costTotalTB.Text = dr["CostTotal"].ToString().Trim();
                    numeMedicTB.Text = dr["Medic"].ToString().Trim();
                }
                dr.Close();
                sc.Dispose();

                stmt = "select SumaTotalAchitata from Chitante where IdConsultatie='" + nrConsultatieDDL.SelectedValue.ToString().Trim() + "'";
                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Chitante");
                DataRow[] sumaTotalAchitata = ds.Tables["Chitante"].Select();
                float sumaTotalA = 0;
                if (sumaTotalAchitata.Length == 0)
                {
                    sumaTotalA = 0;
                }
                else
                {
                    foreach (DataRow row in sumaTotalAchitata)
                    {
                        sumaTotalA = float.Parse(row["SumaTotalAchitata"].ToString().Trim());
                    }
                }
                sumaAchitataBazaDTB.Text = sumaTotalA.ToString();
                sumaRamasaTB.Text = (float.Parse(costTotalTB.Text.Trim()) - float.Parse(sumaAchitataBazaDTB.Text.Trim())).ToString();
                LabelNrConsultatieErr.Text = "";
                LabelAchitatAziErr.Text = "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            receptie = (string)Application["utilizatorR"];

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (!IsPostBack)
                    {
                        dataTB.Text = DateTime.Now.ToString("M/d/yyyy");
                        oraTB.Text = DateTime.Now.ToString("HH:mm");
                        try
                        {
                            numePacientTB.Text = Session["numePacient"].ToString();
                            cnpTB.Text = Session["cnp"].ToString();
                            LabelPrincipalErr.Text = "";
                        }
                        catch (NullReferenceException ex)
                        {
                            LabelPrincipalErr.ForeColor = Color.Red;
                            LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                        }
                    }

                    string stmt = "select IdConsultatie, CostTotal from Consultatii where CNP='" + cnpTB.Text.Trim() + "' order by Data DESC";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Consultatii");
                    DataRow[] consultatii = ds.Tables["Consultatii"].Select(); //toate consultatile pentru persoana respectiva

                    if (!IsPostBack)
                    {
                        nrConsultatieDDL.Items.Clear();
                        nrConsultatieDDL.Items.Add("Selectati...");

                        stmt = "select IdConsultatie, SumaTotalAchitata, CostTotal from Chitante where CostTotal=SumaTotalAchitata and CNP='" + cnpTB.Text.Trim() + "'";
                        SqlDataAdapter da2 = new SqlDataAdapter(stmt, con);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2, "Chitante");
                        DataRow[] achitat = ds2.Tables["Chitante"].Select(); //consultatiele achitate deja de persoana respectiva
                        string consAchitata = "";
                        foreach (DataRow cons in consultatii)
                        {
                            foreach (DataRow achit in achitat)
                            {
                                string consultatie = cons["IdConsultatie"].ToString().Trim();
                                string conA = achit["IdConsultatie"].ToString().Trim();
                                if (cons["IdConsultatie"].ToString().Trim() == achit["IdConsultatie"].ToString().Trim()) //daca consultatia este achitata
                                {
                                    consAchitata = cons["IdConsultatie"].ToString().Trim();//salvam consultataia achitata
                                }
                            }
                            if (cons["IdConsultatie"].ToString().Trim() != consAchitata) //daca consultatia nu este achitata o adugam in DDL
                            {
                                nrConsultatieDDL.Items.Add(cons["IdConsultatie"].ToString().Trim());
                            }
                        }
                        ds2.Dispose();
                        da2.Dispose();
                    }
                    ds.Dispose();
                    da.Dispose();

                    if (!IsPostBack)
                    {
                        if (!string.IsNullOrEmpty(Session["numarConsultatie"] as string))
                        {
                            string test = Session["numarConsultatie"].ToString();
                            nrConsultatieDDL.SelectedValue = Session["numarConsultatie"].ToString();
                            if (nrConsultatieDDL.SelectedIndex >= 1)
                            {
                                IncarcaConsultatiaSelectata();
                            }
                        }
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
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

        protected void nrConsultatieDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            achitatAziTB.Text = "";
            try
            {
                if (nrConsultatieDDL.SelectedIndex >= 1)
                {
                    IncarcaConsultatiaSelectata();
                }
                else
                {
                    dataConsultatieTB.Text = "";
                    oraConsultatieTB.Text = "";
                    interventiTB.Text = "";
                    costTotalTB.Text = "";
                    sumaAchitataBazaDTB.Text = "";
                    sumaRamasaTB.Text = "";
                    LabelNrConsultatieErr.Text = "";
                    LabelAchitatAziErr.Text = "";
                    numeMedicTB.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelNrConsultatieErr.ForeColor = Color.Red;
                LabelNrConsultatieErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        static Boolean testAchitatAzi = false;
        protected void achitatAziTB_TextChanged(object sender, EventArgs e)
        {
            Regex testNum = new Regex("^[0-9]+$");
            if (costTotalTB.Text != "" && sumaAchitataBazaDTB.Text != "")
            {
                float sumaRamasa = float.Parse(costTotalTB.Text.Trim()) - float.Parse(sumaAchitataBazaDTB.Text.Trim());

                try
                {
                    if (achitatAziTB.Text.Trim() != "")
                    {
                        if (testNum.IsMatch(achitatAziTB.Text.Trim()))
                        {
                            if (float.Parse(achitatAziTB.Text.Trim()) <= sumaRamasa && float.Parse(sumaRamasaTB.Text.Trim()) >= 0)
                            {
                                sumaRamasaTB.Text = (sumaRamasa - float.Parse(achitatAziTB.Text.Trim())).ToString();
                                LabelAchitatAziErr.Text = "";
                                testAchitatAzi = true;
                            }
                            else
                            {
                                sumaRamasaTB.Text = sumaRamasa.ToString();
                                LabelAchitatAziErr.ForeColor = Color.Red;
                                LabelAchitatAziErr.Text = "Suma introdusa este mai mare de cat este nevoie de achitat.";
                                testAchitatAzi = false;
                            }
                        }
                        else
                        {
                            sumaRamasaTB.Text = sumaRamasa.ToString();
                            LabelAchitatAziErr.Text = "Suma achitata poate sa contina numai cifre de la 0 la 9.";
                            testAchitatAzi = false;
                        }
                    }
                    else
                    {
                        sumaRamasaTB.Text = sumaRamasa.ToString();
                        LabelAchitatAziErr.ForeColor = Color.Red;
                        LabelAchitatAziErr.Text = "Nu ati introdus suma care urmeaza sa fie achitata.";
                        testAchitatAzi = false;
                    }
                }
                catch (Exception ex)
                {
                    LabelAchitatAziErr.Text = ex.Message;
                }
            }
            else
            {
                LabelAchitatAziErr.ForeColor = Color.Red;
                LabelAchitatAziErr.Text = "Nu ati selectat numarul consultatiei.";
                testAchitatAzi = false;
            }
        }

        protected void tiparireChitantaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (nrConsultatieDDL.SelectedIndex >= 1)
                    {
                        if (testAchitatAzi != false && achitatAziTB.Text != "")
                        {
                            string stmt = "select IdConsultatie, SumaTotalAchitata, CostTotal from Chitante where CostTotal=SumaTotalAchitata and CNP='" + cnpTB.Text.Trim() + "' and IdConsultatie='" + nrConsultatieDDL.SelectedValue.ToString().Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (dr.HasRows)
                            {
                                LabelRezultatTiparireErr.ForeColor = Color.Red;
                                LabelRezultatTiparireErr.Text = "Consultatia a fost deja achitata.";//eraore deja a fost achitata chitanta
                                LabelAchitatAziErr.Text = "";
                                LabelNrConsultatieErr.Text = "";
                                LabelPrincipalErr.Text = "";
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                stmt = "insert into Chitante ([IdConsultatie],[CNP],[NumePacient],[Data],[Ora],[SumaTotalAchitata],[AchitatAzi],[CostTotal]) values (@iC,@cnp,@nP,@d,@o,@sTA,@aA,@cT)";
                                SqlCommand sc2 = new SqlCommand(stmt, con);
                                sc2.Parameters.AddWithValue("@iC", nrConsultatieDDL.SelectedValue.ToString().Trim());
                                sc2.Parameters.AddWithValue("@cnp", cnpTB.Text.Trim());
                                sc2.Parameters.AddWithValue("@nP", numePacientTB.Text.Trim());
                                sc2.Parameters.AddWithValue("@d", DateTime.Now.ToString("MM/dd/yyyy"));
                                sc2.Parameters.AddWithValue("@o", DateTime.Now.ToString("HH:mm") + ":00");
                                float sumaTA = float.Parse(sumaAchitataBazaDTB.Text.Trim()) + float.Parse(achitatAziTB.Text.Trim());
                                sc2.Parameters.AddWithValue("@sTA", sumaTA.ToString());
                                sc2.Parameters.AddWithValue("@aA", achitatAziTB.Text.Trim());
                                sc2.Parameters.AddWithValue("@cT", costTotalTB.Text.Trim());
                                sc2.ExecuteNonQuery();

                                LabelRezultatTiparireErr.ForeColor = Color.Green;
                                LabelRezultatTiparireErr.Text = "Chitanta a fost tiparita. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                LabelPrincipalErr.ForeColor = Color.Green;
                                LabelPrincipalErr.Text = "Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                LabelAchitatAziErr.Text = "";
                                LabelNrConsultatieErr.Text = "";

                                ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "window.print()", true);

                                sc2.Dispose();
                            }

                            dr.Close();
                            sc.Dispose();
                        }
                        else
                        {
                            LabelAchitatAziErr.ForeColor = Color.Red;
                            LabelAchitatAziErr.Text = "Nu ati introdus suma care urmeaza sa fie achitata sau suma introdusa nu este valida.";
                            LabelNrConsultatieErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelRezultatTiparireErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelNrConsultatieErr.ForeColor = Color.Red;
                        LabelNrConsultatieErr.Text = "Nu ati selectat numarul consultatiei.";
                        LabelAchitatAziErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelRezultatTiparireErr.Text = "";
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelRezultatTiparireErr.ForeColor = Color.Red;
                    LabelRezultatTiparireErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelRezultatTiparireErr.ForeColor = Color.Red;
                LabelRezultatTiparireErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
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
            LabelRezultatTiparireErr.ForeColor = Color.Green;
            LabelRezultatTiparireErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelAchitatAziErr.Text = "";
            LabelNrConsultatieErr.Text = "";
            nrConsultatieDDL.SelectedIndex = 0;
            dataConsultatieTB.Text = "";
            oraConsultatieTB.Text = "";
            interventiTB.Text = "";
            costTotalTB.Text = "";
            numeMedicTB.Text = "";
            sumaAchitataBazaDTB.Text = "";
            sumaRamasaTB.Text = "";
            achitatAziTB.Text = "";
        }

        protected void interventiTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = interventiTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }
    }
}