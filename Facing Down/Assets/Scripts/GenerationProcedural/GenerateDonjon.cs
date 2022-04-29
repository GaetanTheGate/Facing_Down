
using System.Collections.Generic;
using UnityEngine;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoomWidth = 5;
    public static int nbRoomHeight = 10; 

    public static int nbRoom = nbRoomHeight - 2;
    public static int idRoom = 0;


    public static float probUp = 0.66f;
    private Room initRoom;
    

    public static List<Door> processDoors = new List<Door>();
    
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    
    public static Room[,] gridMap = new Room[nbRoomHeight, nbRoomWidth];


    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            generate();
        }
    }


    //return the gameObject associated to the name of the room 
    public static GameObject getSpecifyRoom(string name){
        foreach(GameObject room in roomsPrefabs){
            if (room.name == name){
                return room;
            }
        }
        return null;
    }

    //return the next door to generate a room behind
    public static Door getNextDoor(){

        List<Door> doorsUp = new List<Door>();
        List<Door> otherDoors = new List<Door>();

        foreach(Door door in processDoors){
            if (door.roomBehind == null){
                if(door.onSide == Door.side.Up){
                    doorsUp.Add(door);
                }
                else{
                    otherDoors.Add(door);
                }   
            }
        }


        if (doorsUp.Count == 0 && otherDoors.Count == 0){
            return null;
        }

        float i = Random.Range(0f,1f);
        if (otherDoors.Count == 0){
            return doorsUp[Random.Range(0,doorsUp.Count)];
        }
        else if (i < probUp && doorsUp.Count > 0){
            return doorsUp[Random.Range(0,doorsUp.Count)];
        }
        else{
            return otherDoors[Random.Range(0,otherDoors.Count)];
        }

    }

    //Initialize the donjon creating gameManager which contains player and all room generated
    public void initGenerate(){

        GameObject gameManager = new GameObject("GameManager");
        DontDestroyOnLoad(gameManager);

        GameObject player = Resources.Load("Donjon/Player",typeof(GameObject)) as GameObject;
        player = Instantiate(player);
        player.transform.SetParent(gameManager.transform);

        foreach(Object o in Resources.LoadAll("Donjon/Rooms", typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }
        
        GameObject bossRoom = Resources.Load("Donjon/BossRoom/BossRoom",typeof(GameObject)) as GameObject;
        GameObject anteroom = Resources.Load("Donjon/BossRoom/Anteroom",typeof(GameObject)) as GameObject;
        anteroom = Instantiate(anteroom);
        anteroom.name = anteroom.name.Substring(0,anteroom.name.IndexOf('(')) + '-' + idRoom++;
        anteroom.transform.SetParent(gameManager.transform);

        Door.initCurrentRoom(anteroom.GetComponent<Room>());
        
        gridMap[nbRoomHeight - 3, nbRoomWidth / 2] = anteroom.GetComponent<Room>();

        foreach(Door door in anteroom.GetComponent<Room>().doors){
            if(door.onSide == Door.side.Down){
                door.generateSpecificRoom(bossRoom);
            }
            else
                processDoors.Add(door);
        }
    }

    //Generate nbRoom rooms of the donjon
    public void generate(){
        while(nbRoom > 0){
            Door processDoor = getNextDoor();
            
            GameObject generateRoom = processDoor.generateRoom();
            processDoors.Remove(processDoor);

            nbRoom -= 1;

            if(generateRoom == null){
                print("salle non générée : overlaps");
                print("il reste " + processDoors.Count + " portes qui peuvent avoir une roomBehind");
                nbRoom += 1;
            }
            
            if (processDoors.Count == 0 && nbRoom > 0){
                if(generateRoom != null){
                    print("salle détruite");
                    removeSpecificRoomFromGridMap(generateRoom.GetComponent<Room>());
                    Destroy(generateRoom);
                    idRoom -= 1;
                    processDoor.roomBehind = null;
                    nbRoom += 1;
                    processDoors.Add(processDoor);
                }
                else{
                    print("salle détruite : generateRoom est null");
                    List<Room> roomsToDestroy = new List<Room>();
                    for(int i = 0; i < nbRoomHeight; i += 1){
                        for(int j = 0; j < nbRoomWidth; j += 1){
                            if((gridMap[i,j] == null))
                                continue;
                            else{
                                if(gridMap[i,j].doors.Count == 1 && 
                                    ((j - 1 < 0 && gridMap[i,j-1] == null) ||
                                    (j + 1 > nbRoomWidth - 1 && gridMap[i,j+1] == null) ||
                                    (i - 1 < 0 && gridMap[i-1,j] == null) ||
                                    (i + 1 > nbRoomHeight - 1 && gridMap[i+1,j] == null))){
                                        roomsToDestroy.Add(gridMap[i,j]);
                                }   
                            }
                        }
                    }
                    Room roomToDestroy = roomsToDestroy[Random.Range(0,roomsToDestroy.Count)];
                    foreach(Door door in roomToDestroy.doors[0].roomBehind.doors){
                        if(door.roomBehind == roomToDestroy)
                            door.roomBehind = null;
                            processDoors.Add(door);
                            nbRoom += 1;
                    } 
                }
           }

           if (processDoor == null){
                print("Door is null");
                print("il reste " + processDoors.Count + " portes qui peuvent avoir une roomBehind");
                break; 
            } 
        }

        foreach(Room room in gridMap){
            if(room != null){
                foreach(Door door in room.doors){
                    if(door.roomBehind == null){
                        door.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                }
            }
            
        }
        
        Map.generateMap();

        setInitRoom();
        
        Door.changeScene(initRoom);
    }

    //remove a room from the gridMap
    public void removeSpecificRoomFromGridMap(Room room){
        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0 ; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] == room){
                    gridMap[i,j] = null;
                    return;
                }
            }
        }
    }

    //initialize initRoom
    public void setInitRoom(){
        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null){
                    initRoom = gridMap[i,j];
                    return;
                }
            }
        }
    }
}
