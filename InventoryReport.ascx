<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InventoryReport.ascx.cs" Inherits="GIBS.Modules.Inventory.InventoryReport" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css" />

<script type="text/javascript">

    $(function () {
        $("#txtStartDate").datepicker({
            numberOfMonths: 1,
           // minDate: 0,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
        $("#txtEndDate").datepicker({
            showButtonPanel: false
        });
    });

 </script>

<style type="text/css">
     .dnnFormItem.dnnFormHelp { margin-top: 2em; }
</style>
<script type="text/javascript" language="javascript" >

    //   jQuery(function ($) { $("#<%= ClientID %>_txtCasePrice").mask("999.99"); });

   
</script>

<div class="dnnForm" id="form-settings">
<h5>Date Range</h5>
    <fieldset>

        <div class="dnnFormItem">	
            <dnn:Label ID="lblStartDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtStartDate" Suffix=":"  />
            <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" /><asp:Button ID="btnUpdateReport" resourcekey="btnUpdateReport" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Update" onclick="btnUpdateReport_Click" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEndDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtEndDate" Suffix=":" />
            <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" />
        </div>			
	</fieldset>
</div>	


 <asp:DropDownList ID="ddlFilterCategory" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlFilterCategory_SelectedIndexChanged"></asp:DropDownList>
        
        &nbsp;&nbsp;

    <asp:CheckBox ID="cbxShowInActive" runat="server" Text=" &nbsp;Show Inactive Products" 
        AutoPostBack="True" oncheckedchanged="cbxShowInActive_CheckedChanged" Visible="false"  />

<asp:GridView ID="gvProducts" runat="server" 
     AllowPaging="True"
    PageSize="25"
     
    AllowSorting="True"
    PagerSettings-Position="Bottom" 
    OnPageIndexChanging="gvProducts_PageIndexChanging"
    OnRowEditing="gvProducts_RowEditing" 
    OnRowCommand="gvProducts_RowCommand" 
	OnRowDeleting="gvProducts_RowDeleting"
    OnPreRender="gvProducts_PreRender"    
    AutoGenerateColumns="True" 
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
    
</asp:GridView>	

<div style="color: Blue; font-weight: bold">
            <br />
            <i><asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label> Records<br />You are viewing page
                <%=gvProducts.PageIndex + 1%>
                of
                <%=gvProducts.PageCount%>
            </i>
        </div>









<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" resourcekey="btnReturnToFrontDesk" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>
