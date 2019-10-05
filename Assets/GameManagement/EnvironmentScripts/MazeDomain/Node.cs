using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MazeObjects {

    [System.Serializable]
    public class Node {

        public bool Visited { get; set;}

        public Cell Content { get; set; }

        public List<Cell> Neighbours { get; set; }
    }

}