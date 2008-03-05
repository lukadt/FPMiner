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

        public Hashtable Hash
        {
            get { return _hash; }
            set { _hash = value; }
        }

        public void Insert(Object item)
        {
            if (!Hash.ContainsKey(item))
            {
                Hash.Add(item, id++);
            }
        }

        public int GetIndex(Object item)
        {
            if (Hash.ContainsKey(item))
            {
                return (int)Hash[item];
            }
            else return -1;
        }

        public IEnumerator GetEnumerator()
        {
            return this.Hash.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Hash.GetEnumerator();
        }

    }
}
