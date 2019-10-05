using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeObjects {
    [System.Serializable]
    public class Maze {

        public Cell[] Cells { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        public Maze(int x, int y) {
            this.X = x;
            this.Y = y;
            this.Cells = new Cell[x*y];
        } 
        public Maze() { }

    }
}
