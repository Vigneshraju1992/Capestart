using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class publisherdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control myControlMenu = Page.Master.FindControl("user");
            if (myControlMenu != null)
            {
                myControlMenu.Visible = false;
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",

                "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FF9955';");

                e.Row.Attributes.Add("style", "cursor:pointer;");

                e.Row.Attributes.Add("onmouseout",

                "this.style.backgroundColor=this.originalstyle;");


                e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                string username, address;
                username = txtusername.Text;
                address = Txtadd.Text;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_publisherdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Publisher Added Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = "";
                        Txtadd.Text = "";
                    }
                }
            }

            catch (Exception ex)
            {
                string display = "Issuee!Publisher record not saved!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                throw (ex);

            }
            finally
            {
                //command.Connection.Close();
            }

        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                string username;
                username = txtusername.Text;
                SqlCommand command = new SqlCommand();
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_viewpub", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    int id = 0;
                    //string name, address, gender, password = string.Empty;
                    if (dt.Rows.Count > 0)
                    {
                        lblID.Text = dt.Rows[0]["id"].ToString();
                        txtusername.Text = dt.Rows[0]["PublisherName"].ToString();
                        Txtadd.Text = dt.Rows[0]["address"].ToString();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }

                    con.Close();

                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                string username, password, gender, address;
                string id = "0";
                if (lblID.Text != null)
                {
                    id = lblID.Text;
                }
                username = txtusername.Text;
                address = Txtadd.Text;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Publisherupdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Publisher updated Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = "";
                        Txtadd.Text = "";
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                }
            }

            catch (Exception ex)
            {
                string display = "Issue!Publisher record not saved!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                throw (ex);

            }
            finally
            {
                //command.Connection.Close();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                string id = "0";
                if (lblID.Text != null)
                {
                    id = lblID.Text;
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_pubdelete", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;

                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            string display = "Publisher  Deleted Sucussfully!";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                            txtusername.Text = "";
                            Txtadd.Text = "";
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
                else
                {
                    string display = "Please select Publisher  to delete!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }

            }

            catch (Exception ex)
            {
                string display = "Issue!Publisher  record not saved!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                throw (ex);

            }
            finally
            {
                //command.Connection.Close();
            }
        }
    }
}