using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsHandler : MonoBehaviour
{
    public bool left = false;
    public bool right = false;
    public bool top = false;
    public bool bot = false;

    public bool doorClosed = false;

    public void SetDoorsState(bool left, bool right, bool top, bool bot)
    {
        this.left = left;
        this.right = right;
        this.top = top;
        this.bot = bot;
    }

    public void SetClosedState(bool doorClosed)
    {
        this.doorClosed = doorClosed;
    }

    public void SetDoors()
    {
        foreach(Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            switch (child.name)
            {
                case "LeftWall":
                    child.gameObject.SetActive(!left);
                    break;
                case "LeftDoor":
                    child.gameObject.SetActive(left);
                    break;

                case "RightWall":
                    child.gameObject.SetActive(!right);
                    break;
                case "RightDoor":
                    child.gameObject.SetActive(right);
                    break;

                case "TopWall":
                    child.gameObject.SetActive(!top);
                    break;
                case "TopDoor":
                    child.gameObject.SetActive(top);
                    break;

                case "BottomWall":
                    child.gameObject.SetActive(!bot);
                    break;
                case "BottomDoor":
                    child.gameObject.SetActive(bot);
                    break;

                default:
                    break;
            }
        }
    }

    public void SetCloseDoor()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            switch (child.name)
            {
                case "LeftBlock":
                    child.gameObject.SetActive(left && doorClosed);
                    break;

                case "RightBlock":
                    child.gameObject.SetActive(right && doorClosed);
                    break;

                case "TopBlock":
                    child.gameObject.SetActive(top && doorClosed);
                    break;

                case "BottomBlock":
                    child.gameObject.SetActive(bot && doorClosed);
                    break;

                default:
                    break;
            }
        }
    }
}
