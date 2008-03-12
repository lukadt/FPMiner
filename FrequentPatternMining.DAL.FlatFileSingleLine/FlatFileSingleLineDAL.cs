using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.IO;
using System.Configuration;

namespace FrequentPatternMining.DAL.FlatFileSingleLine
{
     /// <summary>
        /// Flat file data access layer, uses functionality
        /// optimized for access a flat file with one 
        /// transaction per line
        /// </summary>
    public class FlatFileSingleLineDAL : IDAL
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
        /// a flat file containing a transaction items values on the same row
        /// </summary>
        /// <returns>Transaction database list</returns>
        public List<Transaction> GetAllTransactions()
        {
            String WholeLine;
            String[] ParsedLine;
            StreamReader InputData;
            int mapIndex;

            List<Transaction> result = new List<Transaction>();

            // create a new map
            map = new Map();

            // retrieve the relative path of the flat file from the configuration data
            String AbsFilepath = ConfigurationManager.ConnectionStrings["BasketConnectionString"].ConnectionString;            
            // open the stream for reading
            InputData = File.OpenText(AbsFilepath);

            // foreach line in the flat file do:
            while (!InputData.EndOfStream)
            {
                // create a new empty transaction
                Transaction trans = new Transaction();

                WholeLine = InputData.ReadLine();

                // split the line in chunks. each chunk is a text-formatted item value delimited by black spaces
                ParsedLine = WholeLine.Split(' ');

                foreach (String TextItem in ParsedLine)
                {
                    // check whether the current chunk is the empty string or not
                    if (TextItem != "")
                    {
                        // parse the actual item integer value from the chunk
                        int item = Int32.Parse(TextItem);

                        // try to get the map index for this item
                        mapIndex = map.GetIndex(item);

                        // check if the item has been already stored in the map or not
                        if (mapIndex == -1)
                        {
                            // if not, add the item to the map and get its map index
                            map.Insert(item);
                            mapIndex = map.GetIndex(item);
                        }
                        // add the map index to the transaction
                        trans.addItem(mapIndex);
                    }
                }
                // add the transaction to result
                result.Add(trans);
            }

            // we are done. close the stream and return result
            InputData.Close();
            return result;
        }        
    }
}
