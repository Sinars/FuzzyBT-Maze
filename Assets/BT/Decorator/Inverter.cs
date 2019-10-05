using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {

    public class Inverter : Node {

        // child node
        private Node m_node;

        public Node node {
            get { return m_node; }
        }


        public Inverter(Node node) {
            m_node = node;
        }

        /*
         * if child fails -> return succes
         * if child succeeds -> return failure
         * running -> running
         * 
         **/
        public override NodeStates Evaluate(int given = 0) {
            switch (m_node.Evaluate(given)) {
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
            }
            m_nodeState = NodeStates.SUCCESS;
            return m_nodeState;
       
        }
    }
}
