using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGenerator : MonoBehaviour
{
    public dungeonGenerationData DungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = dungeonCrawlerController.GenerateDungeon(DungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
        
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        // int i = 0;
        
        roomController.instance.LoadRoom("start", 0, 0);
        foreach(Vector2Int roomLocation in rooms)
        {
            roomController.instance.LoadRoom(roomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);

            /* int x = Random.Range(0, 2);
            Debug.Log(x);
            switch (x)
            {
                case 0:
                    if (i == 0)
                    {                        
                        roomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);    
                    }
                    else
                    {                  
                         roomController.instance.LoadRoom("Corridor", roomLocation.x, roomLocation.y);
                         i = 0;                        
                    }
                    break;

                case 1:
                        roomController.instance.LoadRoom("Corridor", roomLocation.x, roomLocation.y);
                        i = 1;
                    break;
                  
            }
            */
        }
    }
}
