using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class VizualizareFisaPacientMedic : System.Web.UI.Page
    {
        string numeM = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            numeM = (string)Application["numeMedic"];
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(numeM))
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
                    }
                    else
                    {
                        LabelPrincipalErr.ForeColor = Color.Red;
                        LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
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
                if (numeTB.Text != "" && !string.IsNullOrEmpty(numeM))
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
                    LabelRezultatPrintareErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                }
            }
            catch (Exception ex)
            {
                LabelRezultatPrintareErr.ForeColor = Color.Red;
                LabelRezultatPrintareErr.Text = "Eroare: " + ex.Message;
            }
        }
    }
}