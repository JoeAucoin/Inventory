<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewInventory.ascx.cs" Inherits="GIBS.Modules.Inventory.ViewInventory" %>


<p style="text-align: center;">
<asp:Button ID="btnTakeInventory" runat="server"  CssClass="btn btn-lg btn-default" 
    resourcekey="btnTakeInventory" Text="TakeInventory" onclick="btnTakeInventory_Click" /> 
&nbsp;&nbsp;&nbsp;  

<asp:Button ID="btnViewInventory" runat="server"  CssClass="btn btn-lg btn-default" 
    resourcekey="btnViewInventory" Text="btnViewInventory" onclick="btnViewInventory_Click" />

</p>

    <p style="text-align: center;">



<asp:Button ID="btnProducts" runat="server" Text="Products"  
    resourcekey="btnProducts" CssClass="btn btn-sm btn-primary" 
    onclick="btnProducts_Click" /> 
 &nbsp; 
<asp:Button ID="btnSuppliers" resourcekey="btnSuppliers" runat="server" CssClass="btn btn-sm btn-primary" Text="Suppliers" onclick="btnSuppliers_Click" />
 &nbsp; 

<asp:Button ID="btnInvoices" runat="server"  CssClass="btn btn-sm btn-primary" resourcekey="btnInvoices" Text="Invoices" onclick="btnInvoices_Click" />
 &nbsp; 

<asp:Button ID="btnProductCategories" runat="server" Text="ProductCategories" resourcekey="btnProductCategories" CssClass="btn btn-sm btn-primary" 
    onclick="btnProductCategories_Click" />
        </p>