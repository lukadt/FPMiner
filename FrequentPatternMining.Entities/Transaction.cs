using System;
using System.Collections.Generic;
using System.Text;

namespace FrequentPatternMining.Entities
{
    /// <summary>
    /// A Transaction is the basic building block of a transactional data source. 
    /// A transaction simply wrap an ItemSet adding it its own identifier.
    /// </summary>
    public class Transaction 
    {
        private int _id;
        private ItemSet _itemset;

        public Transaction()
        {
            _itemset = new ItemSet();
        }

        /// <summary>
        /// Get or Set the id of the current transaction
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Sort an itemset 
        /// </summary>
        public void sort()
        {
            _itemset.Sort();
        }

        /// <summary>
        /// Add an item to transaction itemset
        /// </summary>
        /// <param name="item">the value of the item to be inserted</param>
        public void addItem(int item)
        {
            _itemset.Add(item);
        }

        /// <summary>
        /// Get or set the Itemset for this transaction
        /// </summary>
        public ItemSet Itemset
        {
            get { return _itemset; }
            set { _itemset = value; }
        }
    }
}
