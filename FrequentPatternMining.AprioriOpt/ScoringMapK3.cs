using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;

namespace FrequentPatternMining.APrioriOpt
{
    /// <summary>
    /// Represent a 3-dimensional map in which each location store a score obtained by a 
    /// complete scan of transaction list database. 
    /// </summary>
    class ScoringMapK3
    {
        Dictionary<int, int> Map;
        int size;
        int[][][] jagged;

        public ScoringMapK3(Dictionary<int, int> FrequentItems)
        {
            int i, j, k;

            i = 0;
            size = FrequentItems.Count;
            Map = new Dictionary<int, int>();
            
            jagged = new int[size][][];
            for (i = 0; i < size; i++)
            {
                jagged[i] = new int[i][];
                for (j = 0; j < i; j++)
                {
                    jagged[i][j] = new int[j];
                }
            }

            int[] itemsort = new int[size];
            i = 0;
            foreach (int itemset in FrequentItems.Keys)
            {
                itemsort[i++] = itemset;
            }
            Array.Sort(itemsort);
            i = 0;
            foreach (int itemset in itemsort)
            {
                Map.Add(itemset, i++);
            }

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < jagged[i].Length; j++)
                {
                    for (k = 0; k < jagged[i][j].Length; k++)
                    {
                        jagged[i][j][k] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Efficient method based on scoring map used to compute candidate support.
        /// A complete database transaction list scan is required
        /// </summary>
        /// <param name="AllTrans">Transactions database list</param>
        /// <param name="Candidates">Candidates list</param>
        public void CalcSupport(List<Transaction> AllTrans, List<ItemSet> Candidates)
        {
            int x, y, z, i, j, k, item1, item2, item3;
            int num;
            // We loop an each transaction, obtain 3 indeces used
            // to access and update a scoring map location where finally we search
            // for candidate support
            foreach (Transaction trans in AllTrans)
            {
                num = trans.Itemset.ItemsNumber;
                if (num > 2)
                {
                    i = 0;
                    while (i < (num - 2))
                    {
                        item1 = trans.Itemset.Items[i];
                        if (Map.ContainsKey(item1))
                        {
                            for (j = (i + 1); j < (num - 1); j++)
                            {
                                item2 = trans.Itemset.Items[j];
                                if (Map.ContainsKey(item2))
                                {
                                    for (k = (j + 1); k < (num); k++)
                                    {
                                        item3 = trans.Itemset.Items[k];
                                        if (Map.ContainsKey(item3))
                                        {
                                            x = Map[item1];
                                            y = Map[item2];
                                            z = Map[item3];
                                            jagged[z][y][x]++;
                                        }
                                    }
                                }
                            }
                        }
                        i++;
                    }
                }
            }
            foreach (ItemSet candidate in Candidates)
            {
                x = Map[candidate.Items[0]];
                y = Map[candidate.Items[1]];
                z = Map[candidate.Items[2]];
                candidate.ItemsSupport = jagged[z][y][x];
            }
        }
    }
}
