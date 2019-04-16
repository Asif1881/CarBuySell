using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarSellBuy
{
    public partial class adminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["userU"] = null;
            Session["userA"] = null;
            //Response.Redirect("~/loginUser.aspx", true);
            if (Session["userU"] == null) Response.Redirect("~/loginUser.aspx", true);
            if (Session["userA"] == null) Response.Redirect("~/loginUser.aspx", true);
        }
    }
}