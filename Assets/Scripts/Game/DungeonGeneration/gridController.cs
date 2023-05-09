using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridController : MonoBehaviour
{

    public room Room;
    
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;

    public GameObject gridTile;

    public List<Vector2> availablePoints = new List<Vector2>();

    private void Awake()
    {
        Room = GetComponentInParent<room>();
        grid.columns = Room.width - 2;
        grid.rows = Room.height - 2;
        generateGrid();
    }
    
    public void generateGrid()
    {
        grid.verticalOffset += Room.transform.localPosition.y;
        grid.horizontalOffset += Room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for(int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitializeObjectSpawn();
    }
}
