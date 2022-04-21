using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoom = 3;
    public static int idRoom = 0;

    public string nameInitRoom;
    private Room initRoom;
    
    public static List<Room> rooms = new List<Room>();
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    void Awake(){
        

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            generateSpecificRoom();
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

    public void initGenerate(){
        GameObject gameManager = new GameObject("GameManager");
        GameObject player = Resources.Load("Donjon/Player",typeof(GameObject)) as GameObject;
        player = Instantiate(player);
        player.transform.SetParent(gameManager.transform);
        foreach(Object o in Resources.LoadAll("Donjon/Rooms", typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }
        GameObject newRoom = getSpecifyRoom(nameInitRoom);
        newRoom = Instantiate(newRoom);
        newRoom.name = newRoom.name.Substring(0,newRoom.name.IndexOf('(')) + '-' + idRoom++;
        //print(newRoom.name);
        newRoom.transform.SetParent(gameManager.transform);
        
        rooms.Add(newRoom.GetComponent<Room>());
        initRoom = newRoom.GetComponent<Room>();
        
        Door.initCurrentRoom(initRoom);
    }

    public void generate(){
        while(rooms.Count <= nbRoom){
            Room processRoom = rooms[0];
            int i = Random.Range(0,processRoom.doors.Count);
            Door processDoor = processRoom.doors[i];
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

    public void generateSpecificRoom(){
        initRoom.doors[0].generateSpecific(getSpecifyRoom("Room2"));
        initRoom.doors[0].roomBehind.doors[1].generateSpecific(getSpecifyRoom("Room3"));
        print("Donjon");
        foreach(Room room in rooms){
            print(room.name);
            foreach(Door door in room.doors){
                print("  " + door + " linked to " + door.roomBehind);
            }
        }
    }

}
