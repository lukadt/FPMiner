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
        private int _item;        //Item value
        private FPNode _parent;   //Pointer to node parent
        private FPNode[] _childs; //Array child nodes
        private int _count;       // Counter frequency
        private FPNode _next;     //Pointer to the next node containing the same item value

        public FPNode[] Childs
        {
            get { return _childs; }
            set { _childs = value; }
        }

        public FPNode[] GetChilds()
        {
            return _childs;
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public FPNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public FPNode Next
        {
            get { return _next; }
            set { _next = value; }
        }

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

        public FPNode(int Item)
        {
            _item = Item;
            _count = default(int);
            _parent = default(FPNode);
            _childs = default(FPNode[]);
        }
    }
}
