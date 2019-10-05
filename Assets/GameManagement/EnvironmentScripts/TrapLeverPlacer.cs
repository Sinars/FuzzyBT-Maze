using MazeObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapLeverPlacer: MonoBehaviour {

    private MazeBuilder mazeBuilder;

    public GameObject lever;

    private List<Cell> trapsPath;

    public GameObject triggerObject;

    private Cell[] cells;

    // Use this for initialization
    void Start () {
        mazeBuilder = GetComponent<MazeBuilder>();
        InitData();
       
	}
    void InitData() {
        Maze maze = mazeBuilder.Maze;
        cells = maze.Cells;
        int trapCount = (int)Mathf.Floor(maze.X * maze.Y / 10);
        List<Node> nodes = Graph.CreateGraph(maze);
        trapsPath = Graph.FindShortestPath(nodes, "targetWall");
        SetUpTraps(trapCount);
    }

    private void SetupLevers(int trapCount) {
        if (trapCount == 0)
            return;
        List<Cell> space = new List<Cell>(cells);
        List<Cell> freeSpace = space.Except(trapsPath).ToList();
        int freeSpaceCount = freeSpace.Count;
        int distance = freeSpaceCount / trapCount;
        for (int i = 0; i < trapCount; i++) {

            int pos = distance * i;
            if (pos > freeSpaceCount)
                pos = freeSpaceCount - 1;
            PlaceLever(freeSpace[pos]);
        }
    }

    private void ClearPath() {
        trapsPath.RemoveAll(cell => (!cell.North && !cell.West && cell.South && cell.East)
        || (!cell.North && !cell.East && cell.South && cell.West)
        || (!cell.South && !cell.West && cell.North && cell.East)
        || (!cell.South && !cell.East && cell.North && cell.West)
        || (!cell.North && !cell.South && !cell.West)
        || (!cell.East && !cell.South && !cell.West)
        || (!cell.East && !cell.South && !cell.North)
        || (!cell.North && !cell.East && !cell.West));
    }


    private void SetUpTraps(int trapCount) {
        Cell optimumCell = FindOptimumPlace();
        ClearPath();
        int distance = trapsPath.Count <= trapCount ? 1 : (int)Math.Ceiling(((trapsPath.Count) / (double)trapCount));
        int i = 0;
        Debug.Log("distance: " + distance);
        Debug.Log("Path length: " + trapsPath.Count);
        bool spaceFound = false;
        int trapsPlaced = 0;
        float xTriggerOffset = 4;
        float trapOffsetX = 3;
        trapsPath.ForEach(cell => {
            if (cell == optimumCell)
                spaceFound = true;
            if (spaceFound)
                if (i % distance == 0) {
                    if (cell.East && cell.West && (!cell.South || !cell.North)) {
                        trapsPlaced++;
                        GameObject temp = Instantiate(triggerObject, new Vector3(cell.East.transform.position.x - xTriggerOffset, 0.062f, cell.East.transform.position.z - trapOffsetX), Quaternion.identity);
                        temp.transform.parent = cell.West.transform;
                        temp.gameObject.name = "horizontalTrap";
                    }
                    else
                        if ((!cell.East || !cell.West) && cell.South && cell.North) {
                        trapsPlaced++;
                        GameObject temp = Instantiate(triggerObject, new Vector3(cell.South.transform.position.x - trapOffsetX, 0.062f, cell.South.transform.position.z + xTriggerOffset), Quaternion.identity);
                        temp.transform.rotation = Quaternion.Euler(temp.transform.rotation.x, 270, 0);
                        temp.gameObject.name = "verticalTrap";
                        temp.transform.parent = cell.South.transform;
                    }
                }
            i = spaceFound == true ? i + 1 : i;

        }
        );
        SetupLevers(trapsPlaced);
    }
    private void PlaceLever(Cell cell)
    {
        Vector3 leverPosition = new Vector3(0.55f, 3.3f, 0.55f);
        if (cell.East)
        {
            GameObject temp = Instantiate(lever, new Vector3(cell.East.transform.position.x - leverPosition.x, cell.East.transform.position.y - leverPosition.y, cell.East.transform.position.z), Quaternion.Euler(0, 270, 0));
            temp.gameObject.name = "LeverEast";
            temp.transform.parent = cell.East.transform;
            return;
        }
        if (cell.West)
        {
            GameObject temp = Instantiate(lever, new Vector3(cell.West.transform.position.x + leverPosition.x, cell.West.transform.position.y - leverPosition.y, cell.West.transform.position.z), Quaternion.Euler(0, 90, 0));
            temp.gameObject.name = "LeverWest";
            temp.transform.parent = cell.West.transform;
            return;
        }
        if (cell.South)
        {
            GameObject temp = Instantiate(lever, new Vector3(cell.South.transform.position.x, cell.South.transform.position.y - leverPosition.y, cell.South.transform.position.z + leverPosition.z), Quaternion.identity);
            temp.gameObject.name = "LeverSouth";
            temp.transform.parent = cell.South.transform;
            return;
        }
        if (cell.North)
        {
            GameObject temp = Instantiate(lever, new Vector3(cell.North.transform.position.x, cell.North.transform.position.y - leverPosition.y, cell.North.transform.position.z + +leverPosition.z), Quaternion.identity);
            temp.gameObject.name = "LeverNorth";
            temp.transform.parent = cell.North.transform;
            return;
        }
    }

    private Cell FindOptimumPlace() {
        Cell firstCell = trapsPath[0];
        Cell startingCell = trapsPath.Find(cell => (!cell.North && !cell.South && cell.West && cell.East)
         || (!cell.West && !cell.East && cell.North && cell.South));
        if (startingCell != firstCell)
            return startingCell;
        if (startingCell.North && startingCell.South) {
            return trapsPath.Find(cell => (!cell.South && !cell.North && cell.East && cell.West));
        }
        else
            return trapsPath.Find(cell => (cell.South && cell.North && !cell.East && !cell.West));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
