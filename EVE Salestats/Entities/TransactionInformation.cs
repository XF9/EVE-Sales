using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace EVE_SaleTools.Entities
{
    /// <summary>
    /// Element to keep general information about an item
    /// </summary>
    class TransactionInformation
    {
        private int typeID;
        [PrimaryKey]
        /// <summary>
        /// the type ID
        /// </summary>
        public int TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        private int marketGroupIDSubLevel;
        /// <summary>
        /// GroupID, second level
        /// </summary>
        public int MarketGroupIDSubLevel
        {
            get { return marketGroupIDSubLevel; }
            set { marketGroupIDSubLevel = value; }
        }

        private string marketGroupNameSubLevel;
        /// <summary>
        /// Group name, second level
        /// </summary>
        public string MarketGroupNameSubLevel
        {
            get { return marketGroupNameSubLevel; }
            set { marketGroupNameSubLevel = value; }
        }

        private int marketGroupIDTopLevel;
        /// <summary>
        /// Group ID, first level
        /// </summary>
        public int MarketGroupIDTopLevel
        {
            get { return marketGroupIDTopLevel; }
            set { marketGroupIDTopLevel = value; }
        }

        private string marketGroupNameTopLevel;
        /// <summary>
        /// Group name, first level
        /// </summary>
        public string MarketGroupNameTopLevel
        {
            get { return marketGroupNameTopLevel; }
            set { marketGroupNameTopLevel = value; }
        }

        private int iconID;
        /// <summary>
        /// the icon
        /// </summary>
        public int IconID
        {
          get { return iconID; }
          set { iconID = value; }
        }

        private string description;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public static async Task<TransactionInformation> Load(int itemID){
            var sqliteStaticData = new SQLiteAsyncConnection(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "\\Assets\\Data\\static.sqlite");
            string query = "SELECT ";
            query += "Item.TypeID AS TypeID, ";
            query += "Item.Description AS Description, ";
            query += "Item.IconID AS IconID, ";
            query += "MarketSub.MarketGroupID AS MarketGroupIDSubLevel, ";
            query += "MarketSub.MarketGroupName AS MarketGroupNameSubLevel, ";
            query += "MarketTop.MarketGroupID AS MarketGroupIDTopLevel, ";
            query += "MarketTop.MarketGroupName AS MarketGroupNameTopLevel ";

            query += "FROM ";
            query += "InventoryTypes AS Item ";
            query += "INNER JOIN MarketGroups AS MarketSub ON Item.MarketGroupID = MarketSub.MarketGroupID ";
            query += "INNER JOIN MarketGroups AS MarketTop ON MarketSub.ParentGroupID = MarketTop.MarketGroupID ";

            query += "WHERE Item.TypeID = " + itemID;

            List<TransactionInformation> result = await sqliteStaticData.QueryAsync<TransactionInformation>(query);
            if (result.Count > 0)
                return result.First();
            
            TransactionInformation tainfo = new TransactionInformation();
            tainfo.TypeID = itemID;
            tainfo.Description = "Nothing to see here";
            tainfo.IconID = 0;
            tainfo.MarketGroupIDSubLevel = -1;
            tainfo.MarketGroupNameSubLevel = "unknonw";
            tainfo.MarketGroupIDTopLevel = -1;
            tainfo.MarketGroupNameTopLevel = "unknonw";
            return tainfo;
        }
    }
}
