using System.Collections.Generic;

namespace BT.Composites {

    public class Selector : Node {

        // child nodes of the selector
        protected List<Node> m_nodes = new List<Node>();

        // keep tabs on running node
        private int runningRank;


        // constructor with list of nodes passed to it
        public Selector(List<Node> nodes) {
            m_nodes = nodes;
        }

        /*
         * evaluate the nodes inside the selector
         * if a node evaluates to success -> return success
         * if a node evaluates faillure -> evaluates the next node from the list
         * if a node evaluates running -> return running
         * all nodes evaluate failure -> return failure
         *
         **/

        public override NodeStates Evaluate(int given = 0) {
            for (int i = runningRank; i < m_nodes.Count; i++) {
                    switch (m_nodes[i].Evaluate(given)) {
                        case NodeStates.FAILURE:
                            SaveRunningNode(0);
                            continue;
                        case NodeStates.SUCCESS:
                            SaveRunningNode(0);
                            m_nodeState = NodeStates.SUCCESS;
                            return m_nodeState;
                        case NodeStates.RUNNING:
                            SaveRunningNode(i);
                            m_nodeState = NodeStates.RUNNING;
                            return m_nodeState;
                        default:
                            continue;
                    }
                }
            m_nodeState = NodeStates.FAILURE;
            return m_nodeState;
        }

        private void SaveRunningNode(int nodeRank) {
            runningRank = nodeRank;
        }

    }
}
