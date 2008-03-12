using System;
using System.Collections.Generic;
using System.Text;

namespace FrequentPatternMining.Entities
{
    /// <summary>
    /// Represent a comparer based on itemset equality;
    /// frequently used combined with dictionary to compare keys equality
    /// </summary>
    public class EqualityComparerItemSet : IEqualityComparer<ItemSet>
    {
        /// <summary>
        /// Define a new concept of equality between itemset
        /// </summary>
        /// <param name="x">first itemset</param>
        /// <param name="y">second itemset</param>
        /// <returns>a boolean indicating if these itemset are equals or not</returns>
        public bool Equals(ItemSet x, ItemSet y)
        {
            if (x.ItemsNumber == y.ItemsNumber)
            {
                foreach (int item in x.Items)
                {
                    if (!y.Items.Contains(item)) return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return the hash code of the specified itemset
        /// </summary>
        /// <param name="x">the specified itemset</param>
        /// <returns>the integer hash code value</returns>
        public int GetHashCode(ItemSet x)
        {
            return x.GetHashCode();
        }
        
    }
}
