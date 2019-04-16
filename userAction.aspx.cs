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
    public partial class userAction : System.Web.UI.Page
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
        void clearLabel()
        {
            lblMessage.Text = "";
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
        private void LoadGrid()
        {
            DbCon();
            string query = "select * from carNew";
            DataTable dt = new DataTable();
            SqlDataAdapter _da = new SqlDataAdapter(query, con);
            _da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                newCarGridView.DataBind();
            }
            
            con.Close();
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            clearLabel();
            DbCon();
            SqlCommand cmd = new SqlCommand("insert into carNew (id_N, brand_N, model_N, cc_N, reg_N, price_N, status_N) values (@a,@b,@c,@d,@e,@f,@g)", con);
            
            cmd.Parameters.AddWithValue("@a", txtCarID.Text);
            cmd.Parameters.AddWithValue("@b", tbxCarBrand.Text);
            cmd.Parameters.AddWithValue("@c", tbxCarModel.Text);
            cmd.Parameters.AddWithValue("@d", Convert.ToInt32(tbxCarCC.Text.ToString().Trim()));
            cmd.Parameters.AddWithValue("@e", txtCarRegDate.Text);
            cmd.Parameters.AddWithValue("@f", Convert.ToInt32(txtCarPrice.Text.ToString().Trim()));
            cmd.Parameters.AddWithValue("@g", txtCarStatus.Text);
            cmd.ExecuteNonQuery();
            lblMessage.Text = "Saved Successfully";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            LoadGrid();
            con.Close();
        }


        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            clearLabel();
            DbCon();
            SqlCommand cmd = new SqlCommand("select * from carNew where id_N=@id_N", con);
            cmd.Parameters.AddWithValue("@id_N", e.CommandArgument);
            var dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            if (dt.Rows.Count > 0)
            {
                txtCarID.Text      = dt.Rows[0]["id_N"].ToString();
                tbxCarBrand.Text   = dt.Rows[0]["brand_N"].ToString();
                tbxCarModel.Text   = dt.Rows[0]["model_N"].ToString();
                tbxCarCC.Text      = dt.Rows[0]["cc_N"].ToString();
                txtCarRegDate.Text = dt.Rows[0]["reg_N"].ToString();
                txtCarPrice.Text   = dt.Rows[0]["price_N"].ToString();
                txtCarStatus.Text  = dt.Rows[0]["status_N"].ToString();
            }
            else
            {
                lblMessage.Text = "Save Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            LoadGrid();
            con.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clearLabel();
            try
            {
                DbCon();
                SqlCommand cmd = new SqlCommand("update carNew set id_N=@id_N, brand_N=@brand_N, model_N=@model_N, cc_N=@cc_N, " +
                    "reg_N=@reg_N, price_N=@price_N, status_N=@status_N where id_N=@id_N", con);
                
                cmd.Parameters.AddWithValue("@id_N", txtCarID.Text);
                cmd.Parameters.AddWithValue("@brand_N", tbxCarBrand.Text);
                cmd.Parameters.AddWithValue("@model_N", tbxCarModel.Text);
                cmd.Parameters.AddWithValue("@cc_N", Convert.ToInt32(tbxCarCC.Text.ToString().Trim()));
                cmd.Parameters.AddWithValue("@reg_N", txtCarRegDate.Text);
                cmd.Parameters.AddWithValue("@price_N", Convert.ToInt32(txtCarPrice.Text.ToString().Trim()));
                cmd.Parameters.AddWithValue("@status_N", txtCarStatus.Text);

                cmd.ExecuteNonQuery();
                lblMessage.Text = "Updated Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                con.Close();
                LoadGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            clearLabel();
            try
            {
                DbCon();
                SqlCommand cmd = new SqlCommand("delete carNew where id_N=@id_N", con);
                cmd.Parameters.AddWithValue("@id_N", e.CommandArgument);
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Delete Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                con.Close();
                LoadGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCarID.Text = "";
            tbxCarBrand.Text = "";
            tbxCarModel.Text = "";
            tbxCarCC.Text = "";
            txtCarRegDate.Text = "";
            txtCarPrice.Text = "";
            txtCarStatus.Text = "";
            clearLabel();
        }
    }
}