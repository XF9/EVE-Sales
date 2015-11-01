using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

using SQLite;

using EVE_Salestats.Entities;

namespace EVE_Salestats.Loader
{
    class TransactionLoader
    {
        public async static Task<bool> Load(string apiKey, string vCode, string charID)
        {
            // open DB
            var sqlite = new SQLiteAsyncConnection(charID);
            await sqlite.CreateTableAsync<Transaction>();

            // build request string
            String request = "https://api.eveonline.com/char/WalletTransactions.xml.aspx";
            request += "?keyID=" + apiKey;
            request += "&vCode=" + vCode;
            request += "&characterID=" + charID;

            // load data as xml string
            HttpResponseMessage response = await new HttpClient().GetAsync(request);
            string xml_data = await response.Content.ReadAsStringAsync();

            // parse as xml and fetch data
            XDocument transactiondata = XDocument.Parse(xml_data);
            var transactions = from transaction in transactiondata.Descendants("row")
                             select new
                             {
                                 TransactionDateTime = transaction.Attribute("transactionDateTime").Value ?? "",
                                 TransactionID = transaction.Attribute("transactionID").Value ?? "",
                                 Quantity = transaction.Attribute("quantity").Value ?? "",
                                 TypeName = transaction.Attribute("typeName").Value ?? "",
                                 TypeID = transaction.Attribute("typeID").Value ?? "",
                                 Price = transaction.Attribute("price").Value ?? "",
                                 ClientID = transaction.Attribute("clientID").Value ?? "",
                                 ClientName = transaction.Attribute("clientName").Value ?? "",
                                 StationID = transaction.Attribute("stationID").Value ?? "",
                                 StationName = transaction.Attribute("stationName").Value ?? "",
                                 TransactionType = transaction.Attribute("transactionType").Value ?? "",
                                 TransactionFor = transaction.Attribute("transactionFor").Value ?? "",
                                 JournalTransactionID = transaction.Attribute("journalTransactionID").Value ?? "",
                                 ClientTypeID = transaction.Attribute("clientTypeID").Value ?? ""
                             };
            
            foreach (var transaction in transactions)
            {
                Transaction ta = new Transaction();

                ta.Time = DateTime.Parse(transaction.TransactionDateTime);
                ta.TransactionID = Int64.Parse(transaction.TransactionID);
                ta.Quantity = Int32.Parse(transaction.Quantity);
                ta.TypeName = transaction.TypeName;
                ta.TypeID = Int32.Parse(transaction.TypeID);
                ta.PricePerUnit = double.Parse(transaction.Price);
                ta.ClientID = Int32.Parse(transaction.ClientID);
                ta.ClientName = transaction.ClientName;
                ta.StationID = Int32.Parse(transaction.StationID);
                ta.StationName = transaction.StationName;
                ta.BuyOrder = transaction.TransactionType.Equals("buy") ? true : false;
                ta.TaFor = transaction.TransactionFor;
                ta.JournalTransactionID = Int64.Parse(transaction.JournalTransactionID);
                ta.ClientTypeID = Int32.Parse(transaction.ClientTypeID);

                //TODO: Insert or ignore to make it faster
                await sqlite.InsertOrReplaceAsync(ta);
            }

            return false;
        }
    }
}
