using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public enum side{
        right,
        left,
        up, 
        down

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

                Door door = null;
                foreach(Door d in roomBehind.doors){
                    if(d.roomBehind == currentRoom){
                        door = d;
                        break;
                    }
                }

                switch(door.onSide){
                    case side.right :
                        x = door.transform.position.x - 1;
                        y = collider2D.transform.position.y;
                        break;
                    case side.left :

                        x = door.transform.position.x + 1;
                        y = collider2D.transform.position.y;
                        break;
                    case side.up :
                        x = collider2D.transform.position.x;
                        y = door.transform.position.y - 1;
                        break;
                    case side.down :
                        x = door.transform.position.x + 1;
                        y = collider2D.transform.position.y;
                        break;
                    default :
                        print("null");
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
            GenerateDonjon.nbRoom -= 1;
            
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
                if(door.onSide == getOppositeSide(onSide) && door.roomBehind == null){
                    door.roomBehind = currentRoom;
                    break;
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
            if (onSide == side.right && go.GetComponent<Room>().hasDoorOnLeft){
                validateRoom.Add(go);
            }
            if (onSide == side.left && go.GetComponent<Room>().hasDoorOnRight){
                validateRoom.Add(go);
            }
            if (onSide == side.up && go.GetComponent<Room>().hasDoorOnDown){
                validateRoom.Add(go);
            }
            if (onSide == side.down && go.GetComponent<Room>().hasDoorOnUp){
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
        if (mySide == side.right){
            return side.left;
        }
        else if (mySide == side.left){
            return side.right;
        }
        else if (mySide == side.up){
            return side.down;
        }
        else{
            return side.up;
        }
    }

     private void changeScene(){      
        
        List<GameObject> gameObjects = new List<GameObject>();
        foreach(Object o in GameObject.FindObjectsOfType(typeof(GameObject), true)){
            gameObjects.Add((GameObject) o);
        }

        List<GameObject> rooms = new List<GameObject>();
        foreach(GameObject go in gameObjects){
            if (go.CompareTag("Room")){
                rooms.Add(go);
            }
        }

        foreach(GameObject room in rooms){
            room.SetActive(false);
            if (room.name == roomBehind.name){
                room.SetActive(true);
            }
        }
    
        SceneManager.LoadScene(roomBehind.name.Substring(0,roomBehind.name.IndexOf('-')));

    }
}
