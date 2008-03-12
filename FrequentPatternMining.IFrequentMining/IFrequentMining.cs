using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;

namespace FrequentPatternMining.IFrequentMining
{
    /// <summary>
    /// Define the standard contract a data mining extraction
    /// frequent pattern algorithm should implement.
    /// </summary>
    public interface IFrequentPatternMining
    {
        /// <summary>
        /// Extract the frequent pattern from basket transactions
        /// </summary>
        /// <param name="allTrans">the list of all transaction</param>
        /// <returns>the frequent itemsets list</returns>
        List<ItemSet> ExtractFrequentPattern(List<Transaction> allTrans);
        
        /// <summary>
        /// Set the minimum support
        /// </summary>
        /// <param name="minSup">minimum support</param>
        void SetMinSup(Double minSup);
    }
}
