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

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public void sort()
        {
            _itemset.Sort();
        }

        public void addItem(int item)
        {
            _itemset.Add(item);
        }

        public ItemSet Itemset
        {
            get { return _itemset; }
            set { _itemset = value; }
        }
    }
}
