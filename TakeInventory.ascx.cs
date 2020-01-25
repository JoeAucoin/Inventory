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

using System.Data;
using System.ComponentModel;
using DotNetNuke.Framework.JavaScriptLibraries;
using GIBS.Inventory.Components;

namespace GIBS.Modules.Inventory
{
    public partial class TakeInventory : PortalModuleBase, IActionable
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

                List<InventoryInfo> items;

                InventoryController controller = new InventoryController();

                items = controller.Products_List(this.ModuleId);

                dt = Components.GridViewTools.ToDataTable(items);

                DataView dv = dt.DefaultView;

                dv.RowFilter = "IsActive = 1";

                if (ddlFilterCategory.SelectedValue.ToString() != "0")
                {
                    dv.RowFilter += " AND ProductCategoryID = " + ddlFilterCategory.SelectedValue;
                }
                if (cbxShowInActive.Checked == true)
                {
                    dv.RowFilter += " or IsActive = 0";
                }


                gvProducts.DataSource = dv;
                gvProducts.DataBind();

                lblTotalRecordCount.Text = dv.Count.ToString();



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
                if (e.CommandName == "Delete")
                {
                    // get the ID of the clicked row
                    int ID = Convert.ToInt32(e.CommandArgument);

                    InventoryController controller = new InventoryController();
                    // Delete the record 
                    controller.Products_Delete(ID);

                    FillProductsGrid();

                }


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


        protected void cbxShowInActive_CheckedChanged(object sender, EventArgs e)
        {
            FillProductsGrid();
        }


        public void SaveInventory(int _ProductID, double _Count)
        {
            try
            {
                InventoryController controller = new InventoryController();
                InventoryInfo item = new InventoryInfo();

                item.ModuleId = this.ModuleId;
                item.ProductCount = _Count;
                item.ProductID = _ProductID;
                
                item.InventoryDate = DateTime.Today;

                controller.Inventory_RecordAdd(item);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void ProcessGrid()
        {

            try
            {

                foreach (GridViewRow row in gvProducts.Rows)
                {
                    double n = 0;

                    TextBox tb = (TextBox)row.FindControl("txtCount");   //you need to give in which row you want to get that textbox

                    string isArecord = tb.Text.ToString();

            //        lblDebug.Text += isArecord.ToString() + "<br />";
                    bool isNumeric = double.TryParse(isArecord.ToString(), out n);
                    if (isNumeric)
                    {
                        string tempProductID = Convert.ToString(((HiddenField)row.FindControl("hidProductID")).Value);
                        string tempCount = isArecord.ToString();

                        SaveInventory(Int32.Parse(tempProductID.ToString()), Double.Parse(tempCount.ToString()));
                    }

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            ProcessGrid();
        }


    }
}