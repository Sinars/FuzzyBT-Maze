using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NT {
    public class TrainedNN {
        private int[] layer;
        private float learningRate;
        private TrainedLayer[] layers;
        public TrainedNN(TextAsset textAsset) {
            List<string> content = textAsset.text.Split(new char[] { '\r', '\n' }).ToList();
            List<string[]> data = new List<string[]>();
            foreach (var line in content) {
                if (line.Equals(""))
                    continue;
                string[] holder = line.Split(' ');
                data.Add(holder);
            }
            this.layer = new int[] { 3, 15, 15, 4 };
            for (int i = 0; i < layer.Length; i++)
                this.layer[i] = layer[i];
            layers = new TrainedLayer[layer.Length - 1];
            int start = 0;
            for (int i = 0; i < layers.Length; i++) {
                layers[i] = new TrainedLayer(data, start, layer[i], layer[i + 1]);
                start += layer[i + 1];
            }

        }

        public float[] FeedForward(float[] inputs) {
            layers[0].FeedForward(inputs);

            for (int i = 1; i < layers.Length; i++) {
                layers[i].FeedForward(layers[i - 1].Outputs);
            }
            return layers[layers.Length - 1].Outputs;
        }
    }
}