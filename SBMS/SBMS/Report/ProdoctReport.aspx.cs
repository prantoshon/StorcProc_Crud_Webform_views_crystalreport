using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBMS.Report
{
    public partial class ProdoctReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                lblError.Text = "Enter Start Date";
                txtDate.Focus();
                return;
            }
            if (txtEndDate.Text == "")
            {
                lblError.Text = "Enter End Date";
                txtEndDate.Focus();
                return;
            }
            string dateFrom ;
            string dateTo ;

            string DateString = txtDate.Text;
            DateTime date = Convert.ToDateTime(DateString);
            dateFrom = date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            string DateEndString = txtEndDate.Text;
            DateTime dateEnd = Convert.ToDateTime(DateEndString);
            dateTo = dateEnd.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            Session["ReportName"] = "StockReport.rpt";
            // Session["Backlink"] = "frmMushakReport.aspx";
            Session["Qurey"] = "SELECT       * FROM dbo.ReportView WHERE (Date BETWEEN CONVERT(DATETIME,'" + dateFrom + "',102) AND CONVERT(DATETIME,'" + dateTo + "',102)) " ;
            Response.Redirect("~/Report/ProductReportViewer.aspx");
        }
    }
}