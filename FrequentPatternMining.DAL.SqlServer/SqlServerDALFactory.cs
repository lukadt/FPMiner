using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FrequentPatternMining.DAL.SqlServer
{
 
    /// <summary>
    /// Represent the concrete class constructor specialized for sql server
    /// </summary>
    public class SqlServerDALFactory : DALFactory
    {

        /// <summary>
        /// Create using reflection the concrete DAL specified in app.config file
        /// </summary>
        /// <returns>the sql server concrete DAL</returns>
        public override IDAL CreateDAL()
        {
            return (IDAL)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["DALType"]));
        }

    }
}
