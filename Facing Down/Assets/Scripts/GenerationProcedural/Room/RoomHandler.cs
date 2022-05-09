using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public bool testFinishRoom = false;
    public bool setAsStartTest = false;

    public bool leftDoor = false;
    public bool rightDoor = false;
    public bool topDoor = false;
    public bool botDoor = false;

    public bool hasVisited = false;
    public bool isCompleted = false;

    private bool isInRoom = false;

    public enum side{
        Right,
        Left,
        Up, 
        Down
    }

    public void SetAsStart()
    {
        Game.currentRoom = this;
        OnEnterRoom();
        OnFinishRoom();
    }

    private void Start()
    {
        InitRoom("test");
        AstarPath.active.Scan();
    }

    public void InitRoom(string category)
    {
        SetRoomInfo(category);
        GetComponentInChildren<DoorsHandler>().SetDoorsState(leftDoor, rightDoor, topDoor, botDoor);
        GetComponentInChildren<DoorsHandler>().SetDoors();
        GetComponentInChildren<DoorsHandler>().SetClosedState(false);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();


        GetComponentInChildren<RoomInfoHandler>().InitRoomInfo();
    }

    private void FixedUpdate()
    {
        GetComponentInChildren<DoorsHandler>().SetDoorsState(leftDoor, rightDoor, topDoor, botDoor);
        GetComponentInChildren<DoorsHandler>().SetDoors();

        if (testFinishRoom)
            OnFinishRoom();

        if (setAsStartTest)
            SetAsStart();
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
        print("enter room " + gameObject.name);

        if (isInRoom)
            return;

        isInRoom = true;

        Game.currentRoom = this;

        hasVisited = true;

        GetComponentInChildren<RoomHiderHandler>().SetBlurState(false);
        GetComponentInChildren<RoomHiderHandler>().SetDarknessState(false);

        if (isCompleted)
            return;

        GetComponentInChildren<RoomInfoHandler>().SpawnEnemy();

        GetComponentInChildren<DoorsHandler>().SetClosedState(true);
        GetComponentInChildren<DoorsHandler>().SetCloseDoor();
    }

    public void OnExitRoom()
    {
        print("exit room " + gameObject.name);
        isInRoom = false;

        GetComponentInChildren<RoomInfoHandler>().DespawnEnemy();

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

    public bool generateRoomOnSide(side onSide){

        Vector2 coordinates = getCoordinates(gameObject);

        bool canGenerate = true;
        switch(onSide){
            case side.Right :
                if(coordinates.y + 1 > GenerateDonjon.nbRoomWidth - 1 || GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y + 1] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Left :
                if(coordinates.y - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y - 1] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Down :
                if(coordinates.x + 1 > GenerateDonjon.nbRoomHeight - 1 || GenerateDonjon.gridMap[(int) coordinates.x + 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break; 

            case side.Up :
                if(coordinates.x - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x - 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }                
                break; 
        }

        if(canGenerate){
            print("génération");
            GameObject newMoldRoom = instantiateNewMoldRoom();

            addRoomToGridMap(newMoldRoom,coordinates,onSide);

            GenerateDonjon.processRooms.Add(newMoldRoom);
            GenerateDonjon.validSideOfRoom.Add(newMoldRoom,new List<side>(){side.Right,side.Left,side.Down});
            GenerateDonjon.validSideOfRoom[newMoldRoom].Remove(onSide);

            setDoorsOn(onSide, newMoldRoom);
            GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide); 
        }
        else{
            print("génération impossible");
        }

        if (GenerateDonjon.validSideOfRoom[gameObject].Count == 0){
            GenerateDonjon.processRooms.Remove(gameObject);
        } 

        return canGenerate;
            
    }

    public void generateSpecificRoomOnSide(side side, string name = ""){
        GameObject newMoldRoom = instantiateNewMoldRoom(name);
        addRoomToGridMap(newMoldRoom, getCoordinates(gameObject), side);
        setDoorsOn(side,newMoldRoom);
    }

    public static Vector2 getCoordinates(GameObject moldRoom){
        Vector2 coordinates = new Vector2(-1,-1);
        for(int i = 0 ; i < GenerateDonjon.nbRoomHeight ; i += 1){
            for(int j = 0 ; j < GenerateDonjon.nbRoomWidth ; j += 1){
                if (GenerateDonjon.gridMap[i,j] != null && GenerateDonjon.gridMap[i,j].name == moldRoom.name){
                    coordinates.x = i;
                    coordinates.y = j;
                }
            }
        }
        return coordinates;
    }

    public GameObject instantiateNewMoldRoom(string name = ""){
        GameObject newMoldRoom = Instantiate(Resources.Load(GenerateDonjon.moldRoomPath, typeof(GameObject)) as GameObject);
        if(name == "")
            newMoldRoom.name = newMoldRoom.name.Substring(0,newMoldRoom.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
        else
            newMoldRoom.name = name;
        newMoldRoom.transform.SetParent(GameObject.Find("Game").transform);

        return newMoldRoom;  
    }

    public static void addRoomToGridMap(GameObject moldRoomToAdd, Vector2 coordinates , side side){
        switch(side){
            case side.Right :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y + 1] = moldRoomToAdd;
                break;
            case side.Left :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y - 1] = moldRoomToAdd;                
                break;
            case side.Down : 
                GenerateDonjon.gridMap[(int) coordinates.x + 1, (int) coordinates.y] = moldRoomToAdd;
                break;
            case side.Up :
                GenerateDonjon.gridMap[(int) coordinates.x - 1, (int) coordinates.y] = moldRoomToAdd;
                break;
        }
    }

    public void setDoorsOn(side onSide, GameObject newMoldRoom){
        switch(onSide){
            case side.Right :
                rightDoor = true;
                newMoldRoom.GetComponent<RoomHandler>().leftDoor = true;
                break;
            case side.Left :
                leftDoor = true;
                newMoldRoom.GetComponent<RoomHandler>().rightDoor = true;
                break;
            case side.Down :
                botDoor = true;
                newMoldRoom.GetComponent<RoomHandler>().topDoor = true;
                break;
            case side.Up :
                topDoor = true;
                newMoldRoom.GetComponent<RoomHandler>().botDoor = true;
                break;
        }
    }
}
