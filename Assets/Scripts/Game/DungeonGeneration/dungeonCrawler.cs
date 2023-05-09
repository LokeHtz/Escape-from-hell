using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public dungeonCrawler(Vector2Int startpos)
        {
            Position = startpos;
        }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}
