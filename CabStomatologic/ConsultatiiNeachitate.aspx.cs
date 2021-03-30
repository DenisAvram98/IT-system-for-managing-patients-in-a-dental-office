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
    public partial class ConsultatiiNeachitate : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string receptie = "";

        protected void IncarcareGridView() //functia care incarca consultatiile neachitate in GridView
        {
            string stmt = "select IdConsultatie as [Numar consultatie], CNP, NumePacient as [Nume pacient], convert(varchar(20),Data,101) as [Data], Ora, Interventii, Medic as [Nume medic], CostTotal as [Cost total] from Consultatii order by NumePacient, Data DESC";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Consultatii");
            DataRow[] consultatii = ds.Tables["Consultatii"].Select(); //toate consultatiile

            stmt = "select IdConsultatie, SumaTotalAchitata, CostTotal from Chitante where CostTotal=SumaTotalAchitata";
            SqlDataAdapter da2 = new SqlDataAdapter(stmt, con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "Chitante");
            DataRow[] achitat = ds2.Tables["Chitante"].Select(); //consultatile achitate

            DataTable table = ds.Tables["Consultatii"].Clone();
            string idConsAchitata = "";
            foreach (DataRow cons in consultatii)
            {
                foreach (DataRow achit in achitat)
                {
                    if (cons["Numar consultatie"].ToString().Trim() == achit["IdConsultatie"].ToString().Trim())
                    {
                        idConsAchitata = cons["Numar consultatie"].ToString().Trim(); //salvam consultatia achitata deja
                    }
                }
                if (cons["Numar consultatie"].ToString().Trim() != idConsAchitata) //daca consultatia nu este achitata o adaugam intrun tabel
                {
                    table.Rows.Add(cons.ItemArray);
                }
            }

            foreach (DataRow row in table.Rows) //cu asta am modificat cum sa arate coloana interventii
            {
                row.SetField<string>("Interventii",
                    row.Field<string>("Interventii").Replace(";", "\n").Replace("_", "\nInterventie: ").Replace("//", " - ").Replace(".", " "));
            }
            GridView1.DataSource = table;
            GridView1.DataBind();

            LabelPrincipalErr.Text = "";

            table.Dispose();
            ds2.Dispose();
            da2.Dispose();
            ds.Dispose();
            da.Dispose();
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
                        IncarcareGridView();
                        LabelButonActivDeactiv.Text = "Pentru ACTIVAREA butonului 'Tiparire chitanta' selectati consultatia.";
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) //ca in colana Interventi sa bage linie noua dupa fiecare interventie
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("\n", "</br>");
                DateTime data = Convert.ToDateTime(e.Row.Cells[3].Text.ToString().Trim());
                e.Row.Cells[3].Text = data.ToString("M/d/yyyy");
                DateTime ora = Convert.ToDateTime(e.Row.Cells[4].Text.ToString().Trim());
                e.Row.Cells[4].Text = ora.ToString("HH:mm");
            }
            /*
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }*/
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

        protected void cautaCnpBtn_Click(object sender, EventArgs e)
        {
            Regex testNum = new Regex("^[0-9]+$");
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (cnpCautatTB.Text.Trim() != "")
                    {
                        if (testNum.IsMatch(cnpCautatTB.Text.Trim()))
                        {
                            if (cnpCautatTB.Text.Trim().Length == 13)
                            {
                                string stmt = "select CNP from Pacienti where CNP='" + cnpCautatTB.Text.Trim() + "'";
                                SqlCommand sc = new SqlCommand(stmt, con);
                                SqlDataReader dr = sc.ExecuteReader();
                                if (!dr.Read())
                                {
                                    LabelCautaErr.ForeColor = Color.Red;
                                    LabelCautaErr.Text = "Pacientul cu CNP-ul " + cnpCautatTB.Text.Trim() + " nu a fost gasit.";
                                    dr.Close();
                                    sc.Dispose();
                                }
                                else
                                {
                                    dr.Close();
                                    sc.Dispose();

                                    stmt = "select IdConsultatie as [Numar consultatie], CNP, NumePacient as [Nume pacient], convert(varchar(20),Data,101) as [Data], Ora, Interventii, Medic as [Nume medic], CostTotal as [Cost total] from Consultatii where CNP='" + cnpCautatTB.Text.Trim() + "' order by NumePacient, Data DESC";
                                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "Consultatii");
                                    DataRow[] consultatii = ds.Tables["Consultatii"].Select(); //toate consultatiile

                                    if (ds.Tables["Consultatii"].Rows.Count == 0)
                                    {
                                        LabelCautaErr.ForeColor = Color.Green;
                                        LabelCautaErr.Text = "Pacientul cu CNP-ul " + cnpCautatTB.Text.Trim() + " nu are nici o consultatie.";
                                        //GridView1.DataSource = ds.Tables["Consultatii"];
                                        //GridView1.DataBind();
                                    }
                                    else
                                    {
                                        stmt = "select IdConsultatie, SumaTotalAchitata, CostTotal from Chitante where CostTotal=SumaTotalAchitata and CNP='" + cnpCautatTB.Text.Trim() + "'";
                                        SqlDataAdapter da2 = new SqlDataAdapter(stmt, con);
                                        DataSet ds2 = new DataSet();
                                        da2.Fill(ds2, "Chitante");
                                        DataRow[] achitat = ds2.Tables["Chitante"].Select(); //consultatile achitate

                                        DataTable table = ds.Tables["Consultatii"].Clone();
                                        string idConsAchitata = "";
                                        foreach (DataRow cons in consultatii)
                                        {
                                            foreach (DataRow achit in achitat)
                                            {
                                                if (cons["Numar consultatie"].ToString().Trim() == achit["IdConsultatie"].ToString().Trim())
                                                {
                                                    idConsAchitata = cons["Numar consultatie"].ToString().Trim(); //salvam consultatia achitata deja
                                                }
                                            }
                                            if (cons["Numar consultatie"].ToString().Trim() != idConsAchitata) //daca consultatia nu este achitata o adaugam intrun tabel
                                            {
                                                table.Rows.Add(cons.ItemArray);
                                            }
                                        }

                                        if (table.Rows.Count == 0)
                                        {
                                            LabelCautaErr.ForeColor = Color.Green;
                                            LabelCautaErr.Text = "Pacientul cu CNP-ul " + cnpCautatTB.Text.Trim() + " nu are nici o consultatie neachitata.";
                                            //GridView1.DataSource = table;
                                            //GridView1.DataBind();
                                        }
                                        else
                                        {
                                            foreach (DataRow row in table.Rows) //cu asta am modificat cum sa arate coloana interventii
                                            {
                                                row.SetField<string>("Interventii",
                                                    row.Field<string>("Interventii").Replace(";", "\n").Replace("_", "\nInterventie: ").Replace("//", " - ").Replace(".", " "));
                                            }
                                            GridView1.DataSource = table;
                                            GridView1.DataBind();
                                            GridView1.SelectedIndex = 0;

                                            LabelCautaErr.ForeColor = Color.Green;
                                            LabelCautaErr.Text = "Pentru pacientul cu CNP-ul " + cnpCautatTB.Text.Trim() + " au fost gasite urmatoarele consultatii neachitate.";
                                            LabelButonActivDeactiv.Text = "";
                                            tiparireChitantaBtn.Font.Bold = true;
                                            tiparireChitantaBtn.ForeColor = Color.Black;
                                            tiparireChitantaBtn.Enabled = true;
                                        }

                                        table.Dispose();
                                        ds2.Dispose();
                                        da2.Dispose();
                                    }
                                    ds.Dispose();
                                    da.Dispose();
                                }
                            }
                            else
                            {
                                LabelCautaErr.ForeColor = Color.Red;
                                LabelCautaErr.Text = "CNP-ul pacientului cautat trebuie sa contina 13 cifre.";
                            }
                        }
                        else
                        {
                            LabelCautaErr.ForeColor = Color.Red;
                            LabelCautaErr.Text = "CNP-ul pacientului cautat nu are voie sa contina litere.";
                        }
                    }
                    else
                    {
                        LabelCautaErr.ForeColor = Color.Red;
                        LabelCautaErr.Text = "Nu ati introdus CNP-ul pacientului cautat.";
                    }
                }
                else
                {
                    LabelCautaErr.ForeColor = Color.Red;
                    LabelCautaErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelCautaErr.ForeColor = Color.Red;
                LabelCautaErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareCautareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    IncarcareGridView();
                    GridView1.SelectedIndex = -1;
                }
                else
                {
                    LabelCautaErr.ForeColor = Color.Red;
                    LabelCautaErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }

                cnpCautatTB.Text = "";
                LabelCautaErr.Text = "";
                numeCautatTB.Text = "";

                LabelButonActivDeactiv.Text = "Pentru ACTIVAREA butonului 'Tiparire chitanta' selectati consultatia.";
                tiparireChitantaBtn.Font.Bold = false;
                tiparireChitantaBtn.ForeColor = Color.Silver;
                tiparireChitantaBtn.Enabled = false;
            }
            catch (Exception ex)
            {
                LabelCautaErr.ForeColor = Color.Red;
                LabelCautaErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
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
                tiparireChitantaBtn.Font.Bold = true;
                tiparireChitantaBtn.Enabled = true;
                tiparireChitantaBtn.ForeColor = Color.Black;
                LabelButonActivDeactiv.Text = "";
            }
        }

        protected void tiparireChitantaBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receptie))
            {
                LabelPrincipalErr.Text = "";
                Session["numePacient"] = GridView1.SelectedRow.Cells[2].Text.ToString().Trim();
                Session["cnp"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim();
                Session["numarConsultatie"] = GridView1.SelectedRow.Cells[0].Text.ToString().Trim();
                Response.Redirect("TiparireChitanta.aspx");
            }
            else
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
            }
        }

        Regex test = new Regex("^[a-zA-Z ]+$");
        protected void cautaNumeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numeCautatTB.Text.Trim() != "")
                    {
                        if (test.IsMatch(numeCautatTB.Text.Trim()))
                        {
                            string stmt = "select Nume from Pacienti where Nume='" + numeCautatTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (!dr.Read())
                            {
                                LabelCautaErr.ForeColor = Color.Red;
                                LabelCautaErr.Text = "Pacientul cu numele " + numeCautatTB.Text.Trim() + " nu a fost gasit.";
                                dr.Close();
                                sc.Dispose();
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();

                                stmt = "select IdConsultatie as [Numar consultatie], CNP, NumePacient as [Nume pacient], convert(varchar(20),Data,101) as [Data], Ora, Interventii, Medic as [Nume medic], CostTotal as [Cost total] from Consultatii where NumePacient like '"+numeCautatTB.Text.Trim()+"%' order by NumePacient, Data DESC";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Consultatii");
                                DataRow[] consultatii = ds.Tables["Consultatii"].Select(); //toate consultatiile

                                if (ds.Tables["Consultatii"].Rows.Count == 0)
                                {
                                    LabelCautaErr.ForeColor = Color.Green;
                                    LabelCautaErr.Text = "Pacientul cu numele " + numeCautatTB.Text.Trim() + " nu are nici o consultatie.";
                                    //GridView1.DataSource = ds.Tables["Consultatii"];
                                    //GridView1.DataBind();
                                }
                                else
                                {
                                    stmt = "select IdConsultatie, SumaTotalAchitata, CostTotal from Chitante where CostTotal=SumaTotalAchitata and NumePacient like '" + numeCautatTB.Text.Trim() + "%'";
                                    SqlDataAdapter da2 = new SqlDataAdapter(stmt, con);
                                    DataSet ds2 = new DataSet();
                                    da2.Fill(ds2, "Chitante");
                                    DataRow[] achitat = ds2.Tables["Chitante"].Select(); //consultatile achitate

                                    DataTable table = ds.Tables["Consultatii"].Clone();
                                    string idConsAchitata = "";
                                    foreach (DataRow cons in consultatii)
                                    {
                                        foreach (DataRow achit in achitat)
                                        {
                                            if (cons["Numar consultatie"].ToString().Trim() == achit["IdConsultatie"].ToString().Trim())
                                            {
                                                idConsAchitata = cons["Numar consultatie"].ToString().Trim(); //salvam consultatia achitata deja
                                            }
                                        }
                                        if (cons["Numar consultatie"].ToString().Trim() != idConsAchitata) //daca consultatia nu este achitata o adaugam intrun tabel
                                        {
                                            table.Rows.Add(cons.ItemArray);
                                        }
                                    }

                                    if (table.Rows.Count == 0)
                                    {
                                        LabelCautaErr.ForeColor = Color.Green;
                                        LabelCautaErr.Text = "Pacientul cu numele " + numeCautatTB.Text.Trim() + " nu are nici o consultatie neachitata.";
                                        //GridView1.DataSource = table;
                                        //GridView1.DataBind();
                                    }
                                    else
                                    {
                                        foreach (DataRow row in table.Rows) //cu asta am modificat cum sa arate coloana interventii
                                        {
                                            row.SetField<string>("Interventii",
                                                row.Field<string>("Interventii").Replace(";", "\n").Replace("_", "\nInterventie: ").Replace("//", " - ").Replace(".", " "));
                                        }
                                        GridView1.DataSource = table;
                                        GridView1.DataBind();

                                        LabelCautaErr.ForeColor = Color.Green;
                                        LabelCautaErr.Text = "Pentru pacientul cu numele " + numeCautatTB.Text.Trim() + " au fost gasite urmatoarele consultatii neachitate.";

                                        if (table.Rows.Count == 1)
                                        {
                                            GridView1.SelectedIndex = 0;
                                            LabelButonActivDeactiv.Text = "";
                                            tiparireChitantaBtn.Font.Bold = true;
                                            tiparireChitantaBtn.ForeColor = Color.Black;
                                            tiparireChitantaBtn.Enabled = true;
                                        }
                                    }

                                    table.Dispose();
                                    ds2.Dispose();
                                    da2.Dispose();
                                }
                                ds.Dispose();
                                da.Dispose();
                            }
                        }
                        else
                        {
                            LabelCautaErr.ForeColor = Color.Red;
                            LabelCautaErr.Text = "Numele pacientului cautat nu are voie sa contina altceva in afara de litere.";
                        }
                    }
                    else
                    {
                        LabelCautaErr.ForeColor = Color.Red;
                        LabelCautaErr.Text = "Nu ati introdus numele pacientului cautat.";
                    }
                }
                else
                {
                    LabelCautaErr.ForeColor = Color.Red;
                    LabelCautaErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelCautaErr.ForeColor = Color.Red;
                LabelCautaErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}