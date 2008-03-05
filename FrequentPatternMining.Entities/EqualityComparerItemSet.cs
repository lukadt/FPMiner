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
        #region IEqualityComparer<ItemSet> Members

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

        public int GetHashCode(ItemSet x)
        {
            return x.GetHashCode();
        }

        #endregion
    }
}
