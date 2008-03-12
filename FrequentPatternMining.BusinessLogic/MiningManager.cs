using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.IFrequentMining;
using FrequentPatternMining.DAL;
using FrequentPatternMining.Entities;
using System.Reflection;
using System.IO;
using System.Collections;


namespace FrequentPatternMining.BusinessLogic
{
    /// <summary>
    /// Manage and communicate with both data access layer and UI;
    /// </summary>
    public class MiningManager
    {
        private IDAL _concreteDAL;
        private PluggableAlgorithmManager _algorithmManager;

        /// <summary>
        /// The manager of the various pluggable mining algorithms
        /// </summary>
        public PluggableAlgorithmManager AlgorithmManager
        {
            get { return _algorithmManager; }
            set { _algorithmManager = value; }
        }

        /// <summary>
        /// The abstract reference that will be replaced at runtime by a concrete data access layer
        /// </summary>
        public IDAL ConcreteDAL
        {
            get { return _concreteDAL; }
            set { _concreteDAL = value; }
        }

        
        /// <summary>
        /// Method responsable of giving back original data source 
        /// representation building an inverse translation Map
        /// </summary>
        /// <returns>The original data source representation</returns>
        public Map GetMap()
        {
            Map map = ConcreteDAL.GetMap();
            Map inverse = new Map();
            foreach (DictionaryEntry coppia in map)
            {
                inverse.Hash.Add(coppia.Value, coppia.Key);
            }
            return inverse;
        }
        /// <summary>
        /// Initialize both algorithm manager and data access layer
        /// </summary>
        public MiningManager()
        {
            AlgorithmManager = new PluggableAlgorithmManager();
            ConcreteDAL = DALProviderFactory.DALFactory.CreateDAL();
        }

        /// <summary>
        /// Invoke concrete data access layer homonymous method
        /// </summary>
        /// <returns>the list of transactions</returns>
        public List<Transaction> GetAllTransactions()
        {
            return ConcreteDAL.GetAllTransactions();
        }

        /// <summary>
        /// Generate association rules by enumerating all possible subsets of input frequent ItemSets
        /// </summary>
        /// <param name="result">Frequent ItemSets computed by a frequent pattern extraction algorithm</param>
        /// <param name="DBCount">Number of total transactions</param>
        /// <returns>List of generated rules</returns>
        public List<AssociationRule> GenerateAssociationRuleBase(List<ItemSet> result, int DBCount)
        {
            Double Minconf = this.AlgorithmManager.Minconf;
            Double conf;
            // Temporary association rule used to compute support
            List<AssociationRule> LookupRules = new List<AssociationRule>();
            
            List<AssociationRule> Rules = new List<AssociationRule>();

            foreach (ItemSet lk in result)
            {
                // we build association rules only for those ItemSets having more than one frequent item
                if (lk.ItemsNumber > 1) 
                {
                    GenRule(lk, LookupRules);
                }
            }

            //All rules has been created by GenRule, we have to compute rules confidence and consider only those with a confidence equal or
            //greater than minimum confidence threshold            
            Dictionary<ItemSet, int> HashtableResult = new Dictionary<ItemSet, int>();
            foreach (ItemSet itemset in result)
            {
                HashtableResult.Add(itemset, itemset.ItemsSupport);
            }
            foreach (AssociationRule rule in LookupRules)
            {
                rule.LeftSide.ItemsSupport = HashtableResult[rule.LeftSide];
                rule.RightSide.ItemsSupport = HashtableResult[rule.RightSide];
                rule.Confidence = rule.Support / rule.LeftSide.ItemsSupport;
            }

            
            foreach (AssociationRule rule in LookupRules)
            {
                if (rule.Confidence >= Minconf)
                {
                    rule.Support = rule.Support / DBCount;
                    Rules.Add(rule);
                }
            }
            return Rules;
        }

        /// <summary>
        /// Iterative rules generating method
        /// </summary>
        /// <param name="itemset">Frequent pattern ItemSet from which we generate rules</param>
        /// <param name="LookupRules">LookUp association rule list</param>
       
        private void GenRule(ItemSet itemset, List<AssociationRule> LookupRules)
       {
           int bits = itemset.ItemsNumber;
           UInt64 enumerator = 1;
           UInt64 combination;
           UInt64 max = 1;
           int index;
           // max represent "overflow" condition 
           max = max << bits; 
            while (enumerator < max)
           {
               index = 0;
               combination = enumerator;
               // left side creation LHR (am_1)
               ItemSet am_1 = new ItemSet();
               while (combination > 0)
               {
                   if ((combination % 2) == 1)
                   {
                       am_1.Add(itemset.Items[index]);
                   }
                   combination = combination >> 1;
                   index++;
               }
               // Current rule creation
               AssociationRule rule = new AssociationRule();
               rule.LeftSide = am_1;
               rule.RightSide = itemset - am_1;
               rule.LeftSide.ItemsSupport = 0;
               rule.RightSide.ItemsSupport = 0;
               rule.Support = itemset.ItemsSupport;
                if(!((rule.LeftSide.ItemsNumber==0)||(rule.RightSide.ItemsNumber==0)))
               {
                   LookupRules.Add(rule); 
               }
               enumerator++;
           }
        }
    }
}
