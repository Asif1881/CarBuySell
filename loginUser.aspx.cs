using System;
using System.Data;
using System.Data.SqlClient;

namespace CarSellBuy
{
    public partial class loginUser : System.Web.UI.Page
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

        

        

        protected void btnUserLogIn_Click(object sender, EventArgs e)
        {
            String userU = tbxUserName.Text;
            String passU = tbxUserPass.Text;
            DbCon();

            SqlCommand cmd = new SqlCommand("select * from loginU where userU=@userU and passU=@passU", con);
            cmd.Parameters.AddWithValue("@userU", userU);
            cmd.Parameters.AddWithValue("@passU", passU);


            var dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            if (dt.Rows.Count > 0)
            {
                //session
                Session["userU"] = dt.Rows[0]["userU"].ToString();
                Response.Redirect("~/userHomePage.aspx", true);
  
                //Cookies
                //Response.Cookies["UserName"].Value= dt.Rows[0]["UserName"].ToString();
                //Response.Cookies["UserName"].Expires = DateTime.Now.AddHours(1);

                //OR
                //HttpCookie cok = new HttpCookie("cookieName");
                //cok.Value= dt.Rows[0]["UserName"].ToString();
                //cok.Expires = DateTime.Now.AddHours(1);
                //Response.Cookies.Add(cok);
                lblMessageShow.Text = "Login Successed";
            }
            else
            {
                lblMessageShow.Text = "Login Failed";
            }
            con.Close();
        }

        protected void btnAdminLogIn_Click(object sender, EventArgs e)
        {
            String userA = tbxAdminName.Text;
            String passA = tbxAdminPass.Text;
            DbCon();

            SqlCommand cmd = new SqlCommand("select * from loginA where userA=@userA and passA=@passA", con);
            cmd.Parameters.AddWithValue("@userA", userA);
            cmd.Parameters.AddWithValue("@passA", passA);



            var dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            if (dt.Rows.Count > 0)
            {
                //session
                Session["userA"] = dt.Rows[0]["userA"].ToString();
                Response.Redirect("~/adminHome.aspx", true);

                //Cookies
                //Response.Cookies["UserName"].Value= dt.Rows[0]["UserName"].ToString();
                //Response.Cookies["UserName"].Expires = DateTime.Now.AddHours(1);

                //OR
                //HttpCookie cok = new HttpCookie("cookieName");
                //cok.Value= dt.Rows[0]["UserName"].ToString();
                //cok.Expires = DateTime.Now.AddHours(1);
                //Response.Cookies.Add(cok);
                lblMessageShow.Text = "Login Successed";
            }
            else
            {
                lblMessageShow.Text = "Login Failed";
            }
            con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SignUp.aspx", true);
        }
    }
}