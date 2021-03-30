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
    public partial class StatusProgramari : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string cnpPacientLogat = "";
        string numePacientLogat = "";

        protected void ToateProgramarile ()
        {
            string stmt = "select Data, Ora, Nume, Prenume, NumeMedic as [Medic], StatusProgramare as [Status programare] from Programari where CNP='" + cnpPacientLogat + "' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, Data, Ora";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            if (ds.Tables["Programari"].Rows.Count == 0)
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelStatusProgramariErr.ForeColor = Color.Green;
                LabelStatusProgramariErr.Text = "Nu aveti solicitata nici o programare.";
            }
            else
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelStatusProgramariErr.ForeColor = Color.Green;
                LabelStatusProgramariErr.Text = "Au fost gasite urmatoarele programari.";
            }
            LabelPrincipalErr.Text = "";
            ds.Dispose();
            da.Dispose();
        }

        protected void ProgramarileDupaStatus (string status)
        {
            string stmt = "select Data, Ora, Nume, Prenume, NumeMedic as [Medic], StatusProgramare as [Status programare] from Programari where CNP='" + cnpPacientLogat + "' and StatusProgramare='"+status+"' order by Data DESC, Ora";
            if (status == "Neconfirmata" || status == "Confirmata")
            {
                stmt = "select Data, Ora, Nume, Prenume, NumeMedic as [Medic], StatusProgramare as [Status programare] from Programari where CNP='" + cnpPacientLogat + "' and StatusProgramare='" + status + "' order by Data, Ora";
            }
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            if (ds.Tables["Programari"].Rows.Count == 0)
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                if (status == "Expirata")
                {
                    LabelStatusProgramariErr.ForeColor = Color.Green;
                    LabelStatusProgramariErr.Text = "Nu aveti nici o programare expirata.";
                }
                else if (status== "Neconfirmata")
                {
                    LabelStatusProgramariErr.ForeColor = Color.Green;
                    LabelStatusProgramariErr.Text = "Nu aveti nici o programare neconfirmata.";
                }
                else//confirmate
                {
                    LabelStatusProgramariErr.ForeColor = Color.Green;
                    LabelStatusProgramariErr.Text = "Nu aveti nici o programare confirmata.";
                }
            }
            else
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelStatusProgramariErr.ForeColor = Color.Green;
                LabelStatusProgramariErr.Text = "Au fost gasite urmatoarele programari.";
            }
            LabelPrincipalErr.Text = "";
            ds.Dispose();
            da.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cnpPacientLogat = (string)Application["cnpPacient"];
            numePacientLogat = (string)Application["numePacient"];

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    LabelIesirePacient.Text = "Iesire, " + numePacientLogat;
                    if (!IsPostBack)
                    {
                        ToateProgramarile();
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    LabelStatusProgramariErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelStatusProgramariErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime ora = Convert.ToDateTime(e.Row.Cells[1].Text.ToString().Trim());
                e.Row.Cells[1].Text = ora.ToString("HH:mm");
                DateTime data = Convert.ToDateTime(e.Row.Cells[0].Text.ToString().Trim());
                e.Row.Cells[0].Text = data.ToString("M/d/yyyy");
                if (e.Row.Cells[5].Text.ToString().Trim()=="Neconfirmata")
                {
                    e.Row.Cells[5].ForeColor = Color.Red;
                }
                else if (e.Row.Cells[5].Text.ToString().Trim() == "Expirata")
                {
                    e.Row.Cells[5].ForeColor = Color.Gray;
                }
                else //confrimata
                {
                    e.Row.Cells[5].ForeColor = Color.Green;
                }
            }
        }

        protected void toateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    ToateProgramarile();
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    LabelStatusProgramariErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelStatusProgramariErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void expirateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    ProgramarileDupaStatus("Expirata");
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    LabelStatusProgramariErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelStatusProgramariErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void neconfirmateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    ProgramarileDupaStatus("Neconfirmata");
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    LabelStatusProgramariErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelStatusProgramariErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void confirmateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(cnpPacientLogat))
                {
                    ProgramarileDupaStatus("Confirmata");
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Pacientul nu este logat.";
                    LabelStatusProgramariErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelStatusProgramariErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}