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
    public partial class PaginaPrincipalaReceptie : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string utilizatorR;
        string receptie = "";

        protected void IncarcaGridView()
        {
            string stmt = "select Prezent, CNP as [CNP], Nume, Prenume, Sex, NumeMama as [Nume mama], NumeTata as [Nume tata], convert(varchar(20),DataNasterii,101) as [Data nasterii], LocNastere as [Locul nasteri], JudetDomiciliu as [Judet domiciliu], OrasDomiciliu as [Oras domiciliu], StradaDomiciliu as [Strada], NumarDomiciliu as [Numar], BlocDomiciliu as [Bloc], ScaraDomiciliu as [Scara], NrApartamentDomiciliu as [Numar apartament], Telefon, Email, MedicFamilie as [Medic familie], Observatii from Pacienti order by Prezent DESC,nume, prenume";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Pacienti");
            GridView1.DataSource = ds.Tables["Pacienti"];
            GridView1.DataBind();
            LabelPrincipalErr.Text = "";

            ds.Dispose();
            da.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["utilizatorR"] as string))
                {
                    utilizatorR = Request.QueryString["utilizatorR"];
                    LabelPrincipalErr.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Application["utilizatorR"] as string))
                    {
                        receptie = (string)Application["utilizatorR"];
                        LabelPrincipalErr.Text = "";
                    }
                    else
                    {
                        throw new NullReferenceException("Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.");
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = ex.Message;
            }
            receptie = (string)Application["utilizatorR"];

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (!IsPostBack)
                    {
                        IncarcaGridView();
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) //ca tabelu sa nu foloseasca warp-text
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime data = Convert.ToDateTime(e.Row.Cells[7].Text.ToString().Trim());
                e.Row.Cells[7].Text = data.ToString("M/d/yyyy");
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                vizEditFisaPacientBtn.Font.Bold = true;
                vizEditFisaPacientBtn.ForeColor = Color.Black;
                vizEditFisaPacientBtn.Enabled = true;

                tiparireChitantaBtn.Font.Bold = true;
                tiparireChitantaBtn.ForeColor = Color.Black;
                tiparireChitantaBtn.Enabled = true;

                LabelActivare.Visible = false;
                LabelPrezent.Visible = true;
                prezentDDL.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string prezent = GridView1.Rows[index].Cells[0].Text.ToString().Trim();
                if (prezent == "&nbsp;")
                {
                    prezent = "";
                }
                prezentDDL.SelectedValue = prezent;
            }
        }

        protected void prezentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();

                    if (!string.IsNullOrEmpty(receptie))
                    {
                        string cnp = GridView1.SelectedRow.Cells[1].Text.ToString().Trim();
                        string stmt = "update Pacienti set Prezent=@p where CNP='" + GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        sc.Parameters.AddWithValue("@p", prezentDDL.SelectedValue.ToString().Trim());
                        sc.ExecuteNonQuery();
                        sc.Dispose();

                        LabelPrezentErr.Text = "";
                        IncarcaGridView();
                        int index = -1;
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            if (row.Cells[1].Text.ToString().Trim() == cnp)
                            {
                                index = row.RowIndex;
                                break;
                            }
                        }
                        GridView1.SelectedIndex = index;
                    }
                    else
                    {
                        LabelPrezentErr.ForeColor = Color.Red;
                        LabelPrezentErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    }
                }
            }
            catch (Exception ex)
            {
                LabelPrezentErr.ForeColor = Color.Red;
                LabelPrezentErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        Regex testNum = new Regex("^[0-9]+$");
        protected void cautaPacientCNPBtn_Click(object sender, EventArgs e)
        {
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
                                string stmt = "select Prezent, CNP as [CNP], Nume, Prenume, Sex, NumeMama as [Nume mama], NumeTata as [Nume tata], convert(varchar(20),DataNasterii,101) as [Data nasterii], LocNastere as [Locul nasteri], JudetDomiciliu as [Judet domiciliu], OrasDomiciliu as [Oras domiciliu], StradaDomiciliu as [Strada], NumarDomiciliu as [Numar], BlocDomiciliu as [Bloc], ScaraDomiciliu as [Scara], NrApartamentDomiciliu as [Numar apartament], Telefon, Email, MedicFamilie as [Medic familie], Observatii from Pacienti where CNP='" + cnpCautatTB.Text.Trim() + "' order by Prezent DESC,nume, prenume";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Pacienti");
                                if (ds.Tables["Pacienti"].Rows.Count == 0) //daca pacientul nu a fost gasit
                                {
                                    LabelCautareErr.ForeColor = Color.Red;
                                    LabelCautareErr.Text = "Pacientul cu CNP-ul " + cnpCautatTB.Text.Trim() + " nu a fost gasit.";
                                }
                                else
                                {
                                    GridView1.DataSource = ds.Tables["Pacienti"];
                                    GridView1.DataBind();
                                    GridView1.SelectedIndex = 0;
                                    LabelCautareErr.ForeColor = Color.Green;
                                    LabelCautareErr.Text = "Pacientul cautat a fost gasit.";

                                    vizEditFisaPacientBtn.Font.Bold = true;
                                    vizEditFisaPacientBtn.ForeColor = Color.Black;
                                    vizEditFisaPacientBtn.Enabled = true;

                                    tiparireChitantaBtn.Font.Bold = true;
                                    tiparireChitantaBtn.ForeColor = Color.Black;
                                    tiparireChitantaBtn.Enabled = true;

                                    LabelActivare.Visible = false;
                                    LabelPrezent.Visible = true;
                                    prezentDDL.Visible = true;
                                    string prezent = GridView1.Rows[0].Cells[0].Text.ToString().Trim();
                                    if (prezent == "&nbsp;")
                                    {
                                        prezent = "";
                                    }
                                    prezentDDL.SelectedValue = prezent;
                                }
                                LabelPrezentErr.Text = "";
                                LabelPrincipalErr.Text = "";

                                ds.Dispose();
                                da.Dispose();
                            }
                            else
                            {
                                LabelCautareErr.ForeColor = Color.Red;
                                LabelCautareErr.Text = "CNP-ul pacientului cautat trebuie sa contina 13 cifre.";
                                LabelPrezentErr.Text = "";
                                LabelPrincipalErr.Text = "";
                            }
                        }
                        else
                        {
                            LabelCautareErr.ForeColor = Color.Red;
                            LabelCautareErr.Text = "CNP-ul pacientului cautat nu are voie sa contina litere.";
                            LabelPrezentErr.Text = "";
                            LabelPrincipalErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelCautareErr.ForeColor = Color.Red;
                        LabelCautareErr.Text = "Nu ati introdus CNP-ul pacientului cautat.";
                        LabelPrezentErr.Text = "";
                        LabelPrincipalErr.Text = "";
                    }
                }
                else
                {
                    LabelCautareErr.ForeColor = Color.Red;
                    LabelCautareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelPrezentErr.Text = "";
                    LabelPrincipalErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelCautareErr.ForeColor = Color.Red;
                LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrezentErr.Text = "";
                LabelPrincipalErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareCautareBtn_Click(object sender, EventArgs e)
        {
            cnpCautatTB.Text = "";
            numeTB.Text = "";
            LabelCautareErr.Text = "";
            LabelActivare.Visible = true;
            LabelPrezent.Visible = false;
            prezentDDL.Visible = false;

            vizEditFisaPacientBtn.Font.Bold = false;
            vizEditFisaPacientBtn.ForeColor = Color.Silver;
            vizEditFisaPacientBtn.Enabled = false;

            tiparireChitantaBtn.Font.Bold = false;
            tiparireChitantaBtn.ForeColor = Color.Silver;
            tiparireChitantaBtn.Enabled = false;

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    IncarcaGridView();
                    GridView1.SelectedIndex = -1;
                }
                else
                {
                    LabelCautareErr.ForeColor = Color.Red;
                    LabelCautareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelPrezentErr.Text = "";
                    LabelPrincipalErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelCautareErr.ForeColor = Color.Red;
                LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrezentErr.Text = "";
                LabelPrincipalErr.Text = "";
            }
            finally
            {
                con.Close();
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
                    if (numeTB.Text.Trim() != "")
                    {
                        if (test.IsMatch(numeTB.Text.Trim()))
                        {
                            string stmt = "select Prezent, CNP as [CNP], Nume, Prenume, Sex, NumeMama as [Nume mama], NumeTata as [Nume tata], convert(varchar(20),DataNasterii,101) as [Data nasterii], LocNastere as [Locul nasteri], JudetDomiciliu as [Judet domiciliu], OrasDomiciliu as [Oras domiciliu], StradaDomiciliu as [Strada], NumarDomiciliu as [Numar], BlocDomiciliu as [Bloc], ScaraDomiciliu as [Scara], NrApartamentDomiciliu as [Numar apartament], Telefon, Email, MedicFamilie as [Medic familie], Observatii from Pacienti where Nume='" + numeTB.Text.Trim() + "' order by Prezent DESC,nume, prenume";
                            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "Pacienti");
                            if (ds.Tables["Pacienti"].Rows.Count == 0) //daca pacientul nu a fost gasit
                            {
                                LabelCautareErr.ForeColor = Color.Red;
                                LabelCautareErr.Text = "Pacientul cu numele " + numeTB.Text.Trim() + " nu a fost gasit.";
                            }
                            else
                            {
                                GridView1.DataSource = ds.Tables["Pacienti"];
                                GridView1.DataBind();
                                LabelCautareErr.ForeColor = Color.Green;
                                LabelCautareErr.Text = "Numele cautat a fost gasit.";

                                if (ds.Tables["Pacienti"].Rows.Count == 1)
                                {
                                    vizEditFisaPacientBtn.Font.Bold = true;
                                    vizEditFisaPacientBtn.ForeColor = Color.Black;
                                    vizEditFisaPacientBtn.Enabled = true;

                                    tiparireChitantaBtn.Font.Bold = true;
                                    tiparireChitantaBtn.ForeColor = Color.Black;
                                    tiparireChitantaBtn.Enabled = true;

                                    GridView1.SelectedIndex = 0;
                                    LabelActivare.Visible = false;
                                    LabelPrezent.Visible = true;
                                    prezentDDL.Visible = true;
                                    string prezent = GridView1.Rows[0].Cells[0].Text.ToString().Trim();
                                    if (prezent == "&nbsp;")
                                    {
                                        prezent = "";
                                    }
                                    prezentDDL.SelectedValue = prezent;
                                }
                                else
                                {
                                    LabelActivare.Visible = true;
                                    LabelPrezent.Visible = false;
                                    prezentDDL.Visible = false;

                                    vizEditFisaPacientBtn.Font.Bold = false;
                                    vizEditFisaPacientBtn.ForeColor = Color.Silver;
                                    vizEditFisaPacientBtn.Enabled = false;

                                    tiparireChitantaBtn.Font.Bold = false;
                                    tiparireChitantaBtn.ForeColor = Color.Silver;
                                    tiparireChitantaBtn.Enabled = false;
                                    GridView1.SelectedIndex = -1;
                                }
                            }
                            LabelPrezentErr.Text = "";
                            LabelPrincipalErr.Text = "";

                            ds.Dispose();
                            da.Dispose();
                        }
                        else
                        {
                            LabelCautareErr.ForeColor = Color.Red;
                            LabelCautareErr.Text = "Numele pacientului cautat nu are voie sa contina altceva in afara de litere.";
                            LabelPrezentErr.Text = "";
                            LabelPrincipalErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelCautareErr.ForeColor = Color.Red;
                        LabelCautareErr.Text = "Nu ati introdus numele pacientului cautat.";
                        LabelPrezentErr.Text = "";
                        LabelPrincipalErr.Text = "";
                    }
                }
                else
                {
                    LabelCautareErr.ForeColor = Color.Red;
                    LabelCautareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelPrezentErr.Text = "";
                    LabelPrincipalErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelCautareErr.ForeColor = Color.Red;
                LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrezentErr.Text = "";
                LabelPrincipalErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void adaugaPacientNouBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receptie))
            {
                Response.Redirect("AdaugaPacientNou.aspx");
                LabelPrincipalErr.Text = "";
            }
            else
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                LabelCautareErr.Text = "";
                LabelPrezentErr.Text = "";
            }
        }

        protected void consultatiiNeachitateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receptie))
            {
                LabelPrincipalErr.Text = "";
                Response.Redirect("ConsultatiiNeachitate.aspx");
            }
            else
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                LabelCautareErr.Text = "";
                LabelPrezentErr.Text = "";
            }
        }

        protected void tiparireChitantaBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receptie))
            {
                Session["numePacient"] = GridView1.SelectedRow.Cells[2].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[3].Text.ToString().Trim();
                Session["cnp"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim();
                Response.Redirect("TiparireChitanta.aspx");
            }
            else
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                LabelCautareErr.Text = "";
                LabelPrezentErr.Text = "";
            }
        }

        protected void vizEditFisaPacientBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receptie))
            {
                Session["numePacient"] = GridView1.SelectedRow.Cells[2].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[3].Text.ToString().Trim();
                Session["cnp"] = GridView1.SelectedRow.Cells[1].Text.ToString().Trim();
                DateTime dataN = Convert.ToDateTime(GridView1.SelectedRow.Cells[7].Text.ToString().Trim());
                Session["dataN"] = dataN.ToString("M/d/yyyy");
                Session["judet"] = GridView1.SelectedRow.Cells[9].Text.ToString().Trim();
                Session["oras"] = GridView1.SelectedRow.Cells[10].Text.ToString().Trim();
                Session["strada"] = GridView1.SelectedRow.Cells[11].Text.ToString().Trim();
                string numar = GridView1.SelectedRow.Cells[12].Text.ToString().Trim();
                if (numar == "&nbsp;")
                {
                    numar = "";
                }
                string bloc = GridView1.SelectedRow.Cells[13].Text.ToString().Trim();
                if (bloc == "&nbsp;")
                {
                    bloc = "";
                }
                string scara = GridView1.SelectedRow.Cells[14].Text.ToString().Trim();
                if (scara == "&nbsp;")
                {
                    scara = "";
                }
                string nrApartament = GridView1.SelectedRow.Cells[15].Text.ToString().Trim();
                if (nrApartament == "&nbsp;")
                {
                    nrApartament = "";
                }
                Session["numar"] = numar;
                Session["bloc"] = bloc;
                Session["scara"] = scara;
                Session["nrApartament"] = nrApartament;
                Session["sex"] = GridView1.SelectedRow.Cells[4].Text.ToString().Trim();
                Session["locNastere"] = GridView1.SelectedRow.Cells[8].Text.ToString().Trim();
                Session["telefon"] = GridView1.SelectedRow.Cells[16].Text.ToString().Trim();
                string numeM = GridView1.SelectedRow.Cells[5].Text.ToString().Trim();
                if (numeM == "&nbsp;")
                {
                    numeM = "";
                }
                Session["numeMama"] = numeM;
                string numeT = GridView1.SelectedRow.Cells[6].Text.ToString().Trim();
                if (numeT == "&nbsp;")
                {
                    numeT = "";
                }
                Session["numeTata"] = numeT;
                string email = GridView1.SelectedRow.Cells[17].Text.ToString().Trim();
                if (email == "&nbsp;")
                {
                    email = "";
                }
                Session["email"] = email;
                string medicFam = GridView1.SelectedRow.Cells[18].Text.ToString().Trim();
                if (medicFam == "&nbsp;")
                {
                    medicFam = "";
                }
                Session["medicFamilie"] = medicFam;
                string obs = GridView1.SelectedRow.Cells[19].Text.ToString().Trim();
                if (obs == "&nbsp;")
                {
                    obs = "";
                }
                Session["observatii"] = obs;
                Response.Redirect("VizualizareFisaPacient.aspx");
            }
            else
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                LabelCautareErr.Text = "";
                LabelPrezentErr.Text = "";
            }
        }
    }
}