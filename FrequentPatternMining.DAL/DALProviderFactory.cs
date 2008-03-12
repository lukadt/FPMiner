using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.Reflection;
using System.Configuration;


namespace FrequentPatternMining.DAL
{
    /// <summary>
    /// Factory implementing Singleton and Abstract Factory patterns, create a single
    /// concrete factory instance specified in the configuration file
    /// </summary>
    public class DALProviderFactory
    {
        private static DALFactory activeFactory = null;
        private static Object _dalFactorySync = new Object();

        /// <summary>
        /// Read only static property (Singleton pattern)        
        /// </summary>
        public static DALFactory DALFactory
        {
            get
            {
                if (activeFactory == null)
                {
                    lock (_dalFactorySync)
                    {
                        if (activeFactory == null)
                        {
                            String dalFactoryType = ConfigurationManager.AppSettings["DALFactoryType"];
                            Type tipo = Type.GetType(dalFactoryType);
                            activeFactory = (DALFactory)Activator.CreateInstance(tipo);
                        }
                    }
                }
                return (activeFactory);
            }
        }
    }
}
