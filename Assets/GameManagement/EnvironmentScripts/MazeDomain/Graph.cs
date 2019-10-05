using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeObjects {

    [System.Serializable]
    public class Graph {

        // Use this for initialization

        public static Graph Instance {
            get {
                if (Instance == null)
                    Instance = new Graph();
                return Instance;

            }
            private set { Instance = value; }

        }

        private static List<Cell> outSideWalls = new List<Cell>();
        public static List<Node> CreateGraph(Maze maze) {
            List<Node> nodes = new List<Node>();
            for (var i = 0; i < maze.Cells.Length; i++) {
                nodes.Add(CreateNode(maze, maze.Cells[i], i));
            }
            outSideWalls = OutSideWalls(maze);
            return nodes;
        }

        private static Node CreateNode(Maze maze, Cell cell, int position) {
            Node node = new Node();
            node.Neighbours = new List<Cell>();
            node.Content = cell;
            if (!cell.East) {
                node.Neighbours.Add(maze.Cells[position + 1]);
            }
            if (!cell.West) {
                node.Neighbours.Add(maze.Cells[position - 1]);
            }
            if (!cell.North) {
                node.Neighbours.Add(maze.Cells[position + maze.Y]);
            }
            if (!cell.South) {
                node.Neighbours.Add(maze.Cells[position - maze.Y]);
            }
            return node;
        }

        public static List<Cell> FindShortestPath(List<Node> graph, String name) {
            Stack<Node> nodeStack = new Stack<Node>();
            List<Cell> path = new List<Cell>();
            nodeStack.Push(graph[0]);
            while (nodeStack.Count > 0) {
                Node takenNode = nodeStack.Peek();
                if (takenNode.Content.North && takenNode.Content.North.gameObject.name.Equals(name))
                    return path;
                while (takenNode.Neighbours.Count == 0 && nodeStack.Count > 0) {
                    path.Remove(takenNode.Content);
                    takenNode.Visited = true;
                    takenNode = nodeStack.Pop();
                }

                //get new neighbour
                Cell neighbour = takenNode.Neighbours[takenNode.Neighbours.Count - 1];
                takenNode.Neighbours.Remove(neighbour);
                Node temp = graph.Find(node => node.Content == neighbour);
                temp.Neighbours.Remove(takenNode.Content);
                path.Add(temp.Content);
                //
                nodeStack.Push(temp);
            }
            return path;
        }

        private static List<Cell> OutSideWalls(Maze maze) {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < maze.X; i++) {
                cells.Add(maze.Cells[i]); // bottom row
                cells.Add(maze.Cells[maze.X * maze.Y - 1 - i]); // top row
                cells.Add(maze.Cells[maze.X * i]); // left 
                cells.Add(maze.Cells[maze.X * (i+1) - 1]); // right
            }

            return cells;
            

        }

        public static bool IsOutSideWall(Cell cell) {
            return outSideWalls.Contains(cell);
        }
    }

}