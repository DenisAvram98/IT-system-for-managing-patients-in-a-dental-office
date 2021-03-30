using AjaxControlToolkit;
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
    public partial class VizualizareRadiografii : System.Web.UI.Page
    {
        SqlConnection con;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=e:\An 3 - Sem 2\Licenta\CabStomatologic\CabStomatologic\App_Data\CabinetStomatologic.mdf;Integrated Security=True";
        string filePath = @"~\RadiografiiPoze\";
        string numeM = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            numeM = (string)Application["numeMedic"];
            try
            {
                if (!IsPostBack)
                {
                    try
                    {
                        numePacientTB.Text = Session["numePacient"].ToString();
                        cnpTB.Text = Session["cnp"].ToString();
                        LabelPrincipalErr.Text = "";
                    }
                    catch (NullReferenceException ex)
                    {
                        LabelPrincipalErr.ForeColor = Color.Red;
                        LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin: " + ex.Message;
                    }
                }

                con = new SqlConnection(conString);
                con.Open();

                if (!string.IsNullOrEmpty(numeM))
                {
                    string stmt = "select Id, Data, NumeImagine, Diagnostic, Observatii from Radiografii where CNP='" + cnpTB.Text.Trim() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(stmt, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Radiografii");
                    DataRow[] radiografii = ds.Tables["Radiografii"].Select();

                    if (cnpTB.Text.Trim() != "")
                    {
                        if (ds.Tables["Radiografii"].Rows.Count == 0)
                        {
                            LabelRadiografiiErr.ForeColor = Color.Green;
                            LabelRadiografiiErr.Text = "Pacientul cu CNP-ul " + cnpTB.Text.Trim() + " nu are nici o radiografie.";
                        }
                        else
                        {
                            int i = 0; //V2: necesar pentru versiunea cu deschidere imediata la click pe Imagine
                            foreach (DataRow nume in radiografii)
                            {
                                ImageButton ibtn = new ImageButton();
                                ibtn.ImageUrl = filePath + nume["NumeImagine"].ToString().Trim();
                                ibtn.CommandName = nume["Id"].ToString().Trim();
                                ibtn.ID = nume["NumeImagine"].ToString().Trim();
                                ibtn.Height = 150;
                                ibtn.Width = 160;
                                ibtn.Attributes.Add("Style", "padding:20px;");
                                ibtn.Attributes.Add("title", nume["NumeImagine"].ToString().Trim());
                                Panel2.Controls.Add(ibtn);
                                ibtn.Click += new ImageClickEventHandler(ibtn_Click); //V1:un click pentru a crea modalPopup si al doilea click sa il deschida

                                /*
                                Panel p = new Panel();
                                p.ID = "p" + i++;
                                p.CssClass = "modalPopup";
                                p.HorizontalAlign = HorizontalAlign.Center;
                                p.Attributes.Add("style", "display:none;");
                                Image img = new Image();
                                img.ImageUrl = filePath + nume["NumeImagine"].ToString().Trim();
                                p.Controls.Add(img);
                                p.Controls.Add(new LiteralControl("<br />"));
                                p.Controls.Add(new LiteralControl("<br />"));
                                Button btn = new Button();
                                btn.Text = "Inchide";
                                btn.ID = "btn" + i++;
                                p.Controls.Add(btn);
                                Panel3.Controls.Add(p);

                                ModalPopupExtender mpe = new ModalPopupExtender();
                                mpe.PopupControlID = p.ID;
                                mpe.TargetControlID = ibtn.ID;
                                mpe.CancelControlID = btn.ID;
                                mpe.BackgroundCssClass = "modalBackground";
                                Panel4.Controls.Add(mpe);
                                *///V2:la click pe imagine se deschide imediat modalPopup
                            }
                            LabelRadiografiiErr.ForeColor = Color.Green;
                            LabelRadiografiiErr.Text = "Pentru pacientul cu CNP-ul " + cnpTB.Text.Trim() + " au fost gasite urmatoarele radiografii.";
                            LabelPrincipalErr.Text = "";
                        }
                    }
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Eroare la transmiterea datelor utilizand sesiunin. Medicul nu este logat.";
                    LabelInformatiiRErr.Text = "";
                    LabelRadiografiiErr.Text = "";
                }
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelRadiografiiErr.Text = "";
                LabelInformatiiRErr.Text = "";
                LabelRadiografiiErr.Text = "";
            }
            finally
            {
                con.Close();
            }
        }

        private void ibtn_Click(object sender, ImageClickEventArgs e) //V1: event handler pentru click 1 si click 2
        {
            try
            {
                ImageButton ibtn = (ImageButton)sender;
                largeImg.Src = filePath + ibtn.ID;
                ModalPopupExtender mpe = new ModalPopupExtender();
                mpe.PopupControlID = "Panel3";
                mpe.TargetControlID = ibtn.ID;
                mpe.CancelControlID = "closeBtn";
                mpe.BackgroundCssClass = "modalBackground";
                Panel4.Controls.Add(mpe);

                con = new SqlConnection(conString);
                con.Open();

                string stmt = "select NumeImagine, Data, Diagnostic, Observatii from Radiografii where CNP='" + cnpTB.Text.Trim() + "' and NumeImagine='" + ibtn.ID + "' and Id='" + ibtn.CommandName + "'";
                SqlCommand sc = new SqlCommand(stmt, con);
                SqlDataReader dr = sc.ExecuteReader();
                if (dr.Read())
                {
                    DateTime data = Convert.ToDateTime(dr["Data"].ToString().Trim());
                    dataTB.Text = data.ToString("M/d/yyyy");
                    string diagnostic= dr["Diagnostic"].ToString().Trim();
                    diagnostic = diagnostic.Replace(";", "\n");
                    diagnostic = diagnostic.Replace("//", "\n- Cod diagnostic: ");
                    diagnosticTB.Text = diagnostic;
                    observatiiTB.Text = dr["Observatii"].ToString().Trim();
                    numeImagineTB.Text = dr["NumeImagine"].ToString().Trim();
                }
                LabelInformatiiRErr.Text = "";
                dr.Close();
                sc.Dispose();
            }
            catch (Exception ex)
            {
                LabelInformatiiRErr.ForeColor = Color.Red;
                LabelInformatiiRErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}