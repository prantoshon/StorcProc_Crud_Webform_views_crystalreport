using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SBMS.Config;

namespace SBMS.Stock
{
    public partial class PurchaseReport : System.Web.UI.Page
    {
        Conncetion con = new Conncetion();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
              
                CatagoryDropDownload();


            }
        }
        private void CatagoryDropDownload()
        {

            con.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Code+' | '+  Name as NAME, Code FROM          Catagory", con.conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            // DropDownList2.Items.Add("--SELECT Please --");
            drpCatagory.DataTextField = "NAME";
            drpCatagory.DataValueField = "Code";
            drpCatagory.DataSource = rdr;
            drpCatagory.DataBind();
            drpCatagory.Items.Insert(0, new ListItem("---Select Catagory---", "0"));
            con.conn.Close();


        }
        private void ProductDropDown(string field)
        {
            DataTable dt = new DataTable();

            try
            {
                con.conn.Open();
                string sqlStatement = "SELECT        dbo.Catagory.Code, dbo.Product.Name AS ProductName, dbo.Product.Code AS ProductCode FROM dbo.Catagory INNER JOIN  dbo.Product ON dbo.Catagory.Code = dbo.Product.CatagoryCode GROUP BY dbo.Catagory.Code, dbo.Product.Name, dbo.Product.Code HAVING(dbo.Catagory.Code = @Value1)";
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.conn);
                sqlCmd.Parameters.AddWithValue("@Value1", field);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    drpProduct.DataSource = dt;
                    drpProduct.DataTextField = "ProductName"; // the items to be displayed in the list items
                    drpProduct.DataValueField = "ProductCode"; // the id of the items displayed

                    drpProduct.DataBind();
                    drpProduct.Items.Insert(0, new ListItem("SELECT Product", "0"));
                }
                else
                {
                    drpProduct.Items.Insert(0, new ListItem("Data not Founded", "0"));
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                con.conn.Close();
            }

        }

        protected void drpCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpProduct.Items.Clear();
            ProductDropDown(drpCatagory.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Session["ReportName"] = "ERP_REPORT.rpt";
            // Session["Backlink"] = "frmMushakReport.aspx";
            Session["Qurey"] = "SELECT       * FROM dbo.PurchaseSummary  WHERE(CatagoryCode= N'" + drpCatagory.SelectedValue + "') and    (ProductCode = N'" + drpProduct.SelectedValue + "')";
            Response.Redirect("~/Stock/ReportView.aspx");
        }
    }
}