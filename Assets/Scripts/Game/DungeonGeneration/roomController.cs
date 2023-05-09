using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}


public class roomController : MonoBehaviour
{

    public static roomController instance;

    string currentWorldName = "Wasteland";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<room> loadedRooms = new List<room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    room currentRoom;

    void Awake()
    {
        instance = this;  
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(spawnBossRoom());
            }   else if (spawnedBossRoom && !updatedRooms)
            {
                foreach(room Room in loadedRooms)
                {
                    Room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator spawnBossRoom()
    {
        spawnedBossRoom = true;

        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            room bossRoom = loadedRooms[loadedRooms.Count - 1];
            room tempRoom = new room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Big", tempRoom.X, tempRoom.Y);
        }
    }
    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(room Room)
    {
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {

            Room.transform.position = new Vector3(
            currentLoadRoomData.X * Room.width,
            currentLoadRoomData.Y * Room.height,
            0);

            Room.X = currentLoadRoomData.X;
            Room.Y = currentLoadRoomData.Y;
            Room.name = currentWorldName + "-"
                + currentLoadRoomData.name + "-"
                + Room.X + "-" + Room.Y;
            Room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {

            }

            loadedRooms.Add(Room);
            checkAndRemoveDoors();
        }
        else
        {
            Destroy(Room.gameObject);
            isLoadingRoom = false;
        }
        
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Empty",
            "Empty",
            "Empty",
            "Corridor",
            "Corridor",
            "Corridor",
            "Corridor",
            "Corridor",
            "Item",
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
    public void checkAndRemoveDoors()
    {
        foreach(room Room in loadedRooms)
        {
            Room.FindUnconnectedDoors();
            Room.RemoveUnconnectedDoors();
        }
    }
    public void OnPlayerEnterRoom(room Room)
    {
        currentRoom = Room;
    }

}
