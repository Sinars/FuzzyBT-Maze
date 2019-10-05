using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT.Condition {
    class OutcomeEquals : ClassificationNode{
        
        private int needed;
        public OutcomeEquals(Node node, int needed) : base(node) {
            this.needed = needed;
        }
        

        public override bool CheckCondition(int given = 0) {
            return given == needed;
        }
    }
}
