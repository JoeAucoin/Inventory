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
using GIBS.Inventory.Components;
using DotNetNuke.Common;

namespace GIBS.Modules.Inventory
{
    public partial class ProductCategories : PortalModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductCategoryGrid();

            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        public void FillProductCategoryGrid()
        {

            try
            {

                List<InventoryInfo> items;
                InventoryController controller = new InventoryController();
                items = controller.ProductCategory_List(this.ModuleId);

                gvProductCategory.DataSource = items;
                gvProductCategory.DataBind();
                lblTotalRecordCount.Text = items.Count.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillProductCategoryGrid();
            gvProductCategory.PageIndex = e.NewPageIndex;
            gvProductCategory.DataBind();
        }

        protected void gvProductCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                int productCategoryID = (int)gvProductCategory.DataKeys[e.NewEditIndex].Value;

                InventoryController controller = new InventoryController();
                InventoryInfo item = controller.ProductCategory_GetByID(this.ModuleId, productCategoryID);

                if (item != null)
                {

                    panelGrid.Visible = false;
                    panelEdit.Visible = true;

                    txtProductCategory.Text = item.ProductCategory.ToString();
                    txtProductCategoryID.Value = item.ProductCategoryID.ToString();
                }
                else
                {
                    txtProductCategoryID.Value = "";
                }


                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryController controller = new InventoryController();
                InventoryInfo item = new InventoryInfo();


                item.ModuleId = this.ModuleId;
                item.PortalId = this.PortalId;

                item.ProductCategory = txtProductCategory.Text.ToString();
                item.LastModifiedByUserID = this.UserId;

                if (txtProductCategoryID.Value.Length > 0)
                {
                    item.ProductCategoryID = Int32.Parse(txtProductCategoryID.Value.ToString());
                    controller.ProductCategory_Update(item);

                }
                else
                {
                    item.CreatedByUserID = this.UserId;
                    controller.ProductCategories_Insert(item);

                }
                FillProductCategoryGrid();
                panelGrid.Visible = true;
                panelEdit.Visible = false;

                // Response.Redirect(EditUrl("ProductCategories"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(EditUrl("ProductCategories"));
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



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductCategoryID.Value = "";
                txtProductCategory.Text = "";

                panelGrid.Visible = false;
                panelEdit.Visible = true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


    }
}