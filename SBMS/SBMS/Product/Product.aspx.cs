using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SBMS.Config;
using System.Globalization;

namespace SBMS.Product
{
    public partial class Product : System.Web.UI.Page
    {
        Conncetion con = new Conncetion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CatagoryDropDownload();
                Grid();
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
        private int Validation()
        {
            if (txtCode.Text == "")
            {
                lblError.Text = "Please Enter Code";
                txtCode.Focus();
                return 1;
            }
            if (drpCatagory.SelectedIndex == 0)
            {
                lblError.Text = "Please Select Catagory";
                drpCatagory.Focus();
                return 1;
            }
         
            if (txtCode.Text.Length < 4)
            {
                lblError.Text = "Please Enter 4 Digit  Code";
                txtCode.Focus();
                return 1;
            }
            if (txtName.Text == "")
            {
                lblError.Text = "Please Enter Name";
                txtName.Focus();
                return 1;
            }
            if (txtDate.Text == "")
            {
                lblError.Text = "Please Enter Date";
                txtDate.Focus();
                return 1;
            }
            if (txtQuantity.Text == "")
            {
                lblError.Text = "Please Enter Quatity";
                txtQuantity.Focus();
                return 1;
            }
            if (txtSuppierName.Text == "")
            {
                lblError.Text = "Please Enter Supplier Name";
                txtSuppierName.Focus();
                return 1;
            }
            if (txtSupplierPhoneNumber.Text == "")
            {
                lblError.Text = "Please Enter Supplier Number";
                txtSupplierPhoneNumber.Focus();
                return 1;
            }

            return 0;

        }
        protected void Grid()
        {
            con.conn.Open();
            DataTable dt = new DataTable();
            {
                string show = "SELECT      *  FROM dbo.GrideData";

                SqlCommand sq = new SqlCommand(show, con.conn);

                SqlDataReader sr = sq.ExecuteReader();

                dt.Load(sr);
                //  TotalRecord = dt.Rows.Count;
                dgvData.DataSource = dt;
                dgvData.DataBind();
            }
            con.conn.Close();
        }
        private int DuplicatData()
        {
            con.conn.Open();
            string selectString = "SELECT COUNT(*) FROM Product WHERE (ProductCode = @ProductCode)";
     
            SqlCommand myCommand = new SqlCommand(selectString, con.conn);
      

            myCommand.Parameters.AddWithValue("@ProductCode", txtCode.Text);
  
            var CodeExists = (Int32)myCommand.ExecuteScalar() > 0;

            if (CodeExists)
            {
                lblError.Text = "Dupicate Code";
                txtCode.Focus();
                return 1;
            }
        
            con.conn.Close();
            return 0;

        }
        private void SaveMethodCall()
        {
            SqlCommand cmd = new SqlCommand("ProductStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Insert");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@ProductName", txtName.Text);
            cmd.Parameters.AddWithValue("@CatagoryCode", drpCatagory.SelectedValue);
            con.conn.Open();
    
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {

        
                con.conn.Close();
                SaveDetails();
          

            }
      
        
        }

        private void SaveDetails()
        {
            SqlCommand cmd = new SqlCommand("ProductDetailsStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Insert");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@Qty", txtQuantity.Text);

            DateTime date = Convert.ToDateTime(txtDate.Text);
            string _modifiedDate = date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            cmd.Parameters.AddWithValue("@Date", _modifiedDate);
            //cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.Parameters.AddWithValue("@supplierName", txtSuppierName.Text);
            cmd.Parameters.AddWithValue("@supplierNumber", txtSupplierPhoneNumber.Text);

            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            con.conn.Open();

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {

                //Response.Write("<script>alert('Save Successfully');</script>");
                lblMessage.Text = "Save successfully";
                lblError.Text = "";
                con.conn.Close();
                Grid();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == 1)
            {
                return;
            }
            if (btnSave.Text == "Save")
            {
               if (DuplicatData() == 1) return;
                SaveMethodCall();

                return;
            }
            if (btnSave.Text == "Update")
            {
                UpdateMethodCall();

                return;
            }
            if (btnNew.Text == "New")
            {
                NewMethodCall();
                lblMessage.Text = "";
                return;
            }
        }

        protected void dgvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpCatagory.SelectedValue = dgvData.SelectedRow.Cells[3].Text;
            txtCode.Text = dgvData.SelectedRow.Cells[1].Text;
            txtName.Text = dgvData.SelectedRow.Cells[2].Text;
            txtQuantity.Text = dgvData.SelectedRow.Cells[4].Text;

             string DateString= dgvData.SelectedRow.Cells[5].Text;
            DateTime date = Convert.ToDateTime(DateString);
            txtDate.Text = date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            


           
            txtSuppierName.Text = dgvData.SelectedRow.Cells[7].Text;
            txtSupplierPhoneNumber.Text = dgvData.SelectedRow.Cells[8].Text;
           
            txtDescription.Text = dgvData.SelectedRow.Cells[6].Text;
            txtCode.ReadOnly = true;
            drpCatagory.Enabled = false;
            btnSave.Text = "Update";
            btnNew.Text = "Delete";
            lblError.Text = "";
            lblMessage.Text = "";
        }
        private void NewMethodCall()
        {
            drpCatagory.Enabled = true;
            CatagoryDropDownload();
            txtCode.ReadOnly = false;
            txtCode.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtDate.Text = "";
            txtSuppierName.Text = "";
            txtSupplierPhoneNumber.Text = "";
  
            txtDescription.Text = "";
            btnSave.Text = "Save";
            lblError.Text = "";
            lblMessage.Text = "";
            Grid();
        }
        protected void SearchMethod()
        {
            con.conn.Open();
            DataTable dt = new DataTable();
            {
                string show = "SELECT      *  FROM GrideData where (ProductCode='" + txtCode.Text + "') ";

                SqlCommand sq = new SqlCommand(show, con.conn);

                SqlDataReader sr = sq.ExecuteReader();

                dt.Load(sr);
                int TotalRecord = dt.Rows.Count;

                dgvData.DataSource = dt;
                dgvData.DataBind();
                if (TotalRecord == 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Data Not Founded')</script>");
                    return;
                }

            }
            con.conn.Close();
        }
        private void Delete()
        {
            SqlCommand cmd = new SqlCommand("ProductStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
      

            con.conn.Open();
            int Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
             
                con.conn.Close();
                DeleteDetails();
                btnNew.Text = "New";
            }
        }

        private void DeleteDetails()
        {
            SqlCommand cmd = new SqlCommand("ProductDetailsStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@supplierName", txtSuppierName.Text);
            con.conn.Open();

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {


                string msg = "Successfully Delete";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + msg + "');", true);
                con.conn.Close();
                Grid();

            }
        }
        private void UpdateMethodCall()
        {
            SqlCommand cmd = new SqlCommand("ProductStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@ProductName", txtName.Text);
            cmd.Parameters.AddWithValue("@CatagoryCode", drpCatagory.SelectedValue);

            con.conn.Open();
            int Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
               
                con.conn.Close();
                UpdateDetails();
             
                btnSave.Text = "Save";
                btnNew.Text = "New";
            }

        }
        private void UpdateDetails()
        {
            SqlCommand cmd = new SqlCommand("ProductDetailsStorePrco", con.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@ProductCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@Qty", txtQuantity.Text);

            DateTime date = Convert.ToDateTime(txtDate.Text);
            string _modifiedDate = date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            cmd.Parameters.AddWithValue("@Date", _modifiedDate);
            //cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.Parameters.AddWithValue("@supplierName", txtSuppierName.Text);
            cmd.Parameters.AddWithValue("@supplierNumber", txtSupplierPhoneNumber.Text);

            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            con.conn.Open();

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {

                //Response.Write("<script>alert('Save Successfully');</script>");
                lblMessage.Text = "Update successfully";
                lblError.Text = "";
                con.conn.Close();
                Grid();

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                lblError.Text = "Please Enter Code";
                txtCode.Focus();
                return;
            }
            lblError.Text = "";
            SearchMethod();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (btnNew.Text == "Delete")
            {
                Delete();
            }
            if (btnNew.Text == "New")
            {
               NewMethodCall();
            }
        }
    }
}