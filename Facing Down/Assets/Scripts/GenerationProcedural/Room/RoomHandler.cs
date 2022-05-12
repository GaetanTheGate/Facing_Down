
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


    //set this room to a start
    public void SetAsStart()
    {
        Game.currentRoom = this;
        OnEnterRoom();
        OnFinishRoom();
    }

    
    /*
    private void Start()
    {
        InitRoom("basic");
        AstarPath.active.Scan();
    }*/

	//choose a room category and set state doors
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

    //instantiate a room prefab according to a category (basic, boss, anteroom, spawn)
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

    //check if the object corresponding to the choosen category and if the configuration of the room corresponding to state doors
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

    //display the room on the UI and set the current room to this
    public void OnEnterRoom()
    {

        Map.changeColorMapicon(Game.currentRoom.gameObject,GetComponentInParent<RoomHandler>().gameObject);
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

    //hide the room on the UI
    public void OnExitRoom()
    {
        
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
        //GetComponent<PedestalHandler>().spawnPedestals();
    }


    //generate a random room on the side onSide 
    public bool generateRoomOnSide(side onSide){

        Vector2 coordinates = getCoordinates(gameObject);

        bool canGenerate = true;
        switch(onSide){
            case side.Right :
                if(coordinates.y + 1 > Floor.nbRoomWidth - 1 || Floor.gridMap[(int) coordinates.x, (int) coordinates.y + 1] != null){
                    canGenerate = false;
                    Floor.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Left :
                if(coordinates.y - 1 < 0 || Floor.gridMap[(int) coordinates.x, (int) coordinates.y - 1] != null){
                    canGenerate = false;
                    Floor.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Down :
                if(coordinates.x + 1 > Floor.nbRoomHeight - 1 || Floor.gridMap[(int) coordinates.x + 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    Floor.validSideOfRoom[gameObject].Remove(onSide);
                }
                break; 

            case side.Up :
                if(coordinates.x - 1 < 0 || Floor.gridMap[(int) coordinates.x - 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    Floor.validSideOfRoom[gameObject].Remove(onSide);
                }                
                break; 
        }

        if(canGenerate){
            print("génération");
            GameObject newMoldRoom = instantiateNewMoldRoom();

            addRoomToGridMap(newMoldRoom,coordinates,onSide);

            Floor.processRooms.Add(newMoldRoom);
            Floor.validSideOfRoom.Add(newMoldRoom,new List<side>(){side.Right,side.Left,side.Down});
            Floor.validSideOfRoom[newMoldRoom].Remove(onSide);

            setDoorsOn(onSide, newMoldRoom);
            Floor.validSideOfRoom[gameObject].Remove(onSide); 
        }
        else{
            print("génération impossible");
        }

        if (Floor.validSideOfRoom[gameObject].Count == 0){
            Floor.processRooms.Remove(gameObject);
        } 

        return canGenerate;
            
    }

    //generate a specific room on the side onSide
    public void generateSpecificRoomOnSide(side onSide, string name = ""){
        GameObject newMoldRoom = instantiateNewMoldRoom(name);
        addRoomToGridMap(newMoldRoom, getCoordinates(gameObject), onSide);
        setDoorsOn(onSide,newMoldRoom);
    }

    //get the coordinates of the moldRoom in the gridMap
    public static Vector2 getCoordinates(GameObject moldRoom){
        Vector2 coordinates = new Vector2(-1,-1);
        for(int i = 0 ; i < Floor.nbRoomHeight ; i += 1){
            for(int j = 0 ; j < Floor.nbRoomWidth ; j += 1){
                if (Floor.gridMap[i,j] != null && Floor.gridMap[i,j].name == moldRoom.name){
                    coordinates.x = i;
                    coordinates.y = j;
                }
            }
        }
        return coordinates;
    }


    public GameObject instantiateNewMoldRoom(string name = ""){
        GameObject newMoldRoom = Instantiate(Resources.Load(Floor.moldRoomPath, typeof(GameObject)) as GameObject);
        if(name == "")
            newMoldRoom.name = newMoldRoom.name.Substring(0,newMoldRoom.name.IndexOf('(')) + '-' + Floor.idRoom++;
        else
            newMoldRoom.name = name;
        newMoldRoom.transform.SetParent(GameObject.Find("Floor").transform);

        return newMoldRoom;  
    }

    public static void addRoomToGridMap(GameObject moldRoomToAdd, Vector2 coordinates , side side){
        switch(side){
            case side.Right :
                Floor.gridMap[(int) coordinates.x , (int) coordinates.y + 1] = moldRoomToAdd;
                break;
            case side.Left :
                Floor.gridMap[(int) coordinates.x , (int) coordinates.y - 1] = moldRoomToAdd;                
                break;
            case side.Down : 
                Floor.gridMap[(int) coordinates.x + 1, (int) coordinates.y] = moldRoomToAdd;
                break;
            case side.Up :
                Floor.gridMap[(int) coordinates.x - 1, (int) coordinates.y] = moldRoomToAdd;
                break;
        }
    }

    //set the boolean side of the current room at true and the opposite side of the room behind at true 
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

    public void CheckIfRoomIsFinish(){
        print("boolCheckIfRoomIsFinish " + GetComponentInChildren<RoomInfoHandler>().checkIfNoEnemy());
        if(GetComponentInChildren<RoomInfoHandler>().checkIfNoEnemy())
            OnFinishRoom();
    }
}
