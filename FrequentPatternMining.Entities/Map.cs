using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FrequentPatternMining.Entities
{
    /// <summary>
    /// Represent a Map which encapsulate an HashTable; it creates and expose
    /// the map conversion between the integer item representation and the one used in
    /// data source (usually int or string). It lacks of strong typing due to the unknown a
    /// priori data source representation
    /// </summary>
    public class Map : IEnumerable
    {
        private Hashtable _hash;
        private int id;

        public Map()
        {
            id = 0;
            _hash = new Hashtable();
        }

        /// <summary>
        /// Get or set the internal hashtable used for mapping
        /// </summary>
        public Hashtable Hash
        {
            get { return _hash; }
            set { _hash = value; }
        }


        /// <summary>
        /// Insert an item in the map if not already present
        /// </summary>
        /// <param name="item">the item to insert</param>
        public void Insert(Object item)
        {
            if (!Hash.ContainsKey(item))
            {
                Hash.Add(item, id++);
            }
        }

        /// <summary>
        /// A method for obtain the index of an item stored in the hashtable
        /// </summary>
        /// <param name="item">the item i want to search index for</param>
        /// <returns>the index of the item searched or -1 if not present</returns>
        public int GetIndex(Object item)
        {
            if (Hash.ContainsKey(item))
            {
                return (int)Hash[item];
            }
            else return -1;
        }

        /// <summary>
        /// Enumerate on map using hashtable enumerator
        /// </summary>
        /// <returns>the enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            return this.Hash.GetEnumerator();
        }

        /// <summary>
        /// Enumerate using foreach constructor
        /// </summary>
        /// <returns>the enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Hash.GetEnumerator();
        }

    }
}
