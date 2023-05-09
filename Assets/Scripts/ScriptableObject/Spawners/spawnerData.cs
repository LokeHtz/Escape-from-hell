using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/spawner")]
public class spawnerData : ScriptableObject
{

    public GameObject itemToSpawn;

    public int minSpawn;

    public int maxSpawn;
}
