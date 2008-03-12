using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;

namespace FrequentPatternMining.APrioriOpt
{
    /// <summary>
    /// Represent a bidimensional map in which each location store a score obtained by a 
    /// complete scan of transaction list database. For each transaction we compute all possible
    /// pair combinations; we derive,for each combinations obtained, a pair of index (i,j) used to 
    /// access the corresponding map location and increment it's score
    /// </summary>
    class ScoringMapK2
    {
        Dictionary<int, int> Map;
        int[][] jagged;
        int size;

               
        /// <param name="FrequentItems">The list of frequent items i want to index for</param>
        public ScoringMapK2(Dictionary<int, int> FrequentItems)
        {
            int i, j;

            i = 0;
            size = FrequentItems.Count;
            Map = new Dictionary<int, int>();

            // Jagged arrays reduce spatial complexity
            jagged = new int[size][];
            for (i = 0; i < size; i++)
            {
                jagged[i] = new int[i];
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

            //Jagged array initialization
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < jagged[i].Length; j++) jagged[i][j] = 0;
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
            int x, y, i, j, item1, item2;
            int num;
            // We loop an each transaction, obtain a pair of index used
            // to access and update a scoring map location where finally we search
            // for candidate support
            foreach (Transaction trans in AllTrans)
            {
                num = trans.Itemset.ItemsNumber;
                if (num > 1)
                {
                    i = 0;
                    while (i < (num - 1))
                    {
                        item1 = trans.Itemset.Items[i];
                        if (Map.ContainsKey(item1))
                        {
                            for (j = (i + 1); j < num; j++)
                            {
                                item2 = trans.Itemset.Items[j];
                                if (Map.ContainsKey(item2))
                                {
                                    x = Map[item1];
                                    y = Map[item2];
                                    jagged[y][x]++;
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
                candidate.ItemsSupport = jagged[y][x];
            }
        }
    }
}
