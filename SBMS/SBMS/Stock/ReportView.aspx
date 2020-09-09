<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="SBMS.Stock.ReportView" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js">
      </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>

         <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ERP_REPORT.rpt">
            </Report>
        </CR:CrystalReportSource>
  
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />


  
    </div>
    </form>
</body>
</html>
