using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace FrequentPatternMining.DAL.SqlServer
{
    /// <summary>
    /// SqlServer data access layer, uses functionality
    /// optimized for sqlserver
    /// </summary>
    public class SqlServerDAL : IDAL
    {
        private Map map;

        /// <summary>
        /// Provide singleton instance map
        /// </summary>
        /// <returns>Map that associate to an item value a corrisponding integer</returns>
        public Map GetMap()
        {
            return map;
        }

        /// <summary>
        /// Method that retrieves the whole transaction list using
        /// SPGetAllOrderTransactions stored procedures 
        /// </summary>
        /// <returns>Transaction database list</returns>
        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> result = new List<Transaction>();
            SqlDataReader reader;
            reader = SqlHelper.ExecSPForReader(ConfigurationManager.ConnectionStrings["BasketConnectionString"].ConnectionString, "SPGetAllOrderTransactions", null);
            //Create a new map
            map = new Map();

            //Keep track of the current and the previous Transaction Id
            int mapIndex = 0;
            int oldTid = 0;
            int newTid = 0;
            Transaction t = null;
            bool first = true;
            //Regex matching integers
            Regex numericalReg = new Regex("[\\d]*");
            //Regex matching characters
            Regex charReg = new Regex("[a-zA-Z]");

            while (reader.Read())
            {
                //Analysis of the column data type
                if (reader.GetFieldType(0) == typeof(int))
                {
                    newTid = !reader.IsDBNull(0) ? reader.GetInt32(0) : -2;
                }
                else
                {
                    newTid = !reader.IsDBNull(0) ? Int32.Parse(reader.GetString(0)) : -2;
                }
                //If the current Tid is the same of the previous Tid the current row belong 
                //to the same transaction
                if ((newTid != oldTid) || first)
                {
                    t = new Transaction();
                    t.Id = newTid;
                    result.Add(t);
                    oldTid = newTid;
                    first = false;
                }
                //Check if item data type is an integer, the column representing the item 
                //contain a single item
                if (reader.GetFieldType(1) == typeof(int))
                {
                    //Check if the item has been already stored in the map or not
                    mapIndex = map.GetIndex(reader.GetInt32(1));
                    if (mapIndex == -1)
                    {
                        if (reader.GetFieldType(1) == typeof(int))
                        {
                            map.Insert((Object)reader.GetInt32(1));
                        }
                        else
                        {
                            map.Insert((Object)reader.GetString(1));
                        }
                        int newmapIndex = 0;
                        //Obtain a new index for the item
                        if (reader.GetFieldType(1) == typeof(int))
                        {
                            newmapIndex = map.GetIndex(reader.GetInt32(1));
                        }
                        else
                        {
                            newmapIndex = map.GetIndex(reader.GetString(1));
                        }
                        t.addItem(newmapIndex);
                    }
                    else
                        t.addItem(mapIndex);
                }
                //Item or items data type are string or strings; we consider both a column item
                //representation where could be stored one item or more items together                
                else
                {
                    String wholeItems = reader.GetString(1);
                    Match MatchCharResults = charReg.Match(wholeItems);
                    if (MatchCharResults.Success)
                    {

                        mapIndex = map.GetIndex(wholeItems);
                        if (mapIndex == -1)
                        {
                            if (reader.GetFieldType(1) == typeof(int))
                            {
                                map.Insert((Object)reader.GetInt32(1));
                            }
                            else
                            {
                                map.Insert((Object)reader.GetString(1));
                            }
                            int newmapIndex = 0;
                            if (reader.GetFieldType(1) == typeof(int))
                            {
                                newmapIndex = map.GetIndex(reader.GetInt32(1));
                            }
                            else
                            {
                                newmapIndex = map.GetIndex(reader.GetString(1));
                            }
                            t.addItem(newmapIndex);
                        }
                        else
                            t.addItem(mapIndex);
                    }
                    else
                    {
                        Match MatchNumResults = numericalReg.Match(wholeItems);
                        while (MatchNumResults.Success)
                        {
                            if (!String.Equals(MatchNumResults.Value, String.Empty))
                            {
                                mapIndex = map.GetIndex(MatchNumResults.Value);

                                if (mapIndex == -1)
                                {
                                    if (reader.GetFieldType(1) == typeof(int))
                                    {
                                        map.Insert((Object)reader.GetInt32(1));
                                    }
                                    else
                                    {
                                        map.Insert(MatchNumResults.Value);
                                    }
                                    int newmapIndex = 0;
                                    if (reader.GetFieldType(1) == typeof(int))
                                    {
                                        newmapIndex = map.GetIndex(reader.GetInt32(1));
                                    }
                                    else
                                    {
                                        newmapIndex = map.GetIndex(MatchNumResults.Value);
                                    }
                                    t.addItem(newmapIndex);
                                }
                                else
                                    t.addItem(mapIndex);
                            }
                            MatchNumResults = MatchNumResults.NextMatch();
                        }
                    }
                }
            }
            return (result);
        }
    }
}
