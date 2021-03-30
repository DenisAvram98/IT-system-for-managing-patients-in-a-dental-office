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
    public partial class PaginaPrincipalaMedic : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string medicLogat;
        string numeM = "";


        protected void IncarcaGridView()
        {
            string stmt = "select CNP as [CNP], Nume, Prenume, Sex, NumeMama as [Nume mama], NumeTata as [Nume tata], convert(varchar(20),DataNasterii,101) as [Data nasterii], LocNastere as [Locul nasteri], JudetDomiciliu as [Judet domiciliu], OrasDomiciliu as [Oras domiciliu], StradaDomiciliu as [Strada], NumarDomiciliu as [Numar], BlocDomiciliu as [Bloc], ScaraDomiciliu as [Scara], NrApartamentDomiciliu as [Numar apartament], Telefon, Email, MedicFamilie as [Medic familie], Observatii from Pacienti where Prezent='Da' order by nume, prenume";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Pacienti");
            if (ds.Tables["Pacienti"].Rows.Count==0)
            {
                GridView1.DataSource = ds.Tables["Pacienti"];
                GridView1.DataBind();
                Label2.ForeColor = Color.Green;
                Label2.Text = "Nici un pacient nu este prezent.";
            }
            else
            {
                GridView1.DataSource = ds.Tables["Pacienti"];
                GridView1.DataBind();
                Label2.Text = "";
            }

            ds.Dispose();
            da.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["numeMedic"] as string))
                {
                    medicLogat = Request.QueryString["numeMedic"];
                    Label1.Text = "Buna, " + (string)Application["numeMedic"];
                    Label2.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Application["numeMedic"] as string))
                    {
                        Label1.Text = "Buna, " + (string)Application["numeMedic"];
                        Label2.Text = "";
                    }
                    else
                    {
                        throw new NullReferenceException("Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.");
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = ex.Message;
            }
            numeM = (string)Application["numeMedic"];

            if (!IsPostBack)
            {
                LabelButoaneActiveDezactive.Text = "Pentru ACTIVAREA butoanelor de mai jos selectati pacientul.";
            }

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(numeM))
                {
                    if (!IsPostBack)
                    {
                        IncarcaGridView();
                    }
                }
                else
                {
                    Label2.ForeColor = Color.Red;
                    Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch (Exception ex)
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) //ca tabelu sa nu foloseasca warp-text
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime data = Convert.ToDateTime(e.Row.Cells[6].Text.ToString().Trim());
                e.Row.Cells[6].Text = data.ToString("M/d/yyyy");
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

        //lasam o fi bun la ceva ca refresh
        protected void anulareCautareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(numeM))
                {
                    IncarcaGridView();
                    GridView1.SelectedIndex = -1;

                    adaugaConsultatieBtn.ForeColor = Color.Silver;
                    adaugaConsultatieBtn.Font.Bold = false;
                    adaugaConsultatieBtn.Enabled = false;

                    adaugaRadiografiiBtn.ForeColor = Color.Silver;
                    adaugaRadiografiiBtn.Font.Bold = false;
                    adaugaRadiografiiBtn.Enabled = false;

                    vizFisaPacientBtn.ForeColor = Color.Silver;
                    vizFisaPacientBtn.Font.Bold = false;
                    vizFisaPacientBtn.Enabled = false;

                    vizConsultatiPacientBtn.ForeColor = Color.Silver;
                    vizConsultatiPacientBtn.Font.Bold = false;
                    vizConsultatiPacientBtn.Enabled = false;

                    vizRadiografiiBtn.ForeColor = Color.Silver;
                    vizRadiografiiBtn.Font.Bold = false;
                    vizRadiografiiBtn.Enabled = false;

                    LabelButoaneActiveDezactive.Text = "Pentru ACTIVAREA butoanelor de mai jos selectati pacientul.";
                }
                else
                {
                    Label2.ForeColor = Color.Red;
                    Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch (Exception ex)
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                adaugaConsultatieBtn.ForeColor = Color.Black;
                adaugaConsultatieBtn.Font.Bold = true;
                adaugaConsultatieBtn.Enabled = true;

                adaugaRadiografiiBtn.ForeColor = Color.Black;
                adaugaRadiografiiBtn.Font.Bold = true;
                adaugaRadiografiiBtn.Enabled = true;

                vizFisaPacientBtn.ForeColor = Color.Black;
                vizFisaPacientBtn.Font.Bold = true;
                vizFisaPacientBtn.Enabled = true;

                vizConsultatiPacientBtn.ForeColor = Color.Black;
                vizConsultatiPacientBtn.Font.Bold = true;
                vizConsultatiPacientBtn.Enabled = true;

                vizRadiografiiBtn.ForeColor = Color.Black;
                vizRadiografiiBtn.Font.Bold = true;
                vizRadiografiiBtn.Enabled = true;

                LabelButoaneActiveDezactive.Text = "";
            }
        }

        protected void PreluareDatePentruConsultatie()
        {
            Session["numePacient"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[2].Text.ToString().Trim();
            Session["cnp"] = GridView1.SelectedRow.Cells[0].Text.ToString().Trim();
            DateTime dataN = Convert.ToDateTime(GridView1.SelectedRow.Cells[6].Text.ToString().Trim());
            Session["dataN"] = dataN.ToString("M/d/yyyy");
            Session["judet"] = GridView1.SelectedRow.Cells[8].Text.ToString().Trim();
            Session["oras"] = GridView1.SelectedRow.Cells[9].Text.ToString().Trim();
            Session["strada"] = GridView1.SelectedRow.Cells[10].Text.ToString().Trim();
            string numar = GridView1.SelectedRow.Cells[11].Text.ToString().Trim();
            if (numar == "&nbsp;")
            {
                numar = "";
            }
            string bloc = GridView1.SelectedRow.Cells[12].Text.ToString().Trim();
            if (bloc == "&nbsp;")
            {
                bloc = "";
            }
            string scara = GridView1.SelectedRow.Cells[13].Text.ToString().Trim();
            if (scara == "&nbsp;")
            {
                scara = "";
            }
            string nrApartament = GridView1.SelectedRow.Cells[14].Text.ToString().Trim();
            if (nrApartament == "&nbsp;")
            {
                nrApartament = "";
            }
            Session["numar"] = numar;
            Session["bloc"] = bloc;
            Session["scara"] = scara;
            Session["nrApartament"] = nrApartament;
        }

        protected void adaugaConsultatieBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                PreluareDatePentruConsultatie();
                Response.Redirect("ConsultatieNoua.aspx");
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void adaugaRadiografiiBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                Session["numePacient"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[2].Text.ToString().Trim();
                Session["cnp"] = GridView1.SelectedRow.Cells[0].Text.ToString().Trim();
                Response.Redirect("RadiografieNoua.aspx");
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void vizRadiografiiBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                Session["numePacient"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[2].Text.ToString().Trim();
                Session["cnp"] = GridView1.SelectedRow.Cells[0].Text.ToString().Trim();
                Response.Redirect("VizualizareRadiografii.aspx");
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void vizConsultatiPacientBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                PreluareDatePentruConsultatie();
                Response.Redirect("VizualizareConsultatii.aspx");
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void vizFisaPacientBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                PreluareDatePentruConsultatie();
                Session["sex"] = GridView1.SelectedRow.Cells[3].Text.ToString().Trim();
                Session["locNastere"] = GridView1.SelectedRow.Cells[7].Text.ToString().Trim();
                Session["telefon"] = GridView1.SelectedRow.Cells[15].Text.ToString().Trim();
                string numeM = GridView1.SelectedRow.Cells[4].Text.ToString().Trim();
                if (numeM == "&nbsp;")
                {
                    numeM = "";
                }
                Session["numeMama"] = numeM;
                string numeT = GridView1.SelectedRow.Cells[5].Text.ToString().Trim();
                if (numeT == "&nbsp;")
                {
                    numeT = "";
                }
                Session["numeTata"] = numeT;
                string email = GridView1.SelectedRow.Cells[16].Text.ToString().Trim();
                if (email == "&nbsp;")
                {
                    email = "";
                }
                Session["email"] = email;
                string medicFam = GridView1.SelectedRow.Cells[17].Text.ToString().Trim();
                if (medicFam == "&nbsp;")
                {
                    medicFam = "";
                }
                Session["medicFamilie"] = medicFam;
                string obs = GridView1.SelectedRow.Cells[18].Text.ToString().Trim();
                if (obs == "&nbsp;")
                {
                    obs = "";
                }
                Session["observatii"] = obs;
                Response.Redirect("VizualizareFisaPacientMedic.aspx");
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }
    }
}