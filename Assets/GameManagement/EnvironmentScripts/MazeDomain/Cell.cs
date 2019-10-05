using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeObjects {

    [System.Serializable]
    public class Cell {

        public bool Visited { get; set; }
        public GameObject North { get; set; } //1
        public GameObject West { get; set; } //2
        public GameObject East { get; set; } //3
        public GameObject South { get; set; } //4
    }
}