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
    public partial class Programari : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string numeM = "";

        protected void ProgramariAziSauDataMnuala(DateTime data)
        {
            string stmt = "select Ora, Nume, Prenume, Telefon, MotivProgramare as [Motivul programari], NumeMedic as [Medic] from Programari where NumeMedic='" + numeM + "' and Data='" + data.Date + "' and StatusProgramare='Confirmata' order by Ora";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            if (ds.Tables["Programari"].Rows.Count == 0)
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelRezultatAfisareProgramari.ForeColor = Color.Green;
                LabelRezultatAfisareProgramari.Text = numeM + ", pentru data de " + data.Date.ToString("M/d/yyyy") + " nu aveti nici o programare.";
            }
            else
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelRezultatAfisareProgramari.ForeColor = Color.Green;
                LabelRezultatAfisareProgramari.Text = numeM + ", pentru data de " + data.Date.ToString("M/d/yyyy") + " aveti urmatoarele programari.";
            }
            LabelPrincipalErr.Text = "";
            ds.Dispose();
            da.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            numeM = (string)Application["numeMedic"];
            dataSelectataTB.Attributes.Add("readonly", "readonly");
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (numeM != "" && !string.IsNullOrEmpty(numeM))
                {
                    if (!IsPostBack)
                    {
                        ProgramariAziSauDataMnuala(DateTime.Today.Date);
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Medicul nu este logat.";
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count == 6)
                {
                    DateTime ora = Convert.ToDateTime(e.Row.Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = ora.ToString("HH:mm");
                }
                else
                {
                    DateTime ora = Convert.ToDateTime(e.Row.Cells[1].Text.ToString().Trim());
                    e.Row.Cells[1].Text = ora.ToString("HH:mm");
                    DateTime data = Convert.ToDateTime(e.Row.Cells[0].Text.ToString().Trim());
                    e.Row.Cells[0].Text = data.ToString("M/d/yyyy");
                }
            }
        }

        protected void programariAziBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                try
                {
                    con = new SqlConnection(conString);
                    con.Open();

                    ProgramariAziSauDataMnuala(DateTime.Today.Date);
                }
                catch (Exception ex)
                {
                    LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                    LabelRezultatAfisareProgramari.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                LabelRezultatAfisareProgramari.Text = "Medicul nu este logat.";
            }
        }

        protected void dataSelectataTB_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                try
                {
                    con = new SqlConnection(conString);
                    con.Open();

                    DateTime data = Convert.ToDateTime(dataSelectataTB.Text.Trim());
                    ProgramariAziSauDataMnuala(data.Date);
                }
                catch (Exception ex)
                {
                    LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                    LabelRezultatAfisareProgramari.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                LabelRezultatAfisareProgramari.Text = "Medicul nu este logat.";
            }
        }

        protected void programariSaptamanaCurentaBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                try
                {
                    con = new SqlConnection(conString);
                    con.Open();

                    DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
                    DateTime endOfWeek = startOfWeek.AddDays(6);
                    string stmt = "select convert(varchar(20),Data,101) as [Data], Ora, Nume, Prenume, Telefon, MotivProgramare as [Motivul programari], NumeMedic as [Medic] from Programari where NumeMedic='" + numeM + "' and Data>='" + DateTime.Today.Date + "' and Data<'" + endOfWeek.Date + "' and StatusProgramare='Confirmata' order by Data, Ora";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Programari");
                    if (ds.Tables["Programari"].Rows.Count == 0)
                    {
                        GridView1.DataSource = ds.Tables["Programari"];
                        GridView1.DataBind();
                        LabelRezultatAfisareProgramari.ForeColor = Color.Green;
                        LabelRezultatAfisareProgramari.Text = numeM + ", pentru saptamana curenta (intervalul " + DateTime.Today.ToString("M/d/yyyy") + " - " + endOfWeek.ToString("M/d/yyyy") + ") nu aveti nici o programare.";
                    }
                    else
                    {
                        GridView1.DataSource = ds.Tables["Programari"];
                        GridView1.DataBind();
                        LabelRezultatAfisareProgramari.ForeColor = Color.Green;
                        LabelRezultatAfisareProgramari.Text = numeM + ", pentru saptamana curenta (intervalul " + DateTime.Today.ToString("M/d/yyyy") + " - " + endOfWeek.ToString("M/d/yyyy") + ") aveti urmatoarele programari.";
                    }
                    LabelPrincipalErr.Text = "";

                    ds.Dispose();
                    da.Dispose();
                }
                catch (Exception ex)
                {
                    LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                    LabelRezultatAfisareProgramari.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                LabelRezultatAfisareProgramari.ForeColor = Color.Red;
                LabelRezultatAfisareProgramari.Text = "Medicul nu este logat.";
            }
        }
    }
}