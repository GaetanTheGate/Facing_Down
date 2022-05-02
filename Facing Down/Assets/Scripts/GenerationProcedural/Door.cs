using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    public enum side{
        Right,
        Left,
        Up, 
        Down

    }

    public side onSide;
    public Room roomBehind;
    public Room currentRoom;

    
    //change room if roomBehind is not null
    public void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.CompareTag("Player")){
            if (roomBehind != null){
                float x = 0;
                float y = 0;


                //get the door from roomBehind linked to currentRoom
                Door doorBehind = null;
                foreach(Door d in roomBehind.doors){
                    if(d.roomBehind == currentRoom){
                        doorBehind = d;
                        break;
                    }
                }

                switch(doorBehind.onSide){
                    case side.Right :
                        x = doorBehind.transform.position.x - 2;
                        y = doorBehind.transform.position.y;
                        break;
                    case side.Left :

                        x = doorBehind.transform.position.x + 2;
                        y = doorBehind.transform.position.y;
                        break;
                    case side.Up :
                        x = doorBehind.transform.position.x;
                        y = doorBehind.transform.position.y - 2;
                        break;
                    case side.Down :
                        x = doorBehind.transform.position.x + 2;
                        y = doorBehind.transform.position.y;
                        break;
                    default :
                        print("unknown side");
                        break;  

                }

                collider2D.transform.position = new Vector2(x,y);


                changeScene(roomBehind);
            
            }
            else {
                print("no room behind");
            }
            
        }
    }

    //Generate specific room in roomBehind
    public void generateSpecificRoom(GameObject newRoom){
       
        instantiateNewRoom(newRoom);
       
        addRoomToGridMap(roomBehind,getCoordinates(),this);
    }

    //Generate random room in roomBehind and return gameObject associated to the room or null if can't generate    
    public GameObject generateRoom() {
        Vector2 coordinates = new Vector2();
        for(int i = 0 ; i < GenerateDonjon.nbRoomHeight ; i += 1){
            for(int j = 0 ; j < GenerateDonjon.nbRoomWidth ; j += 1){
                if (GenerateDonjon.gridMap[i,j] == currentRoom)
                    coordinates = new Vector2(i,j);
            }
        }

        switch(onSide){
            case Door.side.Right :
                if(coordinates.y + 1 > GenerateDonjon.nbRoomWidth - 1|| GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y + 1] != null)
                    return null;
                break;
            case Door.side.Left :
                if(coordinates.y - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y - 1] != null)
                    return null;
                break;
            case Door.side.Down :
                if(coordinates.x + 1 > GenerateDonjon.nbRoomHeight - 1 || GenerateDonjon.gridMap[(int) coordinates.x + 1 ,(int) coordinates.y] != null)
                    return null;
                break; 
            case Door.side.Up :
                if(coordinates.x - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x - 1 ,(int) coordinates.y] != null)
                    return null;
                break; 
        }

        print("génération salle");
        
        List<GameObject> validateRooms = selectRooms();

        List<GameObject> validateRoomsDoorOnUp = new List<GameObject>();
        List<GameObject> validateRoomsDoorNotOnUp = new List<GameObject>();

        GameObject newRoom;
        foreach(GameObject room in validateRooms){
            if (room.GetComponent<Room>().hasDoorOnUp)
                validateRoomsDoorOnUp.Add(room);
            else
                validateRoomsDoorNotOnUp.Add(room);
        }

        float indexRoom = Random.Range(0f,1f);

        if (validateRoomsDoorOnUp.Count == 0)
            newRoom = validateRoomsDoorNotOnUp[Random.Range(0,validateRoomsDoorNotOnUp.Count)];
        else
            if(indexRoom < GenerateDonjon.probUp || validateRoomsDoorNotOnUp.Count == 0)
                newRoom = validateRoomsDoorOnUp[Random.Range(0,validateRoomsDoorOnUp.Count)];
            else
                newRoom = validateRoomsDoorNotOnUp[Random.Range(0,validateRoomsDoorNotOnUp.Count)];

        newRoom = instantiateNewRoom(newRoom);

        addRoomToGridMap(roomBehind,coordinates,this);
     
        return newRoom;
        
    }

    //return all rooms prefab which have a door on the opposite side of the process door
    public List<GameObject> selectRooms(){
        List<GameObject> validateRoom = new List<GameObject>();
        foreach(GameObject go in GenerateDonjon.roomsPrefabs){
            if (onSide == side.Right && go.GetComponent<Room>().hasDoorOnLeft){
                validateRoom.Add(go);
            }
            if (onSide == side.Left && go.GetComponent<Room>().hasDoorOnRight){
                validateRoom.Add(go);
            }
            if (onSide == side.Up && go.GetComponent<Room>().hasDoorOnDown){
                validateRoom.Add(go);
            }
            if (onSide == side.Down && go.GetComponent<Room>().hasDoorOnUp){
                validateRoom.Add(go);
            }
        }
        return validateRoom;
    }

    public static void addRoomToGridMap(Room roomToAdd, Vector2 coordinates , Door fromDoor){
        switch(fromDoor.onSide){
            case Door.side.Right :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y + 1] = roomToAdd;
                break;
            case Door.side.Left :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y - 1] = roomToAdd;                
                break;
            case Door.side.Down : 
                GenerateDonjon.gridMap[(int) coordinates.x + 1, (int) coordinates.y] = roomToAdd;
                break;
            case Door.side.Up :
                GenerateDonjon.gridMap[(int) coordinates.x - 1, (int) coordinates.y] = roomToAdd;
                break;
        }
    }

    public static void initCurrentRoom(Room room){
        foreach(Door door in room.doors){
            door.currentRoom = room;
        }
    }

    public void linkRoombehindToCurrentRoom(){
         foreach(Door d in roomBehind.doors){
            if (d.onSide == getOppositeSide(onSide)){
                d.roomBehind = currentRoom;
                break;
            }
        }
    }

    public void addDoorToProcessDoors(){
        foreach(Door door in roomBehind.doors){
            if (door.roomBehind == null && door.onSide != Door.side.Down){
                GenerateDonjon.processDoors.Add(door);
            }
        }
    }

    public GameObject instantiateNewRoom(GameObject newRoom){
        newRoom = Instantiate(newRoom);
        newRoom.SetActive(false);
        newRoom.name = newRoom.name.Substring(0,newRoom.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
        newRoom.transform.SetParent(GameObject.Find("GameManager").transform);
        roomBehind = newRoom.GetComponent<Room>();

        initCurrentRoom(roomBehind);

        linkRoombehindToCurrentRoom();

        addDoorToProcessDoors();

        return newRoom;  
    }

    //get the coordinates of a room in the gridMap
    public Vector2 getCoordinates(){
        Vector2 coordinates = new Vector2();
        for(int i = 0 ; i < GenerateDonjon.nbRoomHeight ; i += 1){
            for(int j = 0 ; j < GenerateDonjon.nbRoomWidth ; j += 1){
                if (GenerateDonjon.gridMap[i,j] == currentRoom)
                    coordinates = new Vector2(i,j);
            }
        }
        return coordinates;
    }

    public side getOppositeSide(side mySide){
        if (mySide == side.Right){
            return side.Left;
        }
        else if (mySide == side.Left){
            return side.Right;
        }
        else if (mySide == side.Up){
            return side.Down;
        }
        else{
            return side.Up;
        }
    }

     public static void changeScene(Room roomToChange){      

        //get all gameObject which are enable or disablemain
        List<GameObject> gameObjects = new List<GameObject>();
        foreach(Object o in GameObject.FindObjectsOfType(typeof(GameObject), true)){
            gameObjects.Add((GameObject) o);
        }

        //get gameObject xhich are Room
        List<GameObject> rooms = new List<GameObject>();
        foreach(GameObject go in gameObjects){
            if (go.CompareTag("Room")){
                rooms.Add(go);
            }
        }

        //get gameObject wich are mapIcon
        List<GameObject> mapIcons = new List<GameObject>();
        foreach(GameObject go in gameObjects){
            if (go.CompareTag("MapIcon")){
                mapIcons.Add(go);
            }
        }
        
        //set the mapIcon color to blue
        foreach(GameObject mapIcon in mapIcons){
            mapIcon.GetComponent<Image>().color = Color.white;
            if (mapIcon.name.Substring(mapIcon.name.IndexOf('-')) == roomToChange.name.Substring(roomToChange.name.IndexOf('-')))
                mapIcon.GetComponent<Image>().color = Color.blue;

            if(mapIcon.name.Contains("Boss"))
                mapIcon.GetComponent<Image>().color = Color.red;
        }


        //enable roomBehind
        foreach(GameObject room in rooms){
            room.SetActive(false);
            if (room.name == roomToChange.name){
                room.SetActive(true);
            }
        }
    

        SceneManager.LoadScene(roomToChange.name.Substring(0,roomToChange.name.IndexOf('-')));

    }
}
