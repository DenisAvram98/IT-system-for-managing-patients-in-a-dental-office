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
    public partial class AdaugaPacientNou : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string receptie = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dataNasteriiTB.Attributes.Add("readonly", "readonly"); //pentru ca data dupa postback sa ramana in camp
                receptie = (string)Application["utilizatorR"];

                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(receptie))
                {
                    string stmt = "select Judet from Judete order by Judet";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Judete");
                    DataRow[] judete = ds.Tables["Judete"].Select();
                    if (!Page.IsPostBack)
                    {
                        judetDDL.Items.Clear();
                        judetDDL.Items.Add("Selectati...");
                        foreach (DataRow row in judete)
                        {
                            judetDDL.Items.Add(row["Judet"].ToString().Trim());
                        }
                        orasDDL.Items.Add("Selectati judetul...");
                    }
                    Label22.Text = "";
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    Label22.ForeColor = Color.Red;
                    Label22.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                }
            }
            catch (Exception ex)
            {
                Label22.ForeColor = Color.Red;
                Label22.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message; //labelu de desubt de titlu
            }
            finally
            {
                con.Close();
            }
        }

        protected void judetDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (judetDDL.SelectedIndex >= 1) //evitam Selectatii...
                {
                    orasDDL.Items.Clear();
                    orasDDL.Items.Add("Selectati...");
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/OraseJudete/") + judetDDL.SelectedValue.ToString().Trim() + ".txt"))
                    {
                        string oras;
                        while ((oras = sr.ReadLine()) != null)
                        {
                            orasDDL.Items.Add(oras.Trim());
                        }
                        LabelJudetErr.Text = "";
                    }
                }
                else
                {
                    orasDDL.Items.Clear();
                    orasDDL.Items.Add("Selectati judetul...");
                    LabelJudetErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelJudetErr.ForeColor = Color.Red;
                LabelJudetErr.Text = "Fisierul nu poate fi citit: " + ex.Message;
            }
        }

        static Boolean testCNP = false; //pentru validare inainte de apasarea butonului „Adauga pacient nou”

        protected void cnpTB_TextChanged(object sender, EventArgs e)
        {
            Regex testNum = new Regex("^[0-9]+$");
            try
            {
                if (cnpTB.Text.Trim() != "")
                {
                    if (testNum.IsMatch(cnpTB.Text.Trim()))
                    {
                        if (cnpTB.Text.Trim().Length == 13)
                        {
                            if (verificCNP(cnpTB.Text.Trim()) == true) //verificam daca CNP-ul este adevarat
                            {
                                string a1 = null, a2 = null, l1 = null, l2 = null, z1 = null, z2 = null, final = null;
                                string cnp = cnpTB.Text.Trim();
                                string sex = cnp.Substring(0, 1);
                                if (sex == "1" || sex == "2") //nascuti intre 1 ianuarie 1900 si 31 decembrie 1999
                                {
                                    a1 = cnp.Substring(1, 1);
                                    a2 = cnp.Substring(2, 1);
                                    l1 = cnp.Substring(3, 1);
                                    l2 = cnp.Substring(4, 1);
                                    z1 = cnp.Substring(5, 1);
                                    z2 = cnp.Substring(6, 1);
                                    if (l1 == "0" && z1 == "0")
                                    {
                                        final = l2 + "/" + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "0" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l2 + "/" + z1 + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "1" && z1 == "0")
                                    {
                                        final = l1 + l2 + "/" + z2 + "/19" + a1 + a2;
                                    }
                                    else if (l1 == "1" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l1 + l2 + "/" + z1 + z2 + "/19" + a1 + a2;
                                    }
                                    dataNasteriiTB.Text = final;
                                    if (sex == "1")
                                    {
                                        sexDDL.SelectedIndex = 1;
                                    }
                                    else if (sex == "2")
                                    {
                                        sexDDL.SelectedIndex = 2;
                                    }

                                    LabelDataNErr.Text = "";
                                    LabelCnpErr.Text = "";
                                    testCNP = true;
                                }
                                else if (sex == "5" || sex == "6") //nascuti intre 1 ianuarie 2000 si 31 decembrie 2099
                                {
                                    a1 = cnp.Substring(1, 1);
                                    a2 = cnp.Substring(2, 1);
                                    l1 = cnp.Substring(3, 1);
                                    l2 = cnp.Substring(4, 1);
                                    z1 = cnp.Substring(5, 1);
                                    z2 = cnp.Substring(6, 1);
                                    if (l1 == "0" && z1 == "0")
                                    {
                                        final = l2 + "/" + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "0" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l2 + "/" + z1 + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "1" && z1 == "0")
                                    {
                                        final = l1 + l2 + "/" + z2 + "/20" + a1 + a2;
                                    }
                                    else if (l1 == "1" && Convert.ToInt16(z1) != 0)
                                    {
                                        final = l1 + l2 + "/" + z1 + z2 + "/20" + a1 + a2;
                                    }
                                    LabelDataNErr.Text = "";
                                    LabelCnpErr.Text = "";

                                    dataNasteriiTB.Text = final;
                                    if (sex == "5")
                                    {
                                        sexDDL.SelectedIndex = 1;
                                    }
                                    else if (sex == "6")
                                    {
                                        sexDDL.SelectedIndex = 2;
                                    }
                                    testCNP = true;
                                }
                                else //pentru rezidenti si ani 1800 si 1899
                                {
                                    LabelDataNErr.ForeColor = Color.Green;
                                    LabelDataNErr.Text = "Va rugam sa introduceti data nasteri manual.";
                                    LabelCnpErr.Text = "";
                                    if (sex == "3" || sex == "7")
                                    {
                                        sexDDL.SelectedIndex = 1;
                                    }
                                    else if (sex == "4" || sex == "8")
                                    {
                                        sexDDL.SelectedIndex = 2;
                                    }
                                    testCNP = true;
                                }

                                try
                                {
                                    con = new SqlConnection(conString);
                                    con.Open();

                                    if (!string.IsNullOrEmpty(receptie))
                                    {
                                        string stmt = "select Nume, Prenume, Telefon, Email from ContPacienti where CNP='" + cnpTB.Text.Trim() + "'";
                                        SqlCommand sc = new SqlCommand(stmt, con);
                                        SqlDataReader dr = sc.ExecuteReader();
                                        if (dr.Read())
                                        {
                                            numeTB.Text = dr["Nume"].ToString().Trim();
                                            prenumeTB.Text = dr["Prenume"].ToString().Trim();
                                            telefonTB.Text = dr["Telefon"].ToString().Trim();
                                            emailTB.Text = dr["Email"].ToString().Trim();
                                        }
                                        dr.Close();
                                        sc.Dispose();
                                        Label22.Text = "";
                                    }
                                    else
                                    {
                                        Label22.ForeColor = Color.Red;
                                        Label22.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                                    }
                                }
                                catch(SqlException ex)
                                {
                                    Label22.ForeColor = Color.Red;
                                    Label22.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                                }
                                finally
                                {
                                    con.Close();
                                }
                            }
                            else
                            {
                                LabelCnpErr.ForeColor = Color.Red;
                                LabelCnpErr.Text = "CNP-ul introdus este inexistent.";
                                testCNP = false;
                            }
                        }
                        else
                        {
                            LabelCnpErr.ForeColor = Color.Red;
                            LabelCnpErr.Text = "CNP-ul trebuie sa contina 13 cifre.";
                            testCNP = false;
                        }
                    }
                    else
                    {
                        LabelCnpErr.ForeColor = Color.Red;
                        LabelCnpErr.Text = "CNP-ul nu are voie sa contina litere.";
                        testCNP = false;
                    }
                }
                else
                {
                    LabelCnpErr.ForeColor = Color.Red;
                    LabelCnpErr.Text = "Nu ati introuds CNP-ul.";
                    testCNP = false;
                }
            }
            catch (FormatException ex)
            {
                LabelCnpErr.ForeColor = Color.Red;
                LabelCnpErr.Text = "Nu se poate calcula data nasterii din CNP-ul introdus: " + ex.Message;
            }
        }

        public static bool verificCNP(string cnp)
        {
            int s, a1, a2, l1, l2, z1, z2, j1, j2, n1, n2, n3, cifc, u;
            s = Convert.ToInt16(cnp.Substring(0, 1));
            a1 = Convert.ToInt16(cnp.Substring(1, 1));
            a2 = Convert.ToInt16(cnp.Substring(2, 1));
            l1 = Convert.ToInt16(cnp.Substring(3, 1));
            l2 = Convert.ToInt16(cnp.Substring(4, 1));
            z1 = Convert.ToInt16(cnp.Substring(5, 1));
            z2 = Convert.ToInt16(cnp.Substring(6, 1));
            j1 = Convert.ToInt16(cnp.Substring(7, 1));
            j2 = Convert.ToInt16(cnp.Substring(8, 1));
            n1 = Convert.ToInt16(cnp.Substring(9, 1));
            n2 = Convert.ToInt16(cnp.Substring(10, 1));
            n3 = Convert.ToInt16(cnp.Substring(11, 1));
            cifc = Convert.ToInt16(((s * 2 + a1 * 7 + a2 * 9 + l1 * 1 + l2 * 4 + z1 * 6 +
            z2 * 3 + j1 * 5 + j2 * 8 + n1 * 2 + n2 * 7 + n3 * 9) % 11));
            if (cifc == 10)
            {
                cifc = 1;
            }
            u = Convert.ToInt16(cnp.Substring(12, 1));
            if (cifc == u)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Regex testEmail = new Regex("^[a-zA-Z_.\'-'0-9]+[@][a-zA-Z_\'-'0-9]+[.][a-zA-Z_\'-'0-9]+$");
        protected void adaugareBtn_Click(object sender, EventArgs e)
        {
            Regex testNum = new Regex("^[0-9]+$");
            Regex test = new Regex("^[a-zA-Z ]+$");
            Regex locN = new Regex("^[a-zA-Z- ]+$");

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
                            if (prenumeTB.Text.Trim() != "")
                            {
                                if (test.IsMatch(prenumeTB.Text.Trim()))
                                {
                                    if (testCNP == true && cnpTB.Text.Trim() != "" && testNum.IsMatch(cnpTB.Text.Trim()) && verificCNP(cnpTB.Text.Trim()) == true && cnpTB.Text.Trim().Length == 13)
                                    {
                                        if (dataNasteriiTB.Text.Trim() != "")
                                        {
                                            DateTime data = Convert.ToDateTime(dataNasteriiTB.Text.Trim());
                                            if (sexDDL.SelectedIndex >= 1) //omitem Selectati..
                                            {
                                                if (locNastereTB.Text.Trim() != "")
                                                {
                                                    if (locN.IsMatch(locNastereTB.Text.Trim()))
                                                    {
                                                        if (telefonTB.Text.Trim() != "")
                                                        {
                                                            if (testNum.IsMatch(telefonTB.Text.Trim()))
                                                            {
                                                                if (emailTB.Text.Trim() == "" || testEmail.IsMatch(emailTB.Text.Trim()))
                                                                {
                                                                    if (judetDDL.SelectedIndex >= 1)
                                                                    {
                                                                        if (orasDDL.SelectedIndex >= 1)
                                                                        {
                                                                            if (stradaTB.Text.Trim() != "")
                                                                            {
                                                                                string medicFamilie = medicFamilieTB.Text.Trim();
                                                                                string numeMama = numeMamaTB.Text.Trim();
                                                                                string numeTata = numeTataTB.Text.Trim();
                                                                                string numar = numarTB.Text.Trim();
                                                                                string bloc = blocTB.Text.Trim();
                                                                                string scara = scaraTB.Text.Trim();
                                                                                string nrA = nrApartamentTB.Text.Trim();
                                                                                if (numar == "" || Convert.ToInt32(numar) >= 0)
                                                                                {
                                                                                    if (bloc == "" || Convert.ToInt32(bloc) >= 0)
                                                                                    {
                                                                                        if (scara == "" || Convert.ToInt32(scara) >= 0)
                                                                                        {
                                                                                            if (nrA == "" || Convert.ToInt32(nrA) >= 0)
                                                                                            {
                                                                                                if (medicFamilie == "" || test.IsMatch(medicFamilie))
                                                                                                {
                                                                                                    if (numeMama == "" || test.IsMatch(numeMama))
                                                                                                    {
                                                                                                        if (numeTata == "" || test.IsMatch(numeTata))
                                                                                                        {
                                                                                                            string stmt = "insert into Pacienti ([CNP], [Nume], [Prenume], [Sex], [NumeMama], [NumeTata], [DataNasterii], [LocNastere], [JudetDomiciliu], [OrasDomiciliu], [StradaDomiciliu], [NumarDomiciliu], [BlocDomiciliu], [ScaraDomiciliu], [NrApartamentDomiciliu], [MedicFamilie], [Telefon], [Email], [Observatii]) values (@cnp,@n,@p,@s,@nM,@nT,@dN,@lN,@jD,@oD,@strD,@nD,@bD,@sD,@nAD,@mF,@t,@e,@o)";
                                                                                                            SqlCommand sc = new SqlCommand(stmt, con);
                                                                                                            sc.Parameters.AddWithValue("@cnp", cnpTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@n", numeTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@p", prenumeTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@s", sexDDL.SelectedValue.ToString().Trim());
                                                                                                            sc.Parameters.AddWithValue("@nM", numeMama);
                                                                                                            sc.Parameters.AddWithValue("@nT", numeTata);
                                                                                                            sc.Parameters.AddWithValue("@dN", data.ToString("MM/dd/yyyy"));
                                                                                                            sc.Parameters.AddWithValue("@lN", locNastereTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@jD", judetDDL.SelectedValue.ToString().Trim());
                                                                                                            sc.Parameters.AddWithValue("@oD", orasDDL.SelectedValue.ToString().Trim());
                                                                                                            sc.Parameters.AddWithValue("@strD", stradaTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@nD", numarTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@bD", blocTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@sD", scaraTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@nAD", nrApartamentTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@mF", medicFamilie);
                                                                                                            sc.Parameters.AddWithValue("@t", telefonTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@e", emailTB.Text.Trim());
                                                                                                            sc.Parameters.AddWithValue("@o", observatiiTB.Text.Trim());
                                                                                                            sc.ExecuteNonQuery();

                                                                                                            LabelRezultatAdaugare.ForeColor = Color.Green;
                                                                                                            LabelRezultatAdaugare.Text = "Pacientul a fost adaugat cu succes. In 5 secunde veti fi redirectionati pe Pagina Principala.";
                                                                                                            LabelCnpErr.Text = "";
                                                                                                            LabelJudetErr.Text = "";
                                                                                                            LabelNumeErr.Text = "";
                                                                                                            LabelPrenumeErr.Text = "";
                                                                                                            LabelSexErr.Text = "";
                                                                                                            Label22.ForeColor = Color.Green;
                                                                                                            Label22.Text = "Pacientul a fost adaugat cu succes. In 5 secunde veti fi redirectionati pe Pagina Principala.";
                                                                                                            LabelLocNastereErr.Text = "";
                                                                                                            LabelTelefonErr.Text = "";
                                                                                                            LabelNumeMErr.Text = "";
                                                                                                            LabelNumeTErr.Text = "";
                                                                                                            LabelOrasErr.Text = "";
                                                                                                            LabelMedicFErr.Text = "";
                                                                                                            LabelStradaErr.Text = "";
                                                                                                            LabelDataNErr.Text = ""; //15 mesaje de eroare
                                                                                                            LabelNumarApartamentErr.Text = "";
                                                                                                            LabelScaraErr.Text = "";
                                                                                                            LabelBlocErr.Text = "";
                                                                                                            LabelNumarErr.Text = "";
                                                                                                            LabelEmailErr.Text = "";

                                                                                                            sc.Dispose();

                                                                                                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('PaginaPrincipalaReceptie.aspx') }, 5000);", true); //in 5 secunde se va redirectiona la pagina principala
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            LabelRezultatAdaugare.Text = "";
                                                                                                            LabelCnpErr.Text = "";
                                                                                                            LabelJudetErr.Text = "";
                                                                                                            LabelNumeErr.Text = "";
                                                                                                            LabelPrenumeErr.Text = "";
                                                                                                            LabelSexErr.Text = "";
                                                                                                            Label22.Text = "";
                                                                                                            LabelLocNastereErr.Text = "";
                                                                                                            LabelTelefonErr.Text = "";
                                                                                                            LabelNumeMErr.Text = "";
                                                                                                            LabelNumeTErr.ForeColor = Color.Red;
                                                                                                            LabelNumeTErr.Text = "Numele tatalui are voie sa contina numai litere si spatii libere.";
                                                                                                            LabelOrasErr.Text = "";
                                                                                                            LabelMedicFErr.Text = "";
                                                                                                            LabelStradaErr.Text = "";
                                                                                                            LabelDataNErr.Text = "";
                                                                                                            LabelNumarApartamentErr.Text = "";
                                                                                                            LabelScaraErr.Text = "";
                                                                                                            LabelBlocErr.Text = "";
                                                                                                            LabelNumarErr.Text = "";
                                                                                                            LabelEmailErr.Text = "";
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        LabelRezultatAdaugare.Text = "";
                                                                                                        LabelCnpErr.Text = "";
                                                                                                        LabelJudetErr.Text = "";
                                                                                                        LabelNumeErr.Text = "";
                                                                                                        LabelPrenumeErr.Text = "";
                                                                                                        LabelSexErr.Text = "";
                                                                                                        Label22.Text = "";
                                                                                                        LabelLocNastereErr.Text = "";
                                                                                                        LabelTelefonErr.Text = "";
                                                                                                        LabelNumeMErr.ForeColor = Color.Red;
                                                                                                        LabelNumeMErr.Text = "Numele mamei are voie sa contina numai litere si spatii libere";
                                                                                                        LabelNumeTErr.Text = "";
                                                                                                        LabelOrasErr.Text = "";
                                                                                                        LabelMedicFErr.Text = "";
                                                                                                        LabelStradaErr.Text = "";
                                                                                                        LabelDataNErr.Text = "";
                                                                                                        LabelNumarApartamentErr.Text = "";
                                                                                                        LabelScaraErr.Text = "";
                                                                                                        LabelBlocErr.Text = "";
                                                                                                        LabelNumarErr.Text = "";
                                                                                                        LabelEmailErr.Text = "";
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    LabelRezultatAdaugare.Text = "";
                                                                                                    LabelCnpErr.Text = "";
                                                                                                    LabelJudetErr.Text = "";
                                                                                                    LabelNumeErr.Text = "";
                                                                                                    LabelPrenumeErr.Text = "";
                                                                                                    LabelSexErr.Text = "";
                                                                                                    Label22.Text = "";
                                                                                                    LabelLocNastereErr.Text = "";
                                                                                                    LabelTelefonErr.Text = "";
                                                                                                    LabelNumeMErr.Text = "";
                                                                                                    LabelNumeTErr.Text = "";
                                                                                                    LabelOrasErr.Text = "";
                                                                                                    LabelMedicFErr.ForeColor = Color.Red;
                                                                                                    LabelMedicFErr.Text = "Numele medicului de familie are voie sa contina numai litere si spatii libere.";
                                                                                                    LabelStradaErr.Text = "";
                                                                                                    LabelDataNErr.Text = "";
                                                                                                    LabelNumarApartamentErr.Text = "";
                                                                                                    LabelScaraErr.Text = "";
                                                                                                    LabelBlocErr.Text = "";
                                                                                                    LabelNumarErr.Text = "";
                                                                                                    LabelEmailErr.Text = "";
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                //err pentru numar Apartament
                                                                                                LabelNumarApartamentErr.ForeColor = Color.Red;
                                                                                                LabelNumarApartamentErr.Text = "Numarul apartamentului poate avea numai valori pozitive. (>=0)";
                                                                                                LabelRezultatAdaugare.Text = "";
                                                                                                LabelCnpErr.Text = "";
                                                                                                LabelJudetErr.Text = "";
                                                                                                LabelNumeErr.Text = "";
                                                                                                LabelPrenumeErr.Text = "";
                                                                                                LabelSexErr.Text = "";
                                                                                                Label22.Text = "";
                                                                                                LabelLocNastereErr.Text = "";
                                                                                                LabelTelefonErr.Text = "";
                                                                                                LabelNumeMErr.Text = "";
                                                                                                LabelNumeTErr.Text = "";
                                                                                                LabelOrasErr.Text = "";
                                                                                                LabelStradaErr.Text = "";
                                                                                                LabelDataNErr.Text = "";
                                                                                                LabelMedicFErr.Text = "";
                                                                                                LabelScaraErr.Text = "";
                                                                                                LabelBlocErr.Text = "";
                                                                                                LabelNumarErr.Text = "";
                                                                                                LabelEmailErr.Text = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            //err pentru scara
                                                                                            LabelScaraErr.ForeColor = Color.Red;
                                                                                            LabelScaraErr.Text = "Scara poate avea numai valori pozitive. (>=0)";
                                                                                            LabelNumarApartamentErr.Text = "";
                                                                                            LabelRezultatAdaugare.Text = "";
                                                                                            LabelCnpErr.Text = "";
                                                                                            LabelJudetErr.Text = "";
                                                                                            LabelNumeErr.Text = "";
                                                                                            LabelPrenumeErr.Text = "";
                                                                                            LabelSexErr.Text = "";
                                                                                            Label22.Text = "";
                                                                                            LabelLocNastereErr.Text = "";
                                                                                            LabelTelefonErr.Text = "";
                                                                                            LabelNumeMErr.Text = "";
                                                                                            LabelNumeTErr.Text = "";
                                                                                            LabelOrasErr.Text = "";
                                                                                            LabelStradaErr.Text = "";
                                                                                            LabelDataNErr.Text = "";
                                                                                            LabelMedicFErr.Text = "";
                                                                                            LabelBlocErr.Text = "";
                                                                                            LabelNumarErr.Text = "";
                                                                                            LabelEmailErr.Text = "";
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        //err pentru bloc
                                                                                        LabelBlocErr.ForeColor = Color.Red;
                                                                                        LabelBlocErr.Text = "Blocul poate avea numai valori pozitive. (>=0)";
                                                                                        LabelNumarApartamentErr.Text = "";
                                                                                        LabelRezultatAdaugare.Text = "";
                                                                                        LabelCnpErr.Text = "";
                                                                                        LabelJudetErr.Text = "";
                                                                                        LabelNumeErr.Text = "";
                                                                                        LabelPrenumeErr.Text = "";
                                                                                        LabelSexErr.Text = "";
                                                                                        Label22.Text = "";
                                                                                        LabelLocNastereErr.Text = "";
                                                                                        LabelTelefonErr.Text = "";
                                                                                        LabelNumeMErr.Text = "";
                                                                                        LabelNumeTErr.Text = "";
                                                                                        LabelOrasErr.Text = "";
                                                                                        LabelStradaErr.Text = "";
                                                                                        LabelDataNErr.Text = "";
                                                                                        LabelMedicFErr.Text = "";
                                                                                        LabelScaraErr.Text = "";
                                                                                        LabelNumarErr.Text = "";
                                                                                        LabelEmailErr.Text = "";
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    //err pentru numar
                                                                                    LabelNumarErr.ForeColor = Color.Red;
                                                                                    LabelNumarErr.Text = "Numarul poate avea numai valori pozitive. (>=0)";
                                                                                    LabelNumarApartamentErr.Text = "";
                                                                                    LabelRezultatAdaugare.Text = "";
                                                                                    LabelCnpErr.Text = "";
                                                                                    LabelJudetErr.Text = "";
                                                                                    LabelNumeErr.Text = "";
                                                                                    LabelPrenumeErr.Text = "";
                                                                                    LabelSexErr.Text = "";
                                                                                    Label22.Text = "";
                                                                                    LabelLocNastereErr.Text = "";
                                                                                    LabelTelefonErr.Text = "";
                                                                                    LabelNumeMErr.Text = "";
                                                                                    LabelNumeTErr.Text = "";
                                                                                    LabelOrasErr.Text = "";
                                                                                    LabelStradaErr.Text = "";
                                                                                    LabelDataNErr.Text = "";
                                                                                    LabelMedicFErr.Text = "";
                                                                                    LabelScaraErr.Text = "";
                                                                                    LabelBlocErr.Text = "";
                                                                                    LabelEmailErr.Text = "";
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                LabelRezultatAdaugare.Text = "";
                                                                                LabelCnpErr.Text = "";
                                                                                LabelJudetErr.Text = "";
                                                                                LabelNumeErr.Text = "";
                                                                                LabelPrenumeErr.Text = "";
                                                                                LabelSexErr.Text = "";
                                                                                Label22.Text = "";
                                                                                LabelLocNastereErr.Text = "";
                                                                                LabelTelefonErr.Text = "";
                                                                                LabelNumeMErr.Text = "";
                                                                                LabelNumeTErr.Text = "";
                                                                                LabelOrasErr.Text = "";
                                                                                LabelMedicFErr.Text = "";
                                                                                LabelStradaErr.ForeColor = Color.Red;
                                                                                LabelStradaErr.Text = "Nu ati introdus strada domiciliului.";
                                                                                LabelDataNErr.Text = "";
                                                                                LabelNumarApartamentErr.Text = "";
                                                                                LabelScaraErr.Text = "";
                                                                                LabelBlocErr.Text = "";
                                                                                LabelNumarErr.Text = "";
                                                                                LabelEmailErr.Text = "";
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            LabelRezultatAdaugare.Text = "";
                                                                            LabelCnpErr.Text = "";
                                                                            LabelJudetErr.Text = "";
                                                                            LabelNumeErr.Text = "";
                                                                            LabelPrenumeErr.Text = "";
                                                                            LabelSexErr.Text = "";
                                                                            Label22.Text = "";
                                                                            LabelLocNastereErr.Text = "";
                                                                            LabelTelefonErr.Text = "";
                                                                            LabelNumeMErr.Text = "";
                                                                            LabelNumeTErr.Text = "";
                                                                            LabelOrasErr.ForeColor = Color.Red;
                                                                            LabelOrasErr.Text = "Nu ati selectat orasul domiciliu.";
                                                                            LabelMedicFErr.Text = "";
                                                                            LabelStradaErr.Text = "";
                                                                            LabelDataNErr.Text = "";
                                                                            LabelNumarApartamentErr.Text = "";
                                                                            LabelScaraErr.Text = "";
                                                                            LabelBlocErr.Text = "";
                                                                            LabelNumarErr.Text = "";
                                                                            LabelEmailErr.Text = "";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        LabelRezultatAdaugare.Text = "";
                                                                        LabelCnpErr.Text = "";
                                                                        LabelJudetErr.ForeColor = Color.Red;
                                                                        LabelJudetErr.Text = "Nu ati selectat judetul domiciliu.";
                                                                        LabelNumeErr.Text = "";
                                                                        LabelPrenumeErr.Text = "";
                                                                        LabelSexErr.Text = "";
                                                                        Label22.Text = "";
                                                                        LabelLocNastereErr.Text = "";
                                                                        LabelTelefonErr.Text = "";
                                                                        LabelNumeMErr.Text = "";
                                                                        LabelNumeTErr.Text = "";
                                                                        LabelOrasErr.Text = "";
                                                                        LabelMedicFErr.Text = "";
                                                                        LabelStradaErr.Text = "";
                                                                        LabelDataNErr.Text = "";
                                                                        LabelNumarApartamentErr.Text = "";
                                                                        LabelScaraErr.Text = "";
                                                                        LabelBlocErr.Text = "";
                                                                        LabelNumarErr.Text = "";
                                                                        LabelEmailErr.Text = "";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    LabelEmailErr.ForeColor = Color.Red;
                                                                    LabelEmailErr.Text = "E-mail-ul poate sa contina litere, cifre, -, _.<br/>Obligatoriu trebuie sa contina @nume provider.nume domain";
                                                                    LabelRezultatAdaugare.Text = "";
                                                                    LabelCnpErr.Text = "";
                                                                    LabelJudetErr.Text = "";
                                                                    LabelNumeErr.Text = "";
                                                                    LabelPrenumeErr.Text = "";
                                                                    LabelSexErr.Text = "";
                                                                    Label22.Text = "";
                                                                    LabelLocNastereErr.Text = "";
                                                                    LabelTelefonErr.Text = "";
                                                                    LabelNumeMErr.Text = "";
                                                                    LabelNumeTErr.Text = "";
                                                                    LabelOrasErr.Text = "";
                                                                    LabelMedicFErr.Text = "";
                                                                    LabelStradaErr.Text = "";
                                                                    LabelDataNErr.Text = "";
                                                                    LabelNumarApartamentErr.Text = "";
                                                                    LabelScaraErr.Text = "";
                                                                    LabelBlocErr.Text = "";
                                                                    LabelNumarErr.Text = "";
                                                                    LabelEmailErr.Text = "";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                LabelRezultatAdaugare.Text = "";
                                                                LabelCnpErr.Text = "";
                                                                LabelJudetErr.Text = "";
                                                                LabelNumeErr.Text = "";
                                                                LabelPrenumeErr.Text = "";
                                                                LabelSexErr.Text = "";
                                                                Label22.Text = "";
                                                                LabelLocNastereErr.Text = "";
                                                                LabelTelefonErr.ForeColor = Color.Red;
                                                                LabelTelefonErr.Text = "Numarul de telefon poate sa contina numai cifre.";
                                                                LabelNumeMErr.Text = "";
                                                                LabelNumeTErr.Text = "";
                                                                LabelOrasErr.Text = "";
                                                                LabelMedicFErr.Text = "";
                                                                LabelStradaErr.Text = "";
                                                                LabelDataNErr.Text = "";
                                                                LabelNumarApartamentErr.Text = "";
                                                                LabelScaraErr.Text = "";
                                                                LabelBlocErr.Text = "";
                                                                LabelNumarErr.Text = "";
                                                                LabelEmailErr.Text = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            LabelRezultatAdaugare.Text = "";
                                                            LabelCnpErr.Text = "";
                                                            LabelJudetErr.Text = "";
                                                            LabelNumeErr.Text = "";
                                                            LabelPrenumeErr.Text = "";
                                                            LabelSexErr.Text = "";
                                                            Label22.Text = "";
                                                            LabelLocNastereErr.Text = "";
                                                            LabelTelefonErr.ForeColor = Color.Red;
                                                            LabelTelefonErr.Text = "Nu ati introdus numarul de telefon.";
                                                            LabelNumeMErr.Text = "";
                                                            LabelNumeTErr.Text = "";
                                                            LabelOrasErr.Text = "";
                                                            LabelMedicFErr.Text = "";
                                                            LabelStradaErr.Text = "";
                                                            LabelDataNErr.Text = "";
                                                            LabelNumarApartamentErr.Text = "";
                                                            LabelScaraErr.Text = "";
                                                            LabelBlocErr.Text = "";
                                                            LabelNumarErr.Text = "";
                                                            LabelEmailErr.Text = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        LabelRezultatAdaugare.Text = "";
                                                        LabelCnpErr.Text = "";
                                                        LabelJudetErr.Text = "";
                                                        LabelNumeErr.Text = "";
                                                        LabelPrenumeErr.Text = "";
                                                        LabelSexErr.Text = "";
                                                        Label22.Text = "";
                                                        LabelLocNastereErr.ForeColor = Color.Red;
                                                        LabelLocNastereErr.Text = "Localitatea nasterii are voie sa contina: litere, spatii, '-'.";
                                                        LabelTelefonErr.Text = "";
                                                        LabelNumeMErr.Text = "";
                                                        LabelNumeTErr.Text = "";
                                                        LabelOrasErr.Text = "";
                                                        LabelMedicFErr.Text = "";
                                                        LabelStradaErr.Text = "";
                                                        LabelDataNErr.Text = "";
                                                        LabelNumarApartamentErr.Text = "";
                                                        LabelScaraErr.Text = "";
                                                        LabelBlocErr.Text = "";
                                                        LabelNumarErr.Text = "";
                                                        LabelEmailErr.Text = "";
                                                    }
                                                }
                                                else
                                                {
                                                    LabelRezultatAdaugare.Text = "";
                                                    LabelCnpErr.Text = "";
                                                    LabelJudetErr.Text = "";
                                                    LabelNumeErr.Text = "";
                                                    LabelPrenumeErr.Text = "";
                                                    LabelSexErr.Text = "";
                                                    Label22.Text = "";
                                                    LabelLocNastereErr.ForeColor = Color.Red;
                                                    LabelLocNastereErr.Text = "Nu ati introdus localitatea nasterii.";
                                                    LabelTelefonErr.Text = "";
                                                    LabelNumeMErr.Text = "";
                                                    LabelNumeTErr.Text = "";
                                                    LabelOrasErr.Text = "";
                                                    LabelMedicFErr.Text = "";
                                                    LabelStradaErr.Text = "";
                                                    LabelDataNErr.Text = "";
                                                    LabelNumarApartamentErr.Text = "";
                                                    LabelScaraErr.Text = "";
                                                    LabelBlocErr.Text = "";
                                                    LabelNumarErr.Text = "";
                                                    LabelEmailErr.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                LabelRezultatAdaugare.Text = "";
                                                LabelCnpErr.Text = "";
                                                LabelJudetErr.Text = "";
                                                LabelNumeErr.Text = "";
                                                LabelPrenumeErr.Text = "";
                                                LabelSexErr.ForeColor = Color.Red;
                                                LabelSexErr.Text = "Nu ati selectat sexul.";
                                                Label22.Text = "";
                                                LabelLocNastereErr.Text = "";
                                                LabelTelefonErr.Text = "";
                                                LabelNumeMErr.Text = "";
                                                LabelNumeTErr.Text = "";
                                                LabelOrasErr.Text = "";
                                                LabelMedicFErr.Text = "";
                                                LabelStradaErr.Text = "";
                                                LabelDataNErr.Text = "";
                                                LabelNumarApartamentErr.Text = "";
                                                LabelScaraErr.Text = "";
                                                LabelBlocErr.Text = "";
                                                LabelNumarErr.Text = "";
                                                LabelEmailErr.Text = "";
                                            }
                                        }
                                        else
                                        {
                                            LabelRezultatAdaugare.Text = "";
                                            LabelCnpErr.Text = "";
                                            LabelJudetErr.Text = "";
                                            LabelNumeErr.Text = "";
                                            LabelPrenumeErr.Text = "";
                                            LabelSexErr.Text = "";
                                            Label22.Text = "";
                                            LabelLocNastereErr.Text = "";
                                            LabelTelefonErr.Text = "";
                                            LabelNumeMErr.Text = "";
                                            LabelNumeTErr.Text = "";
                                            LabelOrasErr.Text = "";
                                            LabelMedicFErr.Text = "";
                                            LabelStradaErr.Text = "";
                                            LabelDataNErr.ForeColor = Color.Red;
                                            LabelDataNErr.Text = "Nu ati introus data nasterii.";
                                            LabelNumarApartamentErr.Text = "";
                                            LabelScaraErr.Text = "";
                                            LabelBlocErr.Text = "";
                                            LabelNumarErr.Text = "";
                                            LabelEmailErr.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        LabelRezultatAdaugare.Text = "";
                                        LabelCnpErr.ForeColor = Color.Red;
                                        LabelCnpErr.Text = "Nu exista CNP-ul introdus. CNP-ul este gresit.";
                                        LabelJudetErr.Text = "";
                                        LabelNumeErr.Text = "";
                                        LabelPrenumeErr.Text = "";
                                        LabelSexErr.Text = "";
                                        Label22.Text = "";
                                        LabelLocNastereErr.Text = "";
                                        LabelTelefonErr.Text = "";
                                        LabelNumeMErr.Text = "";
                                        LabelNumeTErr.Text = "";
                                        LabelOrasErr.Text = "";
                                        LabelMedicFErr.Text = "";
                                        LabelStradaErr.Text = "";
                                        LabelDataNErr.Text = "";
                                        LabelNumarApartamentErr.Text = "";
                                        LabelScaraErr.Text = "";
                                        LabelBlocErr.Text = "";
                                        LabelNumarErr.Text = "";
                                        LabelEmailErr.Text = "";
                                    }
                                }
                                else
                                {
                                    LabelRezultatAdaugare.Text = "";
                                    LabelCnpErr.Text = "";
                                    LabelJudetErr.Text = "";
                                    LabelNumeErr.Text = "";
                                    LabelPrenumeErr.ForeColor = Color.Red;
                                    LabelPrenumeErr.Text = "Prenumele nu are voie sa contina altceva in afara de litere si spatii libere.";
                                    LabelSexErr.Text = "";
                                    Label22.Text = "";
                                    LabelLocNastereErr.Text = "";
                                    LabelTelefonErr.Text = "";
                                    LabelNumeMErr.Text = "";
                                    LabelNumeTErr.Text = "";
                                    LabelOrasErr.Text = "";
                                    LabelMedicFErr.Text = "";
                                    LabelStradaErr.Text = "";
                                    LabelDataNErr.Text = "";
                                    LabelNumarApartamentErr.Text = "";
                                    LabelScaraErr.Text = "";
                                    LabelBlocErr.Text = "";
                                    LabelNumarErr.Text = "";
                                    LabelEmailErr.Text = "";
                                }
                            }
                            else
                            {
                                LabelRezultatAdaugare.Text = "";
                                LabelCnpErr.Text = "";
                                LabelJudetErr.Text = "";
                                LabelNumeErr.Text = "";
                                LabelPrenumeErr.ForeColor = Color.Red;
                                LabelPrenumeErr.Text = "Nu ati introdus prenumele.";
                                LabelSexErr.Text = "";
                                Label22.Text = "";
                                LabelLocNastereErr.Text = "";
                                LabelTelefonErr.Text = "";
                                LabelNumeMErr.Text = "";
                                LabelNumeTErr.Text = "";
                                LabelOrasErr.Text = "";
                                LabelMedicFErr.Text = "";
                                LabelStradaErr.Text = "";
                                LabelDataNErr.Text = "";
                                LabelNumarApartamentErr.Text = "";
                                LabelScaraErr.Text = "";
                                LabelBlocErr.Text = "";
                                LabelNumarErr.Text = "";
                                LabelEmailErr.Text = "";
                            }
                        }
                        else
                        {
                            LabelRezultatAdaugare.Text = "";
                            LabelCnpErr.Text = "";
                            LabelJudetErr.Text = "";
                            LabelNumeMErr.ForeColor = Color.Red;
                            LabelNumeErr.Text = "Numele nu are voie sa contina altceva in afara de litere.";
                            LabelPrenumeErr.Text = "";
                            LabelSexErr.Text = "";
                            Label22.Text = "";
                            LabelLocNastereErr.Text = "";
                            LabelTelefonErr.Text = "";
                            LabelNumeMErr.Text = "";
                            LabelNumeTErr.Text = "";
                            LabelOrasErr.Text = "";
                            LabelMedicFErr.Text = "";
                            LabelStradaErr.Text = "";
                            LabelDataNErr.Text = "";
                            LabelNumarApartamentErr.Text = "";
                            LabelScaraErr.Text = "";
                            LabelBlocErr.Text = "";
                            LabelNumarErr.Text = "";
                            LabelEmailErr.Text = "";
                        }
                    }
                    else
                    {
                        LabelRezultatAdaugare.Text = "";
                        LabelCnpErr.Text = "";
                        LabelJudetErr.Text = "";
                        LabelNumeMErr.ForeColor = Color.Red;
                        LabelNumeErr.Text = "Nu ati introdus numele.";
                        LabelPrenumeErr.Text = "";
                        LabelSexErr.Text = "";
                        Label22.Text = "";
                        LabelLocNastereErr.Text = "";
                        LabelTelefonErr.Text = "";
                        LabelNumeMErr.Text = "";
                        LabelNumeTErr.Text = "";
                        LabelOrasErr.Text = "";
                        LabelMedicFErr.Text = "";
                        LabelStradaErr.Text = "";
                        LabelDataNErr.Text = "";
                        LabelNumarApartamentErr.Text = "";
                        LabelScaraErr.Text = "";
                        LabelBlocErr.Text = "";
                        LabelNumarErr.Text = "";
                        LabelEmailErr.Text = "";
                    }
                }
                else
                {
                    LabelRezultatAdaugare.ForeColor = Color.Red;
                    LabelRezultatAdaugare.Text = "Eroare la transmiterea datelor utilizand sesiunin. Receptia nu este logata.";
                    LabelCnpErr.Text = "";
                    LabelJudetErr.Text = "";
                    LabelNumeErr.Text = "";
                    LabelPrenumeErr.Text = "";
                    LabelSexErr.Text = "";
                    Label22.Text = "";
                    LabelLocNastereErr.Text = "";
                    LabelTelefonErr.Text = "";
                    LabelNumeMErr.Text = "";
                    LabelNumeTErr.Text = "";
                    LabelOrasErr.Text = "";
                    LabelMedicFErr.Text = "";
                    LabelStradaErr.Text = "";
                    LabelDataNErr.Text = "";
                    LabelNumarApartamentErr.Text = "";
                    LabelScaraErr.Text = "";
                    LabelBlocErr.Text = "";
                    LabelNumarErr.Text = "";
                    LabelEmailErr.Text = "";
                }
            }
            catch (FormatException fe)
            {
                LabelDataNErr.ForeColor = Color.Red;
                LabelDataNErr.Text = "Data introdusa/aleasa are formatl gresit: " + fe.Message;
                LabelRezultatAdaugare.Text = "";
                LabelCnpErr.Text = "";
                LabelJudetErr.Text = "";
                LabelNumeErr.Text = "";
                LabelPrenumeErr.Text = "";
                LabelSexErr.Text = "";
                Label22.Text = "";
                LabelLocNastereErr.Text = "";
                LabelTelefonErr.Text = "";
                LabelNumeMErr.Text = "";
                LabelNumeTErr.Text = "";
                LabelOrasErr.Text = "";
                LabelMedicFErr.Text = "";
                LabelStradaErr.Text = "";
                LabelNumarApartamentErr.Text = "";
                LabelScaraErr.Text = "";
                LabelBlocErr.Text = "";
                LabelNumarErr.Text = "";
                LabelEmailErr.Text = "";
            }
            catch (Exception ex)
            {
                LabelRezultatAdaugare.ForeColor = Color.Red;
                LabelRezultatAdaugare.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelCnpErr.Text = "";
                LabelJudetErr.Text = "";
                LabelNumeErr.Text = "";
                LabelPrenumeErr.Text = "";
                LabelSexErr.Text = "";
                Label22.ForeColor = Color.Red;
                Label22.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelLocNastereErr.Text = "";
                LabelTelefonErr.Text = "";
                LabelNumeMErr.Text = "";
                LabelNumeTErr.Text = "";
                LabelOrasErr.Text = "";
                LabelMedicFErr.Text = "";
                LabelStradaErr.Text = "";
                LabelDataNErr.Text = "";
                LabelNumarApartamentErr.Text = "";
                LabelScaraErr.Text = "";
                LabelBlocErr.Text = "";
                LabelNumarErr.Text = "";
                LabelEmailErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            LabelRezultatAdaugare.ForeColor = Color.Green;
            LabelRezultatAdaugare.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelCnpErr.Text = "";
            LabelJudetErr.Text = "";
            LabelNumeErr.Text = "";
            LabelPrenumeErr.Text = "";
            LabelSexErr.Text = "";
            Label22.ForeColor = Color.Green;
            Label22.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelLocNastereErr.Text = "";
            LabelTelefonErr.Text = "";
            LabelNumeMErr.Text = "";
            LabelNumeTErr.Text = "";
            LabelOrasErr.Text = "";
            LabelMedicFErr.Text = "";
            LabelStradaErr.Text = "";
            LabelDataNErr.Text = "";
            numeTB.Text = "";
            prenumeTB.Text = "";
            cnpTB.Text = "";
            sexDDL.SelectedIndex = 0;
            locNastereTB.Text = "";
            telefonTB.Text = "";
            numeMamaTB.Text = "";
            numeTataTB.Text = "";
            dataNasteriiTB.Text = "";
            emailTB.Text = "";
            judetDDL.SelectedIndex = 0;
            orasDDL.SelectedIndex = 0;
            stradaTB.Text = "";
            numarTB.Text = "";
            blocTB.Text = "";
            scaraTB.Text = "";
            nrApartamentTB.Text = "";
            medicFamilieTB.Text = "";
            observatiiTB.Text = "";
            LabelNumarApartamentErr.Text = "";
            LabelScaraErr.Text = "";
            LabelBlocErr.Text = "";
            LabelNumarErr.Text = "";
            LabelEmailErr.Text = "";
        }
    }
}