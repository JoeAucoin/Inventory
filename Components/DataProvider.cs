using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.Inventory.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.Inventory.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */

        //Suppliers
        public abstract IDataReader Suppliers_List(int moduleId);
        public abstract IDataReader Suppliers_GetByID(int moduleId, int itemId);
        public abstract void Suppliers_Insert(int createdByUserID, int moduleId, string supplierName, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int portalId);
        public abstract void Suppliers_Update(int supplierID, int moduleId, string supplierName, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int lastModifiedByUserID, int portalId);
        //UNUSED
        public abstract void DeleteInventory(int moduleId, int itemId);

        // Products
        public abstract void Products_Insert(string productName, double casePrice, int caseCount, int productCategoryID, int createdByUserID, int moduleId, int portalId, double caseWeight, bool isActive);
        public abstract IDataReader Products_List(int moduleId);
        public abstract IDataReader Products_GetByID(int moduleId, int productID);
        public abstract void Products_Update(int productID, string productName, double casePrice, int caseCount, int productCategoryID, int moduleId, int lastModifiedByUserID, int portalId, double caseWeight, bool isActive);
        public abstract void Products_Delete(int productID);


        //ProductCategories
        public abstract void ProductCategories_Insert(int createdByUserID, int moduleId, string productCategory, int portalId);
        public abstract IDataReader ProductCategory_List(int moduleId);
        public abstract IDataReader ProductCategory_GetByID(int moduleId, int productCategoryID);
        public abstract void ProductCategory_Update(int productCategoryID, int moduleId, string productCategory, int lastModifiedByUserID, int portalId);

        //Invoices
        //public abstract void Invoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId);

        public abstract int Invoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId);

        public abstract IDataReader Invoice_List(int moduleId);
        public abstract IDataReader Invoice_GetByID(int moduleId, int invoiceID);
        public abstract void Invoice_Update(int invoiceID, string invoiceNumber, DateTime invoiceDate, int supplierID, int moduleId, int lastModifiedByUserID, int portalId);
        public abstract IDataReader Invoice_GetInvoiceLineItems(int invoiceID);
        public abstract void Invoice_Delete(int invoiceID);

        //LineItems
        public abstract void LineItems_Insert(int invoiceID, int productID, int Cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType);
        public abstract void LineItems_Update(int lineItemID, int invoiceID, int productID, int cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType);
        public abstract void LineItems_Delete(int lineItemID);
        public abstract IDataReader LineItems_GetByID(int lineItemID);

       // INVENTORY
        public abstract void Inventory_RecordAdd(int productID, int moduleId, double productCount, DateTime inventoryDate);

        public abstract IDataReader Inventory_Report(DateTime startDate, DateTime endDate, int moduleId);

        #endregion

    }



}
