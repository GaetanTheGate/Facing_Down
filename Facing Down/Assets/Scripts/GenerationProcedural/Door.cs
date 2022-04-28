using System.Collections;
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

    
    public void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.CompareTag("Player")){
            if (roomBehind != null){
                float x = 0;
                float y = 0;


                //récupération de la porte connecté à currentRoom dans roomBehind
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


                changeScene();
            
            }
            else {
                print("no room behind");
            }
            
        }
    }


    public void generateSpecific(GameObject r){
        r = Instantiate(r);
        r.SetActive(false);
        r.name = r.name.Substring(0,r.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
        r.transform.SetParent(GameObject.Find("GameManager").transform);
        roomBehind = r.GetComponent<Room>();

        initCurrentRoom(roomBehind);
        foreach(Door d in roomBehind.doors){
            if (d.roomBehind == null && d.onSide == getOppositeSide(onSide)){
                d.roomBehind = currentRoom;
                break;
            }
        }
    }


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

        List<GameObject> validateRoomsDoorOnDown = new List<GameObject>();
        List<GameObject> validateRoomsDoorNotOnDown = new List<GameObject>();

        GameObject newRoom;
        foreach(GameObject room in validateRooms){
            if (room.GetComponent<Room>().hasDoorOnDown)
                validateRoomsDoorOnDown.Add(room);
            else
                validateRoomsDoorNotOnDown.Add(room);
        }

        float indexRoom = Random.Range(0f,1f);

        if (validateRoomsDoorOnDown.Count == 0)
            newRoom = validateRoomsDoorNotOnDown[Random.Range(0,validateRoomsDoorNotOnDown.Count)];
        else
            if(indexRoom < GenerateDonjon.probDown || validateRoomsDoorNotOnDown.Count == 0)
                newRoom = validateRoomsDoorOnDown[Random.Range(0,validateRoomsDoorOnDown.Count)];
            else
                newRoom = validateRoomsDoorNotOnDown[Random.Range(0,validateRoomsDoorNotOnDown.Count)];

        newRoom = Instantiate(newRoom);
        newRoom.SetActive(false);
        newRoom.name = newRoom.name.Substring(0,newRoom.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
        newRoom.transform.SetParent(GameObject.Find("GameManager").transform);
        roomBehind = newRoom.GetComponent<Room>();

        initCurrentRoom(roomBehind);
        
        //associe la porte du bon côté de roomBehind à currentRoom 
        foreach(Door door in roomBehind.doors){
            if(door.onSide == getOppositeSide(onSide)){
                door.roomBehind = currentRoom;
                break;
            }
        }

        addRoomToGridMap(roomBehind,coordinates,this);

        //ajoute toutes les portes qui n'ont pas de roomBehind à processDoors
        foreach(Door door in roomBehind.doors){
            if (door.roomBehind == null){
                GenerateDonjon.processDoors.Add(door);
            }
        }

        return newRoom;
        
    }

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

     private void changeScene(){      
        
        //recupère tous les gamesObjects qu'ils soient actif ou non
        List<GameObject> gameObjects = new List<GameObject>();
        foreach(Object o in GameObject.FindObjectsOfType(typeof(GameObject), true)){
            gameObjects.Add((GameObject) o);
        }

        //recupère tous les gamesObject qui sont des rooms
        List<GameObject> rooms = new List<GameObject>();
        foreach(GameObject go in gameObjects){
            if (go.CompareTag("Room")){
                rooms.Add(go);
            }
        }

        //recupère tous les gamesObject qui sont des mapIcon
        List<GameObject> mapIcons = new List<GameObject>();
        foreach(GameObject go in gameObjects){
            if (go.CompareTag("MapIcon")){
                mapIcons.Add(go);
            }
        }

        //Passe à bleue la couleur de mapIcon de roomBehind
        foreach(GameObject mapIcon in mapIcons){
            mapIcon.GetComponent<Image>().color = Color.white;
            if (mapIcon.name.Substring(mapIcon.name.IndexOf('-')) == roomBehind.name.Substring(roomBehind.name.IndexOf('-'))){
                mapIcon.GetComponent<Image>().color = Color.blue;
            }
        }


        //active roomBehind
        foreach(GameObject room in rooms){
            room.SetActive(false);
            if (room.name == roomBehind.name){
                room.SetActive(true);
            }
        }
    

        SceneManager.LoadScene(roomBehind.name.Substring(0,roomBehind.name.IndexOf('-')));

    }
}
