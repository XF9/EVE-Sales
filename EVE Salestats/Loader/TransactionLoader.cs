﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

using SQLite;

using EVE_SaleTools.Entities;

namespace EVE_SaleTools.Loader
{
    /// <summary>
    /// What order Type should be queried
    /// </summary>
    enum OrderType{
        SELL,
        BUY,
        BOTH
    }

    /// <summary>
    /// This one loads all transactions for a given character
    /// </summary>
    class TransactionLoader
    {
        /// <summary>
        /// Load transaction data and store in local sqlite db
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="vCode"></param>
        /// <param name="charID"></param>
        /// <returns></returns>
        public async static Task<bool> Parse(string apiKey, string vCode, string charID)
        {
            // open DB
            var sqliteTransactionData = new SQLiteAsyncConnection(charID + ".sqlite");
            await sqliteTransactionData.CreateTableAsync<Transaction>();

            //await sqliteTransactionData.ExecuteAsync("ATTACH DATABASE '" + Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "\\Assets\\Data\\static.sqlite' AS Static");

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
            
            // create new transaction for each record
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

                TransactionInformation info = await TransactionInformation.Load(ta.TypeID);

                ta.MarketGroupID = info.MarketGroupIDTopLevel;
                ta.MarketGroupName = info.MarketGroupNameTopLevel;

                //TODO: Insert or ignore to make it faster?
                await sqliteTransactionData.InsertOrReplaceAsync(ta);
            }

            return false;
        }

        async public static Task<List<Transaction>> GetByTypeName(string charID, string TypeName, OrderType orderType, bool groupOnDay)
        {
            //TODO check if file eyists
            var sqlite = new SQLiteAsyncConnection(charID + ".sqlite");
            List<Transaction> resultset = new List<Transaction>();
            List<string> whereClause = new List<string>();

            string requestString = "SELECT * FROM 'Transaction' ";

            // build where
            if (TypeName.Length > 0)
                whereClause.Add("TypeName = " + TypeName);
            
            if(orderType == OrderType.SELL)
                whereClause.Add("BuyOrder = 0");
            else if (orderType == OrderType.BUY)
                whereClause.Add("BuyOrder = 1");

            if (whereClause.Count > 0)
                requestString += "WHERE " + String.Join(" AND ", whereClause.ToArray()) + " ";

            // group by
            if (TypeName.Length > 0 && groupOnDay)
                requestString += "GROUP BY strftime('%d-%m-%Y',Time)";

            try
            {
                resultset = await sqlite.QueryAsync<Transaction>(requestString);
            }
            catch(Exception )
            {
                System.Diagnostics.Debug.WriteLine("query failed: " + requestString);
            }

            return resultset;
        }
    }
}
