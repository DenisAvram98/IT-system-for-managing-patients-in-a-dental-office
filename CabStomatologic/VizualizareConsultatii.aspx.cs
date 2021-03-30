using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class VizualizareConsultatii : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    try
                    {
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
                        LabelPrincipalErr.Text = "";
                    }
                    catch (NullReferenceException ex)
                    {
                        LabelPrincipalErr.ForeColor = Color.Red;
                        LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                    }

                    if (numePacientTB.Text!="" && cnpTB.Text!="" && !string.IsNullOrEmpty(numeM))
                    {
                        numarConsultatieDDL.Items.Clear();
                        numarConsultatieDDL.Items.Add("Selectati...");
                        dataConsultatieiDDL.Items.Clear();
                        dataConsultatieiDDL.Items.Add("Alegeti data.");

                        string stmt = "select IdConsultatie as [Numar consultatie], convert(varchar(20),Data,101) as [Data], Ora from Consultatii where CNP='" + cnpTB.Text.Trim() + "' and Medic='" + numeM + "' order by IdConsultatie DESC";
                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Consultatii");
                        if (ds.Tables["Consultatii"].Rows.Count==0)
                        {
                            LabelNrConsultatieErr.ForeColor = Color.Green;
                            LabelNrConsultatieErr.Text = "Pacientul " + numePacientTB.Text.Trim() + " nu are nici o consultatie la dumneavoastra.";
                        }
                        else
                        {
                            DataRow[] consultatii = ds.Tables["Consultatii"].Select();
                            foreach(DataRow row in consultatii)
                            {
                                numarConsultatieDDL.Items.Add(row["Numar consultatie"].ToString().Trim());
                                DateTime dataC = Convert.ToDateTime(row["Data"].ToString().Trim());
                                dataConsultatieiDDL.Items.Add(new ListItem(dataC.ToString("M/d/yyyy"), row["Numar consultatie"].ToString().Trim())); //folosim asa pentru fiecare data sa adaugam is ID de la consultatie
                            }
                        }
                        LabelPrincipalErr.Text = "";

                        ds.Dispose();
                        da.Dispose();
                    }
                    else
                    {
                        LabelNrConsultatieErr.ForeColor = Color.Red;
                        LabelNrConsultatieErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                    }
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

        protected void CautareConsultatie (string id)
        {
            using (con = new SqlConnection(conString))
            {
                con.Open();

                string stmt = "select Data, Ora, Diagnostic, Interventii, PlanTObs, Medic, CostTotal from Consultatii where IdConsultatie='" + id + "'";
                SqlCommand sc = new SqlCommand(stmt, con);
                SqlDataReader dr = sc.ExecuteReader();
                if (dr.Read())
                {
                    DateTime data = Convert.ToDateTime(dr["Data"].ToString().Trim());
                    dataConsultatieTB.Text = data.ToString("M/d/yyyy");
                    DateTime ora = Convert.ToDateTime(dr["Ora"].ToString().Trim());
                    oraConsultatieTB.Text = ora.ToString("HH:mm");
                    string diagnostic = dr["Diagnostic"].ToString().Trim();
                    diagnostic = diagnostic.Replace(";", "\n");
                    diagnostic = diagnostic.Replace("_", "\nDiagnostic: ");
                    diagnostic = diagnostic.Replace("//", "\n- Cod diagnostic: ");
                    diagosticTB.Text = diagnostic;
                    string interventie = dr["Interventii"].ToString().Trim();
                    interventie = interventie.Replace(";", "\n");
                    interventie = interventie.Replace("_", "\nInterventie: ");
                    interventie = interventie.Replace("//", " - ");
                    interventie = interventie.Replace(".", " ");
                    interventieTB.Text = interventie;
                    planTratamentObsTB.Text = dr["PlanTObs"].ToString().Trim();
                    costTotalTB.Text = dr["CostTotal"].ToString().Trim();
                    numeMedicTB.Text = dr["Medic"].ToString().Trim();
                }
                LabelNrConsultatieErr.Text = "";

                dr.Close();
                sc.Dispose();
            }
        }

        protected void numarConsultatieDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (numarConsultatieDDL.SelectedIndex>=1)
                {
                    CautareConsultatie(numarConsultatieDDL.SelectedValue.ToString().Trim());
                    dataConsultatieiDDL.SelectedIndex = 0;
                    LabelRezultatPrintareErr.Text = "";
                }
                else
                {
                    LabelConsultatieErr.Text = "";
                    dataConsultatieTB.Text = "";
                    oraConsultatieTB.Text = "";
                    diagosticTB.Text = "";
                    interventieTB.Text = "";
                    planTratamentObsTB.Text = "";
                    costTotalTB.Text = "";
                    numeMedicTB.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelConsultatieErr.ForeColor = Color.Red;
                LabelConsultatieErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void interventieTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = interventieTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void diagnosticTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = diagosticTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void planTratamentObsTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = planTratamentObsTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void dataConsultatieiDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataConsultatieiDDL.SelectedIndex >= 1)
                {
                    CautareConsultatie(dataConsultatieiDDL.SelectedValue.ToString().Trim());
                    numarConsultatieDDL.SelectedIndex = 0;
                    LabelRezultatPrintareErr.Text = "";
                }
                else
                {
                    LabelConsultatieErr.Text = "";
                    dataConsultatieTB.Text = "";
                    oraConsultatieTB.Text = "";
                    diagosticTB.Text = "";
                    interventieTB.Text = "";
                    planTratamentObsTB.Text = "";
                    costTotalTB.Text = "";
                    numeMedicTB.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelConsultatieErr.ForeColor = Color.Red;
                LabelConsultatieErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void printareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(numeM))
                {
                    if (numarConsultatieDDL.SelectedIndex >= 1 || dataConsultatieiDDL.SelectedIndex >= 1)
                    {
                        LabelRezultatPrintareErr.ForeColor = Color.Green;
                        LabelRezultatPrintareErr.Text = "Consultatia a fost printata. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                        LabelPrincipalErr.ForeColor = Color.Green;
                        LabelPrincipalErr.Text = "Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                        LabelConsultatieErr.Text = "";
                        LabelNrConsultatieErr.Text = "";
                        ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "window.print()", true);
                    }
                    else
                    {
                        LabelRezultatPrintareErr.ForeColor = Color.Red;
                        LabelRezultatPrintareErr.Text = "Nu ati ales consultatia.";
                    }
                }else
                {
                    LabelRezultatPrintareErr.ForeColor = Color.Red;
                    LabelRezultatPrintareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch (Exception ex)
            {
                LabelRezultatPrintareErr.ForeColor = Color.Red;
                LabelRezultatPrintareErr.Text = "Eroare: " + ex.Message;
            }
        }

        /*
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                DateTime data = Convert.ToDateTime(e.Row.Cells[1].Text.ToString().Trim());
                DateTime ora = Convert.ToDateTime(e.Row.Cells[2].Text.ToString().Trim());
                e.Row.Cells[1].Text = data.ToString("M/d/yyyy");
                e.Row.Cells[2].Text = ora.ToString("HH:mm");
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                    row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    // Set the last parameter to True 
                    // to register for event validation. 
                    row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + row.DataItemIndex, true); //command name Select
                    row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                }
            }
            base.Render(writer);
        }
        */
    }
}