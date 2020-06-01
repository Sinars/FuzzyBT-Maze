using MazeObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlacer : MonoBehaviour {

    private GameObject monsterHolder;
    private GameObject zoneHolder;


    //public GameObject[] monsters;
    public GameObject navSurface;
    public Vector3 monsterWonderAreaOffset;
    public int maxMonsterCount;
    public int minMonsterCount;
    // Use this for initialization

    public void FinalizedWalls(Maze maze)
    {
        Debug.Log("Setting monsters");
        int monsterCount = UnityEngine.Random.Range(minMonsterCount, maxMonsterCount);
        PlaceMonsters(maze, monsterCount);

    }

    // Update is called once per frame
    void Update () {
	    
	}

    private void PlaceMonsters(Maze maze, int monsterCount) {
        zoneHolder = new GameObject();
        zoneHolder.name = "MovementZones";
        monsterHolder = new GameObject();
        monsterHolder.name = "Monsters";
        int startingPlace = maze.X / 2;
        // for tests 
        List<Node> nodes = Graph.CreateGraph(maze);
        List<Cell> path = Graph.FindShortestPath(nodes, "targetWall");
        //List<Cell> path = new List<Cell>(maze.Cells);
        int distance = ((path.Count - startingPlace )/ monsterCount);
        int margin = (int)Math.Floor((double)distance / (distance / 2));
        for (int i = 0; i < monsterCount; i++) {
            int minValue = distance * i + startingPlace - margin;
            int maxValue = distance * i + startingPlace + margin;
            int pos = UnityEngine.Random.Range(minValue, maxValue);
            if (pos > path.Count)
                pos = path.Count - 1;
            InsertMonster(maze, path[pos]);

        }
    }

    private void InsertMonster(Maze maze, Cell cell) {
        if (cell.East) {
            Vector3 offset = new Vector3(monsterWonderAreaOffset.x, 0, 0);
            HandleCreation(cell.East, offset);
            return;
        }
        if (cell.West) {
            Vector3 offset = new Vector3(-monsterWonderAreaOffset.x, 0, 0);
            HandleCreation(cell.West, offset);
            return;
        }
        if (cell.South) {
            Vector3 offset = new Vector3(0, 0, -monsterWonderAreaOffset.z);
            HandleCreation(cell.South, offset);
            return;
        }
        if (cell.North) {
            Vector3 offset = new Vector3(0, 0, monsterWonderAreaOffset.z);
            HandleCreation(cell.North, offset);
            return;
        }
    }

    private void HandleCreation(GameObject wall, Vector3 offset) {
        Vector3 position = wall.transform.position;
        //GameObject tempMonster = Instantiate(monsters[0], new Vector3(position.x - offset.x, position.y, position.z - offset.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //tempMonster.transform.parent = monsterHolder.transform;
        GameObject tempSurface = Instantiate(navSurface, new Vector3(position.x - offset.x, -1.1f, position.z - offset.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //SpiderBehaviour spiderBehaviour = tempMonster.GetComponent<SpiderBehaviour>();
        //spiderBehaviour.area = tempSurface;
        tempSurface.transform.parent = zoneHolder.transform;
    }
}
