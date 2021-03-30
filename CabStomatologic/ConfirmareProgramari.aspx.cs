using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class ConfirmareProgramari : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string receptie = "";

        protected void ToateProgramarile()
        {
            string stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            if (ds.Tables["Programari"].Rows.Count == 0)
            {
                GridView1.DataSource = ds.Tables["Programari"];
                GridView1.DataBind();
                LabelStatusProgramariErr.ForeColor = Color.Green;
                LabelStatusProgramariErr.Text = "Nu sa gasit nici o programare.";
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
            receptie = (string)Application["utilizatorR"];

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (!IsPostBack)
                    {
                        ToateProgramarile();
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
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
                if (e.Row.Cells[7].Text.ToString().Trim() == "Neconfirmata")
                {
                    e.Row.Cells[7].ForeColor = Color.Red;
                }
                else if (e.Row.Cells[7].Text.ToString().Trim() == "Expirata")
                {
                    e.Row.Cells[7].ForeColor = Color.Gray;
                }
                else //confrimata
                {
                    e.Row.Cells[7].ForeColor = Color.Green;
                }
                e.Row.Cells[8].Visible = false;
                GridView1.HeaderRow.Cells[8].Visible = false;
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

        protected void RezultatCautareUnRand ()
        {
            GridView1.SelectedIndex = 0;
            LabelActivare.Visible = false;
            LabelStatus.Visible = true;
            statusPDDL.Visible = true;
            string status = GridView1.Rows[0].Cells[7].Text.ToString().Trim();
            statusPDDL.SelectedValue = status;
        }

        protected void RezultatCautareMaiMulteRanduri ()
        {
            LabelActivare.Visible = true;
            LabelStatus.Visible = false;
            statusPDDL.Visible = false;
            GridView1.SelectedIndex = -1;
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
                            if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Toate")
                            {
                                string stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Programari");
                                if (ds.Tables["Programari"].Rows.Count == 0)
                                {
                                    //GridView1.DataSource = ds.Tables["Programari"];
                                    //GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Red;
                                    LabelStatusProgramariErr.Text = "Nu sa gasit nici o programare pentru pacientul cu numele: " + numeTB.Text.Trim() + ".";
                                }
                                else
                                {
                                    GridView1.DataSource = ds.Tables["Programari"];
                                    GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Green;
                                    LabelStatusProgramariErr.Text = "Pentru pacientul cu numele " + numeTB.Text.Trim() + " au fost gasite urmatoarele programari.";

                                    if(ds.Tables["Programari"].Rows.Count==1)
                                    {
                                        RezultatCautareUnRand();
                                    }
                                    else
                                    {
                                        RezultatCautareMaiMulteRanduri();
                                    }
                                }
                                LabelPrincipalErr.Text = "";
                                LabelNumeErr.Text = "";
                                ds.Dispose();
                                da.Dispose();
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Confirmata")
                            {
                                string stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' and StatusProgramare='Confirmata' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Programari");
                                if (ds.Tables["Programari"].Rows.Count == 0)
                                {
                                    //GridView1.DataSource = ds.Tables["Programari"];
                                    //GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Red;
                                    LabelStatusProgramariErr.Text = "Nu sa gasit nici o programare <u>confirmata</u> pentru pacientul cu numele: " + numeTB.Text.Trim() + ".";
                                }
                                else
                                {
                                    GridView1.DataSource = ds.Tables["Programari"];
                                    GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Green;
                                    LabelStatusProgramariErr.Text = "Pentru pacientul cu numele " + numeTB.Text.Trim() + " au fost gasite urmatoarele programari.";

                                    if (ds.Tables["Programari"].Rows.Count == 1)
                                    {
                                        RezultatCautareUnRand();
                                    }
                                    else
                                    {
                                        RezultatCautareMaiMulteRanduri();
                                    }
                                }
                                LabelNumeErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                ds.Dispose();
                                da.Dispose();
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Neconfirmata")
                            {
                                string stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' and StatusProgramare='Neconfirmata' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Programari");
                                if (ds.Tables["Programari"].Rows.Count == 0)
                                {
                                    //GridView1.DataSource = ds.Tables["Programari"];
                                    //GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Red;
                                    LabelStatusProgramariErr.Text = "Nu sa gasit nici o programare <u>neconfirmata</u> pentru pacientul cu numele: " + numeTB.Text.Trim() + ".";
                                }
                                else
                                {
                                    GridView1.DataSource = ds.Tables["Programari"];
                                    GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Green;
                                    LabelStatusProgramariErr.Text = "Pentru pacientul cu numele " + numeTB.Text.Trim() + " au fost gasite urmatoarele programari.";

                                    if (ds.Tables["Programari"].Rows.Count == 1)
                                    {
                                        RezultatCautareUnRand();
                                    }
                                    else
                                    {
                                        RezultatCautareMaiMulteRanduri();
                                    }
                                }
                                LabelNumeErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                ds.Dispose();
                                da.Dispose();
                            }
                            else //expirate
                            {
                                string stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' and StatusProgramare='Expirata' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC,case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
                                SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "Programari");
                                if (ds.Tables["Programari"].Rows.Count == 0)
                                {
                                    //GridView1.DataSource = ds.Tables["Programari"];
                                    //GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Red;
                                    LabelStatusProgramariErr.Text = "Nu sa gasit nici o programare <u>expirata</u> pentru pacientul cu numele: " + numeTB.Text.Trim() + ".";
                                }
                                else
                                {
                                    GridView1.DataSource = ds.Tables["Programari"];
                                    GridView1.DataBind();
                                    LabelStatusProgramariErr.ForeColor = Color.Green;
                                    LabelStatusProgramariErr.Text = "Pentru pacientul cu numele " + numeTB.Text.Trim() + " au fost gasite urmatoarele programari.";

                                    if (ds.Tables["Programari"].Rows.Count == 1)
                                    {
                                        RezultatCautareUnRand();
                                    }
                                    else
                                    {
                                        RezultatCautareMaiMulteRanduri();
                                    }
                                }
                                LabelNumeErr.Text = "";
                                LabelPrincipalErr.Text = "";
                                ds.Dispose();
                                da.Dispose();
                            }
                        }
                        else
                        {
                            LabelNumeErr.ForeColor = Color.Red;
                            LabelNumeErr.Text = "Numele pacientului cautat nu are voie sa contina altceva in afara de litere.";
                            LabelStatusProgramariErr.Text = "";
                            LabelPrincipalErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelNumeErr.ForeColor = Color.Red;
                        LabelNumeErr.Text = "Nu ati introdus numele pacientului cautat.";
                        LabelStatusProgramariErr.Text = "";
                        LabelPrincipalErr.Text = "";
                    }
                }
                else
                {
                    LabelStatusProgramariErr.ForeColor = Color.Red;
                    LabelStatusProgramariErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelPrincipalErr.Text = "";
                    LabelNumeErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelStatusProgramariErr.ForeColor = Color.Red;
                LabelStatusProgramariErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrincipalErr.Text = "";
                LabelNumeErr.Text = "";
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
                LabelActivare.Visible = false;
                LabelStatus.Visible = true;
                statusPDDL.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string status = GridView1.Rows[index].Cells[7].Text.ToString().Trim();
                statusPDDL.SelectedValue = status;
                LabelStatusProgramareErr.Text = "";
            }
        }

        protected void FunctieChangeSelect(string id) // functia selecteaza inregistrarea din gridview dupa ce sia schimbat pozitia dupa postback
        {
            int index = -1;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[8].Text.ToString().Trim() == id)
                {
                    index = row.RowIndex;
                    break;
                }
            }
            GridView1.SelectedIndex = index;
        }

        protected void RefreshGridViewNume (string statusProgramare)
        {
            string stmt = "";
            if (statusProgramare == "Toate")
            {
                stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
            }
            else
            {
                stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where Nume='" + numeTB.Text.Trim() + "' and StatusProgramare='" + statusProgramare + "' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
            }
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            GridView1.DataSource = ds.Tables["Programari"];
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void RefreshGridViewFaraNume (string statusProgramare)
        {
            string stmt = "";
            if (statusProgramare == "Toate")
            {
                stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
            }
            else
            {
                stmt = "select Data, Ora, Nume, Prenume, Telefon, Email, NumeMedic as [Medic], StatusProgramare as [Status programare], IdProgramare from Programari where StatusProgramare='" + statusProgramare + "' order by case StatusProgramare when 'Confirmata' then StatusProgramare END DESC, case StatusProgramare when 'Neconfirmata' then StatusProgramare END DESC, case StatusProgramare when 'Expirata' then Data END DESC, Data, Ora, Nume, Prenume";
            }
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Programari");
            GridView1.DataSource = ds.Tables["Programari"];
            GridView1.DataBind();
            ds.Dispose();
            da.Dispose();
        }

        protected void statusPDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();

                    DateTime data = Convert.ToDateTime(GridView1.SelectedRow.Cells[0].Text.ToString().Trim());
                    DateTime ora = Convert.ToDateTime(GridView1.SelectedRow.Cells[1].Text.ToString().Trim());
                    string idProgramare = GridView1.SelectedRow.Cells[8].Text.ToString().Trim();
                    if (statusPDDL.SelectedValue == "Confirmata" && data.Date >= DateTime.Today.Date)
                    {
                        if (ora.Hour > DateTime.Now.Hour && data.Date >= DateTime.Today.Date)
                        {
                            string stmt = "update Programari set StatusProgramare=@sP where IdProgramare='" + idProgramare + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@sP", statusPDDL.SelectedValue.ToString().Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();
                        }
                        else if (data.Date > DateTime.Today.Date)
                        {
                            string stmt = "update Programari set StatusProgramare=@sP where IdProgramare='" + idProgramare + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@sP", statusPDDL.SelectedValue.ToString().Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();
                        }
                        else
                        {
                            string stmt = "update Programari set StatusProgramare=@sP where IdProgramare='" + idProgramare + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@sP", "Expirata");
                            sc.ExecuteNonQuery();
                            sc.Dispose();
                        }
                    }
                    else if (statusPDDL.SelectedValue == "Confirmata" && data.Date < DateTime.Today.Date)
                    {
                        string stmt = "update Programari set StatusProgramare=@sP where IdProgramare='" + idProgramare + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        sc.Parameters.AddWithValue("@sP", "Expirata");
                        sc.ExecuteNonQuery();
                        sc.Dispose();
                    }
                    else
                    {
                        string stmt = "update Programari set StatusProgramare=@sP where IdProgramare='" + idProgramare + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        sc.Parameters.AddWithValue("@sP", statusPDDL.SelectedValue.ToString().Trim());
                        sc.ExecuteNonQuery();
                        sc.Dispose();
                    }

                    LabelNumeErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelStatusProgramariErr.Text = "";
                    LabelStatusProgramareErr.Text = "";

                    //trimitere email de confirmare
                    if (statusPDDL.SelectedValue == "Confirmata" && data.Date>=DateTime.Today.Date)
                    {
                        if (ora.Hour > DateTime.Now.Hour && data.Date >= DateTime.Today.Date)
                        {
                            MailAddress to = new MailAddress(GridView1.SelectedRow.Cells[5].Text.ToString().Trim());
                            MailAddress from = new MailAddress("denisavram121@gmail.com", "Denis - Dent");
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "Modificare status programare";
                            message.Body = "Programarea pentru " + GridView1.SelectedRow.Cells[2].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[3].Text.ToString().Trim() + " din data " + GridView1.SelectedRow.Cells[0].Text.ToString().Trim() + " la ora " + GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + " a fost CONFIRMATA.";
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                            {
                                Credentials = new NetworkCredential("denisavram121@gmail.com", "uovcrgrikfmxzvws"),
                                EnableSsl = true

                            };

                            try
                            {
                                client.Send(message);
                                LabelStatusProgramareErr.ForeColor = Color.Green;
                                LabelStatusProgramareErr.Text = "Confirmarea programari a fost trimisa pe email-ul: " + GridView1.SelectedRow.Cells[5].Text.ToString().Trim();
                            }
                            catch (SmtpException ex)
                            {
                                LabelStatusProgramareErr.ForeColor = Color.Red;
                                LabelStatusProgramareErr.Text = "Nu se poate realiza conexiunea la serverul SMTP: " + ex.Message;
                            }
                        }
                        else if (data.Date > DateTime.Today.Date)
                        {
                            MailAddress to = new MailAddress(GridView1.SelectedRow.Cells[5].Text.ToString().Trim());
                            MailAddress from = new MailAddress("denisavram121@gmail.com", "Denis - Dent");
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "Modificare status programare";
                            message.Body = "Programarea pentru " + GridView1.SelectedRow.Cells[2].Text.ToString().Trim() + " " + GridView1.SelectedRow.Cells[3].Text.ToString().Trim() + " din data " + GridView1.SelectedRow.Cells[0].Text.ToString().Trim() + " la ora " + GridView1.SelectedRow.Cells[1].Text.ToString().Trim() + " a fost CONFIRMATA.";
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                            {
                                Credentials = new NetworkCredential("denisavram121@gmail.com", "uovcrgrikfmxzvws"),
                                EnableSsl = true

                            };

                            try
                            {
                                client.Send(message);
                                LabelStatusProgramareErr.ForeColor = Color.Green;
                                LabelStatusProgramareErr.Text = "Confirmarea programari a fost trimisa pe email-ul: " + GridView1.SelectedRow.Cells[5].Text.ToString().Trim();
                            }
                            catch (SmtpException ex)
                            {
                                LabelStatusProgramareErr.ForeColor = Color.Red;
                                LabelStatusProgramareErr.Text = "Nu se poate realiza conexiunea la serverul SMTP: " + ex.Message;
                            }
                        }
                        else
                        {
                            LabelStatusProgramareErr.ForeColor = Color.Red;
                            LabelStatusProgramareErr.Text = "Programarea selectata este expirata. (Data programari si ora a expirat)";
                        }
                    }
                    else if  (statusPDDL.SelectedValue == "Confirmata" && !(data.Date >= DateTime.Today.Date))
                    {
                        LabelStatusProgramareErr.ForeColor = Color.Red;
                        LabelStatusProgramareErr.Text = "Programarea selectata este expirata. (Data programari a expirat)";
                    }

                    //refresh gridView
                    if (test.IsMatch(numeTB.Text.Trim()) && numeTB.Text.Trim() != "")
                    {
                        if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Toate")
                        {
                            RefreshGridViewNume("Toate");

                            FunctieChangeSelect(idProgramare);
                        }
                        else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Confirmata")
                        {
                            RefreshGridViewNume("Confirmata");

                            FunctieChangeSelect(idProgramare);
                        }
                        else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Neconfirmata")
                        {
                            RefreshGridViewNume("Neconfirmata");

                            FunctieChangeSelect(idProgramare);
                        }
                        else //expirate
                        {
                            RefreshGridViewNume("Expirata");

                            FunctieChangeSelect(idProgramare);
                        }
                    }
                    else if (numeTB.Text.Trim()=="")
                    {
                        if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Toate")
                        {
                            RefreshGridViewFaraNume("Toate");

                            FunctieChangeSelect(idProgramare);
                        }
                        else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Confirmata")
                        {
                            RefreshGridViewFaraNume("Confirmata");

                            FunctieChangeSelect(idProgramare);
                        }
                        else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Neconfirmata")
                        {
                            RefreshGridViewFaraNume("Neconfirmata");

                            FunctieChangeSelect(idProgramare);
                        }
                        else //expirate
                        {
                            RefreshGridViewFaraNume("Expirata");

                            FunctieChangeSelect(idProgramare);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LabelStatusProgramareErr.ForeColor = Color.Red;
                LabelStatusProgramareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelNumeErr.Text = "";
                LabelPrincipalErr.Text = "";
                LabelStatusProgramariErr.Text = "";
            }
        }

        protected void anulareCautareBtn_Click(object sender, EventArgs e)
        {
            numeTB.Text = "";
            statusProgramareDDL.SelectedIndex = 0;
            LabelActivare.Visible = true;
            LabelStatus.Visible = false;
            statusPDDL.Visible = false;
            LabelNumeErr.Text = "";
            LabelStatusProgramareErr.Text = "";

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    ToateProgramarile();
                    GridView1.SelectedIndex = -1;
                }
                else
                {
                    LabelStatusProgramariErr.ForeColor = Color.Red;
                    LabelStatusProgramariErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelPrincipalErr.Text = "";
                    LabelStatusProgramareErr.Text = "";
                    LabelNumeErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelStatusProgramariErr.ForeColor = Color.Red;
                LabelStatusProgramariErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrincipalErr.Text = "";
                LabelStatusProgramareErr.Text = "";
                LabelNumeErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void statusProgramareDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();

                    if (!string.IsNullOrEmpty(receptie))
                    {
                        LabelNumeErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelStatusProgramariErr.Text = "";
                        LabelStatusProgramareErr.Text = "";

                        if (test.IsMatch(numeTB.Text.Trim()) && numeTB.Text.Trim() != "")
                        {
                            if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Toate")
                            {
                                RefreshGridViewNume("Toate");
                                if (GridView1.Rows.Count==1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Confirmata")
                            {
                                RefreshGridViewFaraNume("Confirmata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Neconfirmata")
                            {
                                RefreshGridViewNume("Neconfirmata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else //expirate
                            {
                                RefreshGridViewNume("Expirata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                        }
                        else if (numeTB.Text.Trim() == "")
                        {
                            if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Toate")
                            {
                                RefreshGridViewFaraNume("Toate");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Confirmata")
                            {
                                RefreshGridViewFaraNume("Confirmata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else if (statusProgramareDDL.SelectedValue.ToString().Trim() == "Neconfirmata")
                            {
                                RefreshGridViewFaraNume("Neconfirmata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                            else //expirate
                            {
                                RefreshGridViewFaraNume("Expirata");
                                if (GridView1.Rows.Count == 1)
                                {
                                    RezultatCautareUnRand();
                                }
                                else
                                {
                                    RezultatCautareMaiMulteRanduri();
                                }
                            }
                        }
                    }
                    else
                    {
                        LabelStatusProgramariErr.ForeColor = Color.Red;
                        LabelStatusProgramariErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                        LabelPrincipalErr.Text = "";
                        LabelStatusProgramareErr.Text = "";
                        LabelNumeErr.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                LabelStatusProgramariErr.ForeColor = Color.Red;
                LabelStatusProgramariErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrincipalErr.Text = "";
                LabelStatusProgramareErr.Text = "";
                LabelNumeErr.Text = "";
            }
        }
    }
}