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
    private Room initRoom;
    
    public static List<Room> rooms = new List<Room>();

    public static List<Room> roomsForMap = new List<Room>();
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    void Awake(){
        

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            generateSpecificRoom();
            //generate();
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

    public static Door getNextDoor(Room room){
        foreach(Door door in room.doors){
            if(door.roomBehind == null){
                return door;
            }
        }
        return null;
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
        roomsForMap.Add(newRoom.GetComponent<Room>());
        initRoom = newRoom.GetComponent<Room>();
        
        Door.initCurrentRoom(initRoom);
    }

    public void generate(){

        //prends une porte aléatoire de la première salle a traité et lui ajoute une salle si possible
        for(int i = 0; i < nbRoom; i+=1){
            Room processRoom = rooms[0];
            Door processDoor = getNextDoor(processRoom);
            if (processDoor != null){
                processDoor.generateRoom();
            }
            else{
                rooms.Remove(processRoom);
            }
            if (rooms.Count == 0){
                break;
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
