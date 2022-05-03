using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public bool testFinishRoom = false;

    public bool leftDoor = false;
    public bool rightDoor = false;
    public bool topDoor = false;
    public bool botDoor = false;

    public bool hasVisited = false;
    public bool isCompleted = false;

    private bool isInRoom = false;

    void Start()
    {
        //test
        InitRoom("basic");
        Game.currentRoom = this;
        Game.player.transform.position = transform.position;
    }

    public void InitRoom(string category)
    {
        SetRoomInfo(category);
        GetComponentInChildren<DoorsHandler>().SetDoorsState(leftDoor, rightDoor, topDoor, botDoor);
        GetComponentInChildren<DoorsHandler>().SetDoors();
        GetComponentInChildren<DoorsHandler>().SetClosedState(false);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();
    }

    private void FixedUpdate()
    {
        GetComponentInChildren<DoorsHandler>().SetDoorsState(leftDoor, rightDoor, topDoor, botDoor);
        GetComponentInChildren<DoorsHandler>().SetDoors();

        if (testFinishRoom)
            OnFinishRoom();
    }

    private string roomInfoFolder = "Prefabs/Rooms/RoomsInfo";
    public void SetRoomInfo(string category)
    {
        Object[] roomList = Resources.LoadAll(roomInfoFolder, typeof(GameObject));


        List<Object> fileToChooseFrom = new List<Object>();
        foreach(Object o in roomList)
        {
            if (isFileCorrect(o, category))
                fileToChooseFrom.Add(o);
        }

        foreach (Object o in fileToChooseFrom)
            print(o.name);

        GameObject roomInfo = Instantiate( (GameObject) fileToChooseFrom[Game.random.Next(0, fileToChooseFrom.Count)]);
        roomInfo.transform.SetParent(transform);
        roomInfo.transform.localPosition = new Vector3();
    }

    public bool isFileCorrect(Object o, string category)
    {
        if ( ! o.name.Contains(category))
            return false;

        char doorState;

        doorState = o.name[o.name.Length - 4];
        if ((doorState.Equals('1') && !leftDoor) || (doorState.Equals('0') && leftDoor))
            return false;
        doorState = o.name[o.name.Length - 3];
        if ((doorState.Equals('1') && !rightDoor) || (doorState.Equals('0') && rightDoor))
            return false;
        doorState = o.name[o.name.Length - 2];
        if ((doorState.Equals('1') && !topDoor) || (doorState.Equals('0') && topDoor))
            return false;
        doorState = o.name[o.name.Length - 1];
        if ((doorState.Equals('1') && !botDoor) || (doorState.Equals('0') && botDoor))
            return false;

        return true;
    }

    public void OnEnterRoom()
    {
        if (isInRoom)
            return;

        isInRoom = true;

        Game.currentRoom = this;

        hasVisited = true;

        GetComponentInChildren<RoomHiderHandler>().SetBlurState(false);

        if (isCompleted)
            return;

        GetComponentInChildren<RoomHiderHandler>().SetDarknessState(false);

        GetComponentInChildren<DoorsHandler>().SetClosedState(true);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();
    }

    public void OnExitRoom()
    {
        isInRoom = false;

        GetComponentInChildren<RoomHiderHandler>().SetBlurState(true);

        GetComponentInChildren<DoorsHandler>().SetClosedState(false);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();

        if( !isCompleted)
            GetComponentInChildren<RoomHiderHandler>().SetDarknessState(true);
    }

    public void OnFinishRoom()
    {
        isCompleted = true;

        GetComponentInChildren<DoorsHandler>().SetClosedState(false);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();
    }
}
