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
    public partial class RadiografieNoua : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string numeM = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            numeM = (string)Application["numeMedic"];

            try
            {
                dataTB.Attributes.Add("readonly", "readonly");

                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(numeM))
                {
                    string stmt = "select NumeClasa from ClasaDiagnostice order by NumeClasa";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ClasaDiagnostice");
                    DataRow[] clasaD = ds.Tables["ClasaDiagnostice"].Select();
                    if (!IsPostBack)
                    {
                        clasaDiagnosticeDDL.Items.Clear();
                        clasaDiagnosticeDDL.Items.Add("Selectati...");
                        foreach (DataRow row in clasaD)
                        {
                            clasaDiagnosticeDDL.Items.Add(row["NumeClasa"].ToString().Trim());
                        }
                        diagnosticDDL.Items.Add("Selectati clasa diagnostice...");

                        dataTB.Text = DateTime.Now.ToString("M/d/yyyy");

                        try
                        {
                            numePacientTB.Text = Session["numePacient"].ToString();
                            cnpTB.Text = Session["cnp"].ToString();
                        }
                        catch (NullReferenceException ex)
                        {
                            LabelPrincipalErr.ForeColor = Color.Red;
                            LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                        }
                    }
                    LabelClasaDErr.Text = "";

                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch(Exception ex)
            {
                LabelClasaDErr.ForeColor = Color.Red;
                LabelClasaDErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void clasaDiagnosticeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clasaDiagnosticeDDL.SelectedIndex >= 1)
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
                            LabelClasaDErr.Text = "";
                        }
                        else
                        {
                            LabelClasaDErr.Text = "Clasa diagnostice nu a fost gasita in baza de date.";
                        }
                        dr.Close();
                        sc.Dispose();

                        stmt = "select NumeDiagnostic from " + grupa + " order by CodDiagnostic";
                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, grupa);
                        DataRow[] numeD = ds.Tables[grupa].Select();
                        foreach(DataRow row in numeD)
                        {
                            diagnosticDDL.Items.Add(row["NumeDiagnostic"].ToString().Trim());
                        }
                        LabelClasaDErr.Text = "";
                        codDiagnosticTB.Text = "";
                        ds.Dispose();
                        da.Dispose();
                    }
                }
                else
                {
                    diagnosticDDL.Items.Clear();
                    diagnosticDDL.Items.Add("Selectati clasa diagnostice...");
                    LabelClasaDErr.Text = "";
                    codDiagnosticTB.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelClasaDErr.ForeColor = Color.Red;
                LabelClasaDErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
        }

        protected void diagnosticDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (diagnosticDDL.SelectedIndex>=1)
                {
                    codDiagnosticTB.Text = "";
                    using (con = new SqlConnection(conString))
                    {
                        con.Open();

                        string stmt = "select Grupa from ClasaDiagnostice where numeClasa='" + clasaDiagnosticeDDL.SelectedValue.ToString().Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        SqlDataReader dr = sc.ExecuteReader();
                        string grupa = "";
                        if (dr.Read())
                        {
                            grupa = dr["Grupa"].ToString().Trim();
                            LabelClasaDErr.Text = "";
                            LabelDiagnosticErr.Text = "";
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
                        dr.Close();
                        sc.Dispose();
                    }
                }
                else
                {
                    codDiagnosticTB.Text = "";
                    LabelDiagnosticErr.Text = "";
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
                if (clasaDiagnosticeDDL.SelectedIndex >= 1)
                {
                    if (diagnosticDDL.SelectedIndex >= 1)
                    {
                        diagnosticTB.Visible = true;
                        diagnosticTB.Text = diagnosticTB.Text + diagnosticDDL.SelectedValue.ToString().Trim() + "//" + codDiagnosticTB.Text.Trim() + ";";

                        LabelClasaDErr.Text = "";
                        LabelDiagnosticErr.Text = "";
                        LabelDiagnosticBoxErr.Text = "";
                        LabelImagineErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelRezultatAdaugareErr.Text = "";
                    }
                    else
                    {
                        LabelDiagnosticErr.ForeColor = Color.Red;
                        LabelDiagnosticErr.Text = "Nu ati selectat diagnosticul.";
                        LabelClasaDErr.Text = "";
                        LabelImagineErr.Text = "";
                        LabelPrincipalErr.Text = "";
                        LabelRezultatAdaugareErr.Text = "";
                        LabelDiagnosticBoxErr.Text = "";
                    }
                }
                else
                {
                    LabelClasaDErr.ForeColor = Color.Red;
                    LabelClasaDErr.Text = "Nu ati selectat clasa diagnostice.";
                    LabelDiagnosticErr.Text = "";
                    LabelImagineErr.Text = "";
                    LabelPrincipalErr.Text = "";
                    LabelRezultatAdaugareErr.Text = "";
                    LabelDiagnosticBoxErr.Text = "";
                }
            }
            else
            {
                LabelDiagnosticBoxErr.ForeColor = Color.Red;
                LabelDiagnosticBoxErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                LabelClasaDErr.Text = "";
                LabelDiagnosticErr.Text = "";
                LabelImagineErr.Text = "";
                LabelPrincipalErr.Text = "";
                LabelRezultatAdaugareErr.Text = "";
            }
        }

        protected void adaugareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(numeM))
                {
                    if (LabelNumeRadiografie.Text != "")
                    {
                        if (diagnosticTB.Text != "")
                        {
                            con = new SqlConnection(conString);
                            con.Open();

                            string stmt = "select Id, CNP, NumePacient from Radiografii where CNP='" + cnpTB.Text.Trim() + "' and NumeImagine='" + LabelNumeRadiografie.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            SqlDataReader dr = sc.ExecuteReader();
                            if (dr.HasRows)
                            {
                                LabelRezultatAdaugareErr.ForeColor = Color.Red;
                                LabelRezultatAdaugareErr.Text = "Radiografia a fost deja adaugata la persoana respectiva.<br>O puteti vizualiza in sectiunea de 'Vizualizare radiografii' din 'Pagina Principala'.";
                                LabelPrincipalErr.ForeColor = Color.Red;
                                LabelPrincipalErr.Text = "Radiografia a fost deja adaugata la persoana respectiva.<br>O puteti vizualiza in sectiunea de 'Vizualizare radiografii' din 'Pagina Principala'.";
                                LabelClasaDErr.Text = "";
                                LabelDiagnosticBoxErr.Text = "";
                                LabelDiagnosticErr.Text = "";
                                LabelImagineErr.Text = "";
                                dr.Close();
                                sc.Dispose();
                            }
                            else
                            {
                                dr.Close();
                                sc.Dispose();

                                stmt = "insert into Radiografii ([CNP], [NumePacient], [Data], [NumeImagine], [Diagnostic], [Observatii]) values (@cnp, @nP,@data,@nI,@d,@o)";
                                sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@cnp", cnpTB.Text.Trim());
                                sc.Parameters.AddWithValue("@nP", numePacientTB.Text.Trim());
                                DateTime data = Convert.ToDateTime(dataTB.Text.Trim());
                                sc.Parameters.AddWithValue("@data", data.ToString("MM/dd/yyyy"));
                                sc.Parameters.AddWithValue("@nI", LabelNumeRadiografie.Text.Trim());
                                sc.Parameters.AddWithValue("@d", diagnosticTB.Text.Trim());
                                sc.Parameters.AddWithValue("@o", observatiiTB.Text);
                                sc.ExecuteNonQuery();
                                sc.Dispose();

                                if (reintoarcerePPCB.Checked == true)
                                {
                                    LabelRezultatAdaugareErr.ForeColor = Color.Green;
                                    LabelRezultatAdaugareErr.Text = "Radiografia a fost adaugata cu succes. In 5 secunde veti fi redirectionati pe Pagina Principala";
                                    LabelClasaDErr.Text = "";
                                    LabelDiagnosticBoxErr.Text = "";
                                    LabelDiagnosticErr.Text = "";
                                    LabelImagineErr.Text = "";
                                    LabelPrincipalErr.ForeColor = Color.Green;
                                    LabelPrincipalErr.Text = "In 5 secunde veti fi redirectionati pe Pagina Principala";
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('PaginaPrincipalaMedic.aspx') }, 5000);", true); //in 5 secunde se va redirectiona la pagina principala
                                }
                                else
                                {
                                    LabelRezultatAdaugareErr.ForeColor = Color.Green;
                                    LabelRezultatAdaugareErr.Text = "Radiografia a fost adaugata cu succes. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                    LabelClasaDErr.Text = "";
                                    LabelDiagnosticBoxErr.Text = "";
                                    LabelDiagnosticErr.Text = "";
                                    LabelImagineErr.Text = "";
                                    LabelPrincipalErr.ForeColor = Color.Green;
                                    LabelPrincipalErr.Text = "Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                                }
                            }
                        }
                        else
                        {
                            LabelDiagnosticBoxErr.ForeColor = Color.Red;
                            LabelDiagnosticBoxErr.Text = "Nu ati adaugat nici un diagnostic.";
                            LabelRezultatAdaugareErr.Text = "";
                            LabelClasaDErr.Text = "";
                            LabelDiagnosticErr.Text = "";
                            LabelImagineErr.Text = "";
                            LabelPrincipalErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelImagineErr.ForeColor = Color.Red;
                        LabelImagineErr.Text = "Nu ati incarcat radiografia.";
                        LabelDiagnosticBoxErr.Text = "";
                        LabelRezultatAdaugareErr.Text = "";
                        LabelClasaDErr.Text = "";
                        LabelDiagnosticErr.Text = "";
                        LabelPrincipalErr.Text = "";
                    }
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                    LabelImagineErr.Text = "";
                    LabelDiagnosticBoxErr.Text = "";
                    LabelRezultatAdaugareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                    LabelClasaDErr.Text = "";
                    LabelDiagnosticErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelImagineErr.Text = "";
                LabelDiagnosticBoxErr.Text = "";
                LabelRezultatAdaugareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelClasaDErr.Text = "";
                LabelDiagnosticErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void incarcaRadBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numeM))
            {
                if (FileUpload1.HasFile)
                {
                    string filePath = @"~\RadiografiiPoze\";
                    string fileName = FileUpload1.FileName.ToString();
                    //RadiografieI.ImageUrl = filePath + fileName;
                    pozaRadIBtn.Visible = true;
                    LabelImagineMare.Visible = true;
                    pozaRadIBtn.ImageUrl = filePath + fileName;
                    largeImg.Src = filePath + fileName;
                    LabelNumeRadiografie.Text = fileName;
                    LabelImagineErr.Text = "";
                    LabelRezultatAdaugareErr.Text = "";
                    LabelPrincipalErr.Text = "";
                }
                else
                {
                    LabelImagineErr.ForeColor = Color.Red;
                    LabelImagineErr.Text = "Nu ati selectat radiografia pentru a fi incarcata.";
                }
            }
            else
            {
                LabelImagineErr.ForeColor = Color.Red;
                LabelImagineErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            LabelClasaDErr.Text = "";
            LabelDiagnosticBoxErr.Text = "";
            LabelDiagnosticErr.Text = "";
            LabelImagineErr.Text = "";
            LabelPrincipalErr.ForeColor = Color.Green;
            LabelPrincipalErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelRezultatAdaugareErr.ForeColor = Color.Green;
            LabelRezultatAdaugareErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            dataTB.Text = DateTime.Now.ToString("M/d/yyyy");
            LabelNumeRadiografie.Text = "";
            LabelImagineMare.Visible = false;
            pozaRadIBtn.Visible = false;
            clasaDiagnosticeDDL.SelectedIndex = 0;
            diagnosticDDL.SelectedIndex = 0;
            codDiagnosticTB.Text = "";
            diagnosticTB.Text = "";
            observatiiTB.Text = "";
            reintoarcerePPCB.Checked = false;
            diagnosticTB.Visible = false;
        }
    }
}