using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace EVE_Salestats.Char
{
    enum TransactionType{
        BUY,
        SELL
    }

    class Transaction
    {
        private int id;
        /// <summary>
        /// Transaction id
        /// </summary>
        [PrimaryKey]
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private float pricePerUnit;
        /// <summary>
        /// Price per unit
        /// </summary>
        public float PricePerUnit
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

        private TransactionType transactionType;
        /// <summary>
        /// Type: Buy or Sell
        /// </summary>
        internal TransactionType TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }

        private int journalTransactionID;
        /// <summary>
        /// Transaction ID within the players journal
        /// </summary>
        public int JournalTransactionID
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
