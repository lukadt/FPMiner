using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FrequentPatternMining.DAL.SqlServer
{
    public class SqlServerDALFactory : DALFactory
    {

        public override IDAL CreateDAL()
        {
            return (IDAL)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["DALType"]));
        }

    }
}
