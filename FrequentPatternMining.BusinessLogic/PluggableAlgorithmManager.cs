using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using FrequentPatternMining.IFrequentMining;
using System.Configuration;


namespace FrequentPatternMining.BusinessLogic
{
    /// <summary>
    /// Represent the manager of the various pluggabble algorithm
    /// loaded at runtime
    /// </summary>
    public class PluggableAlgorithmManager
    {
        private List<IFrequentPatternMining> _miningAlgorithms;

        /// <summary>
        /// Minimum relative Confidence 
        /// </summary>
        public Double Minconf
        {
            get
            {
                Double minconf;
                Double.TryParse(ConfigurationManager.AppSettings["ConfidenzaMinima"], out minconf);
                return minconf / 100;
            }
        }

        /// <summary>
        /// Minimum relative support
        /// </summary>
        public Double Minsupp
        {
            get
            {
                Double minsupp;
                Double.TryParse(ConfigurationManager.AppSettings["SupportoMinimo"], out minsupp);
                return minsupp / 100;
            }
        }

        
        /// <summary>
        /// Get or Set the list of avalaible mining algorithms
        /// </summary>
        public List<IFrequentPatternMining> MiningAlgorithms
        {
            get { return _miningAlgorithms; }
            set { _miningAlgorithms = value; }
        }

        
        /// <summary>
        /// Default constructor, searching for algorithms in the folder defined in app.config 
        /// </summary>
        public PluggableAlgorithmManager()
        {
            String AlgorithmFolder = String.Concat(System.Environment.CurrentDirectory, @"\", ConfigurationManager.AppSettings["AlgorithmFolder"]);
            MiningAlgorithms = GetPlugins<IFrequentPatternMining>(AlgorithmFolder);
        }
        
        /// <summary>
        /// Method for loading runtime algorithms using reflection
        /// </summary>
        /// <typeparam name="T">The type we want to load plugin for</typeparam>
        /// <param name="folder">The folder where looking for plugin algorithms</param>
        /// <returns>list of plugin of type T</returns>
        public List<T> GetPlugins<T>(string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");
            List<T> tList = new List<T>();
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type[] types = new Type[] { type };
                        if (type.GetInterface("FrequentPatternMining.IFrequentMining.IFrequentPatternMining") != null)
                        {
                            object obj = Activator.CreateInstance(type);
                            T t = (T)obj;
                            tList.Add(t);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return tList;
        }

        /// <summary>
        /// Return the algorithm specified by an integer index
        /// </summary>
        /// <param name="algorithmIndex">index in the algorithm list</param>
        /// <returns>the indexed algorithm</returns>
        public IFrequentPatternMining CreateInstance(int algorithmIndex)
        {
            return MiningAlgorithms[algorithmIndex];
        }
    }
}