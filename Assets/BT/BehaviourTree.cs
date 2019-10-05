using NT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT {

    public class BehaviourTree {

        private Node root;
        private TrainedNN network; 
        public BehaviourTree(Node root, TrainedNN network) {
            
            this.root = root;
            this.network = network;
        }

        public void Run(float[] args, Func<float[], int> function) {
            int networkClassification = function(network.FeedForward(args));
            root.Evaluate(networkClassification);
        }

    }
}
