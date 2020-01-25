<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TakeInventory.ascx.cs" Inherits="GIBS.Modules.Inventory.TakeInventory" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>


<style type="text/css">
     .dnnFormItem.dnnFormHelp { margin-top: 2em; }
</style>
<script type="text/javascript" language="javascript" >

    //   jQuery(function ($) { $("#<%= ClientID %>_txtCasePrice").mask("999.99"); });

   
</script>
<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
<asp:Panel runat="server" ID="panelGrid" Visible="True">


 <asp:DropDownList ID="ddlFilterCategory" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlFilterCategory_SelectedIndexChanged"></asp:DropDownList>
        
        &nbsp;&nbsp;

    <asp:CheckBox ID="cbxShowInActive" runat="server" Text=" &nbsp;Show Inactive Products" 
        AutoPostBack="True" oncheckedchanged="cbxShowInActive_CheckedChanged"  />

<asp:GridView ID="gvProducts" runat="server" 
     AllowPaging="True"
    PageSize="25"
     
    AllowSorting="True"
    PagerSettings-Position="Bottom" 
    OnPageIndexChanging="gvProducts_PageIndexChanging"
    OnRowEditing="gvProducts_RowEditing" 
    OnRowCommand="gvProducts_RowCommand" 
	OnRowDeleting="gvProducts_RowDeleting"
    DataKeyNames="ProductID"       
    AutoGenerateColumns="False" 
     GridLines="Horizontal"
    CellPadding="4" CellSpacing="1" BorderWidth="1px" 
     
    resourcekey="gvProducts">
    
    <AlternatingRowStyle cssclass="dnnGridAltItem" />
    <FooterStyle cssclass="dnnGridFooter" />
    <HeaderStyle cssclass="dnnGridHeader" />
    <PagerStyle cssclass="dnnGridPager" />
    <RowStyle cssclass="dnnGridItem" />
    <PagerStyle HorizontalAlign="Left"  BackColor="DodgerBlue" Height="20px" Font-Size="Small" ForeColor="Snow" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>




<asp:BoundField HeaderText="Category" DataField="ProductCategory" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        <asp:BoundField HeaderText="Item" DataField="ProductName" ></asp:BoundField>


            <asp:TemplateField ItemStyle-HorizontalAlign="Center">                     
        <HeaderTemplate>
            Count
        </HeaderTemplate>
        <ItemTemplate>
               <asp:TextBox ID="txtCount" runat="server" Width="60px" /><asp:HiddenField ID="hidProductID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
        </ItemTemplate>
    </asp:TemplateField>



    </Columns>
</asp:GridView>	

<div style="color: Blue; font-weight: bold">
            <br />
            <i><asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label> Records<br />You are viewing page
                <%=gvProducts.PageIndex + 1%>
                of
                <%=gvProducts.PageCount%>
            </i>
        </div>


<div style="margin-top:15px;">
<asp:Button runat="server" Text="Button" ID="btnUpdateInventory" ResourceKey="btnUpdateInventory" CssClass="dnnPrimaryAction" 
    onclick="btnUpdateInventory_Click" /></div>


<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" resourcekey="btnReturnToFrontDesk" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>
</asp:Panel>
