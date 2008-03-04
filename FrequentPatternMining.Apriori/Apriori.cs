using System;
using System.Collections;
using System.Text;
using FrequentPatternMining.IFrequentMining;
using FrequentPatternMining.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using FrequentPatternMining.APriori;

namespace FrequentPatternMining.APriori
{
    /// <summary>
    /// Basic Implementation of the well-known Apriori algorithm. For more info look at original paper:
    /// "Fast Algorithms for Mining Association Rules",Rakesh Agrawal e Ramakrishnan Srikant (1994).
    /// </summary>
    public class Apriori : IFrequentPatternMining
    {
        private Double _minSup; // Minimum relative support value
        private int absMinSup;  // Minimum absolute support value
        private Dictionary<ItemSet, bool> FrequentItemSets; // Frequent ItemSets generic set
        private List<ItemSet> CandidateItemSets; // Candidate ItemSets generic set 
        private List<ItemSet> Result; // final frequent ItemSets result
        private int k; // Apriori k-generation loop corrisponding to itemset of lenght k
        private Dictionary<int, int> FrequentItems; // Frequent 1-ItemSet look up table (item vs frequency)
        private List<Transaction> FlaggedDB; // transaction list containing only frequent item (unfrequent removed)


        public Apriori()
        {
            FrequentItemSets = new Dictionary<ItemSet, bool>(new EqualityComparerItemSet());
            FrequentItems = new Dictionary<int, int>();
            Result = new List<ItemSet>();
            FlaggedDB = new List<Transaction>();
        }
        
        public void SetMinSup(Double Minsup)
        {
            this._minSup = Minsup;
        }

        /// <summary>
        /// Main frequent pattern extraction method that implement Apriori logic
        /// </summary>
        /// <param name="AllTrans">Total list of input transaction</param>
        /// <returns>Frequent ItemSets</returns>
        public List<ItemSet> ExtractFrequentPattern(List<Transaction> AllTrans)
        {
            // compute absolute support = relative support * # of transactions
            absMinSup = (int)System.Math.Ceiling(AllTrans.Count * _minSup);

            // Sort item inside every database transaction (itemset sorting)
            foreach (Transaction trans in AllTrans)
            {
                trans.sort();
            }

            // Compute support for 1-itemset and store in FrequentItems dictionary
            foreach (Transaction trans in AllTrans)
            {
                foreach (int product in trans.Itemset.Items)
                {
                    if (FrequentItems.ContainsKey(product))
                    {
                        FrequentItems[product]++;
                    }
                    else
                    {
                        FrequentItems.Add(product, 1);
                    }
                }
            }
            
            // Evaluate those 1-itemset whose support is greater than minimum support 
            // and add them to both frequent 1-itemset and final frequent itemsets result
            foreach (int product in FrequentItems.Keys)
            {
                if (FrequentItems[product] >= absMinSup) 
                {
                    ItemSet frequent = new ItemSet();  
                    frequent.Add(product);
                    frequent.ItemsSupport = FrequentItems[product];
                    FrequentItemSets.Add(frequent, true);
                    Result.Add(frequent); 
                }
            }

            //Build Look Up frequent items table
            FrequentItems.Clear();
            foreach (ItemSet itemset in FrequentItemSets.Keys)
            {
                FrequentItems.Add(itemset.Items[0], 0);
            }

            // Remove unfrequent item from transactions list
            foreach (Transaction trans in AllTrans)
            {
                Transaction ReducedTrans = new Transaction();
                foreach (int item in trans.Itemset.Items)
                {
                    if (FrequentItems.ContainsKey(item))
                    {
                        ReducedTrans.addItem(item);
                    }
                }
                if (ReducedTrans.Itemset.Items.Count > 0)
                {
                    FlaggedDB.Add(ReducedTrans);
                }
            }
            
            AllTrans = FlaggedDB;

            // Apriori main loop, we already computed frequent (k=1) itemset
            for (k = 2; FrequentItemSets.Count > 0; k++)
            {
                // Candidate generation
                CandidateItemSets = CandidateGen(FrequentItemSets, k); 
                // Evalute support for the generated frequent candidate
                CalcSupport(AllTrans, CandidateItemSets);
                
                FrequentItemSets.Clear();
                // Check if  every candidate itemset is a frequent or not and if it is, 
                // add it to final result
                foreach (ItemSet itemset in CandidateItemSets)
                {
                    if (itemset.ItemsSupport >= absMinSup)
                    {
                        FrequentItemSets.Add(itemset, true);
                        Result.Add(itemset);
                    }
                }
            }
            return Result; 
        }


        /// <summary>
        /// Compute support using brute force technique!!! This imply checking all
        /// candidate ItemSet against transaction database list O(mn)
        /// </summary>
        /// <param name="AllTrans">The list of database transaction</param>
        /// <param name="CandidateItemset">The list of candidates</param>
        void CalcSupport(List<Transaction> AllTrans, List<ItemSet> CandidateItemset)
        {
            foreach (ItemSet itemset in CandidateItemset)
            {
                itemset.ItemsSupport = 0;
                foreach (Transaction trans in AllTrans)
                {
                    if (itemset < trans.Itemset)
                    {
                        itemset.ItemsSupport++;
                    }
                }
            }
        }
        
        /// <summary>
        /// First step in Apriori algorithm responsible for the generation of candidate k+1 itemset
        /// starting from k frequent itemset. The generated candidate itemsets could be frequent or not
        /// hence the term candidate
        /// </summary>
        /// <param name="FrequentItemSets">The k generation Frequent ItemSets</param>
        /// <param name="k">the generation (lenght of current itemsets)</param>
        /// <returns>Generated list of candidate ItemSets</returns>
        
        private List<ItemSet> CandidateGen(Dictionary<ItemSet, bool> FrequentItemSets, int k)
        {
            List<ItemSet> NewCandidateSet = new List<ItemSet>();
            ItemSet NewCandidate;
            int ok;

            //Join Step: a candidate is generated joining two frequent itemsets following a  
            //particular query:
            //select p.item1,p.item2,. . . ,p.itemk,q.itemk, (k+1 elements)
            //from Lk p,Lk q
            //where (p.item1 = q.item1,p.item2 = q.item2,. . . ,p.itemk < q.itemk)
            foreach (ItemSet itemset in FrequentItemSets.Keys)
            {
                foreach (int frequentitem in FrequentItems.Keys)
                {
                    if (itemset.Items[itemset.ItemsNumber - 1] < frequentitem)
                    {
                        NewCandidate = new ItemSet();
                        NewCandidate.ItemsSupport = 0;
                        foreach (int item in itemset.Items)
                        {
                            NewCandidate.Add(item);
                        }
                        NewCandidate.Add(frequentitem);

                        //Pruning, based on anti-monotonicity itemset principle (see paper)
                        ok = 0;
                        for (int i = 0; i < k; i++)
                        {
                            ItemSet test = new ItemSet();
                            for (int j = 0; j < k; j++)
                            {
                                if (j != i) test.Add(NewCandidate.Items[j]);
                            }
                            if (FrequentItemSets.ContainsKey(test)) ok++; // This itemset is contained in the frequent list
                            else i = k; // exit from loop this itemset is not contained
                        }
                        if (ok == k) NewCandidateSet.Add(NewCandidate); 
                    }
                }
            }
            return NewCandidateSet; 
        }
    }
}