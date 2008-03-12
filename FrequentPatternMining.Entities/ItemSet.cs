using System;
using System.Collections.Generic;
using System.Text;

namespace FrequentPatternMining.Entities
{
    /// <summary>
    /// Representation of an ItemSet.
    /// An ItemSet is a set of Items. Each Item is an attribute value (coca,milk,butter..).
    /// Due to performance reason we represent an Item as signed int; this kind of representation 
    /// is given by a conversion Map class which convert data source item representation to int and then
    /// back when it comes to User Interface.
    /// This class define semantic Set representation defining insiemistic method as inclusion, redefining 
    /// the concept of equality between ItemSets through Equals and GetHashCode methods.
    /// </summary>
    
   public class ItemSet: IEquatable<ItemSet>
    {
        /// <summary>
       /// Items are represented through a list due to the unpredictable size of ItemSet
       /// </summary>
        private List<int> _items;       
        
        /// <summary>
       /// Support is used to measure the popularity of an itemset. Support of an itemset
       /// {A, B} is made up of the total number of transactions that contain both A and B.
       /// Support ({A, B}) = NumberofTransactions(A, B)
       /// </summary>
        private int _itemsSupport;


       /// <summary>
       /// Get or set the suppoty for the current itemset
       /// </summary>
        public int ItemsSupport
        {
            get { return _itemsSupport; }
            set { _itemsSupport = value; }
        }

       /// <summary>
       /// Get or sets the items this itemset is composed of
       /// </summary>
        public List<int> Items
        {
            get { return _items; }
            set { _items = value; }
        }

       /// <summary>
       /// Get the number of items of this itemset
       /// </summary>
        public int ItemsNumber
        {
            get { return _items.Count; }
        }

        public ItemSet()
        {
            _items = new List<int>();
            ItemsSupport = 1;
        }

       /// <summary>
       /// Add an item to the itemset if not contained
       /// </summary>
       /// <param name="item">the value of the item to insert</param>
        public void Add(int item)
        {
            if (!_items.Contains(item)) _items.Add(item);
        }

        /// <summary>
       /// Sort based on item representation int value
       /// </summary>
        public void Sort()
        {
            _items.Sort();
        }        

        /// <summary>
        /// Override GetHashCode() method;
        /// Our custom hashing logic is based only on items value leaving out memory address;
        /// We want two ItemSets have same hash value if they have same items.
        /// We use the XOR operator, computing XOR beetween every hash code item value
        /// </summary>
        /// <returns>the int hash code computed</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            int i;

            for (i = 0; i < ItemsNumber; i++)
            {
                int g = _items[i].ToString().GetHashCode();
                hash = hash ^ g;
            }
            return hash;
        }

        #region IEquatable<ItemSet> Members

        /// <summary>
        /// Overriding Equals() method. Two ItemSets are equals if they share the same items
        /// Time complexity: O(n)
        /// </summary>
        /// <param name="other">The other Itemset to compare </param>
        /// <returns>a boolean indicating if the two objects are equals or not</returns>
        public bool Equals(ItemSet other)
        {
            int i = 0;

            if (this.ItemsNumber == other.ItemsNumber)
            {
                foreach (int item in this.Items)
                {
                    if (other.Items.Contains(item)) i++;
                }
                if (i == this.ItemsNumber) return true;
            }
            return false;
        }

        #endregion
        
        #region Set Operations

        /// <summary>
        /// Inclusion operator (a is contained in b?); a brute force standard implementation
        /// would require a time proportional to ItemSets size O(mn).
        /// We use an approach similar to inverted index fusion algorithm with a complexity O(m+n);
        /// ItemSet are previously sorted
        /// </summary>
        /// <param name="a">Sorted ItemSet a</param>
        /// <param name="b">Sorted ItemSet b</param>
        /// <returns>a boolean indicating where a is contained in b</returns>
        public static bool operator <(ItemSet a, ItemSet b)
        {
            int i, j;

            i = 0;
            j = 0;
            if (a.ItemsNumber > b.ItemsNumber) return false;
            while (j < a.ItemsNumber && i < b.ItemsNumber)
            {
                if (a.Items[j] == b.Items[i]) j++;
                i++;
            }
            return j == a.ItemsNumber;
        }

        /// <summary>
        /// a contain b? method not implemented
        /// </summary>
        /// <param name="a">Sorted ItemSet a</param>
        /// <param name="b">Sorted ItemSet b</param>
        /// <returns>a boolean indicating where a contain b</returns>
        public static bool operator >(ItemSet a, ItemSet b)
        {
            throw new Exception("Not implemented");
        }
        
        /// <summary>
        /// Intersection Operator
        /// </summary>
        /// <param name="a">ItemSet a</param>
        /// <param name="b">ItemSet b</param>
        /// <returns>Cardinality intersection Set between a and b</returns>
        public static int operator &(ItemSet a, ItemSet b)
        {
            int card = 0;
            foreach (int item in a.Items)
            {
                if (b.Items.Contains(item)) card++;
            }
            return card;
        }
        
        /// <summary>
        /// Union Operator
        /// </summary>
        /// <param name="a">ItemSet a</param>
        /// <param name="b">ItemSet b</param>
        /// <returns>Union set between a and b</returns>
        public static ItemSet operator |(ItemSet a, ItemSet b)
        {
            ItemSet union = new ItemSet();
            foreach (int item in a.Items) union.Add(item);
            foreach (int item in b.Items) union.Add(item);
            return union;
        }

        /// <summary>
        /// Difference operator
        /// </summary>
        /// <param name="a">ItemSet a</param>
        /// <param name="b">ItemSet b</param>
        /// <returns>DIfference set between a and b</returns>
        public static ItemSet operator -(ItemSet a, ItemSet b)
        {
            ItemSet diff = new ItemSet();
            foreach (int item in a.Items)
            {
                if (!b.Items.Contains(item))
                {
                    diff.Items.Add(item);
                }
            }
            return diff;
        }

        /// <summary>
        /// Equality Operator
        /// </summary>
        /// <param name="a">ItemSet a</param>
        /// <param name="b">ItemSet b</param>
        /// <returns>a boolean indicating where the two itemset are equals or not</returns>
        public static bool operator ==(ItemSet a, ItemSet b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Inequality Operator
        /// </summary>
        /// <param name="a">ItemSet a</param>
        /// <param name="b">ItemSet b</param>
        /// <returns>a boolean indicating where the two itemset are equals or not</returns>
        public static bool operator !=(ItemSet a, ItemSet b)
        {
            return !a.Equals(b);
        }
        
        #endregion

        /// <summary>
        /// Override ToString() method; 
        /// </summary>
        /// <returns>A string representing items through comma separeted values</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.ItemsNumber - 1; i++)
            {
                builder.Append(Items[i]);
                builder.Append(',');
            }
            builder.Append(Items[ItemsNumber - 1]);
            return builder.ToString();
        }
    }
}
