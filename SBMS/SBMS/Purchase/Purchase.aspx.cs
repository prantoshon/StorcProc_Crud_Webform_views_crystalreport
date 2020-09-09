using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using SBMS.Config;

namespace SBMS.Purchase
{
    public partial class Purchase : System.Web.UI.Page
    {
        Conncetion con = new Conncetion();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SupplierDropDownload();
                VoucherNoSl();
                CatagoryDropDownload();
                
                btnSave.Visible = false;
                btnDelete.Visible = false;
                PurchaseCodeSl();
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[9] {

            new DataColumn("Code"),new DataColumn("Manufactured Date"),
            new DataColumn("Expire Date"),new DataColumn("Quatity"),
            new DataColumn("Unit Price"),new DataColumn("Total Price"),
            new DataColumn("MRP"),new DataColumn("Remarks"),new DataColumn("Product Code")
        });
                ViewState["Info"] = dt;
                this.BindGrid();

            }

            

        }

        private int Validation()
        {
            if (drpSupplier.SelectedIndex == 0)
            {
                lblError.Text = "Please select Supplier";

                return 1;
            }
            if (drpCatagory.SelectedIndex == 0)
            {
                lblError.Text = "Please select Catagory";
                return 1;
            }
            if (drpProduct.SelectedIndex == 0)
            {
                lblError.Text = "Please select Product";
                return 1;
            }
            if (txtQuantity.Text=="")
            {
                lblError.Text = "Quatity Required";
                return 1;
            }
            if (txtUnitPrice.Text == "")
            {
                lblError.Text = "Unit Price Required";
                return 1;
            }
            if (txtTotalPrice.Text == "")
            {
                lblError.Text = "Total Price Required";
                return 1;
            }
            return 0;
        }

        protected void BindGrid()
        {
            dgvData.DataSource = (DataTable)ViewState["Info"];
            dgvData.DataBind();
        }
        private void SupplierDropDownload()
        {

           con.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT SupplierCode+' | '+  SupplierName as NAME, SupplierCode FROM          Supplier", con.conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            // DropDownList2.Items.Add("--SELECT Please --");
            drpSupplier.DataTextField = "NAME";
            drpSupplier.DataValueField = "SupplierCode";
            drpSupplier.DataSource = rdr;
            drpSupplier.DataBind();
            drpSupplier.Items.Insert(0, new ListItem("---Select Supplier---", "0"));
            con.conn.Close();


        }
        private void VoucherNoSl()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT         MAX(PurchaseVoucher)+1 AS PurchaseVoucher FROM             Purchase", con.conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con.conn;
                con.conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                   txtVoucherNo.Text = sdr["PurchaseVoucher"].ToString();
                    txtVoucherNo.ReadOnly = true;
                }
                con.conn.Close();
            }
        }

        private void PurchaseCodeSl()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT         Min(PurchaseCode)-1 AS PurchaseCode FROM             Purchase", con.conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con.conn;
                con.conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    txtCode.Text = sdr["PurchaseCode"].ToString();
                    txtCode.ReadOnly = true;
                }
                con.conn.Close();
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

        protected void drpProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAvailableQty.Text = "";
            AvaQtyLoad();
        }
        private void AvaQtyLoad()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT        RecordLevel, Name, Code FROM            dbo.Product WHERE(Name = N'" + drpProduct.SelectedItem.Text+"')", con.conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con.conn;
                con.conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    txtAvailableQty.Text = sdr["RecordLevel"].ToString();

                }
                con.conn.Close();
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            txtTotalPrice.Text = "";
            txtMRK.Text = "";
            if (txtQuantity.Text == "" || txtUnitPrice.Text == "")
            {
                return;
            }

            txtTotalPrice.Text = (Convert.ToDouble((txtUnitPrice.Text)) * Convert.ToDouble((txtQuantity.Text))).ToString();
            double MRP = Convert.ToDouble(txtUnitPrice.Text) + Convert.ToDouble(txtUnitPrice.Text) * .25;
            txtMRK.Text = MRP.ToString();
       
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            txtTotalPrice.Text = "";
            if (txtUnitPrice.Text == "" || txtQuantity.Text == "")
            {
                return;
            }
            txtTotalPrice.Text = (Convert.ToDouble((txtUnitPrice.Text)) * Convert.ToDouble((txtQuantity.Text))).ToString();
        }
     

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validation() == 1)
            {
                return;
            }
            if (btnAdd.Text == "Add")
            {
                lblError.Text = "";
                lblMessage.Text = "";
                AddMethod();
                return;
            }
            if (btnAdd.Text == "Update")
            {
                lblError.Text = "";
                lblMessage.Text = "";
                Update();
                return;
            }
        }
        private void Update()
        {
            string sql = @"Update  PurchaseDetails
                         set   ManufactureDate=@ManufactureDate,ExpireDate=@ExpireDate, Quantity=@Quantity, UnitPrice=@UnitPrice, TotalPrice=@TotalPrice, Remarks=@Remarks, MRP=@MRP where( code ='" + txtCode.Text+ "'  and ProductCode='"+drpProduct.SelectedValue+"')";

            SqlCommand MyCommand = new SqlCommand(sql, con.conn);

           
            MyCommand.Parameters.AddWithValue("@ManufactureDate", txtManufactureDate.Text);
            MyCommand.Parameters.AddWithValue("@ExpireDate", txtExpireDate.Text);
            MyCommand.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
            MyCommand.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
            MyCommand.Parameters.AddWithValue("@TotalPrice", txtTotalPrice.Text);
            MyCommand.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
            MyCommand.Parameters.AddWithValue("@MRP", txtMRK.Text);
            con.conn.Open();
            int Result = MyCommand.ExecuteNonQuery();
            con.conn.Close();

            if (Result > 0)
            {
             
                PurchaseCodeSl();
                lblMessage.Text = "Successfully Upadted.";
                SerachDetails();
            }
            else
            {

            }
        }
        protected void dgvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string item="";
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   item = e.Row.Cells[0].Text;
                    foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = item;
            }
        }

        protected void dgvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Info"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Info"] = dt;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == 1) { return; }
            string sql = @"INSERT INTO  Purchase
                            (PurchaseCode,PurchaseVoucher,PurchaseDate,SuppilerCode,CatagoryCode)
                            VALUES        (@PurchaseCode,@PurchaseVoucher,@PurchaseDate,@SuppilerCode,@CatagoryCode)";

            SqlCommand MyCommand = new SqlCommand(sql, con.conn);

            MyCommand.Parameters.AddWithValue("@PurchaseCode", txtCode.Text);
            MyCommand.Parameters.AddWithValue("@PurchaseVoucher", txtVoucherNo.Text);
            MyCommand.Parameters.AddWithValue("@PurchaseDate", txtDate.Text);
            MyCommand.Parameters.AddWithValue("@SuppilerCode", drpSupplier.SelectedValue);
            MyCommand.Parameters.AddWithValue("@CatagoryCode", drpCatagory.SelectedValue);

            //MyCommand.Parameters.AddWithValue("@PurchaseVoucher", txtVoucherNo);
            //MyCommand.Parameters.AddWithValue("@ManufactureDate", CustomerName);



            con.conn.Open();
            int Result = MyCommand.ExecuteNonQuery();
            con.conn.Close();

            if (Result == 1)
            {
                SaveDetails();
                PurchaseCodeSl();
                lblMessage.Text = "Successfully saved.";
            }
            else
            {

            }
        }
        private void AddMethod()
        {
           
            DataTable dt = (DataTable)ViewState["Info"];
            dt.Rows.Add(txtCode.Text, txtManufactureDate.Text, txtExpireDate.Text, txtQuantity.Text, txtUnitPrice.Text, txtTotalPrice.Text, txtMRK.Text, txtRemarks.Text, drpProduct.SelectedValue);

            ViewState["Info"] = dt;
            this.BindGrid();
            btnSave.Visible = true;
            drpCatagory.Enabled = false;
        }
        private void SaveDetails()
        {
            SqlCommand com;
            foreach (GridViewRow g1 in dgvData.Rows)
            {
                com = new SqlCommand("insert into PurchaseDetails(Code,Remarks,ManufactureDate,ExpireDate,Quantity,UnitPrice,TotalPrice,MRP,ProductCode) values ('" + g1.Cells[1].Text + "','"+g1.Cells[8].Text+ "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "','" + g1.Cells[4].Text + "','" + g1.Cells[5].Text + "','" + g1.Cells[6].Text + "','" + g1.Cells[7].Text + "','" + g1.Cells[9].Text + "')", con.conn);

                con.conn.Open();
                com.ExecuteNonQuery();

                con.conn.Close();
            }
           
      
        }

        protected void dgvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txtCode.Text = dgvData.SelectedRow.Cells[1].Text;
            string date = dgvData.SelectedRow.Cells[2].Text; ;
            
            txtManufactureDate.Text = date;


            txtExpireDate.Text = dgvData.SelectedRow.Cells[3].Text;
            txtQuantity.Text = dgvData.SelectedRow.Cells[4].Text;
            txtUnitPrice.Text = dgvData.SelectedRow.Cells[5].Text;
            txtRemarks.Text = dgvData.SelectedRow.Cells[8].Text;
            ProductDropDown(drpCatagory.SelectedValue);
            drpProduct.SelectedValue = dgvData.SelectedRow.Cells[9].Text;

            
            txtMRK.Text = dgvData.SelectedRow.Cells[7].Text;
            txtTotalPrice.Text = dgvData.SelectedRow.Cells[6].Text;
            txtCode.ReadOnly = true;
            drpCatagory.Enabled = false;
            drpProduct.Enabled = false;
            btnAdd.Text = "Update";
            btnDelete.Visible = true;
        }
        private void Delete()
        {
            string sql = @"DELETE FROM            dbo.PurchaseDetails
                            WHERE (Code= '" + txtCode.Text+ "' AND ProductCode='"+drpProduct.SelectedValue+"')";

            SqlCommand MyCommand = new SqlCommand(sql, con.conn);
            con.conn.Open();
            int Result = MyCommand.ExecuteNonQuery();
            con.conn.Close();

            if (Result >0)
            {
              
                PurchaseCodeSl();
                lblMessage.Text = "Successfully Deleted.";
              //  SerachDetails();
            }
            else
            {

            }
        }
         protected void SearchMethod()
        {
           
            using (SqlCommand cmd = new SqlCommand("SELECT     * FROM            SearchByPurchaseCode  WHERE (PurchaseCode = N'"+txtFind.Text+"')", con.conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con.conn;
                con.conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    //txtAvailableQty.Text = sdr["RecordLevel"].ToString();
                    txtVoucherNo.Text = sdr["PurchaseVoucher"].ToString();
                    drpSupplier.SelectedValue= sdr["SuppilerCode"].ToString();
                    string s= sdr["Code"].ToString();
                    drpCatagory.SelectedValue = s.ToString();
;                 //   
                    string DateString = sdr["PurchaseDate"].ToString();
                    DateTime date = Convert.ToDateTime(DateString);
                    string _modifiedDate = date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
                    txtDate.Text = _modifiedDate;

              }
                con.conn.Close();
                SerachDetails();

            }
        }
        private void SerachDetails()
        {

            con.conn.Open();
            DataTable dt = new DataTable();
            {
                string show = "SELECT     * FROM            SerachDetails  WHERE (Code = N'" + txtFind.Text + "')";

                SqlCommand sq = new SqlCommand(show, con.conn);

                SqlDataReader sr = sq.ExecuteReader();
               // drpProduct.SelectedValue = sr["ProductCode"].ToString();
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
        protected void btnFind_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            if (txtFind.Text == "") {
                string sMsg = "Code Required";

                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + sMsg + "');", true);
                return; }
            SearchMethod();
       
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                lblError.Text = "Code Reqired!"; return;
            }
            if (drpProduct.SelectedIndex == -1)
            {
                lblError.Text = "Product!"; return;
            }
            Delete();
        }
    }
}