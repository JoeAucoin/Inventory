using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.Inventory.Components
{
    public class InventoryInfo
    {
        //private vars exposed thro the
        //properties
        private int moduleId;
        private int supplierID;
        private string supplierName;
        
        private string address;
        private string city;
        private string state;
        private string zip;
        private string supplierPhone;
        private string salesman;
        private string salesmanPhone;
        private int createdByUserID;
        private DateTime createdOnDate;
        private int lastModifiedByUserID;
        private DateTime lastModifiedOnDate;
        private string createdByUserName = null;
        private string lastModifiedByUserName;
        private int portalId;

        private string productCategory;
        private int productCategoryID;

        //Products
        private int productID;
        private string productName;
        private string productNameDropdown;
        private double casePrice;
        private int caseCount;
        private double caseWeight;
        private bool isActive;

        // Invoices
        // InvoiceID, InvoiceNumber, InvoiceDate
        private int invoiceID;
        private string invoiceNumber;
        private DateTime invoiceDate;

        //LineItems
        private int lineItemID;
        private int cases;
        private int countPerCase;
        private double pricePerCase;
        private double weightPerCase;
        private double totalCostExtended;
        private double totalWeightPerCase;
        private string reportType;

        // Reports
        private double totalProductWeight;
        private double totalProductCost;

        // Inventory
        private double productCount;
        private DateTime inventoryDate;
        private string takeCount;

        /// <summary>
        /// empty cstor
        /// </summary>
        public InventoryInfo()
        {
        }


        #region properties

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int PortalId
        {
            get { return portalId; }
            set { portalId = value; }
        }

        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        }

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }




        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public string City
        {
            get { return city; }
            set { city = value; }
        }


        public string State
        {
            get { return state; }
            set { state = value; }
        }


        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string SupplierPhone
        {
            get { return supplierPhone; }
            set { supplierPhone = value; }
        }

        public string Salesman
        {
            get { return salesman; }
            set { salesman = value; }
        }

        public string SalesmanPhone
        {
            get { return salesmanPhone; }
            set { salesmanPhone = value; }
        }

        //ProductCategory
        public int ProductCategoryID
        {
            get { return productCategoryID; }
            set { productCategoryID = value; }
        }

        public string ProductCategory
        {
            get { return productCategory; }
            set { productCategory = value; }
        }

        //Products
        public double CasePrice
        {
            get { return casePrice; }
            set { casePrice = value; }
        }

        public int CaseCount
        {
            get { return caseCount; }
            set { caseCount = value; }
        }

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        //productNameDropdown
        public string ProductNameDropdown
        {
            get { return productNameDropdown; }
            set { productNameDropdown = value; }
        }

        public double CaseWeight
        {
            get { return caseWeight; }
            set { caseWeight = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        //totalCostExtended TotalCostExtended
        public double TotalCostExtended
        {
            get { return totalCostExtended; }
            set { totalCostExtended = value; }
        }

        public double TotalWeightPerCase
        {
            get { return totalWeightPerCase; }
            set { totalWeightPerCase = value; }
        }

        public double TotalProductWeight
        {
            get { return totalProductWeight; }
            set { totalProductWeight = value; }
        }

        public double TotalProductCost
        {
            get { return totalProductCost; }
            set { totalProductCost = value; }
        }

        //Invoices
        public int InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }

        //LineItems
        public int LineItemID
        {
            get { return lineItemID; }
            set { lineItemID = value; }
        }

        public int Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        public int CountPerCase
        {
            get { return countPerCase; }
            set { countPerCase = value; }
        }

        public double PricePerCase
        {
            get { return pricePerCase; }
            set { pricePerCase = value; }
        }

        public double WeightPerCase
        {
            get { return weightPerCase; }
            set { weightPerCase = value; }
        }

        //string reportType string ReportType
        public string ReportType
        {
            get { return reportType; }
            set { reportType = value; }
        }

        // Inventory

        public double ProductCount
        {
            get { return productCount; }
            set { productCount = value; }
        }
        //
        public string TakeCount
        {
            get { return takeCount; }
            set { takeCount = value; }
        }


        public DateTime InventoryDate
        {
            get { return inventoryDate; }
            set { inventoryDate = value; }
        }

        // Common
        public int CreatedByUserID
        {
            get { return createdByUserID; }
            set { createdByUserID = value; }
        }

        public DateTime CreatedOnDate
        {
            get { return createdOnDate; }
            set { createdOnDate = value; }
        }

        public int LastModifiedByUserID
        {
            get { return lastModifiedByUserID; }
            set { lastModifiedByUserID = value; }
        }

        public DateTime LastModifiedOnDate
        {
            get { return lastModifiedOnDate; }
            set { lastModifiedOnDate = value; }
        }

        public string LastModifiedByUserName
        {
            get { return lastModifiedByUserName; }
            set { lastModifiedByUserName = value; }
        }

        public string CreatedByUserName
        {
            get
            {
                if (createdByUserName == null)
                {
                    int portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
                    UserController controller = new UserController();
                    UserInfo user = controller.GetUser(portalId, createdByUserID);
                    createdByUserName = user.DisplayName;
                }
                return createdByUserName;
            }
        }


        #endregion
    }
}
