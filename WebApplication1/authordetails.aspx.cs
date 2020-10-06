using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class authordetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control myControlMenu = Page.Master.FindControl("user");
            if (myControlMenu != null)
            {
                myControlMenu.Visible = false;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                string username, age, gender, address;
                username = txtusername.Text;
                age = txtage.Text;
                gender = rblgender.SelectedValue.ToString();
                address = Txtadd.Text;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_authoradd", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = age;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = gender;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Author Added Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = "";
                        txtage.Text = "";
                        Txtadd.Text = "";
                    }
                }
            }

            catch (Exception ex)
            {
                string display = "Issuu!Author record not saved!";
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
                    SqlCommand cmd = new SqlCommand("Sp_viewauthor", con);
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
                        txtusername.Text = dt.Rows[0]["authorname"].ToString();
                        txtage.Text = dt.Rows[0]["age"].ToString();
                        Txtadd.Text = dt.Rows[0]["address"].ToString();
                        rblgender.Text = dt.Rows[0]["gender"].ToString();
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
                password = txtage.Text;
                gender = rblgender.SelectedValue.ToString();
                address = Txtadd.Text;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_authorupdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = gender;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Author updated Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = "";
                        txtage.Text = "";
                        Txtadd.Text = "";
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                }
            }

            catch (Exception ex)
            {
                string display = "Issue!User record not saved!";
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
                        SqlCommand cmd = new SqlCommand("sp_authordelete", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;

                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            string display = "User Deleted Sucussfully!";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                            txtusername.Text = "";
                            txtage.Text = "";
                            Txtadd.Text = "";
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
                else
                {
                    string display = "Please select author to delete!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }

            }

            catch (Exception ex)
            {
                string display = "Issue!User record not saved!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                throw (ex);

            }
            finally
            {
                //command.Connection.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}