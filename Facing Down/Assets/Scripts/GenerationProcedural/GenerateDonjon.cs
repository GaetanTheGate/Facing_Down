using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoom = 2;

    public string nameInitRoom;
    private Room initRoom;
    
    public static List<Room> rooms = new List<Room>();
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    void Awake(){
        GameObject player = Resources.Load("Donjon/Player",typeof(GameObject)) as GameObject;
        player = Instantiate(player);
        player.transform.SetParent(transform);
        foreach(Object o in Resources.LoadAll("Donjon/Rooms", typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }
        GameObject newRoom = getSpecifyRoom(nameInitRoom);
        newRoom = Instantiate(newRoom);
        newRoom.transform.SetParent(transform);
        
        rooms.Add(newRoom.GetComponent<Room>());
        initRoom = newRoom.GetComponent<Room>();
        
        Door.initCurrentRoom(initRoom);

    }

    void Start() {
        while(rooms.Count <= nbRoom){
            print("nbRoom " + nbRoom);
            Room processRoom = rooms[0];
            int i = Random.Range(0,processRoom.doors.Count);
            Door processDoor = processRoom.doors[i];
            print(processDoor.name);
            if(!processDoor.generateRoom()){
                nbRoom -= 1;
            }
        }

        print("Donjon");
        foreach(Room room in rooms){
            print(room.name);
            foreach(Door door in room.doors){
                print("  " + door + " linked to " + door.roomBehind);
            }
        }
    }


    public static GameObject getSpecifyRoom(string name){
        foreach(GameObject room in roomsPrefabs){
            if (room.name == name){
                return room;
            }
        }
        return null;
    }


}
