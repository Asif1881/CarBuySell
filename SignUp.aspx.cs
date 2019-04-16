using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarSellBuy
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DbCon()
        {
            try
            {
                String strCon = "Data Source=NAEEEM;Initial Catalog=CarSellBuyWeb;Integrated Security=True";
                con = new SqlConnection(strCon);
                con.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnUserSubmit_Click(object sender, EventArgs e)
        {
            DbCon();
            
            SqlCommand cmd = new SqlCommand("insert into loginU (userU, passU) values (@a,@b)", con);

            cmd.Parameters.AddWithValue("@a", tbxUserNameS.Text);
            cmd.Parameters.AddWithValue("@b", tbxUserPassS.Text);
            
            cmd.ExecuteNonQuery();
            //lblShow.Text = "Save Successfully";
            //lblShow.ForeColor = System.Drawing.Color.Green;
            con.Close();
        }

        protected void btnSignUpBack_Click(object sender, EventArgs e)
        {
            //Session["user_U"] = dt.Rows[0]["user_U"].ToString();
            Response.Redirect("~/loginUser.aspx", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/loginUser.aspx", true);
        }
    }
}