using System;
using System.Collections.Generic;
using System.Text;

namespace FrequentPatternMining.FPGrowth
{
    /// <summary>
    /// Class that rappresent a node of an FPTree
    /// </summary>
    public class FPNode
    {
        private int _item;        
        private FPNode _parent;   
        private FPNode[] _childs; 
        private int _count;       
        private FPNode _next;     

        /// <summary>
        /// Get or set the list of childs of the node
        /// </summary>
        public FPNode[] Childs
        {
            get { return _childs; }
            set { _childs = value; }
        }
        /// <summary>
        /// Get the child list of the node
        /// </summary>
        /// <returns>Array of child nodes</returns>
        public FPNode[] GetChilds()
        {
            return _childs;
        }

        /// <summary>
        /// Get or set the counter of the frequency (shared path) of the node
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// Get or set the parent of the node
        /// </summary>
        public FPNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Get or set the next node in the list containing all nodes with this node value        
        /// </summary>
        public FPNode Next
        {
            get { return _next; }
            set { _next = value; }
        }

        /// <summary>
        /// Get or set the value of the node
        /// </summary>
        public int Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public FPNode()
        {
            _item = _count = default(int);
            _parent = default(FPNode);
            _childs = default(FPNode[]);
            _next = default(FPNode);
        }

        /// <summary>
        /// Create a new object with the passed value 
        /// </summary>
        /// <param name="Item">the passed value</param>
        public FPNode(int Item)
        {
            _item = Item;
            _count = default(int);
            _parent = default(FPNode);
            _childs = default(FPNode[]);
        }
    }
}
