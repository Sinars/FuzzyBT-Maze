using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NT {
    class TrainedLayer {
        private int numberOfInputs;

        private int numberOfOutputs;

        private float[] outputs;

        private float[] inputs;

        private float[,] weights;



        public float[,] Weights { get { return weights; } }

        public float[] Outputs { get { return outputs; } }

        public float[] Inputs { get { return inputs; } }

        public TrainedLayer(List<string[]> data, int start, int numberOfInputs, int numberOfOutputs) {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOutputs = numberOfOutputs;

            //initilize datastructures
            outputs = new float[numberOfOutputs];
            inputs = new float[numberOfInputs];
            weights = new float[numberOfOutputs, numberOfInputs];
            InitializeWeights(data, start);
        }

        private void InitializeWeights(List<string[]> data, int start) {
            for (int i = 0; i < numberOfOutputs; i++) {
                for (int j = 0; j < numberOfInputs; j++) {
                    weights[i, j] = float.Parse(data[i + start][j]);
                }
            }
        }

        public float[] FeedForward(float[] inputs) {
            this.inputs = inputs;// keep shallow copy which can be used for back propagation

            //feed forwards
            for (int i = 0; i < numberOfOutputs; i++) {
                outputs[i] = 0;
                for (int j = 0; j < numberOfInputs; j++) {
                    outputs[i] += inputs[j] * weights[i, j];
                }

                outputs[i] = (float)Math.Tanh(outputs[i]);
            }

            return outputs;
        }
    }
}
