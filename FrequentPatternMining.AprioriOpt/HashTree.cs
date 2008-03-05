using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using System.Collections;

namespace FrequentPatternMining.APrioriOpt
{
    /// <summary>
    /// HashTree data structure; an Hashtree is a tree where leaf nodes contain a list of
    /// candidate while internal nodes contain an hash table in which each bucket points to
    /// another tree node. HashTree result in a parametric data
    /// structure in the lenght of the leaf node list and the number of bucket of the internal 
    /// hash table nodes. It's an hybrid data structure between a list and an hashtable.
    /// </summary>
    public class HashTree
    {
        // sp is the number of bucket in internal node hash table, h is the lenght of leaf node list
        private int sp, h, i, hash;
        private Node root; 
        private Node travel;
        private int count;
        private Dictionary<ItemSet, bool> result; // Candidate itemset Set
        private Transaction test;
        private List<ItemSet> allcands; // Candidate itemset List
        private List<string> Distr = new List<string>();

        /// <summary>
        /// HashTree Node. This class both represent an internal node or a leaf.
        /// It represent an internal node through an hash table using a modulo function 
        /// combined with a number of child given by the sp parameter. 
        /// It represent a leaf node containing a list of candidate itemsets.
        /// </summary>
        private class Node
        {
            private bool isleaf;
            private Node[] childs;
            private List<ItemSet> cands;
            private int hash;

            public Node()
            {
                isleaf = true;
                childs = null;
                cands = new List<ItemSet>();
            }

            public List<ItemSet> GetCands()
            {
                return cands;
            }

            /// <summary>
            /// Method used when a leaf should be converted in an internal node
            /// </summary>
            /// <param name="count">Node Depth</param>
            /// <param name="sp">Number of childs</param>
            /// <param name="last">Last itemset stored in the leaf node</param>
            public void Convert(int count, int sp, ItemSet last)
            {
                isleaf = false;
                childs = new Node[sp];
                for (int i = 0; i < sp; i++)
                {
                    childs[i] = new Node();
                }
                //Assign the previous candidates to the corrisponding node using hash function
                foreach (ItemSet itemset in cands)
                {
                    hash = (itemset.Items[count].GetHashCode()) % sp;
                    childs[hash].Add(itemset);
                }
                hash = last.Items[count].GetHashCode() % sp;
                childs[hash].Add(last);
                cands.Clear();
            }

            public bool IsLeaf()
            {
                return isleaf;
            }

            public Node GetChildAt(int index)
            {
                return childs[index];
            }

            public void Add(ItemSet itemset)
            {
                cands.Add(itemset);
            }

            public int GetCount()
            {
                return cands.Count;
            }
        }

        public HashTree(int sp, int h)
        {
            this.sp = sp;
            this.h = h;
            root = new Node();
            allcands = new List<ItemSet>();
        }

        //Add a candidate itemset to HashTree
        public void Add(ItemSet itemset)
        {
            // Travel node is used to traverse the tree
            travel = root;
            
            count = 0;
            while (!travel.IsLeaf())
            {
                hash = (itemset.Items[count++].GetHashCode()) % sp;
                travel = travel.GetChildAt(hash);
            }

            // We are in a leaf node, check if i can add a candidate 
            if ((travel.GetCount() < 4) || count >= h) 
            {
                travel.Add(itemset); 
            }
            // We must convert our leaf node in an internal node, 
            //the max lenght of candidate list has already been reached
            else
            {
                travel.Convert(count, sp, itemset);
            }
            allcands.Add(itemset);
        }

        /// <summary>
        /// Compute candidate support counting traversing the Hashtree and visiting leaf node
        /// where candidate are stored
        /// </summary>
        /// <param name="k">starting itemset item position</param>
        /// <param name="start">starting node</param>
        private void ProbeTree(int k, Node start)
        {
            int i;
            //If we are in a leaf we evaluate if update or not candidate support
            if (start.IsLeaf())
            {
                foreach (ItemSet candidate in start.GetCands())
                {
                    //check if candidate itemset is present in current transaction
                    if (candidate < test.Itemset) 
                    {
                        if (!result.ContainsKey(candidate))
                        {
                            result.Add(candidate, true);
                            candidate.ItemsSupport++;
                        }

                    }
                }
                return;
            }
            // We're not in a leaf, go down the tree following path given by hash value of
            // i-th item in current transaction (test)
            else
            {
                for (i = k; i < test.Itemset.ItemsNumber; i++)
                {
                    hash = (test.Itemset.Items[i].GetHashCode()) % sp;
                    ProbeTree(i, start.GetChildAt(hash));
                }
                return;
            }
        }

        /// <summary>
        /// Efficient method based on hashTree used for candidate support counting. The method 
        /// verify the support of candidate itemset stored in hashtree against the transaction
        /// database list
        /// </summary>
        /// <param name="AllTrans">The transaction database list</param>
        public void Calcsupport(List<Transaction> AllTrans)
        {
            foreach (Transaction trans in AllTrans)
            {
                test = trans;
                result = new Dictionary<ItemSet, bool>();
                for (i = 0; i < test.Itemset.ItemsNumber; i++)
                {
                    ProbeTree(i, root);
                }
            }
        }

      
        public List<ItemSet> GetCands()
        {
            return allcands;
        }
    }
}
