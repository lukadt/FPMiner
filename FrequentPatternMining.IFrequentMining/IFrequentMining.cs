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
        List<ItemSet> ExtractFrequentPattern(List<Transaction> allTrans);
        void SetMinSup(Double minSup);
    }
}
