<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="SBMS.Purchase.Purchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <a href="css/"></a><link href="css/pikaday.css" rel="stylesheet" />
    <link href="css/site.css" rel="stylesheet" />
    <link href="css/theme.css" rel="stylesheet" />
    <script src="pikaday.js"></script>
         
       <link rel="stylesheet" href="css/foundation.css">
    <link rel="stylesheet" href="css/app.css">
    <style>.error_txt{margin-bottom: 10px; display: block; color: #f00; font-size: 13px;}
.inactive{display:none;}

/*foundation-example*/
#foundation-example{}
.foundation-example__ex-wrapper{
	background-color: lightblue;
}
.foundation-example__in-wrapper{  
	margin: auto;
    max-width: 750px;
    background: white;
    padding: 30px;
}

.foundation-example__title{  
	text-align: center;
    font-size: 40px;
    margin-bottom: 32px;
}

.foundation-example__form-wrapper{  

}
</style>
    <style>
table {
  /*font-family: arial, sans-serif;*/
  border-collapse: collapse;
  width: 90%;
}

td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 25px;
}

tr:nth-child(even) {
  background-color: #dddddd;
}
        .auto-style1 {
            width: 56%;
        }
   
        .auto-style2 {
            margin-top: 0px;
        }
   
    </style>
     <body>

  	<section id="foundation-example">
  		<div style="height: 30px;background: lightblue;"></div>
  		<div class="foundation-example__ex-wrapper">
  			<div class="foundation-example__in-wrapper">
	  			<div class="foundation-example__title"> Purchase</div>
	  			<div class="foundation-example__form-wrapper">
                  
        
                      <center>
                      <table class="auto-style1">

                          <tr>
    <td>  <asp:Button ID="btnFind" class="button tiny" Width="80"  runat="server" Text="Find" OnClick="btnFind_Click" /></td>

    <td> 
 
        <asp:TextBox ID="txtFind" runat="server" placeholder="Search By Code" ></asp:TextBox>
  </td>

    </tr>
  <tr>
    <td>Date</td>

    <td> 
<asp:TextBox ID="txtDate" runat="server" TextMode="Date" ></asp:TextBox>
  </td>

    </tr>
  
  <tr>
    <td>Voucher No</td>
    <td><asp:TextBox ID="txtVoucherNo" runat="server"></asp:TextBox></td>
   
  </tr>
 <tr>
    <td>Supplier</td>
    <td><asp:DropDownList ID="drpSupplier" runat="server"></asp:DropDownList></td>
   
  </tr>

</table>
     </center>           
	<asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
                
					
                      	<div class="row">
							<div class="large-6 columns">
								<label>Catagory
								<asp:DropDownList ID="drpCatagory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCatagory_SelectedIndexChanged" ></asp:DropDownList>
								</label>
							</div>
      
                     
							<div class="large-6 columns">
								<label>Quantity
					<asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="True" OnTextChanged="txtQuantity_TextChanged" ></asp:TextBox>
							    </label>
							</div>
						</div>
                    	<div class="row">
							<div class="large-6 columns">
								<label>Product
								<asp:DropDownList ID="drpProduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpProduct_SelectedIndexChanged"></asp:DropDownList>
								</label>
							</div>
       
							<div class="large-6 columns">
								<label>Unit Price
					<asp:TextBox ID="txtUnitPrice" runat="server" OnTextChanged="txtUnitPrice_TextChanged" AutoPostBack="True" ></asp:TextBox>
							    </label>
							</div>
						</div>
    
       
           	<div class="row">
							<div class="large-6 columns">
								<label>Code
							<asp:TextBox ID="txtCode" runat="server"  MaxLength="4"></asp:TextBox>
								</label>
							</div>
               
							<div class="large-6 columns">
								<label>Total Price
					<asp:TextBox ID="txtTotalPrice" runat="server" AutoPostBack="True"  ></asp:TextBox>
							    </label>
							</div>

						</div>      
                              	<div class="row">
							<div class="large-6 columns">
								<label>Available Qty
							                   <asp:TextBox ID="txtAvailableQty" runat="server" ReadOnly="True"></asp:TextBox>
								</label>
							</div>
     
               
							<div class="large-6 columns">
			
                                	<label> MRK
					<asp:TextBox ID="txtMRK" runat="server"  ></asp:TextBox>
							    </label>
							    </label>
							</div>
						</div>                             
 <%--    </ContentTemplate>
     <Triggers>
      <asp:AsyncPostbackTrigger ControlID="drpCatagory" EventName="SelectedIndexChanged" />
           <asp:AsyncPostbackTrigger ControlID="drpProduct" EventName="SelectedIndexChanged" />
          <asp:AsyncPostbackTrigger ControlID="txtUnitPrice" EventName="TextChanged" />
          <asp:AsyncPostbackTrigger ControlID="txtQuantity" EventName="TextChanged" />
        
   
   </Triggers>
          </asp:UpdatePanel> --%>
          
                        	<div class="row">
							<div class="large-6 columns">
								<label>Manufacture Date

							                   <asp:TextBox ID="txtManufactureDate" runat="server" TextMode="Date"></asp:TextBox>
								</label>
							</div>
               
							<div class="large-6 columns">
								<label>Pervious MRK
					 <asp:TextBox ID="txtPerviousMRP" runat="server" ></asp:TextBox>
							    </label>
							</div>
						</div>  
        
                    	<div class="row">
							<div class="large-6 columns">
								<label>Expire Date
							                   <asp:TextBox ID="txtExpireDate" runat="server" TextMode="Date"></asp:TextBox>
								</label>
							</div>
               
							<div class="large-6 columns">
                                					<label>Pervious Unit Price
					<asp:TextBox ID="txtPerviousUnitPrice" runat="server"  ></asp:TextBox>
						</label>
							</div>
						</div>  
             
							<div class="row">
							<div class="large-12 columns">
							       <label>Remarks
					<asp:TextBox ID="txtRemarks" runat="server"  TextMode="MultiLine" Width="750px"></asp:TextBox>
							    </label>    
							</div>
  
      
  			</div>
                               
               </div>
      </ContentTemplate>
     <Triggers>
      <asp:AsyncPostbackTrigger ControlID="drpCatagory" EventName="SelectedIndexChanged" />
           <asp:AsyncPostbackTrigger ControlID="drpProduct" EventName="SelectedIndexChanged" />
          <asp:AsyncPostbackTrigger ControlID="txtUnitPrice" EventName="TextChanged" />
          <asp:AsyncPostbackTrigger ControlID="txtQuantity" EventName="TextChanged" />
        
   
   </Triggers>
          </asp:UpdatePanel> 
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>
                  <div class="row">
							   <asp:Button ID="btnAdd" class="button tiny" Font-Size="Medium" Width="120px"  runat="server" Text="Add" OnClick="btnAdd_Click" />
                              <asp:Button ID="btnDelete" class="alert button" Font-Size="Medium" Width="120px"  runat="server" Text="Delete" OnClick="btnDelete_Click" />
         <%--                  <asp:Button ID="btnAdd" class="success button" runat="server" Text="Add" Font-Size="Medium" Width="180px" ></asp:Button />--%>
                            
					   
                               </div>
         
      
                    <div style="width: 100%; height: 145px; overflow: scroll">
                    <asp:GridView ID="dgvData" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="auto-style2" OnRowDataBound="dgvData_RowDataBound" OnRowDeleting="dgvData_RowDeleting" AutoGenerateColumns="False" OnSelectedIndexChanged="dgvData_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL" >
        <ItemTemplate >
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
     </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="Code" />
                            <asp:BoundField DataField="Manufactured Date" HeaderText="Manufactured Date" />
                            <asp:BoundField DataField="Expire Date" HeaderText="Expire Date" />
                            <asp:BoundField DataField="Quatity" HeaderText="Quatity" />
                            <asp:BoundField DataField="Unit Price" HeaderText="Unit Price" />
                            <asp:BoundField DataField="Total Price" HeaderText="Total Price" />
                            <asp:BoundField DataField="MRP" HeaderText="MRP" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="Product Code" HeaderText="Product Code" />
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                               <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                       </asp:GridView>
                                              	 </div>
       
        
        
           

              <div class="row">
							   <asp:Button ID="btnSave" class="success button" Font-Size="Medium" Width="180px"  runat="server" Text="Save" OnClick="btnSave_Click"  />

                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                               <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
					   
                               </div>
 
	     </ContentTemplate>
     <Triggers>
     <asp:AsyncPostbackTrigger ControlID="btnSave" EventName="Click" />
 </Triggers>
    </asp:UpdatePanel> 
                              </div> 

						</div>

      
     
					
                                                                            
			
	  			</div>
  			</div>
  		</div>
  		<div style="height: 50px;background: lightblue;"></div>
  	</section>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="js/vendor/jquery.js"></script>
    <script src="js/vendor/what-input.js"></script>
    <script src="js/vendor/foundation.js"></script>
    <script src="js/app.js"></script>

  </body>
    <link href="//cdnjs.cloudflare.com/ajax/libs/foundation/6.3.1/css/foundation.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//cdnjs.cloudflare.com/ajax/libs/foundation/6.3.1/js/foundation.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</asp:Content>
