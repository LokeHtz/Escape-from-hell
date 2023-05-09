using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct randomSpawner
    {
        public string name;
        public spawnerData SpawnerData;
    }

    public gridController grid;
    public randomSpawner[] SpawnerData;


    void Start()
    {
        //grid = GetComponentInChildren<gridController>();
    }

    public void InitializeObjectSpawn()
    {
        foreach(randomSpawner rs in SpawnerData)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(randomSpawner data)
    {
        int randomIteration = Random.Range(data.SpawnerData.minSpawn, data.SpawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.SpawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("spawned object");
        }
    }
}
