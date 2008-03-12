using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FrequentPatternMining.DAL.FlatFile
{
 
    /// <summary>
    /// Represent the concrete class constructor specialized for working with flat file
    /// </summary>
    public class FlatFileDALFactory : DALFactory
    {

        /// <summary>
        /// Create using reflection the concrete DAL specified in app.config file
        /// </summary>
        /// <returns>the flat file concrete DAL</returns>
        public override IDAL CreateDAL()
        {
            return (IDAL)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["DALType"]));
        }

    }
}
