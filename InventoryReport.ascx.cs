using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using DotNetNuke.Common;
using System.Drawing;
//using Telerik;
//using Telerik.Web.UI;
using System.Data;
using System.ComponentModel;
using DotNetNuke.Framework.JavaScriptLibraries;
using GIBS.Inventory.Components;
using System.Dynamic;
using System.Data.SqlClient;
using DotNetNuke.UI.Skins;

namespace GIBS.Modules.Inventory
{
    public partial class InventoryReport : PortalModuleBase, IActionable
    {
        // To show custom operations...
        private List<int> mQuantities = new List<int>();
        public DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            DotNetNuke.Framework.jQuery.RequestRegistration();
            DotNetNuke.Framework.jQuery.RequestUIRegistration();

            //JavaScript.RequestRegistration(CommonJs.jQuery);
            //JavaScript.RequestRegistration(CommonJs.jQueryUI);
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));




            if (!IsPostBack)
            {
                //     GroupIt();
                txtStartDate.Text = DateTime.Today.AddDays(-30).ToShortDateString();
                txtEndDate.Text = DateTime.Today.ToShortDateString();

                FillProductCategoryDropDown();

                FillProductsGrid();

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                     true, false);

                actions.Add(GetNextActionID(), Localization.GetString("Suppliers", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("Suppliers"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                return actions;
            }
        }

        #endregion





        public void FillProductsGrid()
        {

            try
            {

                DataTable dt = new DataTable();

                Dictionary<string, object> paras = new Dictionary<string, object>();

                paras.Add("StartDate", Convert.ToDateTime(txtStartDate.Text.ToString()));
                paras.Add("EndDate", Convert.ToDateTime(txtEndDate.Text.ToString()));
                paras.Add("ModuleID", this.ModuleId);
                using (SqlDataReader results = Components.Expando.executeProcedure("GIBS_Inventory_Report", paras))
                {
                    //while (results.Read())
                    //{
                    //    //do something with the rows returned
                    //}
                    dt.Load(results);
                }

                DataView dv = dt.DefaultView;

          
                if (ddlFilterCategory.SelectedValue.ToString() != "0")
                {
                    // CID = ProductCategoryID
                    dv.RowFilter += "  CID = " + ddlFilterCategory.SelectedValue;
                }
          

                gvProducts.DataSource = dv;
                gvProducts.DataBind();

                lblTotalRecordCount.Text = dt.DefaultView.Count.ToString();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }





        private void SaveQuantity(string column, string group, object value)
        {
            mQuantities.Add(Convert.ToInt32(value));
        }

        private object GetMinQuantity(string column, string group)
        {
            int[] qArray = new int[mQuantities.Count];
            mQuantities.CopyTo(qArray);
            Array.Sort(qArray);
            return qArray[0];
        }

        private void helper_Bug(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            row.BackColor = Color.LightCyan;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = values[0] + " TOTALS&nbsp;";
        }


        public void FillProductCategoryDropDown()
        {

            try
            {
                // CreatePrefixSuffixDropdowns();

                List<InventoryInfo> items;
                InventoryController controller = new InventoryController();

                items = controller.ProductCategory_List(this.ModuleId);


                //ddlProductCategory.DataTextField = "ProductCategory";
                //ddlProductCategory.DataValueField = "ProductCategoryID";
                //ddlProductCategory.DataSource = items;
                //ddlProductCategory.DataBind();
                //ddlProductCategory.Items.Insert(0, new ListItem("-- Select --", "0"));


                ddlFilterCategory.DataTextField = "ProductCategory";
                ddlFilterCategory.DataValueField = "ProductCategoryID";
                ddlFilterCategory.DataSource = items;
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, new ListItem("Filter Category", "0"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //GridViewEditEventArgs
        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillProductsGrid();
            gvProducts.PageIndex = e.NewPageIndex;
            gvProducts.DataBind();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            try
            {
                //if (e.CommandName == "Delete")
                //{
                //    // get the ID of the clicked row
                //    int ID = Convert.ToInt32(e.CommandArgument);

                //    InventoryController controller = new InventoryController();
                //    // Delete the record 
                //    controller.Products_Delete(ID);

                //    FillProductsGrid();

                //}


                //if (e.CommandName == "Edit")
                //{
                //    int ID = Convert.ToInt32(e.CommandArgument);


                //    InventoryController controller = new InventoryController();
                //    InventoryInfo item = controller.Products_GetByID(this.ModuleId, ID);

                //    if (item != null)
                //    {
                //        panelGrid.Visible = false;
                //        panelEdit.Visible = true;
                //        txtProductName.Text = item.ProductName.ToString();
                //        txtCasePrice.Text = string.Format("{0:0.00}", item.CasePrice.ToString());
                //        //txtCasePrice.Text = item.CasePrice.ToString();
                //        txtCaseCount.Text = item.CaseCount.ToString();
                //        ddlProductCategory.SelectedValue = item.ProductCategoryID.ToString();
                //        txtCaseWeight.Text = item.CaseWeight.ToString();
                //        rblIsActive.SelectedValue = item.IsActive.ToString();
                //        txtProductID.Value = item.ProductID.ToString();
                //    }
                //    else
                //    {
                //        txtProductID.Value = "";
                //    }


                //}

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void gvProducts_PreRender(object sender, EventArgs e)
        {

            if (gvProducts.Columns.Count > 0)
                gvProducts.Columns[1].Visible = false;



            else
            {
                gvProducts.HeaderRow.Cells[1].Visible = false;
                foreach (GridViewRow gvr in gvProducts.Rows)
                {
                    gvr.Cells[1].Visible = false;
                }
            }
        }


        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        InventoryController controller = new InventoryController();
        //        InventoryInfo item = new InventoryInfo();


        //        item.ModuleId = this.ModuleId;
        //        item.PortalId = this.PortalId;

        //        item.ProductName = txtProductName.Text.ToString();

        //        item.CaseWeight = double.Parse(txtCaseWeight.Text.ToString());
        //        item.CasePrice = double.Parse(txtCasePrice.Text.ToString());
        //        item.CaseCount = Int32.Parse(txtCaseCount.Text.ToString());
        //        item.ProductCategoryID = Int32.Parse(ddlProductCategory.SelectedValue.ToString());

        //        item.LastModifiedByUserID = this.UserId;

        //        item.IsActive = bool.Parse(rblIsActive.SelectedValue.ToString());

        //        if (txtProductID.Value.Length > 0)
        //        {
        //            item.ProductID = Int32.Parse(txtProductID.Value.ToString());
        //            controller.Products_Update(item);



        //        }
        //        else
        //        {
        //            item.CreatedByUserID = this.UserId;
        //            controller.Products_Insert(item);

        //        }


        //        //Response.Redirect(EditUrl("Products"));
        //       // ResetForm();
        //        FillProductsGrid();
        //        panelGrid.Visible = true;
        //        panelEdit.Visible = false;


        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}

        //public void ResetForm()
        //{

        //    try
        //    {
        //        txtProductID.Value = "";
        //        txtProductName.Text = "";
        //        txtCaseWeight.Text = "0";
        //        txtCasePrice.Text = "0";
        //        txtCaseCount.Text = "0";
        //        ddlProductCategory.SelectedIndex = 0;
        //        rblIsActive.SelectedValue = "True";



        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Response.Redirect(EditUrl("Products"));
        //        ResetForm();
        //        panelGrid.Visible = true;
        //        panelEdit.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}

        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        panelGrid.Visible = false;
        //        panelEdit.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}


        protected void btnReturnToFrontDesk_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                gvProducts.PageIndex = 0;
                FillProductsGrid();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int ID = Convert.ToInt32(txtProductID.Value.ToString());

        //        InventoryController controller = new InventoryController();
        //        // Delete the record 
        //        controller.Products_Delete(ID);

        //        txtProductID.Value = "";

        //        FillProductsGrid();
        //        panelGrid.Visible = true;
        //        panelEdit.Visible = false;


        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}

        protected void cbxShowInActive_CheckedChanged(object sender, EventArgs e)
        {
            FillProductsGrid();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            foreach (DataControlField column in gvProducts.Columns)
            {
                //Get the first Cell /Column
                TableCell cell = row.Cells[0];
                // Then Remove it after
                row.Cells.Remove(cell);
                //And Add it to the List Collections
                columns.Add(cell);
            }

            // Add cells
            row.Cells.AddRange(columns.ToArray());
        } 

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
       //     e.Row.Cells[2].Visible = false;
         //   e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    BoundField field = (BoundField)((DataControlFieldCell)e.Row.Cells[i]).ContainingField;

                    if (field.HeaderText.Contains("ProductID"))
                    {
                      //   .......... do something
                        e.Row.Cells[i].Visible = false;
                        field.Visible = false;
                    }
                    if (field.HeaderText.Contains("CID"))
                    {
                        //   .......... do something
                        e.Row.Cells[i].Visible = false;
                        field.Visible = false;
                    }
                }
       }


        }


        protected void btnUpdateReport_Click(object sender, EventArgs e)
        {
            try
            {
                FillProductsGrid();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void LinkButtonPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "InventoryReport", "SkinSrc=[G]" + Globals.QueryStringEncode(SkinController.RootSkin + "/" + Globals.glbHostSkinFolder + "/" + "popUpSkin") + "&ContainerSrc=" + Globals.QueryStringEncode("/portals/_default/containers/_default/no%20container") + "&mid=" + ModuleId.ToString() + "&dnnprintmode=true"), true);

        }
    }
}