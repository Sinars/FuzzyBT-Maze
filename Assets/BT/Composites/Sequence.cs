using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT.Composites {
    public class Sequence : Node{

        // nodes of the sequence

        private List<Node> m_nodes = new List<Node>();
        

        public Sequence(List<Node> nodes) {
            m_nodes = nodes;
            runningRank = 0;
        }


        private int runningRank;
        


        /*
         * evaluate nodes from sequence
         * node fails -> return failure
         * node success -> next node
         * node running -> return running
         * all nodes success -> return success
         * 
         * **/


        public override NodeStates Evaluate(int given = 0) {
            for (int i = runningRank; i < m_nodes.Count; i++) {
                switch (m_nodes[i].Evaluate(given)) {
                    case NodeStates.FAILURE:
                        m_nodeState = NodeStates.FAILURE;
                        SaveRunningNode(0);
                        return m_nodeState;
                    case NodeStates.SUCCESS:
                        SaveRunningNode(0);
                        continue;
                    case NodeStates.RUNNING:
                        SaveRunningNode(i);
                        m_nodeState = NodeStates.RUNNING;
                        return m_nodeState;
                    default:
                        m_nodeState = NodeStates.SUCCESS;
                        return m_nodeState;
                }
            }
            m_nodeState = NodeStates.SUCCESS;
            return m_nodeState;
        }

        private void SaveRunningNode(int nodeRank) {
            runningRank = nodeRank;
        }

    }
}
