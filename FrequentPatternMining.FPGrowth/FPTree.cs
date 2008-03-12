using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;

namespace FrequentPatternMining.FPGrowth
{
    /// <summary>
    /// Represent an FPTree a prefix tree that keep tracks of common transactions prefixes
    /// </summary>
    public class FPTree
    {
        private int _minSup;   
        private FPNode _root;  
        private bool _hasSinglePath; 
        private Dictionary<int, int> FrequencyItemCounter; //LookUp Table(Item,Frequency)
        private List<ItemSet> _singleFrequent;  
        private HeaderTable ht;  
        private int _depth;
        private FPNode _travel;

        /// <summary>
        /// Header Table is a nested class inside an FPTree, that contains an entry
        /// for each frequent item in the FPTree; it acts as an entry point
        /// for retrieve all node lists belonging to each node present in the header table
        /// </summary>
        private class HeaderTable
        {
            private int position;
            private HTrecord[] HTarray;

            /// <summary>
            /// Create and headr table with the specified size given by the number of frequent
            /// 1-itemset
            /// </summary>
            /// <param name="size">size of header table</param>
            public HeaderTable(int size)
            {
                position = 0;
                HTarray = new HTrecord[size];
            }

            /// <summary>
            /// Get the size of the header table
            /// </summary>
            /// <returns>size of header table</returns>
            public int GetSize()
            {
                return HTarray.Length;
            }

            /// <summary>
            /// Add to header table an entry with the specified value and frequency
            /// </summary>
            /// <param name="item">the value of the item to insert</param>
            /// <param name="freq">the frequency of the item to insert</param>
            public void addRecord(int item, int freq)
            {
                HTarray[position] = new HTrecord();
                HTarray[position].Freq = freq;
                HTarray[position].Item = item;
                HTarray[position].Head = default(FPNode);
                HTarray[position].Tail = default(FPNode);
                position++;
                if (position == HTarray.Length)
                    Array.Sort(HTarray, new ComparatorByItem());
            }

            /// <summary>
            /// Sort an header table using frequency values
            /// </summary>
            public void SortByFreq()
            {
                Array.Sort(HTarray, new ComparatorByFreq());
            }

            /// <summary>
            /// A comparator class based on items integer value
            /// </summary>
            public class ComparatorByItem : IComparer<HTrecord>
            {
                /// <summary>
                /// A custom comparison method between two htrecord based on
                /// their item values
                /// </summary>
                /// <param name="x">first htrecord</param>
                /// <param name="y">second htrecord</param>
                /// <returns>an integer representing the comparison result
                /// </returns>
                public int Compare(HTrecord x, HTrecord y)
                {
                    if (x.Item < y.Item)
                        return -1;
                    if (x.Item > y.Item)
                        return 1;
                    return 0;
                }
            }

            /// <summary>
            /// A comparator class based on items frequency
            /// </summary>
            public class ComparatorByFreq : IComparer<HTrecord>
            {

                /// <summary>
                /// A custom comparison method between two htrecord based on
                /// their frequencies
                /// </summary>
                /// <param name="x">first htrecord</param>
                /// <param name="y">second htrecord</param>
                /// <returns>an integer representing the comparison result
                /// </returns>
                public int Compare(HTrecord x, HTrecord y)
                {
                    if (x.Freq < y.Freq)
                        return -1;
                    if (x.Freq > y.Freq)
                        return 1;
                    return 0;
                }
            }

            /// <summary>
            /// A method that uses a binary search based on custom ComparatorByItem
            /// that return the index of the searched Node if founded, otherwise a negative number
            /// </summary>
            /// <param name="node">The FPNode looking index for</param>
            /// <returns>the index of the founded node</returns>
            public int GetIndex(FPNode node)
            {
                HTrecord test = new HTrecord();
                test.Item = node.Item;
                return Array.BinarySearch(HTarray, test, new ComparatorByItem());
            }

            /// <summary>
            /// Get the initial pointer to the specified item header table list given by index
            /// </summary>
            /// <param name="index">the index of the list of the header table</param>
            /// <returns>the initial node pointer </returns>
            public FPNode GetHead(int index)
            {
                return HTarray[index].Head;
            }

            /// <summary>
            /// Set the initial pointer of the specified header table list to first node
            /// </summary>
            /// <param name="first">the first node of the header table list given  by index</param>
            /// <param name="index">the index of the list of the header table</param>
            public void SetHead(FPNode first, int index)
            {
                HTarray[index].Head = first;
            }

            /// <summary>
            /// Get the item of the header table entry given by index
            /// </summary>
            /// <param name="index">the index used to access header table entries</param>
            /// <returns>the integer item value</returns>
            internal int GetItem(int index)
            {
                return HTarray[index].Item;
            }

            /// <summary>
            /// Get the ending pointer to the specified item header table list given by index
            /// </summary>
            /// <param name="index">the index of the list of the header table</param>
            /// <returns>the initial node pointer </returns>
            internal FPNode GetTail(int index)
            {
                return HTarray[index].Tail;
            }

            /// <summary>
            /// Set the ending pointer of the specified header table list to first node
            /// </summary>
            /// <param name="last">the last node of the header table list given  by index</param>
            /// <param name="index">the index of the list of the header table</param>
            internal void SetTail(FPNode last, int index)
            {
                HTarray[index].Tail = last;
            }

            /// <summary>
            /// Get the frequency of the header table entry specified by index
            /// </summary>
            /// <param name="index">index of the header table entry looking frequency for</param>
            /// <returns>the integer frequency</returns>
            internal int GetFreq(int index)
            {
                return HTarray[index].Freq;
            }
        }

        /// <summary>
        /// Represent a single record inside an Header Table; it's composed of an integer
        /// representing an item value, it's frequency value and two references for the head and
        /// the tail of the item FPNode list
        /// </summary>
        private class HTrecord : IComparable<HTrecord>
        {
            private int item;
            private int freq;
            private FPNode head;
            private FPNode tail;

            /// <summary>
            /// Get or set the value of header table record
            /// </summary>
            public int Item
            {
                get { return item; }
                set { item = value; }
            }

            /// <summary>
            /// Get or set the last node of header table entry list
            /// </summary>
            public FPNode Tail
            {
                get { return tail; }
                set { tail = value; }
            }

            /// <summary>
            /// Get or set the frequency of the header table entry
            /// </summary>
            public int Freq
            {
                get { return freq; }
                set { freq = value; }
            }

            /// <summary>
            /// Get or set the first node of header table entry list
            /// </summary>
            public FPNode Head
            {
                get { return head; }
                set { head = value; }
            }
            /// <summary>
            /// The comparison between two HTRecords is based on their item values
            /// </summary>
            /// <param name="other">the other object</param>
            /// <returns>an integer representing the comparison result</returns>
            public int CompareTo(HTrecord other)
            {
                if (this.item > other.item) return 1;
                if (this.item < other.item) return -1;
                return 0;
            }
        }

        /// <summary>
        /// Get or set the root of the tree
        /// </summary>
        public FPNode Root
        {
            get { return _root; }
            set { _root = value; }
        }

        /// <summary>
        /// Get or set the minimum support for the fptree
        /// </summary>
        public int MinSup
        {
            get { return _minSup; }
            set { _minSup = value; }
        }

        /// <summary>
        /// Get or set the frequent 1-itemset
        /// </summary>
        public List<ItemSet> SingleFrequent
        {
            get { return _singleFrequent; }
            set { _singleFrequent = value; }
        }

        /// <summary>
        /// Check if the tree has a single path. Test used for speed up
        /// association rule generation
        /// </summary>
        public bool HasSinglePath
        {
            get { return _hasSinglePath; }
            set { _hasSinglePath = value; }
        }

        /// <summary>
        /// Get or set the tree Depth
        /// </summary>
        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        /// <summary>
        /// Get or set a node used to traverse the fptree
        /// </summary>
        public FPNode Travel
        {
            get { return _travel; }
            set { _travel = value; }
        }

        public FPTree()
        {
            MinSup = default(int);

            Root = new FPNode();
            Root.Item = default(int);
            Root.Parent = default(FPNode);
            _singleFrequent = new List<ItemSet>();
            _hasSinglePath = true;
            _depth = 0;
        }

        /// <summary>
        /// Method invoked for building the first FPTree representation of the
        /// original database
        /// </summary>
        /// <param name="startdb">The list of database transactions</param>
        public void BuildFirstTree(List<ItemSet> startdb)
        {
            // Counting the frequency of single items            
            FrequencyItemCounter = new Dictionary<int, int>();
            FrequencyItemCounter = CountFrequencyItem(startdb);

            // Evaluate header table dimension
            int HTsize = 0;
            foreach (int item in FrequencyItemCounter.Keys)
            {
                if (FrequencyItemCounter[item] >= _minSup) HTsize++;
            }
            
            ht = new HeaderTable(HTsize);

            // Add every frequent single itemset to header table
            foreach (KeyValuePair<int, int> coppia in FrequencyItemCounter)
            {
                if (coppia.Value >= _minSup)
                    ht.addRecord(coppia.Key, coppia.Value);
            }            
            // Removal of non frequent items, sorting and final insertion in the FPTreee 
            ItemSet SortedList = new ItemSet();
            foreach (ItemSet itemset in startdb)
            {
                SortedList.Items.Clear();
                for (int i = 0; i < itemset.ItemsNumber; i++)
                {
                    if (FrequencyItemCounter[itemset.Items[i]] >= _minSup)
                    {
                        SortedList.Add(itemset.Items[i]);
                    }
                }
                if (SortedList.ItemsNumber > 0)
                {
                    SortedList.Items.Sort(new ItemSortingStrategy(FrequencyItemCounter));
                    if (_depth < SortedList.ItemsNumber) _depth = SortedList.ItemsNumber;
                    AddItemSetTree(SortedList, Root);
                }
            }
            startdb = null;
        }

        /// <summary>
        /// Test the empty condition on tree, used for optimization 
        /// purpose
        /// </summary>
        /// <returns>a boolean indicating if the fptree has childs or not</returns>
        public bool isEmpty()
        {
            if (_root.Childs == default(FPNode[]))
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Get the size of the fptree header table
        /// </summary>
        /// <returns>the integer size</returns>
        public int GetHTSize()
        {
            return ht.GetSize();
        }
        
        /// <summary>
        /// Method that return from the Header Table,the corresponding item value indicating by index
        /// input parameter
        /// </summary>
        /// <param name="index">index of the item looking for</param>
        /// <returns>the item retrieved</returns>
        public int GetHTItem(int index)
        {
            return ht.GetItem(index);
        }
        
        /// <summary>
        /// Sort header table entries using item frequency values
        /// </summary>
        public void SortHeaderTable()
        {
            ht.SortByFreq();
        }
      
        /// <summary>
        /// Set the minimum absolute support value
        /// </summary>
        /// <param name="minSup">minimum support</param>
        public void SetMinSup(int minSup)
        {
            _minSup = minSup;
        }

        /// <summary>
        /// Sorting HTRecord strategy, based on corresponding item frequency 
        /// The comparison uses the FrequencyItemCounter previously built
        /// </summary>
        public class ItemSortingStrategy : IComparer<int>
        {
            private Dictionary<int, int> _frequencyCounter;
            /// <summary>
            /// Create a comparer based on item frequency 
            /// </summary>
            /// <param name="FrequencyCounter">the dictionary used to retrieve elements frequency</param>
            public ItemSortingStrategy(Dictionary<int, int> FrequencyCounter)
            {
                _frequencyCounter = FrequencyCounter;
            }
            /// <summary>
            /// Compare two item
            /// </summary>
            /// <param name="x">first item</param>
            /// <param name="y">second item</param>
            /// <returns>an integer indicating comparison result</returns>
            public int Compare(int x, int y)
            {
                if (_frequencyCounter[x] < _frequencyCounter[y]) return +1;
                if (_frequencyCounter[x] > _frequencyCounter[y]) return -1;
                if (x < y)
                {
                    return 1;
                }
                if (x > y)
                {
                    return -1;
                }
                return 0;
            }

        }

        /// <summary>
        /// Add an itemset to an FPTree
        /// </summary>
        /// <param name="SortedList">itemset to be added (already sorted)</param>
        /// <param name="me">node where to start the insertion</param>
        private void AddItemSetTree(ItemSet SortedList, FPNode me)
        {
            FPNode found;

            //looping on each item and searching if it's present or not in the FPTree
            for (int i = 0; i < SortedList.ItemsNumber; i++)
            {
                //if the me node has got no childs let's create one new
                if (me.Childs == default(FPNode[]))
                {
                    me.Childs = new FPNode[0];
                }
                int j = 0;
                found = default(FPNode);
                // j represented a sliding index that point to the position where i want to 
                // add or insert the current item
                while ((j < me.Childs.Length) && (me.Childs[j].Item != SortedList.Items[i]))
                {
                    j++;
                }
                if (j < me.Childs.Length)
                {
                    found = me.Childs[j];
                }

                // If founded node is present in the tree, increase it's support and
                // move on to the next item updating me reference
                if (found != default(FPNode))
                {
                    found.Count += SortedList.ItemsSupport;
                    me = found;
                }
                // Create a new FPNode and set header table head and tail corresponding list
                else
                {
                    if (me.Childs.Length > 0) _hasSinglePath = false;
                    FPNode newnode = new FPNode(SortedList.Items[i]);

                    int index = ht.GetIndex(newnode);
                    if (ht.GetTail(index) == default(FPNode))
                    {
                        ht.SetHead(newnode, index);
                        ht.SetTail(newnode, index);
                    }
                    else
                    {
                        ht.GetTail(index).Next = newnode;
                        ht.SetTail(newnode, index);
                    }
                    newnode.Count = SortedList.ItemsSupport;
                    // Temporary Array where to store the new list of child nodes
                    FPNode[] tmpArray = new FPNode[me.Childs.Length + 1];
                    for (int k = 0; k < me.Childs.Length; k++)
                    {
                        tmpArray[k] = me.Childs[k];
                    }
                    tmpArray[me.Childs.Length] = newnode;
                    me.Childs = tmpArray;
                    newnode.Parent = me;
                    // Me reference set to the new node
                    me = newnode;
                }
            }
        }

        /// <summary>
        /// Method that count the frequency of single item
        /// </summary>
        /// <param name="ConditionalPatternBase">the projected database itemset list</param>
        /// <returns>Dictionary containing single items and their frequency</returns>
        private Dictionary<int, int> CountFrequencyItem(List<ItemSet> ConditionalPatternBase)
        {
            Dictionary<int, int> FrequencyItemCounter = new Dictionary<int, int>();
            foreach (ItemSet itemset in ConditionalPatternBase)
            {
                foreach (int item in itemset.Items)
                {
                    if (FrequencyItemCounter.ContainsKey(item))
                    {
                        FrequencyItemCounter[item] += itemset.ItemsSupport;
                    }
                    else
                    {
                        FrequencyItemCounter.Add(item, itemset.ItemsSupport);
                    }
                }
            }
            return FrequencyItemCounter;
        }
        
        /// <summary>
        /// Return frequency of the item in the header table specified by the index 
        /// </summary>
        /// <param name="index">index item looking for</param>
        /// <returns>frequency value</returns>
        internal int GetHTFreq(int index)
        {
            return ht.GetFreq(index);
        }

       /// <summary>
       /// Method that build a new FPTree starting from i-th header table entry
       /// </summary>
       /// <param name="i">index of header table entry (head of Conditional Pattern Base)</param>
       /// <returns>the created FPTree</returns>
        public FPTree CreateFPtree(int i)
        {
            FPNode ResultRootNode = new FPNode();
            FPTree Result = new FPTree();

            Result.Root = ResultRootNode;
            Result.MinSup = _minSup;
            
            // Counting the frequency of single items            
            Dictionary<int, int> FrequencyItemCounter2 = new Dictionary<int, int>();
            Result.FrequencyItemCounter = FrequencyItemCounter2;


            FPNode node;
            //Obtain a reference to head of the list of the i.th htrecord
            node = ht.GetHead(i);

            while (node != default(FPNode))
            {
                int support;
                _travel = node;
                support = _travel.Count;
                _travel = _travel.Parent;
                if (_travel.Parent != default(FPNode))
                {
                    while (_travel.Parent != default(FPNode))
                    {
                        if (FrequencyItemCounter2.ContainsKey(_travel.Item))
                        {
                            FrequencyItemCounter2[_travel.Item] += support;
                        }
                        else
                        {
                            FrequencyItemCounter2.Add(_travel.Item, support);
                        }
                        _travel = _travel.Parent;
                    }
                }
                node = node.Next;
            }


            //Evaluate header table dimension
            int HTsize = 0;
            foreach (int item in FrequencyItemCounter2.Keys)
            {
                if (FrequencyItemCounter2[item] >= _minSup) HTsize++;
            }
            
            HeaderTable newht = new HeaderTable(HTsize);
            Result.ht = newht;

            //Insertion of frequent items in header table
            foreach (KeyValuePair<int, int> coppia in FrequencyItemCounter2)
            {
                if (coppia.Value >= _minSup)
                    newht.addRecord(coppia.Key, coppia.Value);
            }
            
            // Removal of non frequent items, sorting and final insertion in the FPTreee 
            node = ht.GetHead(i);
            while (node != default(FPNode))
            {
                _travel = node;
                ItemSet path = new ItemSet();
                path.ItemsSupport = _travel.Count;
                _travel = _travel.Parent;
                if (_travel.Parent != default(FPNode))
                {
                    while (_travel.Parent != default(FPNode))
                    {
                        path.Add(_travel.Item);
                        _travel = _travel.Parent;
                    }
                    ItemSet SortedList = new ItemSet();
                    SortedList.ItemsSupport = path.ItemsSupport;
                    foreach (int item in path.Items)
                    {
                        if (FrequencyItemCounter2[item] >= _minSup)
                        {
                            SortedList.Add(item);
                        }
                    }
                    if (SortedList.Items.Count > 0)
                    {
                        SortedList.Items.Sort(new ItemSortingStrategy(FrequencyItemCounter2));
                        if (Result._depth < SortedList.ItemsNumber) Result._depth = SortedList.ItemsNumber;
                        Result.AddItemSetTree(SortedList, ResultRootNode);
                    }
                }
                node = node.Next;
            }
            return Result;
        }
    }
}
