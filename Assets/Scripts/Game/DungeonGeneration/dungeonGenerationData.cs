using UnityEngine;

[CreateAssetMenu(fileName ="DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dugeon Data")]
public class dungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers;

    public int iterationMin;
    public int iterationMax;
}
