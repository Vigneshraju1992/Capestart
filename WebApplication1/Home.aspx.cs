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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataTable Login(string UserName, string Password)
        {

            SqlCommand command = new SqlCommand();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    dt = ds.Tables[0];
                    con.Close();
                }

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;

                }
            }

            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                //command.Connection.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txt_Username.Text;
                string password = txt_PWD.Text;
                DataTable userdet = Login(username, password);
                

                if (userdet.Rows.Count > 0)
                {
                    string Isadmin = userdet.Rows[0]["IsAdmin"].ToString(); 
                    if (Isadmin == "1")
                    {
                        Response.Redirect("userdetails.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("lendbook.aspx", false);
                    }
                }
                else
                {
                    string display = "No User record found!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}