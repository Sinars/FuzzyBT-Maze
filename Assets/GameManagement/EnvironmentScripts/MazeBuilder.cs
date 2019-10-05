using MazeObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class MazeBuilder : MonoBehaviour {


    //variables 
    public GameObject endWall;
    public GameObject wall;
    public float wallLength = 1;
    public int xSize = 5;
    public int ySize = 5;
    private GameObject wallHolder;
    private int totalCells;
    private int currentCell = 0;
    private int VisitedCells = 0;
    public Maze Maze { get; private set; }
    bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;

    public GameObject player;

    



    void Awake () {

        Maze = new Maze {
            X = xSize,
            Y = ySize
        };
        CreateWalls();
        VisitedCells = 0;

    }


    void CreateWalls()
    {
        float wallPosY = 5.0f;
        wallHolder = new GameObject();
        wallHolder.name = "Maze";
        //initialPos = new Vector3(-xSize / 2 + wallLength / 2, 0f, -ySize / 2 + wallLength / 2);
        Vector3 initialPos = new Vector3(player.transform.position.x - 1, 0, player.transform.position.z - 1 + ySize - 1);
        Vector3 myPos = initialPos;
        GameObject tempWall;
        //for x Axis
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, wallPosY, initialPos.z + i * wallLength - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
                tempWall.name = "Wall X" + i + j;
            }
        }

        //for y Axis
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), wallPosY, initialPos.z + (i * wallLength) - wallLength);
                if (i == ySize && j == xSize - 1)
                {
                    tempWall = Instantiate(endWall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                    tempWall.name = "targetWall";
                }
                else
                {
                    tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                    tempWall.name = "Wall Y" + i + j;
                }
                tempWall.transform.parent = wallHolder.transform;
                
            }
        }
        CreateCells();
    }
	void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        int children = wallHolder.transform.childCount;

        GameObject[] allWalls = new GameObject[children];
        Cell[] cells = new Cell[xSize * ySize];
        int EastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;
        // Gets all the children 

        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        // Assignes walls to the Cells

        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++)
        {
            cells[cellprocess] = new Cell();
            cells[cellprocess].West = allWalls[EastWestProcess];
            cells[cellprocess].South = allWalls[childProcess + (xSize + 1) * ySize];          
            EastWestProcess++;
            childProcess++;
            cells[cellprocess].East = allWalls[EastWestProcess];
            cells[cellprocess].North = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
            termCount++;
            EastWestProcess = termCount % xSize == 0 ? EastWestProcess + 1 : EastWestProcess;
            
        }
        Maze.Cells = cells;
        
        CreateMaze();
    }

    void CreateMaze()
    {

        while (VisitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighbour();
                if (!Maze.Cells[currentNeighbour].Visited && Maze.Cells[currentCell].Visited)
                {
                    BreakWall();
                    Maze.Cells[currentNeighbour].Visited = true;
                    VisitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = UnityEngine.Random.Range(0, totalCells);
                Maze.Cells[currentCell].Visited = true;
                VisitedCells++;
                startedBuilding = true;
            }
        }
        Debug.Log("Finished");
    }

    void BreakWall()
    {
        switch(wallToBreak)
        {
            case 1:
                DestroyImmediate(Maze.Cells[currentCell].North);
                Maze.Cells[currentCell].North = null;
                break;
            case 2:
                DestroyImmediate(Maze.Cells[currentCell].West);
                Maze.Cells[currentCell].West = null;
                break;
            case 3:
                DestroyImmediate(Maze.Cells[currentCell].East);
                Maze.Cells[currentCell].East = null;
                break;
            case 4:
                Vector3 position = Maze.Cells[currentCell].South.transform.position;

                DestroyImmediate(Maze.Cells[currentCell].South);
                Maze.Cells[currentCell].South = null;
                break;
        }
    }

    void GiveMeNeighbour()
    {

        totalCells = xSize * ySize;
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;
        check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;

        //East

        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if(Maze.Cells[currentCell + 1].Visited == false)
            {
                neighbours[length] = currentCell + 1;
                connectingWall[length++] = 3;
            }
        }

        //West

        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (Maze.Cells[currentCell - 1].Visited == false)
            {
                neighbours[length] = currentCell - 1;
                connectingWall[length++] = 2;
            }
        }


        //North

        if (currentCell +xSize< totalCells)
        {
            if (Maze.Cells[currentCell + xSize].Visited == false)
            {
                neighbours[length] = currentCell + xSize;
                connectingWall[length++] = 1;
            }
        }

        //South

        if (currentCell - xSize >= 0)
        {
            if (Maze.Cells[currentCell - xSize].Visited == false)
            {
                neighbours[length] = currentCell - xSize;
                connectingWall[length++] = 4;
            }
        }
        if (length != 0)
        {
            int theOne = UnityEngine.Random.Range(0, length);
            currentNeighbour = neighbours[theOne];
            wallToBreak = connectingWall[theOne];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    public List<Cell> MonsterPath { get; private set; }


    // Update is called once per frame
    void Update () {
		
	}
}
