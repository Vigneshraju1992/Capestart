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
    public partial class Bookdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control myControlMenu = Page.Master.FindControl("user");
            if (myControlMenu != null)
            {
                myControlMenu.Visible = false;
            }
            if (!IsPostBack)
            {
                Author();
                Publisher();
            }
            
        }

        public DataTable Author()
        {
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_viewauthordet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0]["id"].ToString();
                    txtusername.Text = dt.Rows[0]["authorname"].ToString();
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = dt.Columns["authorname"].ToString();
                    DropDownList1.DataValueField = dt.Columns["id"].ToString();
                    DropDownList1.DataBind();
                }
                con.Close();
            }
            return dt;
        }

        public DataTable Publisher()
        {
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_viewpubdetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblID.Text = dt.Rows[0]["id"].ToString();
                    txtusername.Text = dt.Rows[0]["publishername"].ToString();
                    DropDownList2.DataSource = dt;
                    DropDownList2.DataTextField = dt.Columns["publishername"].ToString();
                    DropDownList2.DataValueField = dt.Columns["id"].ToString();
                    DropDownList2.DataBind();
                }
                con.Close();
            }
            return dt;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                string bookname, page , URL;
                int iAut, iPub = 0;
                bookname = txtusername.Text;
                page = Txtdate.Text;
                URL = FileUpload1.FileName;
                iAut = DropDownList1.SelectedIndex;
                iPub = DropDownList2.SelectedIndex;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_bookadd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bookname", SqlDbType.VarChar).Value = bookname;
                    cmd.Parameters.Add("@page", SqlDbType.VarChar).Value = page;
                    cmd.Parameters.Add("@URL", SqlDbType.VarChar).Value = URL;
                    cmd.Parameters.Add("@iAut", SqlDbType.Int).Value = iAut;
                    cmd.Parameters.Add("@iPub", SqlDbType.Int).Value = iPub;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Book Added Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = ""; 
                        Txtdate.Text = "";
                        DropDownList1.SelectedIndex = 0;
                        DropDownList2.SelectedIndex = 0;
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
                    SqlCommand cmd = new SqlCommand("Sp_viewbook", con);
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
                        txtusername.Text = dt.Rows[0]["bookname"].ToString();
                        Txtdate.Text = dt.Rows[0]["page"].ToString();
                        DropDownList1.SelectedIndex = Convert.ToInt32(dt.Columns["author"].ToString());
                        DropDownList2.SelectedIndex = Convert.ToInt32(dt.Columns["publisher"]);
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
                string bookname, page, URL, id;
                int iAut, iPub = 0;
                bookname = txtusername.Text;
                page = Txtdate.Text;
                URL = FileUpload1.FileName;
                iAut = DropDownList1.SelectedIndex;
                iPub = DropDownList2.SelectedIndex;
                if (lblID.Text != null)
                {
                    id = lblID.Text;
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_bookupdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bookname", SqlDbType.VarChar).Value = bookname;
                    cmd.Parameters.Add("@page", SqlDbType.VarChar).Value = page;
                    cmd.Parameters.Add("@URL", SqlDbType.VarChar).Value = URL;
                    cmd.Parameters.Add("@iAut", SqlDbType.Int).Value = iAut;
                    cmd.Parameters.Add("@iPub", SqlDbType.Int).Value = iPub;

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string display = "Book updated Sucussfully!";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                        txtusername.Text = "";
                        txtusername.Text = "";
                        Txtdate.Text = "";
                        DropDownList1.SelectedIndex = 0;
                        DropDownList2.SelectedIndex = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                string display = "Issue!Book record not saved!";
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
            string display = "Book record not saved!";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
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
    }
}