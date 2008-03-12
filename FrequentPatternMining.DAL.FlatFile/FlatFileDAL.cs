using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace FrequentPatternMining.DAL.FlatFile
{
    /// <summary>
    /// Flat file data access layer, uses functionality
    /// optimized for access a flat file
    /// </summary>
    public class FlatFileDAL: IDAL
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
        /// Method that retrieves the whole transaction list parsing
        /// a flat file containing a transaction Id and an item value
        /// per row
        /// </summary>
        /// <returns>Transaction database list</returns>
        public List<Transaction> GetAllTransactions()
        {

            List<Transaction> result = new List<Transaction>();
            FileStream flatFile = new FileStream(ConfigurationManager.ConnectionStrings["BasketConnectionString"].ConnectionString, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(flatFile);            
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

            string actualRow = null;
            while ((actualRow = reader.ReadLine())!=null )
            {

                Match integerMatches = numericalReg.Match(actualRow);

                if(integerMatches.Success==true)
                {
                newTid = Int32.Parse(integerMatches.Value);
                integerMatches = integerMatches.NextMatch();
                //We move forward and skip the whitespaces 
                integerMatches = integerMatches.NextMatch();
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

                    //Check if the item has been already stored in the map or not
                    mapIndex = map.GetIndex(integerMatches.Value);
                    if (mapIndex == -1)
                    {
                        map.Insert((Object)integerMatches.Value);
                        int newmapIndex = 0;
                        //Obtain a new index for the item                                                
                        newmapIndex = map.GetIndex(integerMatches.Value);                                                
                        t.addItem(newmapIndex);
                    }
                    else
                        t.addItem(mapIndex);
            }
            reader.Close();
            flatFile.Close();
            return (result);
        }        
    }
}
