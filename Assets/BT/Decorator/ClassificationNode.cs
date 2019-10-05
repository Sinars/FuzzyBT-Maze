using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {
    abstract class ClassificationNode: Node {
        // node to evaluate if condition returns true
        public Node Node { get; private set; }

        public ClassificationNode(Node node) {
            Node = node;
        }
        // checks the classification given by the network
        public abstract bool CheckCondition(int given = 0);


        // if condition return true, evaluate the node
        public override NodeStates Evaluate(int given = 0) {
            if (CheckCondition(given))
                return Node.Evaluate();
            return NodeStates.FAILURE;
        }
    }
}
