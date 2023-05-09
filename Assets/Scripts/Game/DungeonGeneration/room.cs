using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{

    public int width;
    public int height;
    public int X;
    public int Y;

    private bool updatedDoors = false;

    public room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public door leftDoor;
    public door rightDoor;
    public door topDoor;
    public door bottomDoor;

    public door topleftDoor;
    public door botleftDoor;
    public door botrightDoor;
    public door lefttopDoor;
    public door leftbotDoor;
    public door rightbotDoor;

    public List<door> doors = new List<door>();

    void Start()
    {
        if (roomController.instance == null)
        {
            Debug.Log("you played in the wrong scene fool!");
            return;
        }
 
        roomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("Big") && updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }
    public void FindUnconnectedDoors()
    {
        door[] ds = GetComponentsInChildren<door>();
        foreach (door d in ds)
        {
            doors.Add(d);
            switch (d.DoorType)
            {
                case door.doorType.right:
                    rightDoor = d;
                    break;
                case door.doorType.left:
                    leftDoor = d;
                    break;
                case door.doorType.top:
                    topDoor = d;
                    break;
                case door.doorType.bottom:
                    bottomDoor = d;
                    break;

                case door.doorType.rightbot:
                    rightbotDoor = d;
                    break;
                case door.doorType.topleft:
                    topleftDoor = d;
                    break;
                case door.doorType.lefttop:
                    lefttopDoor = d;
                    break;
                case door.doorType.leftbot:
                    leftbotDoor = d;
                    break;
                case door.doorType.botleft:
                    botleftDoor = d;
                    break;
                case door.doorType.botright:
                    botrightDoor = d;
                    break;
            }
        }
    }
    public void RemoveUnconnectedDoors()
    {
        foreach(door Door in doors)
        {
            switch (Door.DoorType)
            {
                case door.doorType.right:
                    if (GetRight() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.left:
                    if (GetLeft() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.top:
                    if (GetTop() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.bottom:
                    if (GetBottom() != null)
                        Door.gameObject.SetActive(false);
                    break;

                case door.doorType.rightbot:
                    if (GetBotRight() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.topleft:
                    if (GetLeftTop() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.lefttop:
                    if (GetTopLeft() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.leftbot:
                    if (GetBotLeft() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.botleft:
                    if (GetLeftBottom() != null)
                        Door.gameObject.SetActive(false);
                    break;
                case door.doorType.botright:
                    if (GetRightBottom() != null)
                        Door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public room GetRight()
    {
        if (roomController.instance.DoesRoomExist(X + 1, Y))
        {
            return roomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public room GetLeft()
    {
        if (roomController.instance.DoesRoomExist(X - 1, Y))
        {
            return roomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public room GetTop()
    {
        if (roomController.instance.DoesRoomExist(X, Y + 1))
        {
            return roomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public room GetBottom()
    {
        if (roomController.instance.DoesRoomExist(X, Y - 1))
        {
            return roomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    public room GetBotRight()
    {
        if (roomController.instance.DoesRoomExist(X + 1, Y -1))
        {
            return roomController.instance.FindRoom(X + 1, Y -1);
        }
        return null;
    }

    public room GetTopLeft()
    {
        if (roomController.instance.DoesRoomExist(X - 2, Y))
        {
            return roomController.instance.FindRoom(X - 2, Y);
        }
        return null;
    }
    public room GetBotLeft()
    {
        if (roomController.instance.DoesRoomExist(X - 2, Y - 1))
        {
            return roomController.instance.FindRoom(X - 2, Y - 1);
        }
        return null;
    }

    public room GetLeftTop()
    {
        if (roomController.instance.DoesRoomExist(X - 1, Y + 1))
        {
            return roomController.instance.FindRoom(X - 1, Y + 1);
        }
        return null;
    }

    public room GetRightBottom()
    {
        if (roomController.instance.DoesRoomExist(X, Y - 2))
        {
            return roomController.instance.FindRoom(X, Y - 2);
        }
        return null;
    }

    public room GetLeftBottom()
    {
        if (roomController.instance.DoesRoomExist(X - 1, Y - 2))
        {
            return roomController.instance.FindRoom(X - 1, Y - 2);
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomController.instance.OnPlayerEnterRoom(this);
        }
    }

}
