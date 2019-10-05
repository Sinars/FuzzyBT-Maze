using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {
    public abstract class ActionNode : Node{


        // method that defines the action done by the node
        public abstract NodeStates Action();

        // evaluate the node using the passed action
        public override NodeStates Evaluate(int given = 0) {
            switch(Action()) {
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                default:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
            }
        }

    }
}
