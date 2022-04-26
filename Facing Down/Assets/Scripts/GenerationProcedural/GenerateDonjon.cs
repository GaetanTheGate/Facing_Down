using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoom = 10;
    public static int idRoom = 0;

    public string nameInitRoom;

    public static float probDown = 0.66f;
    private Room initRoom;
    
    public static List<Room> rooms = new List<Room>();

    public static List<Door> processDoors = new List<Door>();

    
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    void Awake(){
        

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            //generateSpecificRoom();
            generate();
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

    public static Door getNextDoor(){

        List<Door> doorsDown = new List<Door>();
        List<Door> otherDoors = new List<Door>();

        foreach(Door door in processDoors){
            if (door.roomBehind == null){
                if(door.onSide == Door.side.Down){
                    doorsDown.Add(door);
                }
                else{
                    otherDoors.Add(door);
                }   
            }
        }
        if (doorsDown.Count == 0 && otherDoors.Count == 0){
            return null;
        }

        float i = Random.Range(0f,1f);
        if (otherDoors.Count == 0){
            return doorsDown[Random.Range(0,doorsDown.Count)];
        }
        else if (i < probDown && doorsDown.Count > 0){
            return doorsDown[Random.Range(0,doorsDown.Count)];
        }
        else{
            return otherDoors[Random.Range(0,otherDoors.Count)];
        }

    }

    public void initGenerate(){

        //Création d'un gameManager qui permet la gestion du donjon (stocke le joueur ainsi que toute les salles générées) 
        GameObject gameManager = new GameObject("GameManager");
        DontDestroyOnLoad(gameManager);

        GameObject player = Resources.Load("Donjon/Player",typeof(GameObject)) as GameObject;
        player = Instantiate(player);
        player.transform.SetParent(gameManager.transform);


        foreach(Object o in Resources.LoadAll("Donjon/Rooms", typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }
        
        GameObject newRoom = getSpecifyRoom(nameInitRoom);
        newRoom = Instantiate(newRoom);
        newRoom.name = newRoom.name.Substring(0,newRoom.name.IndexOf('(')) + '-' + idRoom++;
        newRoom.transform.SetParent(gameManager.transform);
        
        rooms.Add(newRoom.GetComponent<Room>());
        initRoom = newRoom.GetComponent<Room>();
        
        Door.initCurrentRoom(initRoom);

        foreach(Door door in initRoom.doors){
            processDoors.Add(door);
        }
    }

    public void generate(){

        while(nbRoom > 0){
           Door processDoor = getNextDoor();
           if (processDoor == null){ 
           }
           processDoor.generateRoom();
           nbRoom -= 1;
           processDoors.Remove(processDoor);
           if (processDoors.Count == 0 && nbRoom > 0){
               Destroy(GameObject.Find(processDoor.roomBehind.name));
               processDoor.roomBehind = null;
               nbRoom += 1;
               processDoors.Add(processDoor);
           } 
        }
        
        Map.generateMap(initRoom);
    }

    public void generateSpecificRoom(){
        initRoom.doors[0].generateSpecific(getSpecifyRoom("Room5"));
        initRoom.doors[0].roomBehind.doors[3].generateSpecific(getSpecifyRoom("Room7"));
        Map.generateMap(initRoom);
    }

    public void dysplayDonjonInConsole(){
        print("Donjon");
        foreach(Room room in rooms){
            print(room.name);
            foreach(Door door in room.doors){
                print("  " + door + " linked to " + door.roomBehind);
            }
        }
    }
}
