using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {
    public abstract class ConditionNode : Node {

        // node to evaluate if condition returns true
        public Node Node { get; private set; }

        public ConditionNode(Node node) {
            Node = node;
        }

        public abstract bool CheckCondition();


        // if condition return true, evaluate the node
        public override NodeStates Evaluate(int given = 0) {
            if (CheckCondition())
                return Node.Evaluate();
            return NodeStates.FAILURE;
        }
    }
}
