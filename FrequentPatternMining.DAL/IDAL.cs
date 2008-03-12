using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;

namespace FrequentPatternMining.DAL
{
    /// <summary>
    /// DAL Interface
    /// </summary>
    public interface IDAL
    {
        /// <summary>
        /// Get the map used for representing and abstracting itemset values
        /// </summary>
        /// <returns>the Map created by data access layer</returns>
        Map GetMap();
        
        /// <summary>
        /// Retrieve all transaction from a data source
        /// </summary>
        /// <returns>the list of all transaction</returns>
        List<Transaction> GetAllTransactions();        
    }
}
