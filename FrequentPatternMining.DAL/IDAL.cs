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
        Map GetMap();
        List<Transaction> GetAllTransactions();        
    }
}
