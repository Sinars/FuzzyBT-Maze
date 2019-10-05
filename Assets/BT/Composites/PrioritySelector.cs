using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT.Composites {
    public class PrioritySelector : Node {

        private List<Node> m_nodes;

        public PrioritySelector(List<Node> nodes) {
            m_nodes = nodes;
        }



        /**
         * Evaluate child nodes
         * if node returns success-> return success
         * if node returns failure -> evaluate next node
         * if node returns running -> return running
         * all nodes return failure -> return failure
         * */
        public override NodeStates Evaluate(int given = 0) {
                foreach (Node node in m_nodes) {
                    switch (node.Evaluate(given)) {
                        case NodeStates.FAILURE:
                            continue;
                        case NodeStates.SUCCESS:
                            m_nodeState = NodeStates.SUCCESS;
                            return m_nodeState;
                        case NodeStates.RUNNING:
                            m_nodeState = NodeStates.RUNNING;
                            return m_nodeState;
                        default:
                            continue;
                    }
                }
            m_nodeState = NodeStates.FAILURE;
            return m_nodeState;
        }


    }
}
