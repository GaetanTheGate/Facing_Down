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


    


    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Destroy(GameObject.Find("GameManager"));
        }
    }

    
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
        GenerateDonjon.rooms.Insert(0,r.GetComponent<Room>());
        roomBehind = r.GetComponent<Room>();

        initCurrentRoom(roomBehind);
        foreach(Door d in roomBehind.doors){
            if (d.roomBehind == null && d.onSide == getOppositeSide(onSide)){
                d.roomBehind = currentRoom;
                break;
            }
        }
    }


    public bool generateRoom() {
        if (roomBehind == null){
            print("génération salle");
            
            List<GameObject> validateRooms = selectRooms();
            int indexRoom = Random.Range(0,validateRooms.Count);
            GameObject newRoom = validateRooms[indexRoom];
            newRoom = Instantiate(newRoom);
            newRoom.SetActive(false);
            newRoom.name = newRoom.name.Substring(0,newRoom.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
            newRoom.transform.SetParent(GameObject.Find("GameManager").transform);
            GenerateDonjon.rooms.Insert(0,newRoom.GetComponent<Room>());
            roomBehind = newRoom.GetComponent<Room>();

            initCurrentRoom(roomBehind);
            
            foreach(Door door in roomBehind.doors){
                if(door.onSide == getOppositeSide(onSide)){
                    door.roomBehind = currentRoom;
                    break;
                }
            }

            foreach(Door door in newRoom.GetComponent<Room>().doors){
                if (door.roomBehind == null){
                    GenerateDonjon.processDoors.Add(door);
                }
            }

            return true;
            
        }
        else {
            print("roomBehind est déjà généré");
            return false;
        }
        
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
            if (mapIcon.name.Contains(roomBehind.name)){
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
