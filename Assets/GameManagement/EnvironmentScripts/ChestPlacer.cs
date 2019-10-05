using MazeObjects;
using System.Collections.Generic;
using UnityEngine;

public class ChestPlacer : MonoBehaviour {

    // Use this for initialization
    private MazeBuilder mazeBuilder;
    private GameObject holder;
    public GameObject chest;
    public Vector3 chestOffset;
    public int maxChestCount;
    public int minChestCount;
    private int chestCount;
	void Start () {
        holder = new GameObject();
        holder.name = "Chests";
        mazeBuilder = GetComponent<MazeBuilder>();
        chestCount = Random.Range(minChestCount, maxChestCount);
        PlaceChests();
	}

    private List<Cell> GetPlaces() {
        List<Cell> cells = new List<Cell>(mazeBuilder.Maze.Cells);
        cells.RemoveAll(cell =>
            !((cell.East && cell.West && cell.North && !cell.South) ||
                (cell.South && cell.West && cell.North && !cell.East) ||
                (cell.East && cell.North && cell.South && !cell.West) ||
                (cell.West && cell.East && cell.South && !cell.North))
        );
        return cells;
    }
	
    private void PlaceChests() {
        List<Cell> cells = GetPlaces();
        int distance = (int)Mathf.Floor((cells.Count - 1)/ chestCount);
        for (int i = 1; i < chestCount + 1; i++) {
            PlaceChest(cells[distance * i]); 
        }
    }

    private void PlaceChest(Cell cell) {
        if (!cell.East) {
            ChestPosition(cell.West, new Vector3(chestOffset.x, chestOffset.y, 0));
            return;
        }
        if (!cell.North) {
            ChestPosition(cell.South, new Vector3(0, chestOffset.y, chestOffset.z));
            return;
        }
        if (!cell.South) {
            ChestPosition(cell.North, new Vector3(0, chestOffset.y, -chestOffset.z), -1);
            return;
        }
        if (!cell.West) {
            ChestPosition(cell.East, new Vector3(-chestOffset.x, chestOffset.y, 0));
            return;
        }


    }

    private void ChestPosition(GameObject wall, Vector3 offset, float negativeRotation = 1) {
        Vector3 position = new Vector3(wall.transform.position.x, 0, wall.transform.position.z);
        Vector3 rotation = negativeRotation * wall.transform.rotation.eulerAngles;
        Vector3 chestRotation = new Vector3(rotation.x, rotation.y, rotation.z);
        GameObject temp = Instantiate(chest, position + offset, Quaternion.Euler(chestRotation));
        temp.transform.parent = holder.transform;
    }
}
