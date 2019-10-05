using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {
    
    public abstract class Node {

        // return the state of the node

        public delegate NodeStates NodeReturn();


        // current state of the node

        protected NodeStates m_nodeState;

        public NodeStates nodeState { get { return m_nodeState; } }

        // constructor
        public Node() { }

        // abstract method to evaluate the node

        public abstract NodeStates Evaluate(int given = 0);

    }
}
