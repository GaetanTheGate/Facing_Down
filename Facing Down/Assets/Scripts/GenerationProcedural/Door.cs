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


    void Start(){
        //generateRoom();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Destroy(GameObject.Find("GameManager"));
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.CompareTag("Player")){  
            if (roomBehind != null){
                //TO DO : changer la position du joueur
                collider2D.transform.position = new Vector2(0,-3);
                GameObject gameManager = GameObject.Find("GameManager");
                SceneManager.LoadScene(roomBehind.name.Substring(0,roomBehind.name.IndexOf('(')),LoadSceneMode.Additive);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(roomBehind.name.Substring(0,roomBehind.name.IndexOf('('))));
                print("Scene active : " + SceneManager.GetActiveScene().name);

                
            }
            else {
                print("no room behind");
            }
            
        }
    }


    public void generateRoom() {
        print("nbRoom = " + Donjon.nbRoom);
        print("roomBehind = " + roomBehind);
        if (Donjon.nbRoom > 0 && roomBehind == null){
            print("generation");
            Donjon.nbRoom -= 1;
            List<Room> validateRoom = selectRoom();
            int i = Random.Range(0,validateRoom.Count);
            roomBehind = validateRoom[i];
            print("current room : door " + gameObject + " has " + roomBehind + " in roomBehind");
            Room room = Donjon.getSpecifyRoom(roomBehind.name);
            print("room name " + room);
            foreach(Door door in room.doors){
                print(door);
                if (getOppositeSide(onSide) == door.onSide && door.roomBehind == null){
                    door.roomBehind = currentRoom;
                    print("roomBehind : door " + door + "has " + currentRoom + " in roomBehind");
                    break;
                }
            }
        }
        else{
            print("génération terminée");
        }
        
    }

    public List<Room> selectRoom(){
        List<Room> validateRoom = new List<Room>();
        foreach(Room room in Donjon.rooms){
            if (onSide == side.right && room.hasDoorOnLeft){
                validateRoom.Add(room);
            }
            if (onSide == side.left && room.hasDoorOnRight){
                validateRoom.Add(room);
            }
            if (onSide == side.up && room.hasDoorOnDown){
                validateRoom.Add(room);
            }
            if (onSide == side.down && room.hasDoorOnUp){
                validateRoom.Add(room);
            }
        }
        return validateRoom;
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

     private IEnumerator changeScene(GameObject entity){
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(roomBehind.name.Substring(0,roomBehind.name.IndexOf('(')), LoadSceneMode.Additive);
        while(!asyncOperation.isDone){
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(entity,SceneManager.GetSceneByName(roomBehind.name.Substring(0,roomBehind.name.IndexOf('('))));
        SceneManager.UnloadSceneAsync(currentRoom.name.Substring(0,currentRoom.name.IndexOf('(')));
    }
}
