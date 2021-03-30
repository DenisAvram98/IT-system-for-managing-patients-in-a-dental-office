using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CabStomatologic
{
    public partial class Logare : System.Web.UI.Page
    {
        protected void contPacientBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContPacientAutentificare.aspx");
        }

        protected void contMedicBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContMedicPage.aspx");
        }

        protected void contReceptieBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContReceptieAutentificare.aspx");
        }
    }
}