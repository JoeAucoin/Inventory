using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.Inventory.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        public override IDataReader Suppliers_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Suppliers_List"), moduleId);
        }

        public override IDataReader Suppliers_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Suppliers_GetByID"), moduleId, itemId);
        }

        public override void Suppliers_Insert(int createdByUserID, int moduleId, string supplierName, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int portalId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Suppliers_Insert"), createdByUserID, moduleId, supplierName, address, city, state, zip, supplierPhone, salesman, salesmanPhone, portalId);
        }

        public override void Suppliers_Update(int supplierID, int moduleId, string supplierName, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int lastModifiedByUserID, int portalId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Suppliers_Update"), supplierID, moduleId, supplierName, address, city, state, zip, supplierPhone, salesman, salesmanPhone, lastModifiedByUserID, portalId);
        }

        public override void DeleteInventory(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_DeleteInventory"), moduleId, itemId);
        }


        //ProductCategory
        public override IDataReader ProductCategory_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_ProductCategory_List"), moduleId);
        }

        public override void ProductCategories_Insert(int createdByUserID, int moduleId, string productCategory, int portalId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_ProductCategories_Insert"), createdByUserID, moduleId, productCategory, portalId);
        }

        public override IDataReader ProductCategory_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_ProductCategory_GetByID"), moduleId, itemId);
        }

        public override void ProductCategory_Update(int productCategoryID, int moduleId, string productCategory, int lastModifiedByUserID, int portalId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_ProductCategory_Update"), productCategoryID, moduleId, productCategory, lastModifiedByUserID, portalId);
        }

        //Products
        public override IDataReader Products_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Products_List"), moduleId);
        }

        public override void Products_Insert(string productName, double casePrice, int caseCount, int productCategoryID, int createdByUserID, int moduleId, int portalId, double caseWeight, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Products_Insert"), productName, casePrice, caseCount, productCategoryID, createdByUserID, moduleId, portalId, caseWeight, isActive);
        }

        public override IDataReader Products_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Products_GetByID"), moduleId, itemId);
        }

        public override void Products_Update(int productID, string productName, double casePrice, int caseCount, int productCategoryID, int moduleId, int lastModifiedByUserID, int portalId, double caseWeight, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Products_Update"), productID, productName, casePrice, caseCount, productCategoryID, moduleId, lastModifiedByUserID, portalId, caseWeight, isActive);
        }

        public override void Products_Delete(int productID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Products_Delete"), productID);
        }

        //Invoices
        public override IDataReader Invoice_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Invoice_List"), moduleId);
        }

        //public override void Invoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId)
        //{
        //    SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Invoice_Insert"), invoiceNumber, invoiceDate, supplierID, createdByUserID, moduleId, portalId);
        //}

        public override int Invoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("Inventory_Invoice_Insert"), invoiceNumber, invoiceDate, supplierID, createdByUserID, moduleId, portalId));
        }

        public override IDataReader Invoice_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Invoice_GetByID"), moduleId, itemId);
        }

        public override void Invoice_Update(int invoiceID, string invoiceNumber, DateTime invoiceDate, int supplierID, int moduleId, int lastModifiedByUserID, int portalId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Invoice_Update"), invoiceID, invoiceNumber, invoiceDate, supplierID, moduleId, lastModifiedByUserID, portalId);
        }

        public override void Invoice_Delete(int invoiceID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_Invoice_Delete"), invoiceID);
        }

        public override IDataReader Invoice_GetInvoiceLineItems(int invoiceID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Invoice_GetInvoiceLineItems"), invoiceID);
        }

        //LineItems
        public override void LineItems_Insert(int invoiceID, int productID, int Cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_LineItems_Insert"), invoiceID, productID, Cases, countPerCase, pricePerCase, weightPerCase, reportType);
        }

        public override IDataReader LineItems_GetByID(int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_LineItems_GetByID"), itemId);
        }

        public override void LineItems_Update(int lineItemID, int invoiceID, int productID, int cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_LineItems_Update"), lineItemID, invoiceID, productID, cases, countPerCase, pricePerCase, weightPerCase, reportType);
        }
        public override void LineItems_Delete(int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_LineItems_Delete"), itemId);
        }

        // Inventory
        public override void Inventory_RecordAdd(int productID, int moduleId, double productCount, DateTime inventoryDate)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Inventory_RecordAdd"), productID, moduleId, productCount, inventoryDate);
        }

        public override IDataReader Inventory_Report(DateTime startDate, DateTime endDate, int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Inventory_Report"), startDate, endDate, moduleId);
        }	


        #endregion
    }
}
