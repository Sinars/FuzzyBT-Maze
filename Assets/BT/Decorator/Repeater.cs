using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT.Decorator {
    public class Repeater : Node {


        // child node
        private Node node;

        public Repeater(Node node) {
            this.node = node;
        }


        // if child returns success or running -> return running
        // return failure otherwise
        public override NodeStates Evaluate(int given = 0) {
            // for the life of me if I know how I should implement this
            switch (node.Evaluate(given)) {
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                default:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
            }    
        }


    }
}
