<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="SBMS.Product.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    max-width: 700px;
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

     <body>

  	<section id="foundation-example">
  		<div style="height: 30px;background: lightblue;"></div>
  		<div class="foundation-example__ex-wrapper">
  			<div class="foundation-example__in-wrapper">
	  			<div class="foundation-example__title"> Product</div>
	  			<div class="foundation-example__form-wrapper">
                 <asp:ScriptManager ID="ScriptManager1" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
               
	                  <div class="row">
							
                    <div class="large-4 columns">
							
							     <asp:TextBox ID="txtCode" runat="server" Placeholder="Product Code"  MaxLength="4"></asp:TextBox> 

							</div>
                    <asp:Button ID="btnSearch" class="button primary" Width="90px" runat="server" Text="Search" OnClick="btnSearch_Click" />
						</div>
                      
						<div class="row" >
                           
							<div class="large-6 columns" style="text-align: left">
								<label>Catagory
								<asp:DropDownList ID="drpCatagory" runat="server"></asp:DropDownList>
								</label>
							</div>
                            <div class="large-6 columns" style="text-align: left">
								<label>Date
							        <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
								</label>
							</div>
                      
                   
                           </div>
                   
                         <div class="row">
							<div class="large-6 columns">
								<label>Product Name
							     <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
							    
							    </label>
							</div>
                 <div class="large-6 columns">
								<label>Product Quatity
							     <asp:TextBox ID="txtQuantity" TextMode="Number" min="10" runat="server"></asp:TextBox>
							    
							    </label>
							</div>
                           
						</div>
                          <div class="row">
							  <div class="large-6 columns">
								<label>Supplier Name
							     <asp:TextBox ID="txtSuppierName"  runat="server"></asp:TextBox>
							    
							    </label>
							</div>
                 	  <div class="large-6 columns">
								<label>Supplier Phone Number
							     <asp:TextBox ID="txtSupplierPhoneNumber"  runat="server" MaxLength="11"></asp:TextBox>
							    
							    </label>
							</div>
                           
						</div>
                       <div class="row">
							<div class="large-12 columns">
								<label>Description
							     <asp:TextBox ID="txtDescription" runat="server" Height="80px" TextMode="MultiLine"></asp:TextBox>
							    
							    </label>
							</div>
                 
                           
						</div>
                        
						<div class="row" >
                          <div class="large-8 columns">  
  
							
					    <asp:Button ID="btnSave" class="success button" runat="server" Text="Save" Font-Size="Medium" Width="180px" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNew" class="alert button" runat="server" Text="New" Font-Size="Medium" Width="180px" OnClick="btnNew_Click" />   
                               <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                               <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                               </div>
	
                               
						</div>

                      <div style="width: 100%; height: 400px; overflow: scroll">
                    <asp:GridView ID="dgvData" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"  AutoGenerateColumns="False" OnSelectedIndexChanged="dgvData_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL" >
        <ItemTemplate >
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
     </asp:TemplateField>
                            <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="CatagoryCode" HeaderText="CatagoryCode" />
                            <asp:BoundField DataField="Qty" HeaderText="Qunatity" />
                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}"  />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="supplierName" HeaderText="Supplier" />
                            <asp:BoundField DataField="supplierNumber" HeaderText="Supplier PhoneNumber" />
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
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
            
			   </ContentTemplate>
     <Triggers>
 </Triggers>
          </asp:UpdatePanel>
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
