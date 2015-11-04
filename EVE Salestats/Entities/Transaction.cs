using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace EVE_SaleTools.Entities
{
    /// <summary>
    /// A transaction, done by the 
    /// </summary>
    class Transaction
    {
        private long transactionID;
        /// <summary>
        /// Transaction id
        /// </summary>
        [PrimaryKey]
        public long TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }

        private DateTime time;
        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        private int quantity;
        /// <summary>
        /// Quantity of moved items
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private int typeID;
        /// <summary>
        /// ID of the sold items
        /// </summary>
        public int TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        private String typeName;
        /// <summary>
        /// Name of the sold items
        /// </summary>
        public String TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private double pricePerUnit;
        /// <summary>
        /// Price per unit
        /// </summary>
        public double PricePerUnit
        {
            get { return pricePerUnit; }
            set { pricePerUnit = value; }
        }

        private int clientID;
        /// <summary>
        /// Customer ID
        /// </summary>
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        private String clientName;
        /// <summary>
        /// Customer name
        /// </summary>
        public String ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        private int stationID;
        /// <summary>
        /// Station on which this item was traded
        /// </summary>
        public int StationID
        {
            get { return stationID; }
            set { stationID = value; }
        }

        private String stationName;
        /// <summary>
        /// Name of the station where this item was traded
        /// </summary>
        public String StationName
        {
            get { return stationName; }
            set { stationName = value; }
        }

        private bool buyOrder;
        /// <summary>
        /// 1 - buy
        /// 0 - sell
        /// </summary>
        public bool BuyOrder
        {
            get { return buyOrder; }
            set { buyOrder = value; }
        }

        public String taFor;
        /// <summary>
        /// Wether the transaction is personal or corp wide
        /// </summary>
        public String TaFor
        {
            get { return taFor; }
            set { taFor = value; }
        }

        private long journalTransactionID;
        /// <summary>
        /// Transaction ID within the players journal
        /// </summary>
        public long JournalTransactionID
        {
            get { return journalTransactionID; }
            set { journalTransactionID = value; }
        }

        private int clientTypeID;
        /// <summary>
        /// ???
        /// </summary>
        public int ClientTypeID
        {
            get { return clientTypeID; }
            set { clientTypeID = value; }
        }
    }
}
