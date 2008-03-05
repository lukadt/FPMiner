using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.Configuration;

namespace FrequentPatternMining.DAL
{
    /// <summary>
    /// Represent an Abstract Factory. 
    /// </summary>
    public abstract class DALFactory
    {
        /// <summary>
        /// Factory method that creates through reflection a concrete DAL.
        /// This method could be overriden
        /// </summary>
        /// <returns></returns>
        public virtual IDAL CreateDAL()
        {
            Type tipo = Type.GetType(ConfigurationManager.AppSettings["DALType"]);
            return (IDAL)Activator.CreateInstance(tipo);
        }
    }
}
