using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class ConsultatieNoua : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string numeM = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            numeM = (string)Application["numeMedic"];
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(numeM))
                {
                    string stmt = "select NumeClasa from ClasaDiagnostice order by NumeClasa";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ClasaDiagnostice");
                    DataRow[] ClasaDiagnostice = ds.Tables["ClasaDiagnostice"].Select();
                    if (!IsPostBack)
                    {
                        interventiaDDL.Items.Add("Selectati clasa interventii...");

                        clasaDiagnosticeDDL.Items.Clear();
                        clasaDiagnosticeDDL.Items.Add("Selectati...");
                        foreach (DataRow row in ClasaDiagnostice)
                        {
                            clasaDiagnosticeDDL.Items.Add(row["NumeClasa"].ToString().Trim());
                        }
                        diagnosticDDL.Items.Add("Selectati clasa diagnostice...");

                        dataTB.Text = DateTime.Now.ToString("M/d/yyyy");
                        oraTB.Text = DateTime.Now.ToString("HH:mm");
                        try
                        {
                            numeMedicTB.Text = (string)Application["numeMedic"];
                            numePacientTB.Text = Session["numePacient"].ToString();
                            cnpTB.Text = Session["cnp"].ToString();
                            dataNasteriiTB.Text = Session["dataN"].ToString();
                            judetTB.Text = Session["judet"].ToString();
                            orasTB.Text = Session["oras"].ToString();
                            stradaTB.Text = Session["strada"].ToString();
                            numarTB.Text = Session["numar"].ToString();
                            blocTB.Text = Session["bloc"].ToString();
                            scaraTB.Text = Session["scara"].ToString();
                            nrApartamentTB.Text = Session["nrApartament"].ToString();
                        }
                        catch (NullReferenceException ex)
                        {
                            LabelPrincipalErr.ForeColor = Color.Red;
                            LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                        }
                    }
                    LabelClasaDiagnosticeErr.Text = "";
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch (Exception ex)
            {
                LabelClasaDiagnosticeErr.ForeColor = Color.Red;
                LabelClasaDiagnosticeErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void dinte18IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Treilea Molar - 18";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte17IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Molar - 17";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte16IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Molar - 16";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte15IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Premolar - 15";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte14IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Premolar - 14";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte13IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Canin - 13";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte12IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Lateral - 12";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte11IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Central - 11";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte21IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Central - 21";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte22IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Lateral - 22";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte23IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Canin - 23";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte24IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Premolar - 24";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte25IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Premolar - 25";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte26IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Molar - 26";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte27IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Molar - 27";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte28IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Treilea Molar - 28";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte48IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Treilea Molar - 48";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte47IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Molar - 47";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte46IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Molar - 46";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte45IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Premolar - 45";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte44IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Premolar - 44";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte43IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Canin - 43";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte42IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Lateral - 42";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte41IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Central - 41";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte31IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Central - 31";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte32IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Incisiv Lateral - 32";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte33IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Canin - 33";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte34IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Premolar - 34";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte35IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Premolar - 35";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte36IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Primul Molar - 36";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte37IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Doilea Molar - 37";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void dinte38IBtn_Click(object sender, ImageClickEventArgs e)
        {
            dinteTB.Text = "Al Treilea Molar - 38";
            LabelDinteErr.Text = "";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
        }

        protected void clasaInterventiiDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clasaInterventiiDDL.SelectedIndex >= 1)
                {
                    interventiaDDL.Items.Clear();
                    interventiaDDL.Items.Add("Selectati...");
                    using (con = new SqlConnection(conString))
                    {
                        con.Open();

                        string stmt = "";
                        if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Consultatie")
                        {
                            stmt = "select Id, Nume, Pret from CISimple";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Albire dentara si tratamente speciale")
                        {
                            stmt = "select Id, Nume, Pret from CIAlbireDentaraTratamenteSpeciale";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Protetica")
                        {
                            stmt = "select Id, Nume, Pret from CIProtetica";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Chirurgie")
                        {
                            stmt = "select Id, Nume, Pret from CIChirurgie";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Implant dentar")
                        {
                            stmt = "select Id, Nume, Pret from CIImplantDentar";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Bonturi protetice")
                        {
                            stmt = "select Id, Nume, Pret from CIBonturiProtetice";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Proteze speciale pe implant")
                        {
                            stmt = "select Id, Nume, Pret from CIProtezeSpecialeImplant";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Proteze")
                        {
                            stmt = "select Id, Nume, Pret from CIProteze";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Radiologie")
                        {
                            stmt = "select Id, Nume, Pret from CIRadiologie";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Ablatii/cimentari/amprente")
                        {
                            stmt = "select Id, Nume, Pret from CIAblatiiCimentariAmprente";
                        }
                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Catalog");
                        DataRow[] Catalog = ds.Tables["Catalog"].Select();
                        foreach (DataRow row in Catalog)
                        {
                            interventiaDDL.Items.Add(row["Nume"].ToString().Trim());
                        }
                        LabelClasaIErr.Text = "";
                        costInterventieTB.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.Text = "";

                        ds.Dispose();
                        da.Dispose();
                    }
                }
                else
                {
                    interventiaDDL.Items.Clear();
                    interventiaDDL.Items.Add("Selectati clasa interventii...");
                    LabelClasaIErr.Text = "";
                    costInterventieTB.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelClasaIErr.ForeColor = Color.Red;
                LabelClasaIErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void stergeDinteBtn_Click(object sender, EventArgs e)
        {
            dinteTB.Text = "";
        }

        protected void interventiaDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (interventiaDDL.SelectedIndex >= 1)
                {
                    costInterventieTB.Text = "";
                    using (con = new SqlConnection(conString))
                    {
                        con.Open();

                        string stmt = "";
                        string nume = interventiaDDL.SelectedValue.ToString().Trim();
                        if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Consultatie")
                        {
                            stmt = "select Pret from CISimple where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Albire dentara si tratamente speciale")
                        {
                            stmt = "select Pret from CIAlbireDentaraTratamenteSpeciale where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Protetica")
                        {
                            stmt = "select Pret from CIProtetica where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Chirurgie")
                        {
                            stmt = "select Pret from CIChirurgie where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Implant dentar")
                        {
                            stmt = "select Pret from CIImplantDentar where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Bonturi protetice")
                        {
                            stmt = "select Pret from CIBonturiProtetice where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Proteze speciale pe implant")
                        {
                            stmt = "select Pret from CIProtezeSpecialeImplant where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Proteze")
                        {
                            stmt = "select Pret from CIProteze where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Radiologie")
                        {
                            stmt = "select Pret from CIRadiologie where Nume='" + nume + "'";
                        }
                        else if (clasaInterventiiDDL.SelectedValue.ToString().Trim() == "Ablatii/cimentari/amprente")
                        {
                            stmt = "select Pret from CIAblatiiCimentariAmprente where Nume='" + nume + "'";
                        }
                        SqlCommand sc = new SqlCommand(stmt, con);
                        SqlDataReader dr = sc.ExecuteReader();
                        if (dr.Read())
                        {
                            costInterventieTB.Text = dr[0].ToString();
                        }
                        LabelInterventieErr.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.Text = "";

                        dr.Close();
                        sc.Dispose();
                    }
                }
                else
                {
                    costInterventieTB.Text = "";
                    LabelInterventieErr.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelInterventieErr.ForeColor = Color.Red;
                LabelInterventieErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void clasaDiagnosticeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clasaDiagnosticeDDL.SelectedIndex >= 1)//evitam selectati...
                {
                    diagnosticDDL.Items.Clear();
                    diagnosticDDL.Items.Add("Selectati...");

                    using (con = new SqlConnection(conString))
                    {
                        con.Open();

                        string stmt = "select Grupa from ClasaDiagnostice where NumeClasa='" + clasaDiagnosticeDDL.SelectedValue.ToString().Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        SqlDataReader dr = sc.ExecuteReader();
                        string grupa = "";
                        if (dr.Read())
                        {
                            grupa = dr["Grupa"].ToString().Trim();
                            LabelClasaDiagnosticeErr.Text = "";
                            LabelAdaugaDiagnosticErr.Text = "";
                            LabelAdaugaInterventieErr.Text = "";
                        }
                        else
                        {
                            LabelClasaDiagnosticeErr.ForeColor = Color.Red;
                            LabelClasaDiagnosticeErr.Text = "Clasa diagnostice nu a fost gasita in baza de date.";
                        }
                        dr.Close();
                        sc.Dispose();

                        stmt = "select NumeDiagnostic from " + grupa + " order by CodDiagnostic";
                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, grupa);
                        DataRow[] numeDiagnostic = ds.Tables[grupa].Select();
                        foreach (DataRow row in numeDiagnostic)
                        {
                            diagnosticDDL.Items.Add(row["NumeDiagnostic"].ToString().Trim());
                        }
                        LabelClasaDiagnosticeErr.Text = "";
                        codDiagnosticTB.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.Text = "";

                        ds.Dispose();
                        da.Dispose();
                    }
                }
                else
                {
                    diagnosticDDL.Items.Clear();
                    diagnosticDDL.Items.Add("Selectati clasa diagnostice...");
                    LabelClasaDiagnosticeErr.Text = "";
                    codDiagnosticTB.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelClasaDiagnosticeErr.ForeColor = Color.Red;
                LabelClasaDiagnosticeErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void diagnosticDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (diagnosticDDL.SelectedIndex >= 1)
                {
                    codDiagnosticTB.Text = "";
                    using (con = new SqlConnection(conString))
                    {
                        con.Open();

                        string stmt = "select Grupa from ClasaDiagnostice where NumeClasa='" + clasaDiagnosticeDDL.SelectedValue.ToString().Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        SqlDataReader dr = sc.ExecuteReader();
                        string grupa = "";
                        if (dr.Read())
                        {
                            grupa = dr["Grupa"].ToString().Trim();
                            LabelDiagnosticErr.Text = "";
                            LabelAdaugaDiagnosticErr.Text = "";
                            LabelAdaugaInterventieErr.Text = "";
                        }
                        else
                        {
                            LabelDiagnosticErr.Text = "Clasa diagnostice nu a fost gasita in baza de date.";
                        }
                        dr.Close();
                        sc.Dispose();

                        stmt = "select CodDiagnostic from " + grupa + " where NumeDiagnostic='" + diagnosticDDL.SelectedValue.ToString().Trim() + "'";
                        sc = new SqlCommand(stmt, con);
                        dr = sc.ExecuteReader();
                        if (dr.Read())
                        {
                            codDiagnosticTB.Text = dr["CodDiagnostic"].ToString().Trim();
                        }
                        LabelDiagnosticErr.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.Text = "";
                        dr.Close();
                        sc.Dispose();
                    }
                }
                else
                {
                    codDiagnosticTB.Text = "";
                    LabelDiagnosticErr.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelDiagnosticErr.ForeColor = Color.Red;
                LabelDiagnosticErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void adaugaDiagnosticBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                if (dinteTB.Text != "")
                {
                    if (clasaDiagnosticeDDL.SelectedIndex >= 1)
                    {
                        if (diagnosticDDL.SelectedIndex >= 1)
                        {
                            diagnosticTB.Visible = true;
                            diagnosticTB.Text = diagnosticTB.Text + dinteTB.Text + "_" + diagnosticDDL.SelectedValue.ToString().Trim() + "//" + codDiagnosticTB.Text.Trim() + ";";

                            LabelClasaIErr.Text = "";
                            LabelInterventieErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelClasaDiagnosticeErr.Text = "";
                            LabelDinteErr.Text = "";
                            LabelDiagnosticErr.Text = "";
                            LabelDiagnosticBoxErr.Text = "";
                            LabelAdaugaDiagnosticErr.Text = "";
                            LabelAdaugaInterventieErr.Text = "";
                            LabelInterventieBoxErr.Text = "";
                            LabelRezulatAdaugare.Text = "";
                        }
                        else
                        {
                            LabelDiagnosticErr.ForeColor = Color.Red;
                            LabelDiagnosticErr.Text = "Nu ati selectat diagnosticul.";
                            LabelClasaIErr.Text = "";
                            LabelInterventieErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelClasaDiagnosticeErr.Text = "";
                            LabelDinteErr.Text = "";
                            LabelDiagnosticBoxErr.Text = "";
                            LabelAdaugaDiagnosticErr.ForeColor = Color.Red;
                            LabelAdaugaDiagnosticErr.Text = LabelDiagnosticErr.Text;
                            LabelAdaugaInterventieErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelClasaDiagnosticeErr.ForeColor = Color.Red;
                        LabelClasaDiagnosticeErr.Text = "Nu ati selectat clasa diagnostice.";
                        LabelDinteErr.Text = "";
                        LabelClasaIErr.Text = "";
                        LabelDiagnosticErr.Text = "";
                        LabelInterventieErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelDiagnosticBoxErr.Text = "";
                        LabelAdaugaDiagnosticErr.ForeColor = Color.Red;
                        LabelAdaugaDiagnosticErr.Text = LabelClasaDiagnosticeErr.Text;
                        LabelAdaugaInterventieErr.Text = "";
                    }
                }
                else
                {
                    LabelDinteErr.ForeColor = Color.Red;
                    LabelDinteErr.Text = "Nu ati selectat dintele.";
                    LabelClasaDiagnosticeErr.Text = "";
                    LabelClasaIErr.Text = "";
                    LabelDiagnosticErr.Text = "";
                    LabelInterventieErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelDiagnosticBoxErr.Text = "";
                    LabelAdaugaDiagnosticErr.ForeColor = Color.Red;
                    LabelAdaugaDiagnosticErr.Text = LabelDinteErr.Text;
                    LabelAdaugaInterventieErr.Text = "";
                }
            }
            else
            {
                LabelAdaugaDiagnosticErr.ForeColor = Color.Red;
                LabelAdaugaDiagnosticErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                LabelClasaIErr.Text = "";
                LabelInterventieErr.Text = "";
                LabelPrincipalErr.Text = "";
                LabelClasaDiagnosticeErr.Text = "";
                LabelDinteErr.Text = "";
                LabelDiagnosticErr.Text = "";
                LabelDiagnosticBoxErr.Text = "";
                LabelAdaugaInterventieErr.Text = "";
                LabelInterventieBoxErr.Text = "";
                LabelRezulatAdaugare.Text = "";
            }
        }

        protected void adaugaInterventieBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                if (dinteTB.Text != "")
                {
                    if (diagnosticTB.Text != "")
                    {
                        if (clasaInterventiiDDL.SelectedIndex >= 1)
                        {
                            if (interventiaDDL.SelectedIndex >= 1)
                            {
                                interventieTB.Visible = true;
                                interventieTB.Text = interventieTB.Text + dinteTB.Text + "_" + interventiaDDL.SelectedValue.ToString().Trim() + "//" + costInterventieTB.Text + ".LEI;";
                                if (costTotalTB.Text != "")
                                {
                                    costTotalTB.Text = (Int32.Parse(costTotalTB.Text.Trim()) + Int32.Parse(costInterventieTB.Text.Trim())).ToString();
                                }
                                else
                                {
                                    costTotalTB.Text = costInterventieTB.Text;
                                }

                                LabelDinteErr.Text = "";
                                LabelClasaDiagnosticeErr.Text = "";
                                LabelClasaIErr.Text = "";
                                LabelDiagnosticErr.Text = "";
                                LabelInterventieErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                LabelDiagnosticBoxErr.Text = "";
                                LabelAdaugaDiagnosticErr.Text = "";
                                LabelAdaugaInterventieErr.Text = "";
                                LabelInterventieBoxErr.Text = "";
                                LabelRezulatAdaugare.Text = "";
                            }
                            else
                            {
                                LabelDinteErr.Text = "";
                                LabelClasaDiagnosticeErr.Text = "";
                                LabelClasaIErr.Text = "";
                                LabelDiagnosticErr.Text = "";
                                LabelInterventieErr.ForeColor = Color.Red;
                                LabelInterventieErr.Text = "Nu ati selectat interventia.";
                                LabelPrincipalErr.Text = "";
                                LabelDiagnosticBoxErr.Text = "";
                                LabelAdaugaDiagnosticErr.Text = "";
                                LabelAdaugaInterventieErr.ForeColor = Color.Red;
                                LabelAdaugaInterventieErr.Text = LabelInterventieErr.Text;
                            }
                        }
                        else
                        {
                            LabelDinteErr.Text = "";
                            LabelClasaDiagnosticeErr.Text = "";
                            LabelClasaIErr.ForeColor = Color.Red;
                            LabelClasaIErr.Text = "Nu ati selectat clasa interventii.";
                            LabelDiagnosticErr.Text = "";
                            LabelInterventieErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelDiagnosticBoxErr.Text = "";
                            LabelAdaugaDiagnosticErr.Text = "";
                            LabelAdaugaInterventieErr.ForeColor = Color.Red;
                            LabelAdaugaInterventieErr.Text = LabelClasaIErr.Text;
                        }
                    }
                    else
                    {
                        LabelDiagnosticBoxErr.ForeColor = Color.Red;
                        LabelDiagnosticBoxErr.Text = "Nu ati adaugat nici un diagnostic.";
                        LabelDinteErr.Text = "";
                        LabelClasaDiagnosticeErr.Text = "";
                        LabelClasaIErr.Text = "";
                        LabelDiagnosticErr.Text = "";
                        LabelInterventieErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.ForeColor = Color.Red;
                        LabelAdaugaInterventieErr.Text = LabelDiagnosticBoxErr.Text;
                    }
                }
                else
                {
                    LabelDinteErr.ForeColor = Color.Red;
                    LabelDinteErr.Text = "Nu ati selectat dintele.";
                    LabelClasaDiagnosticeErr.Text = "";
                    LabelClasaIErr.Text = "";
                    LabelDiagnosticErr.Text = "";
                    LabelInterventieErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelDiagnosticBoxErr.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.ForeColor = Color.Red;
                    LabelAdaugaInterventieErr.Text = LabelDinteErr.Text;
                }
            }
            else
            {
                LabelDinteErr.Text = "";
                LabelClasaDiagnosticeErr.Text = "";
                LabelClasaIErr.Text = "";
                LabelDiagnosticErr.Text = "";
                LabelInterventieErr.Text = "";
                LabelPrincipalErr.Text = "";
                LabelDiagnosticBoxErr.Text = "";
                LabelAdaugaDiagnosticErr.Text = "";
                LabelAdaugaInterventieErr.ForeColor = Color.Red;
                LabelAdaugaInterventieErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void adaugareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (diagnosticTB.Text != "")
                {
                    if (interventieTB.Text != "")
                    {
                        if (numeMedicTB.Text != "")
                        {
                            con = new SqlConnection(conString);
                            con.Open();

                            DateTime dataTest = Convert.ToDateTime(dataTB.Text.Trim());
                            string stmt = "select IdConsultatie from Consultatii where CNP='" + cnpTB.Text.Trim() + "' and Data='" + dataTest.ToString("M/d/yyyy") + "' and Diagnostic='" + diagnosticTB.Text.Trim() + "' and Interventii='" + interventieTB.Text.Trim() + "' and PlanTObs='" + planTratamentObservatiiTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (dr.HasRows)
                            {
                                LabelRezulatAdaugare.ForeColor = Color.Red;
                                LabelRezulatAdaugare.Text = "Consultatia a fost adaugata. Nu se poate adauga aceasi consultatie de mai multe ori.";
                                LabelPrincipalErr.ForeColor = Color.Red;
                                LabelPrincipalErr.Text = "Consultatia a fost adaugata. Nu se poate adauga aceasi consultatie de mai multe ori.";
                                LabelAdaugaDiagnosticErr.Text = "";
                                LabelAdaugaInterventieErr.Text = "";
                                LabelClasaDiagnosticeErr.Text = "";
                                LabelClasaIErr.Text = "";
                                LabelDiagnosticBoxErr.Text = "";
                                LabelDiagnosticErr.Text = "";
                                LabelDinteErr.Text = "";
                                LabelInterventieBoxErr.Text = "";
                                LabelInterventieErr.Text = "";
                                LabelMedicErr.Text = "";
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();
                                stmt = "insert into Consultatii ([CNP], [NumePacient], [DataNasterii], [Judet], [Oras], [Strada], [Numar], [Bloc], [Scara], [NrApartament], [Data], [Ora], [Diagnostic], [Interventii], [PlanTObs], [Medic], [CostTotal]) values (@cnp,@nP,@dN,@j,@o,@str,@n,@b,@s,@nA,@d,@ora,@di,@i,@pTO,@m,@cT)";
                                sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@cnp", cnpTB.Text.Trim());
                                sc.Parameters.AddWithValue("@nP", numePacientTB.Text.Trim());
                                DateTime dataN = Convert.ToDateTime(dataNasteriiTB.Text.Trim());
                                sc.Parameters.AddWithValue("@dN", dataN.ToString("MM/dd/yyyy"));
                                sc.Parameters.AddWithValue("@j", judetTB.Text.Trim());
                                sc.Parameters.AddWithValue("@o", orasTB.Text.Trim());
                                sc.Parameters.AddWithValue("@str", stradaTB.Text.Trim());
                                sc.Parameters.AddWithValue("@n", numarTB.Text.Trim());
                                sc.Parameters.AddWithValue("@b", blocTB.Text.Trim());
                                sc.Parameters.AddWithValue("@s", scaraTB.Text.Trim());
                                sc.Parameters.AddWithValue("@nA", nrApartamentTB.Text.Trim());
                                DateTime data = Convert.ToDateTime(dataTB.Text.Trim());
                                sc.Parameters.AddWithValue("@d", data.ToString("MM/dd/yyyy"));
                                string ora = oraTB.Text.Trim();
                                ora = ora + ":00";
                                sc.Parameters.AddWithValue("@ora", ora);
                                sc.Parameters.AddWithValue("@di", diagnosticTB.Text.Trim());
                                sc.Parameters.AddWithValue("@i", interventieTB.Text.Trim());
                                sc.Parameters.AddWithValue("@pTO", planTratamentObservatiiTB.Text.Trim());
                                sc.Parameters.AddWithValue("@m", numeMedicTB.Text.Trim());
                                sc.Parameters.AddWithValue("@cT", costTotalTB.Text.Trim());
                                sc.ExecuteNonQuery();

                                LabelRezulatAdaugare.ForeColor = Color.Green;
                                LabelRezulatAdaugare.Text = "Consultatia a fost adaugata cu succes. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                LabelPrincipalErr.ForeColor = Color.Green;
                                LabelPrincipalErr.Text = "Consultatia a fost adaugata cu succes. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                LabelAdaugaDiagnosticErr.Text = "";
                                LabelAdaugaInterventieErr.Text = "";
                                LabelClasaDiagnosticeErr.Text = "";
                                LabelClasaIErr.Text = "";
                                LabelDiagnosticBoxErr.Text = "";
                                LabelDiagnosticErr.Text = "";
                                LabelDinteErr.Text = "";
                                LabelInterventieBoxErr.Text = "";
                                LabelInterventieErr.Text = "";
                                LabelMedicErr.Text = "";

                                sc.Dispose();
                            }
                            dr.Close();
                            sc.Dispose();

                            //cod pentru TB-PRINT
                            string diagnostic = diagnosticTB.Text.Trim();
                            diagnostic = diagnostic.Replace(";", "\n");
                            diagnostic = diagnostic.Replace("_", "\nDiagnostic: ");
                            diagnostic = diagnostic.Replace("//", "\n- Cod diagnostic: ");
                            diagnosticPrintTB.Text = diagnostic;
                            string interventie = interventieTB.Text.Trim();
                            interventie = interventie.Replace(";", "\n");
                            interventie = interventie.Replace("_", "\nInterventie: ");
                            interventie = interventie.Replace("//", " - ");
                            interventie = interventie.Replace(".", " ");
                            interventiePrintTB.Text = interventie;
                            planTratamentObservatiiPrintTB.Text = planTratamentObservatiiTB.Text.Trim();

                            if (printPreviewCB.Checked == true)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "window.print()", true);
                            }
                        }
                        else
                        {
                            LabelMedicErr.ForeColor = Color.Red;
                            LabelMedicErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                            LabelRezulatAdaugare.Text = "";
                            LabelInterventieBoxErr.Text = "";
                            LabelDiagnosticBoxErr.Text = "";
                            LabelPrincipalErr.Text = "";
                            LabelAdaugaDiagnosticErr.Text = "";
                            LabelAdaugaInterventieErr.Text = "";
                            LabelClasaDiagnosticeErr.Text = "";
                            LabelClasaIErr.Text = "";
                            LabelDiagnosticErr.Text = "";
                            LabelDinteErr.Text = "";
                            LabelInterventieErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelRezulatAdaugare.ForeColor = Color.Red;
                        LabelRezulatAdaugare.Text = "Nu ati adaugat nici o interventie.";
                        LabelInterventieBoxErr.ForeColor = Color.Red;
                        LabelInterventieBoxErr.Text = "Nu ati adaugat nici o interventie.";
                        LabelDiagnosticBoxErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelAdaugaDiagnosticErr.Text = "";
                        LabelAdaugaInterventieErr.Text = "";
                        LabelClasaDiagnosticeErr.Text = "";
                        LabelClasaIErr.Text = "";
                        LabelDiagnosticErr.Text = "";
                        LabelDinteErr.Text = "";
                        LabelInterventieErr.Text = "";
                        LabelMedicErr.Text = "";
                    }
                }
                else
                {
                    LabelRezulatAdaugare.ForeColor = Color.Red;
                    LabelRezulatAdaugare.Text = "Nu ati adaugat nici un diagnostic.";
                    LabelDiagnosticBoxErr.ForeColor = Color.Red;
                    LabelDiagnosticBoxErr.Text = "Nu ati adaugat nici un diagnositc.";
                    LabelPrincipalErr.Text = "";
                    LabelAdaugaDiagnosticErr.Text = "";
                    LabelAdaugaInterventieErr.Text = "";
                    LabelClasaDiagnosticeErr.Text = "";
                    LabelClasaIErr.Text = "";
                    LabelDiagnosticErr.Text = "";
                    LabelDinteErr.Text = "";
                    LabelInterventieBoxErr.Text = "";
                    LabelInterventieErr.Text = "";
                    LabelMedicErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelRezulatAdaugare.ForeColor = Color.Red;
                LabelRezulatAdaugare.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelAdaugaDiagnosticErr.Text = "";
                LabelAdaugaInterventieErr.Text = "";
                LabelClasaDiagnosticeErr.Text = "";
                LabelClasaIErr.Text = "";
                LabelDiagnosticBoxErr.Text = "";
                LabelDiagnosticErr.Text = "";
                LabelDinteErr.Text = "";
                LabelInterventieBoxErr.Text = "";
                LabelInterventieErr.Text = "";
                LabelMedicErr.Text = "";
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
            dinteTB.Text = "";
            diagnosticTB.Text = "";
            diagnosticTB.Visible = false;
            clasaDiagnosticeDDL.SelectedIndex = 0;
            diagnosticDDL.SelectedIndex = 0;
            codDiagnosticTB.Text = "";
            interventieTB.Text = "";
            interventieTB.Visible = false;
            clasaInterventiiDDL.SelectedIndex = 0;
            interventiaDDL.SelectedIndex = 0;
            costInterventieTB.Text = "";
            planTratamentObservatiiTB.Text = "";
            costTotalTB.Text = "";
            LabelRezulatAdaugare.ForeColor = Color.Green;
            LabelRezulatAdaugare.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelAdaugaDiagnosticErr.Text = "";
            LabelAdaugaInterventieErr.Text = "";
            LabelClasaDiagnosticeErr.Text = "";
            LabelClasaIErr.Text = "";
            LabelDiagnosticBoxErr.Text = "";
            LabelDiagnosticErr.Text = "";
            LabelDinteErr.Text = "";
            LabelInterventieBoxErr.Text = "";
            LabelInterventieErr.Text = "";
            LabelMedicErr.Text = "";
            printPreviewCB.Checked = false;
        }

        protected void diagnosticPrintTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = diagnosticPrintTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void interventiePrintTB_TextChanged(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = interventiePrintTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void planTratamentObservatiiPrintTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = planTratamentObservatiiPrintTB.Text;
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