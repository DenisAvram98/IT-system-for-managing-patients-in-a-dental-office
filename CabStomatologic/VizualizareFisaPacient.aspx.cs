using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class VizualizareFisaPacient : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string receptie = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            receptie = (string)Application["utilizatorR"];
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(receptie))
                    {
                        string nume = Session["numePacient"].ToString();
                        string[] f = nume.Split(' ');
                        numeTB.Text = f[0];
                        prenumeTB.Text = nume.Replace(f[0] + " ", "");
                        cnpTB.Text = Session["cnp"].ToString();
                        dataNasteriiTB.Text = Session["dataN"].ToString();//verificat cum arata
                        locNastereTB.Text = Session["locNastere"].ToString();
                        sexTB.Text = Session["sex"].ToString();
                        telefonTB.Text = Session["telefon"].ToString();
                        numeMamaTB.Text = Session["numeMama"].ToString();
                        numeTataTB.Text = Session["numeTata"].ToString();
                        emailTB.Text = Session["email"].ToString();
                        judetTB.Text = Session["judet"].ToString();
                        orasTB.Text = Session["oras"].ToString();
                        stradaTB.Text = Session["strada"].ToString();
                        numarTB.Text = Session["numar"].ToString();
                        blocTB.Text = Session["bloc"].ToString();
                        scaraTB.Text = Session["scara"].ToString();
                        numarApartamentTB.Text = Session["nrApartament"].ToString();
                        medicFamilieTB.Text = Session["medicFamilie"].ToString();
                        observatiiTB.Text = Session["observatii"].ToString();
                        LabelPrincipalErr.Text = "";
                        old = "";
                    }
                    else
                    {
                        LabelPrincipalErr.ForeColor = Color.Red;
                        LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    }
                }
                catch (NullReferenceException ex)
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                }
            }
        }

        protected void observatiiTB_PreRender(object sender, EventArgs e)
        {
            int charRows = 0;
            string tbCOntent;
            TextBox TextBox1 = sender as TextBox;
            tbCOntent = observatiiTB.Text;
            string[] split = tbCOntent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string s in split)
            {
                charRows += Convert.ToInt32(s.Length / 100) + 1;
            }
            TextBox1.Rows = charRows + 1;
            TextBox1.TextMode = TextBoxMode.MultiLine;
        }

        protected void printareBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (numeTB.Text != "" && !string.IsNullOrEmpty(receptie))
                {
                    LabelRezultatPrintareErr.ForeColor = Color.Green;
                    LabelRezultatPrintareErr.Text = "Fisa pacientului a fost printata. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                    LabelPrincipalErr.ForeColor = Color.Green;
                    LabelPrincipalErr.Text = "Fisa pacientului a fost printata. Pentru a revenii la pagina principala, dati click pe Pagina Principala din meniu.";
                    ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "window.print()", true);
                }
                else
                {
                    LabelRezultatPrintareErr.ForeColor = Color.Red;
                    LabelRezultatPrintareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelRezultatPrintareErr.ForeColor = Color.Red;
                LabelRezultatPrintareErr.Text = "Eroare: " + ex.Message;
            }
        }

        static string old = "";
        protected void editPrenumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            prenumeTB.Enabled = true;
            savePrenumeIBtn.Visible = true;
            cancelPrenumeIBtn.Visible = true;
            editPrenumeIBtn.Visible = false;
            old = prenumeTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
        }

        protected void cancelPrenumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            prenumeTB.Enabled = false;
            savePrenumeIBtn.Visible = false;
            cancelPrenumeIBtn.Visible = false;
            editPrenumeIBtn.Visible = true;
            prenumeTB.Text = old;
            LabelPrenumeSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
        }

        Regex test = new Regex("^[a-zA-Z ]+$");
        protected void savePrenumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (prenumeTB.Text != "")
                    {
                        if (test.IsMatch(prenumeTB.Text))
                        {
                            if (cnpTB.Text.Trim() != "")
                            {
                                string stmt = "update Pacienti set Prenume=@p where CNP='" + cnpTB.Text.Trim() + "'";
                                SqlCommand sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@p", prenumeTB.Text.Trim());
                                sc.ExecuteNonQuery();
                                sc.Dispose();

                                old = prenumeTB.Text.Trim();
                                prenumeTB.Enabled = false;
                                savePrenumeIBtn.Visible = false;
                                cancelPrenumeIBtn.Visible = false;
                                editPrenumeIBtn.Visible = true;
                                LabelPrenumeSaveErr.ForeColor = Color.Green;
                                LabelPrenumeSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                                editBlocIBtn.Visible = true;
                                editEmailIBtn.Visible = true;
                                editJudetIBtn.Visible = true;
                                editMedicFamIBtn.Visible = true;
                                editNumarApartamentIBtn.Visible = true;
                                editNumarIBtn.Visible = true;
                                editNumeIBtn.Visible = true;
                                editNumeMIBtn.Visible = true;
                                editNumeTIBtn.Visible = true;
                                editObsIBtn.Visible = true;
                                editOrasIBtn.Visible = true;
                                editScaraIBtn.Visible = true;
                                editStradaIBtn.Visible = true;
                                editTelefonIBtn.Visible = true;
                            }
                            else
                            {
                                LabelPrenumeSaveErr.ForeColor = Color.Red;
                                LabelPrenumeSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                            }
                        }
                        else
                        {
                            LabelPrenumeSaveErr.ForeColor = Color.Red;
                            LabelPrenumeSaveErr.Text = "Prenumele nu are voie sa contina altceva in afara de litere si spatii libere.";
                        }
                    }
                    else
                    {
                        LabelPrenumeSaveErr.ForeColor = Color.Red;
                        LabelPrenumeSaveErr.Text = "Nu ati introdus prenumele.";
                    }
                }
                else
                {
                    LabelPrenumeSaveErr.ForeColor = Color.Red;
                    LabelPrenumeSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelPrenumeSaveErr.ForeColor = Color.Red;
                LabelPrenumeSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editNumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeTB.Enabled = true;
            saveNumeIBtn.Visible = true;
            cancelNumeIBtn.Visible = true;
            editNumeIBtn.Visible = false;
            old = numeTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelNumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeTB.Enabled = false;
            saveNumeIBtn.Visible = false;
            cancelNumeIBtn.Visible = false;
            editNumeIBtn.Visible = true;
            numeTB.Text = old;
            LabelNumeSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveNumeIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numeTB.Text != "")
                    {
                        if (test.IsMatch(numeTB.Text))
                        {
                            if (cnpTB.Text.Trim() != "")
                            {
                                string stmt = "update Pacienti set Nume=@n where CNP='" + cnpTB.Text.Trim() + "'";
                                SqlCommand sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@n", numeTB.Text.Trim());
                                sc.ExecuteNonQuery();
                                sc.Dispose();

                                old = numeTB.Text.Trim();
                                numeTB.Enabled = false;
                                saveNumeIBtn.Visible = false;
                                cancelNumeIBtn.Visible = false;
                                editNumeIBtn.Visible = true;
                                LabelNumeSaveErr.ForeColor = Color.Green;
                                LabelNumeSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                                editBlocIBtn.Visible = true;
                                editEmailIBtn.Visible = true;
                                editJudetIBtn.Visible = true;
                                editMedicFamIBtn.Visible = true;
                                editNumarApartamentIBtn.Visible = true;
                                editNumarIBtn.Visible = true;
                                editNumeMIBtn.Visible = true;
                                editNumeTIBtn.Visible = true;
                                editObsIBtn.Visible = true;
                                editOrasIBtn.Visible = true;
                                editScaraIBtn.Visible = true;
                                editStradaIBtn.Visible = true;
                                editTelefonIBtn.Visible = true;
                                editPrenumeIBtn.Visible = true;
                            }
                            else
                            {
                                LabelNumeSaveErr.ForeColor = Color.Red;
                                LabelNumeSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                            }
                        }
                        else
                        {
                            LabelNumeSaveErr.ForeColor = Color.Red;
                            LabelNumeSaveErr.Text = "Numele nu are voie sa contina altceva in afara de litere si spatii libere.";
                        }
                    }
                    else
                    {
                        LabelNumeSaveErr.ForeColor = Color.Red;
                        LabelNumeSaveErr.Text = "Nu ati introdus numele.";
                    }
                }
                else
                {
                    LabelNumeSaveErr.ForeColor = Color.Red;
                    LabelNumeSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelNumeSaveErr.ForeColor = Color.Red;
                LabelNumeSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editNumeMIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeMamaTB.Enabled = true;
            saveNumeMIBtn.Visible = true;
            cancelNumeMIBtn.Visible = true;
            editNumeMIBtn.Visible = false;
            old = numeMamaTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelNumeMIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeMamaTB.Enabled = false;
            saveNumeMIBtn.Visible = false;
            cancelNumeMIBtn.Visible = false;
            editNumeMIBtn.Visible = true;
            numeMamaTB.Text = old;
            LabelNumeMSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveNumeMIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numeMamaTB.Text == "" || test.IsMatch(numeMamaTB.Text))
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set NumeMama=@nM where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@nM", numeMamaTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = numeMamaTB.Text.Trim();
                            numeMamaTB.Enabled = false;
                            saveNumeMIBtn.Visible = false;
                            cancelNumeMIBtn.Visible = false;
                            editNumeMIBtn.Visible = true;
                            LabelNumeMSaveErr.ForeColor = Color.Green;
                            LabelNumeMSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelNumeMSaveErr.ForeColor = Color.Red;
                            LabelNumeMSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelNumeMSaveErr.ForeColor = Color.Red;
                        LabelNumeMSaveErr.Text = "Numele mamei (Nume Prenume) nu are voie sa contina altceva in afara de litere si spatii libere.";
                    }
                }
                else
                {
                    LabelNumeMSaveErr.ForeColor = Color.Red;
                    LabelNumeMSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelNumeMSaveErr.ForeColor = Color.Red;
                LabelNumeMSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editNumeTIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeTataTB.Enabled = true;
            saveNumeTIBtn.Visible = true;
            cancelNumeTIBtn.Visible = true;
            editNumeTIBtn.Visible = false;
            old = numeTataTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelNumeTIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numeTataTB.Enabled = false;
            saveNumeTIBtn.Visible = false;
            cancelNumeTIBtn.Visible = false;
            editNumeTIBtn.Visible = true;
            numeTataTB.Text = old;
            LabelNumeTSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveNumeTIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numeTataTB.Text == "" || test.IsMatch(numeTataTB.Text))
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set NumeTata=@nT where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@nT", numeTataTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = numeTataTB.Text.Trim();
                            numeTataTB.Enabled = false;
                            saveNumeTIBtn.Visible = false;
                            cancelNumeTIBtn.Visible = false;
                            editNumeTIBtn.Visible = true;
                            LabelNumeTSaveErr.ForeColor = Color.Green;
                            LabelNumeTSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelNumeTSaveErr.ForeColor = Color.Red;
                            LabelNumeTSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelNumeTSaveErr.ForeColor = Color.Red;
                        LabelNumeTSaveErr.Text = "Numele tatalui (Nume Prenume) nu are voie sa contina altceva in afara de litere si spatii libere.";
                    }
                }
                else
                {
                    LabelNumeTSaveErr.ForeColor = Color.Red;
                    LabelNumeTSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelNumeTSaveErr.ForeColor = Color.Red;
                LabelNumeTSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editTelefonIBtn_Click(object sender, ImageClickEventArgs e)
        {
            telefonTB.Enabled = true;
            saveTelefonIBtn.Visible = true;
            cancelTelefonIBtn.Visible = true;
            editTelefonIBtn.Visible = false;
            old = telefonTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelTelefonIBtn_Click(object sender, ImageClickEventArgs e)
        {
            telefonTB.Enabled = false;
            saveTelefonIBtn.Visible = false;
            cancelTelefonIBtn.Visible = false;
            editTelefonIBtn.Visible = true;
            telefonTB.Text = old;
            LabelTelefonSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        Regex testNum = new Regex("^[0-9]+$");
        protected void saveTelefonIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (telefonTB.Text != "")
                    {
                        if (testNum.IsMatch(telefonTB.Text))
                        {
                            if (cnpTB.Text.Trim() != "")
                            {
                                string stmt = "update Pacienti set Telefon=@t where CNP='" + cnpTB.Text.Trim() + "'";
                                SqlCommand sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@t", telefonTB.Text.Trim());
                                sc.ExecuteNonQuery();
                                sc.Dispose();

                                old = telefonTB.Text.Trim();
                                telefonTB.Enabled = false;
                                saveTelefonIBtn.Visible = false;
                                cancelTelefonIBtn.Visible = false;
                                editTelefonIBtn.Visible = true;
                                LabelTelefonSaveErr.ForeColor = Color.Green;
                                LabelTelefonSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                                editBlocIBtn.Visible = true;
                                editEmailIBtn.Visible = true;
                                editJudetIBtn.Visible = true;
                                editMedicFamIBtn.Visible = true;
                                editNumarApartamentIBtn.Visible = true;
                                editNumarIBtn.Visible = true;
                                editNumeIBtn.Visible = true;
                                editNumeMIBtn.Visible = true;
                                editNumeTIBtn.Visible = true;
                                editObsIBtn.Visible = true;
                                editOrasIBtn.Visible = true;
                                editScaraIBtn.Visible = true;
                                editStradaIBtn.Visible = true;
                                editPrenumeIBtn.Visible = true;
                            }
                            else
                            {
                                LabelTelefonSaveErr.ForeColor = Color.Red;
                                LabelTelefonSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                            }
                        }
                        else
                        {
                            LabelTelefonSaveErr.ForeColor = Color.Red;
                            LabelTelefonSaveErr.Text = "Numarul de telefon poate sa contina numai cifre.";
                        }
                    }
                    else
                    {
                        LabelTelefonSaveErr.ForeColor = Color.Red;
                        LabelTelefonSaveErr.Text = "Nu ati introdus numarul de telefon.";
                    }
                }
                else
                {
                    LabelTelefonSaveErr.ForeColor = Color.Red;
                    LabelTelefonSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelTelefonSaveErr.ForeColor = Color.Red;
                LabelTelefonSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editEmailIBtn_Click(object sender, ImageClickEventArgs e)
        {
            emailTB.Enabled = true;
            saveEmailIBtn.Visible = true;
            cancelEmailIBtn.Visible = true;
            editEmailIBtn.Visible = false;
            old = emailTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelEmailIBtn_Click(object sender, ImageClickEventArgs e)
        {
            emailTB.Enabled = false;
            saveEmailIBtn.Visible = false;
            cancelEmailIBtn.Visible = false;
            editEmailIBtn.Visible = true;
            emailTB.Text = old;
            LabelEmailSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void saveEmailIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (emailTB.Text.Trim() == "" || testEmail.IsMatch(emailTB.Text.Trim()))
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set Email=@e where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@e", emailTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = emailTB.Text.Trim();
                            emailTB.Enabled = false;
                            saveEmailIBtn.Visible = false;
                            cancelEmailIBtn.Visible = false;
                            editEmailIBtn.Visible = true;
                            LabelEmailSaveErr.ForeColor = Color.Green;
                            LabelEmailSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelEmailSaveErr.ForeColor = Color.Red;
                            LabelEmailSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelEmailSaveErr.ForeColor = Color.Red;
                        LabelEmailSaveErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                    }
                }
                else
                {
                    LabelEmailSaveErr.ForeColor = Color.Red;
                    LabelEmailSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelEmailSaveErr.ForeColor = Color.Red;
                LabelEmailSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editMedicFamIBtn_Click(object sender, ImageClickEventArgs e)
        {
            medicFamilieTB.Enabled = true;
            saveMedicFamIBtn.Visible = true;
            cancelMedicFamIBtn.Visible = true;
            editMedicFamIBtn.Visible = false;
            old = medicFamilieTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelMedicFamIBtn_Click(object sender, ImageClickEventArgs e)
        {
            medicFamilieTB.Enabled = false;
            saveMedicFamIBtn.Visible = false;
            cancelMedicFamIBtn.Visible = false;
            editMedicFamIBtn.Visible = true;
            medicFamilieTB.Text = old;
            LabelMedicFamSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveMedicFamIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (medicFamilieTB.Text == "" || test.IsMatch(medicFamilieTB.Text))
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set MedicFamilie=@mF where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@mF", medicFamilieTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = medicFamilieTB.Text.Trim();
                            medicFamilieTB.Enabled = false;
                            saveMedicFamIBtn.Visible = false;
                            cancelMedicFamIBtn.Visible = false;
                            editMedicFamIBtn.Visible = true;
                            LabelMedicFamSaveErr.ForeColor = Color.Green;
                            LabelMedicFamSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelMedicFamSaveErr.ForeColor = Color.Red;
                            LabelMedicFamSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelMedicFamSaveErr.ForeColor = Color.Red;
                        LabelMedicFamSaveErr.Text = "Numele medicului de familie are voie sa contina numai litere si spatii libere.";
                    }
                }
                else
                {
                    LabelMedicFamSaveErr.ForeColor = Color.Red;
                    LabelMedicFamSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelMedicFamSaveErr.ForeColor = Color.Red;
                LabelMedicFamSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editObsIBtn_Click(object sender, ImageClickEventArgs e)
        {
            observatiiTB.Enabled = true;
            saveObsIBtn.Visible = true;
            cancelObsIBtn.Visible = true;
            editObsIBtn.Visible = false;
            old = observatiiTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelObsIBtn_Click(object sender, ImageClickEventArgs e)
        {
            observatiiTB.Enabled = false;
            saveObsIBtn.Visible = false;
            cancelObsIBtn.Visible = false;
            editObsIBtn.Visible = true;
            observatiiTB.Text = old;
            LabelObservatiiSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveObsIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (cnpTB.Text.Trim() != "")
                    {
                        string stmt = "update Pacienti set Observatii=@o where CNP='" + cnpTB.Text.Trim() + "'";
                        SqlCommand sc = new SqlCommand(stmt, con);
                        sc.Parameters.AddWithValue("@o", observatiiTB.Text.Trim());
                        sc.ExecuteNonQuery();
                        sc.Dispose();

                        old = observatiiTB.Text.Trim();
                        observatiiTB.Enabled = false;
                        saveObsIBtn.Visible = false;
                        cancelObsIBtn.Visible = false;
                        editObsIBtn.Visible = true;
                        LabelObservatiiSaveErr.ForeColor = Color.Green;
                        LabelObservatiiSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                        editBlocIBtn.Visible = true;
                        editEmailIBtn.Visible = true;
                        editJudetIBtn.Visible = true;
                        editMedicFamIBtn.Visible = true;
                        editNumarApartamentIBtn.Visible = true;
                        editNumarIBtn.Visible = true;
                        editNumeIBtn.Visible = true;
                        editNumeMIBtn.Visible = true;
                        editNumeTIBtn.Visible = true;
                        editOrasIBtn.Visible = true;
                        editScaraIBtn.Visible = true;
                        editStradaIBtn.Visible = true;
                        editTelefonIBtn.Visible = true;
                        editPrenumeIBtn.Visible = true;
                    }
                    else
                    {
                        LabelObservatiiSaveErr.ForeColor = Color.Red;
                        LabelObservatiiSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                    }
                }
                else
                {
                    LabelObservatiiSaveErr.ForeColor = Color.Red;
                    LabelObservatiiSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelObservatiiSaveErr.ForeColor = Color.Red;
                LabelObservatiiSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editJudetIBtn_Click(object sender, ImageClickEventArgs e)
        {
            //judetTB.Enabled = true;
            judetTB.Visible = false;
            judeteDDL.Visible = true;
            orasTB.Visible = false;
            oraseDDL.Visible = true;
            saveJudetIBtn.Visible = true;
            cancelJudetIBtn.Visible = true;
            editJudetIBtn.Visible = false;
            old = judetTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;

            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (cnpTB.Text.Trim() != "")
                    {
                        string stmt = "select Judet from Judete order by Judet";
                        SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Judete");
                        DataRow[] judete = ds.Tables["Judete"].Select();
                        judeteDDL.Items.Clear();
                        judeteDDL.Items.Add("Selectati...");
                        foreach (DataRow row in judete)
                        {
                            judeteDDL.Items.Add(row["Judet"].ToString().Trim());
                        }
                        judeteDDL.SelectedValue = old;
                        ds.Dispose();
                        da.Dispose();

                        oraseDDL.Items.Clear();
                        oraseDDL.Items.Add("Selectati...");
                        using (StreamReader sr = new StreamReader(Server.MapPath("~/OraseJudete/") + judeteDDL.SelectedValue.ToString().Trim() + ".txt"))
                        {
                            string oras;
                            while ((oras = sr.ReadLine()) != null)
                            {
                                oraseDDL.Items.Add(oras.Trim());
                            }
                        }
                        string oldOras = orasTB.Text.Trim();
                        oraseDDL.SelectedValue = oldOras;

                        LabelJudetSaveErr.Text = "";
                    }
                    else
                    {
                        LabelJudetSaveErr.ForeColor = Color.Red;
                        LabelJudetSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                    }
                }
                else
                {
                    LabelJudetSaveErr.ForeColor = Color.Red;
                    LabelJudetSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelJudetSaveErr.ForeColor = Color.Red;
                LabelJudetSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void cancelJudetIBtn_Click(object sender, ImageClickEventArgs e)
        {
            //judetTB.Enabled = false;
            judetTB.Visible = true;
            judeteDDL.Visible = false;
            orasTB.Visible = true;
            oraseDDL.Visible = false;
            saveJudetIBtn.Visible = false;
            cancelJudetIBtn.Visible = false;
            editJudetIBtn.Visible = true;
            judetTB.Text = old;
            LabelJudetSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void judeteDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (judeteDDL.SelectedIndex >= 1)
                {
                    oraseDDL.Items.Clear();
                    oraseDDL.Items.Add("Selectati...");
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/OraseJudete/") + judeteDDL.SelectedValue.ToString().Trim() + ".txt"))
                    {
                        string oras;
                        while ((oras = sr.ReadLine()) != null)
                        {
                            oraseDDL.Items.Add(oras.Trim());
                        }
                    }
                    LabelJudetSaveErr.Text = "";
                }
                else
                {
                    oraseDDL.Items.Clear();
                    oraseDDL.Items.Add("Selectati judetul...");
                    LabelJudetSaveErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelJudetSaveErr.ForeColor = Color.Red;
                LabelJudetSaveErr.Text = "Fisierul nu poate fi citit: " + ex.Message;
            }
        }

        protected void saveJudetIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (judeteDDL.SelectedIndex >= 1)
                    {
                        if (oraseDDL.SelectedIndex >= 1)
                        {
                            if (cnpTB.Text.Trim() != "")
                            {
                                string stmt = "update Pacienti set JudetDomiciliu=@jD, OrasDomiciliu=@oD where CNP='" + cnpTB.Text.Trim() + "'";
                                SqlCommand sc = new SqlCommand(stmt, con);
                                sc.Parameters.AddWithValue("@jD", judeteDDL.SelectedValue.ToString().Trim());
                                sc.Parameters.AddWithValue("@oD", oraseDDL.SelectedValue.ToString().Trim());
                                sc.ExecuteNonQuery();
                                sc.Dispose();

                                old = judeteDDL.SelectedValue.ToString().Trim();
                                //aici trebuie adaugate valorile noi la judet tb si oras tb depinde ce ii schimbat
                                judetTB.Text = judeteDDL.SelectedValue.ToString().Trim();
                                orasTB.Text = oraseDDL.SelectedValue.ToString().Trim();
                                //judetTB.Enabled = false;
                                judetTB.Visible = true;
                                judeteDDL.Visible = false;
                                orasTB.Visible = true;
                                oraseDDL.Visible = false;
                                saveJudetIBtn.Visible = false;
                                cancelJudetIBtn.Visible = false;
                                editJudetIBtn.Visible = true;
                                LabelJudetSaveErr.ForeColor = Color.Green;
                                LabelJudetSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                                editBlocIBtn.Visible = true;
                                editEmailIBtn.Visible = true;
                                editMedicFamIBtn.Visible = true;
                                editNumarApartamentIBtn.Visible = true;
                                editNumarIBtn.Visible = true;
                                editNumeIBtn.Visible = true;
                                editNumeMIBtn.Visible = true;
                                editNumeTIBtn.Visible = true;
                                editObsIBtn.Visible = true;
                                editOrasIBtn.Visible = true;
                                editScaraIBtn.Visible = true;
                                editStradaIBtn.Visible = true;
                                editTelefonIBtn.Visible = true;
                                editPrenumeIBtn.Visible = true;
                            }
                            else
                            {
                                LabelJudetSaveErr.ForeColor = Color.Red;
                                LabelJudetSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                            }
                        }
                        else
                        {
                            LabelOrasSaveErr.ForeColor = Color.Red;
                            LabelOrasSaveErr.Text = "Nu ati selectat orasul domiciliu.";
                        }
                    }
                    else
                    {
                        LabelJudetSaveErr.ForeColor = Color.Red;
                        LabelJudetSaveErr.Text = "Nu ati selectat judetul domiciliu.";
                    }
                }
                else
                {
                    LabelJudetSaveErr.ForeColor = Color.Red;
                    LabelJudetSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelJudetSaveErr.ForeColor = Color.Red;
                LabelJudetSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editOrasIBtn_Click(object sender, ImageClickEventArgs e)
        {
            //orasTB.Enabled = true;
            orasTB.Visible = false;
            oraseDDL.Visible = true;
            saveOrasIBtn.Visible = true;
            cancelOrasIBtn.Visible = true;
            editOrasIBtn.Visible = false;
            old = orasTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;

            try
            {
                if (!string.IsNullOrEmpty(receptie))
                {
                    if (cnpTB.Text.Trim() != "")
                    {
                        oraseDDL.Items.Clear();
                        oraseDDL.Items.Add("Selectati...");
                        using (StreamReader sr = new StreamReader(Server.MapPath("~/OraseJudete/") + judetTB.Text.Trim() + ".txt"))
                        {
                            string oras;
                            while ((oras = sr.ReadLine()) != null)
                            {
                                oraseDDL.Items.Add(oras.Trim());
                            }
                        }
                        oraseDDL.SelectedValue = old;
                        LabelOrasSaveErr.Text = "";
                    }
                    else
                    {
                        LabelOrasSaveErr.ForeColor = Color.Red;
                        LabelOrasSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                    }
                }
                else
                {
                    LabelOrasSaveErr.ForeColor = Color.Red;
                    LabelOrasSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelOrasSaveErr.ForeColor = Color.Red;
                LabelOrasSaveErr.Text = "Fisierul nu poate fi citit: " + ex.Message;
            }
        }

        protected void cancelOrasIBtn_Click(object sender, ImageClickEventArgs e)
        {
            //orasTB.Enabled = false;
            orasTB.Visible = true;
            oraseDDL.Visible = false;
            saveOrasIBtn.Visible = false;
            cancelOrasIBtn.Visible = false;
            editOrasIBtn.Visible = true;
            orasTB.Text = old;
            LabelOrasSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveOrasIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (oraseDDL.SelectedIndex >= 1)
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set OrasDomiciliu=@oD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@oD", oraseDDL.SelectedValue.ToString().Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = oraseDDL.SelectedValue.ToString().Trim();
                            //aici trebuie adaugate valorile noi la oras tb
                            orasTB.Text = oraseDDL.SelectedValue.ToString().Trim();
                            //judetTB.Enabled = false;
                            orasTB.Visible = true;
                            oraseDDL.Visible = false;
                            saveOrasIBtn.Visible = false;
                            cancelOrasIBtn.Visible = false;
                            editOrasIBtn.Visible = true;
                            LabelOrasSaveErr.ForeColor = Color.Green;
                            LabelOrasSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelOrasSaveErr.ForeColor = Color.Red;
                            LabelOrasSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelOrasSaveErr.ForeColor = Color.Red;
                        LabelOrasSaveErr.Text = "Nu ati selectat orasul domiciliu.";
                    }
                }
                else
                {
                    LabelOrasSaveErr.ForeColor = Color.Red;
                    LabelOrasSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelOrasSaveErr.ForeColor = Color.Red;
                LabelOrasSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editStradaIBtn_Click(object sender, ImageClickEventArgs e)
        {
            stradaTB.Enabled = true;
            saveStradaIBtn.Visible = true;
            cancelStradaIBtn.Visible = true;
            editStradaIBtn.Visible = false;
            old = stradaTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelStradaIBtn_Click(object sender, ImageClickEventArgs e)
        {
            stradaTB.Enabled = false;
            saveStradaIBtn.Visible = false;
            cancelStradaIBtn.Visible = false;
            editStradaIBtn.Visible = true;
            stradaTB.Text = old;
            LabelStradaSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveStradaIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (stradaTB.Text != "")
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set StradaDomiciliu=@sD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@sD", stradaTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = stradaTB.Text.Trim();
                            stradaTB.Enabled = false;
                            saveStradaIBtn.Visible = false;
                            cancelStradaIBtn.Visible = false;
                            editStradaIBtn.Visible = true;
                            LabelStradaSaveErr.ForeColor = Color.Green;
                            LabelStradaSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelStradaSaveErr.ForeColor = Color.Red;
                            LabelStradaSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelStradaSaveErr.ForeColor = Color.Red;
                        LabelStradaSaveErr.Text = "Nu ati introdus strada domiciliului.";
                    }
                }
                else
                {
                    LabelStradaSaveErr.ForeColor = Color.Red;
                    LabelStradaSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelStradaSaveErr.ForeColor = Color.Red;
                LabelStradaSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editNumarIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numarTB.Enabled = true;
            saveNumarIBtn.Visible = true;
            cancelNumarIBtn.Visible = true;
            editNumarIBtn.Visible = false;
            old = numarTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelNumarIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numarTB.Enabled = false;
            saveNumarIBtn.Visible = false;
            cancelNumarIBtn.Visible = false;
            editNumarIBtn.Visible = true;
            numarTB.Text = old;
            LabelNumarSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveNumarIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numarTB.Text.Trim() == "" || Convert.ToInt32(numarTB.Text.Trim()) >= 0)
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set NumarDomiciliu=@nD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@nD", numarTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = numarTB.Text.Trim();
                            numarTB.Enabled = false;
                            saveNumarIBtn.Visible = false;
                            cancelNumarIBtn.Visible = false;
                            editNumarIBtn.Visible = true;
                            LabelNumarSaveErr.ForeColor = Color.Green;
                            LabelNumarSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelNumarSaveErr.ForeColor = Color.Red;
                            LabelNumarSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelNumarSaveErr.ForeColor = Color.Red;
                        LabelNumarSaveErr.Text= "Numarul poate avea numai valori pozitive. (>=0)";
                    }
                }
                else
                {
                    LabelNumarSaveErr.ForeColor = Color.Red;
                    LabelNumarSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelNumarSaveErr.ForeColor = Color.Red;
                LabelNumarSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editBlocIBtn_Click(object sender, ImageClickEventArgs e)
        {
            blocTB.Enabled = true;
            saveBlocIBtn.Visible = true;
            cancelBlocIBtn.Visible = true;
            editBlocIBtn.Visible = false;
            old = blocTB.Text.Trim();
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelBlocIBtn_Click(object sender, ImageClickEventArgs e)
        {
            blocTB.Enabled = false;
            saveBlocIBtn.Visible = false;
            cancelBlocIBtn.Visible = false;
            editBlocIBtn.Visible = true;
            blocTB.Text = old;
            LabelBlocSaveErr.Text = "";
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveBlocIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (blocTB.Text.Trim() == "" || Convert.ToInt32(blocTB.Text.Trim()) >= 0)
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set BlocDomiciliu=@bD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@bD", blocTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = blocTB.Text.Trim();
                            blocTB.Enabled = false;
                            saveBlocIBtn.Visible = false;
                            cancelBlocIBtn.Visible = false;
                            editBlocIBtn.Visible = true;
                            LabelBlocSaveErr.ForeColor = Color.Green;
                            LabelBlocSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelBlocSaveErr.ForeColor = Color.Red;
                            LabelBlocSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelBlocSaveErr.ForeColor = Color.Red;
                        LabelBlocSaveErr.Text = "Blocul poate avea numai valori pozitive. (>=0)";
                    }
                }
                else
                {
                    LabelBlocSaveErr.ForeColor = Color.Red;
                    LabelBlocSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelBlocSaveErr.ForeColor = Color.Red;
                LabelBlocSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editScaraIBtn_Click(object sender, ImageClickEventArgs e)
        {
            scaraTB.Enabled = true;
            saveScaraIBtn.Visible = true;
            cancelScaraIBtn.Visible = true;
            editScaraIBtn.Visible = false;
            old = scaraTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelScaraIBtn_Click(object sender, ImageClickEventArgs e)
        {
            scaraTB.Enabled = false;
            saveScaraIBtn.Visible = false;
            cancelScaraIBtn.Visible = false;
            editScaraIBtn.Visible = true;
            scaraTB.Text = old;
            LabelScaraSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveScaraIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (scaraTB.Text.Trim() == "" || Convert.ToInt32(scaraTB.Text.Trim()) >= 0)
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set ScaraDomiciliu=@sD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@sD", scaraTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = scaraTB.Text.Trim();
                            scaraTB.Enabled = false;
                            saveScaraIBtn.Visible = false;
                            cancelScaraIBtn.Visible = false;
                            editScaraIBtn.Visible = true;
                            LabelScaraSaveErr.ForeColor = Color.Green;
                            LabelScaraSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarApartamentIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelScaraSaveErr.ForeColor = Color.Red;
                            LabelScaraSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelScaraSaveErr.ForeColor = Color.Red;
                        LabelScaraSaveErr.Text = "Scara poate avea numai valori pozitive. (>=0)";
                    }
                }
                else
                {
                    LabelScaraSaveErr.ForeColor = Color.Red;
                    LabelScaraSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelScaraSaveErr.ForeColor = Color.Red;
                LabelScaraSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void editNumarApartamentIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numarApartamentTB.Enabled = true;
            saveNumarApartamentIBtn.Visible = true;
            cancelNumarApartamentIBtn.Visible = true;
            editNumarApartamentIBtn.Visible = false;
            old = numarApartamentTB.Text.Trim();
            editBlocIBtn.Visible = false;
            editEmailIBtn.Visible = false;
            editJudetIBtn.Visible = false;
            editMedicFamIBtn.Visible = false;
            editNumarIBtn.Visible = false;
            editNumeIBtn.Visible = false;
            editNumeMIBtn.Visible = false;
            editNumeTIBtn.Visible = false;
            editObsIBtn.Visible = false;
            editOrasIBtn.Visible = false;
            editScaraIBtn.Visible = false;
            editStradaIBtn.Visible = false;
            editTelefonIBtn.Visible = false;
            editPrenumeIBtn.Visible = false;
        }

        protected void cancelNumarApartamentIBtn_Click(object sender, ImageClickEventArgs e)
        {
            numarApartamentTB.Enabled = false;
            saveNumarApartamentIBtn.Visible = false;
            cancelNumarApartamentIBtn.Visible = false;
            editNumarApartamentIBtn.Visible = true;
            numarApartamentTB.Text = old;
            LabelNumarApartamentSaveErr.Text = "";
            editBlocIBtn.Visible = true;
            editEmailIBtn.Visible = true;
            editJudetIBtn.Visible = true;
            editMedicFamIBtn.Visible = true;
            editNumarIBtn.Visible = true;
            editNumeIBtn.Visible = true;
            editNumeMIBtn.Visible = true;
            editNumeTIBtn.Visible = true;
            editObsIBtn.Visible = true;
            editOrasIBtn.Visible = true;
            editScaraIBtn.Visible = true;
            editStradaIBtn.Visible = true;
            editTelefonIBtn.Visible = true;
            editPrenumeIBtn.Visible = true;
        }

        protected void saveNumarApartamentIBtn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    if (numarApartamentTB.Text.Trim() == "" || Convert.ToInt32(numarApartamentTB.Text.Trim()) >= 0)
                    {
                        if (cnpTB.Text.Trim() != "")
                        {
                            string stmt = "update Pacienti set NrApartamentDomiciliu=@nAD where CNP='" + cnpTB.Text.Trim() + "'";
                            SqlCommand sc = new SqlCommand(stmt, con);
                            sc.Parameters.AddWithValue("@nAD", numarApartamentTB.Text.Trim());
                            sc.ExecuteNonQuery();
                            sc.Dispose();

                            old = numarApartamentTB.Text.Trim();
                            numarApartamentTB.Enabled = false;
                            saveNumarApartamentIBtn.Visible = false;
                            cancelNumarApartamentIBtn.Visible = false;
                            editNumarApartamentIBtn.Visible = true;
                            LabelNumarApartamentSaveErr.ForeColor = Color.Green;
                            LabelNumarApartamentSaveErr.Text = "Modificarea efectuata a fost salvata cu succes.";
                            editBlocIBtn.Visible = true;
                            editEmailIBtn.Visible = true;
                            editJudetIBtn.Visible = true;
                            editMedicFamIBtn.Visible = true;
                            editNumarIBtn.Visible = true;
                            editNumeIBtn.Visible = true;
                            editNumeMIBtn.Visible = true;
                            editNumeTIBtn.Visible = true;
                            editObsIBtn.Visible = true;
                            editOrasIBtn.Visible = true;
                            editScaraIBtn.Visible = true;
                            editStradaIBtn.Visible = true;
                            editTelefonIBtn.Visible = true;
                            editPrenumeIBtn.Visible = true;
                        }
                        else
                        {
                            LabelNumarApartamentSaveErr.ForeColor = Color.Red;
                            LabelNumarApartamentSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin.";
                        }
                    }
                    else
                    {
                        LabelNumarApartamentSaveErr.ForeColor = Color.Red;
                        LabelNumarApartamentSaveErr.Text = "Numarul apartamentului poate avea numai valori pozitive. (>=0)";
                    }
                }
                else
                {
                    LabelNumarApartamentSaveErr.ForeColor = Color.Red;
                    LabelNumarApartamentSaveErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                LabelNumarApartamentSaveErr.ForeColor = Color.Red;
                LabelNumarApartamentSaveErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}