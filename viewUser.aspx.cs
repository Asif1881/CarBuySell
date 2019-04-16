using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarSellBuy
{
    public partial class viewUser : System.Web.UI.Page
    {
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["userU"] == null) Response.Redirect("~/loginUser.aspx", true);
            //if (Session["userA"] == null) Response.Redirect("~/loginUser.aspx", true);
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }
        private void LoadGrid()
        {
            DbCon();
            string query = "select * from carNew";
            DataTable dt = new DataTable();
            SqlDataAdapter _da = new SqlDataAdapter(query, con);
            _da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //newCarGridView.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                //newCarGridView.DataSource = dt;
                GridView1.DataBind();
            }
            con.Close();
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

        protected void btnDetail_Command(object sender, CommandEventArgs e)
        {
            DbCon();
            SqlCommand cmd = new SqlCommand("select * from carNew where id_N=@id_N", con);
            cmd.Parameters.AddWithValue("@id_N", e.CommandArgument);
            var dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            if (dt.Rows.Count > 0)
            {
                txtID.Text = dt.Rows[0]["id_N"].ToString();
                txtname.Text = "";
                txtbrand.Text = dt.Rows[0]["brand_N"].ToString();
                txtmodel.Text = dt.Rows[0]["model_N"].ToString();                
                txtcost.Text = dt.Rows[0]["price_N"].ToString();
                txtstatus.Text = dt.Rows[0]["status_N"].ToString();
                lblBuyShow.Text = "";
            }
            else
            {
                lblBuyShow.Text = "Something is Wrong";
                lblBuyShow.ForeColor = System.Drawing.Color.Red;
            }
            LoadGrid();
            con.Close();
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                DbCon();
                SqlCommand cmd = new SqlCommand("insert into customerInfo (id, nameCus, brandCar, modelCar, costCar, statusCar) " +
                    "values (@a,@b,@c,@d,@e,@f)", con);

                cmd.Parameters.AddWithValue("@a", txtID.Text);
                cmd.Parameters.AddWithValue("@b", txtname.Text);
                cmd.Parameters.AddWithValue("@c", txtbrand.Text);
                cmd.Parameters.AddWithValue("@d", txtmodel.Text);
                cmd.Parameters.AddWithValue("@e", Convert.ToInt32(txtcost.Text.ToString().Trim()));
                cmd.Parameters.AddWithValue("@f", txtstatus.Text);


                cmd.ExecuteNonQuery();
                lblBuyShow.Text = "Congratulations!! Your order is Confirmed!!";
                lblBuyShow.ForeColor = System.Drawing.Color.Green;
                LoadGrid();
                con.Close();
            }
            catch (Exception)
            {
                lblBuyShow.Text = "No Stock!!";
                lblBuyShow.ForeColor = System.Drawing.Color.Red;
            }
           
        }
    }
}