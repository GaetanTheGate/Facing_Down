using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Donjon : MonoBehaviour
{

    public static int nbRoom = 4;
    public Room initRoom;
    
    public static List<Room> rooms = new List<Room>();
    void Awake(){
        Object[] prefab = Resources.LoadAll("Donjon/Rooms",typeof(GameObject));
        List<GameObject> listGameObject = new List<GameObject>();
        //transforme tous les Objects en GameObject
        foreach(Object o in prefab){
            GameObject newRoom = (GameObject) Instantiate(o);
            if (newRoom.name == "Room1(Clone)"){
                initRoom = newRoom.GetComponent<Room>();
            }
            newRoom.transform.SetParent(transform);
            listGameObject.Add(newRoom);
        }

        //Récupère tous le component Room du gameObjects et l'ajoute au tableau de Room
        foreach(GameObject go in listGameObject){
            rooms.Add(go.GetComponent<Room>());
        }
        
        //initialise currentRoom de chaque porte
        foreach(Room room in rooms){
            foreach(Door door in room.doors){
                door.currentRoom = room;
            }
        }
    }

    void Start() {
       foreach(Door door in initRoom.doors){
           door.generateRoom();
       }
    }

    public static Room getSpecifyRoom(string name){
        foreach(Room room in rooms){
            if (room.name == name){
                return room;
            }
        }
        return null;
    }


}
