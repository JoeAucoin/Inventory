using System;
using System.Collections.Generic;
using System.Xml;
using DotNetNuke.Common.Utilities;
using System.Data;


namespace GIBS.Inventory.Components
{
    public class InventoryController 
    {

        #region public method

        public List<InventoryInfo> Suppliers_List(int moduleId)
        {
            return CBO.FillCollection<InventoryInfo>(DataProvider.Instance().Suppliers_List(moduleId));
        }

        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public InventoryInfo Suppliers_GetByID(int moduleId, int supplierID)
        {
        
            return CBO.FillObject<InventoryInfo>(DataProvider.Instance().Suppliers_GetByID(moduleId, supplierID));
        }


        /// <summary>
        /// Adds a new InventoryInfo object into the database
        /// </summary>
        /// <param name="info"></param>
        public void Suppliers_Insert(InventoryInfo info)
        {
            //check we have some content to store
            if (info.SupplierName != string.Empty)
            {
                DataProvider.Instance().Suppliers_Insert(info.CreatedByUserID, info.ModuleId, info.SupplierName, info.Address, info.City, info.State, info.Zip, info.SupplierPhone, info.Salesman, info.SalesmanPhone, info.PortalId);
            }
        }

        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void Suppliers_Update(InventoryInfo info)
        {
            //check we have some content to update
            if (info.SupplierName != string.Empty)
            {
                DataProvider.Instance().Suppliers_Update(info.SupplierID, info.ModuleId, info.SupplierName, info.Address, info.City, info.State, info.Zip, info.SupplierPhone, info.Salesman, info.SalesmanPhone, info.LastModifiedByUserID, info.PortalId);
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteInventory(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteInventory(moduleId, itemId);
        }



        // ProductCategories
        public void ProductCategories_Insert(InventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductCategory != string.Empty)
            {
                DataProvider.Instance().ProductCategories_Insert(info.CreatedByUserID, info.ModuleId, info.ProductCategory, info.PortalId);
            }
        }

        public List<InventoryInfo> ProductCategory_List(int moduleId)
        {
            return CBO.FillCollection<InventoryInfo>(DataProvider.Instance().ProductCategory_List(moduleId));
        }

        public InventoryInfo ProductCategory_GetByID(int moduleId, int productCategoryID)
        {
         
            return CBO.FillObject<InventoryInfo>(DataProvider.Instance().ProductCategory_GetByID(moduleId, productCategoryID));
        }

        public void ProductCategory_Update(InventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductCategory != string.Empty)
            {
                DataProvider.Instance().ProductCategory_Update(info.ProductCategoryID, info.ModuleId, info.ProductCategory, info.LastModifiedByUserID, info.PortalId);
            }
        }


        // Products
        public void Products_Insert(InventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductName != string.Empty)
            {
                DataProvider.Instance().Products_Insert(info.ProductName, info.CasePrice, info.CaseCount, info.ProductCategoryID, info.CreatedByUserID, info.ModuleId, info.PortalId, info.CaseWeight, info.IsActive);
            }
        }

        public List<InventoryInfo> Products_List(int moduleId)
        {
            return CBO.FillCollection<InventoryInfo>(DataProvider.Instance().Products_List(moduleId));
        }

        public InventoryInfo Products_GetByID(int moduleId, int productID)
        {
       
            return CBO.FillObject<InventoryInfo>(DataProvider.Instance().Products_GetByID(moduleId, productID));
        }

        public void Products_Update(InventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductCategoryID > 0)
            {
                DataProvider.Instance().Products_Update(info.ProductID, info.ProductName, info.CasePrice, info.CaseCount, info.ProductCategoryID, info.ModuleId, info.LastModifiedByUserID, info.PortalId, info.CaseWeight, info.IsActive);
            }
        }

        public void Products_Delete(int productID)
        {
            DataProvider.Instance().Products_Delete(productID);
        }

        // Invoices
        //public void Invoice_Insert(InventoryInfo info)
        //{
        //    //check we have some content to store
        //    if (info.InvoiceNumber != string.Empty)
        //    {
        //        DataProvider.Instance().Invoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId);
        //    }
        //}

        public int Invoice_Insert(InventoryInfo info)
        {

            //   return Convert.ToInt32(DataProvider.Instance().Invoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId));
            //check we have some content to store
            if (info.InvoiceNumber != string.Empty)
            {
                return Convert.ToInt32(DataProvider.Instance().Invoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId));
            }
            else
            {
                return 0;
            }
        }



        public List<InventoryInfo> Invoice_List(int moduleId)
        {
            return CBO.FillCollection<InventoryInfo>(DataProvider.Instance().Invoice_List(moduleId));
        }

        public InventoryInfo Invoice_GetByID(int moduleId, int invoiceID)
        {
          
            return CBO.FillObject<InventoryInfo>(DataProvider.Instance().Invoice_GetByID(moduleId, invoiceID));
        }

        public void Invoice_Update(InventoryInfo info)
        {
            //check we have some content to update
            if (info.InvoiceNumber != string.Empty)
            {
                DataProvider.Instance().Invoice_Update(info.InvoiceID, info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.ModuleId, info.LastModifiedByUserID, info.PortalId);
            }
        }

        public void Invoice_Delete(int invoiceID)
        {
            DataProvider.Instance().Invoice_Delete(invoiceID);
        }

        public List<InventoryInfo> Invoice_GetInvoiceLineItems(int invoiceID)
        {
            return CBO.FillCollection<InventoryInfo>(DataProvider.Instance().Invoice_GetInvoiceLineItems(invoiceID));
        }

        // LineItems
        public void LineItems_Insert(InventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductID != 0)
            {
                DataProvider.Instance().LineItems_Insert(info.InvoiceID, info.ProductID, info.Cases, info.CountPerCase, info.PricePerCase, info.WeightPerCase, info.ReportType);
            }
        }

        public InventoryInfo LineItems_GetByID(int lineItemID)
        {
          
            return CBO.FillObject<InventoryInfo>(DataProvider.Instance().LineItems_GetByID(lineItemID));
        }

        public void LineItems_Update(InventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductID != 0)
            {
                DataProvider.Instance().LineItems_Update(info.LineItemID, info.InvoiceID, info.ProductID, info.Cases, info.CountPerCase, info.PricePerCase, info.WeightPerCase, info.ReportType);
            }
        }

        public void LineItems_Delete(int itemId)
        {
            DataProvider.Instance().LineItems_Delete(itemId);
        }

        // Inventory
        public void Inventory_RecordAdd(InventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductName != string.Empty)
            {
                DataProvider.Instance().Inventory_RecordAdd(info.ProductID, info.ModuleId, info.ProductCount, info.InventoryDate);
            }
        }


        public IDataReader Inventory_Report(DateTime startDate, DateTime endDate, int moduleId)
        {
            //SqlDataReader dr = null; 
            //dr = CBO.FillSortedList(DataProvider.Instance().Inventory_Report(startDate, endDate, moduleId));
            //return dr.Read();

            return CBO.FillObject<IDataReader>(DataProvider.Instance().Inventory_Report(startDate, endDate, moduleId));

         //   CBO.FillObject
            //return CBO.FillCollection<ExpandoObject>(DataProvider.Instance().Inventory_Report(startDate, endDate, moduleId));
            //List<Person> personList = new List<Person>();
            //personList = DataReaderMapToList<Person>(dataReaderForPerson);
        }




        #endregion


        //public static List<T> DataReaderMapToList<T>(IDataReader dr)
        //{
        //    List<T> list = new List<T>();
        //    T obj = default(T);
        //    while (dr.Read())
        //    {
        //        obj = Activator.CreateInstance<T>();
        //        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        //        {
        //            if (!object.Equals(dr[prop.Name], DBNull.Value))
        //            {
        //                prop.SetValue(obj, dr[prop.Name], null);
        //            }
        //        }
        //        list.Add(obj);
        //    }
        //    return list;
        //}



        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        //public string ExportModule(int moduleID)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    List<InventoryInfo> infos = GetInventorys(moduleID);

        //    if (infos.Count > 0)
        //    {
        //        sb.Append("<Inventorys>");
        //        foreach (InventoryInfo info in infos)
        //        {
        //            sb.Append("<Inventory>");
        //            sb.Append("<content>");
        //            sb.Append(XmlUtils.XMLEncode(info.ProductName));
        //            sb.Append("</content>");
        //            sb.Append("</Inventory>");
        //        }
        //        sb.Append("</Inventorys>");
        //    }

        //    return sb.ToString();
        //}

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "Inventorys");

            foreach (XmlNode info in infos.SelectNodes("Inventory"))
            {
                InventoryInfo InventoryInfo = new InventoryInfo();
                InventoryInfo.ModuleId = ModuleID;
                InventoryInfo.ProductName = info.SelectSingleNode("ProductName").InnerText;
                InventoryInfo.CreatedByUserID = UserID;

             //   AddInventory(InventoryInfo);
            }
        }

        #endregion
    }
}
