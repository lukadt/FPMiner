using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using FrequentPatternMining.IFrequentMining;

namespace FrequentPatternMining.FPGrowth
{
    /// <summary>
    /// Optimized Implementation of the well-known FPGrowth algorithm. For more look at original paper:
    /// "Mining Frequent Patterns without Candidate Generation", Jiawei Han,Jian Pei e Yiwen Yin (1999).
    /// </summary>
    public class FPGrowth : IFrequentPatternMining
    {
        private Double _minSup; // Minimum relative support value
        private int absMinSup;  // Minimum absolute support value
        private List<ItemSet> result; // Final pool list frequent ItemSets

        /// <summary>
        /// Get or set the minumum support
        /// </summary>
        public Double MinSup
        {
            get { return _minSup; }
            set { _minSup = value; }
        }

        public FPGrowth()
        {
            MinSup = default(int);
        }
        
        /// <summary>
        /// Set the minimum support
        /// </summary>
        /// <param name="minSup">the minimum support</param>
        public void SetMinSup(Double minSup)
        {
            _minSup = minSup;
        }

        /// <summary>
        /// Frequent pattern extraction method that implement FPGrowth logic using an 
        /// iterative pattern growth approach
        /// </summary>
        /// <param name="allTrans">Total list of input transaction</param>
        /// <returns>Frequent ItemSets</returns>
        public List<ItemSet> ExtractFrequentPattern(List<Transaction> allTrans)
        {
            // compute absolute support = relative support * # of transactions
            absMinSup = (int)System.Math.Ceiling(allTrans.Count * _minSup);
            result = new List<ItemSet>();
            //We could have used two stacks instead of these lists!!!
            List<FPTree> FPTreeList = new List<FPTree>();
            List<FPTree> FPTreeListNext = new List<FPTree>();
            List<ItemSet> GivenListNow = new List<ItemSet>();
            List<ItemSet> GivenListNext = new List<ItemSet>();
            FPTree Next;
            ItemSet Given;
            ItemSet Beta;

            
            List<ItemSet> wholeTransToItemSet = new List<ItemSet>();
            foreach (Transaction trans in allTrans)
            {
                wholeTransToItemSet.Add(trans.Itemset);
            }


            FPTree StartFPtree = new FPTree();
            StartFPtree.SetMinSup(absMinSup);
            //Build the first tree on the whole transaction database list
            StartFPtree.BuildFirstTree(wholeTransToItemSet);
            //Add the first tree to the list of the fptree to process
            FPTreeList.Add(StartFPtree);

            //Here our given prefix is null
            GivenListNow.Add(new ItemSet());

            //Looping on each fptree until there are ones to process
            while (FPTreeList.Count > 0) 
            {
                for (int j = 0; j < FPTreeList.Count; j++) 
                {
                    Next = FPTreeList[j];
                    Given = GivenListNow[j];
                    if (!Next.isEmpty())
                    {
                        // If the FPTree we are examining is composed of a single path
                        // we use an optimization based on path node combination which
                        // arrest current iteration
                        if (Next.HasSinglePath)
                        {
                            GenerateCombPattern(Next, Given);
                        }
                        else
                        {
                            // Header table sorting
                            Next.SortHeaderTable();
                            //Loop on each header table entry
                            for (int i = 0; i < Next.GetHTSize(); i++) 
                            {
                                //New beta ItemSet representing a frequent pattern
                                Beta = new ItemSet();
                                //Concatenate with items present in the given ItemSet
                                foreach (int item in Given.Items)
                                {
                                    Beta.Add(item);
                                }
                                Beta.Add(Next.GetHTItem(i));
                                Beta.ItemsSupport = Next.GetHTFreq(i);

                                // Add beta to frequent pattern result
                                result.Add(Beta);

                                // Here we generate the so called Conditional FPTree using a projection 
                                // of the original database called Conditional Pattern Base
                                FPTreeListNext.Add(Next.CreateFPtree(i)); 
                                
                                // Insert current beta in next given list
                                GivenListNext.Add(Beta);
                            }
                            FPTreeList[j] = null;
                            GivenListNow[j] = null;
                        }
                    }
                }
                FPTreeList.Clear();
                GivenListNow.Clear();
                for (int j = 0; j < GivenListNext.Count; j++)
                {
                    FPTreeList.Add(FPTreeListNext[j]);
                    GivenListNow.Add(GivenListNext[j]);
                    GivenListNext[j] = null;
                    FPTreeListNext[j] = null;
                }
                GivenListNext.Clear();
                FPTreeListNext.Clear();
            }
            return result;
        }

        /// <summary>
        /// Efficient optimization in frequent pattern generation when an FPTree present only a single path
        /// Generated  frequent are automatically inserted in result (frequent pattern pool).
        /// The method enumerates all combinations of node in FPTree fp using efficiently bit operators
        /// </summary>
        /// <param name="fp">Single path FPtree </param>
        /// <param name="given">ItemSet given prefix</param>
        private void GenerateCombPattern(FPTree fp, ItemSet given)
        {
            int bits = fp.Depth;
            UInt64 enumerator = 1;
            UInt64 combination;
            UInt64 max = 1;
            int index;
            int[] itemsetArray = new int[fp.Depth];
            int[] supportArray = new int[fp.Depth];
            int betaSupport;

            fp.Travel = fp.Root;
            for (int i = 0; i < fp.Depth; i++)
            {
                fp.Travel = fp.Travel.Childs[0];
                itemsetArray[i] = fp.Travel.Item;
                supportArray[i] = fp.Travel.Count;
            }
            Array.Reverse(itemsetArray);
            Array.Reverse(supportArray);
            //Max represent the overflow condition
            max = max << bits; 
            //Our enumerator is represented through a 64 bit integer
            while (enumerator < max)
            {
                // Create a new beta result
                ItemSet beta = new ItemSet();
                betaSupport = 0;
                foreach (int item in given.Items)
                {
                    beta.Add(item);
                }

                index = 0;
                combination = enumerator;

                while (combination > 0)
                {
                    if ((combination % 2) == 1)
                    {
                        beta.Add(itemsetArray[index]);
                        if ((betaSupport > supportArray[index]) || (betaSupport == 0))
                        {
                            betaSupport = supportArray[index];
                        }
                    }
                    combination = combination >> 1;
                    index++;
                }
                enumerator++;
                beta.ItemsSupport = betaSupport;
                result.Add(beta);
            }
        }
    }
}
