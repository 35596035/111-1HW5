using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace _111_1HW5
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s_Str = ConfigurationManager.ConnectionStrings["SQLLOCALDB"].ConnectionString;
            if (!IsPostBack)
            {
                try
                {
                    SqlConnection o_Str = new SqlConnection(s_Str);
                    o_Str.Open();
                    SqlDataAdapter o_A = new SqlDataAdapter("Select * from Users", o_Str);
                    DataSet o_D = new DataSet();
                    o_A.Fill(o_D, "ds_Res");//SQL轉接器物件名稱.Fill(資料集物件名稱, 資料表名稱);
                    gd_View.DataSource = o_D;
                    gd_View.DataBind();
                    o_Str.Close();
                }
                catch (Exception o_ex)
                {
                    Response.Write(o_ex.ToString());
                }
            }
        }

        protected void btn_Insert_Click(object sender, EventArgs e)
        {
            string s_Str = ConfigurationManager.ConnectionStrings["SQLLOCALDB"].ConnectionString;
            try
            {
                SqlConnection o_Str = new SqlConnection(s_Str);
                SqlDataAdapter o_A = new SqlDataAdapter("Select * from Users", o_Str);
                o_Str.Open();
                SqlCommand o_cmd = new SqlCommand("Insert into Users (Name, Birthday)" + "values(@Name, @DateTime)", o_Str);
                o_cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                o_cmd.Parameters["@Name"].Value = "阿貓阿狗";
                o_cmd.Parameters.Add("@DateTime", SqlDbType.DateTime);
                o_cmd.Parameters["@DateTime"].Value = "2000/10/10";
                o_cmd.ExecuteNonQuery();
                Response.Redirect("https://localhost:44393/Test.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                o_Str.Close();
            }
            catch (Exception o_ex)
            {
                Response.Write(o_ex.ToString());
            }
        }
    }
}