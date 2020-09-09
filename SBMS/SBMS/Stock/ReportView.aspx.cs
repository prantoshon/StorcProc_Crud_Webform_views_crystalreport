using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using SBMS.Config;

namespace SBMS.Stock
{
    public partial class ReportView : System.Web.UI.Page
    {
        Conncetion con = new Conncetion();
        protected void Page_Load(object sender, EventArgs e)
        {
            string ReportPath = "~/Stock/" + Session["ReportName"] + "";
            string sql = Session["Qurey"].ToString();
            // string sql = @"SELECT *  FROM VMushok_6_1";
            SqlCommand cmd = new SqlCommand(sql, con.conn);
            con.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.conn.Close();
            DataTable Formula = dt;
            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
            crystalReport.Load(Server.MapPath(ReportPath));
            crystalReport.SetDatabaseLogon("", "", "Localhost", "SBMS");
            crystalReport.SetDataSource(Formula);    // binding datatable
                                                     //crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Balance Sheet Report");
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.RefreshReport();
        }
    }
}