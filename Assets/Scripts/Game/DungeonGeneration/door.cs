using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public enum doorType
    {
        left, right, top, bottom, rightbot, leftbot, lefttop, botright, botleft, topleft 
    }

    public doorType DoorType;
}
