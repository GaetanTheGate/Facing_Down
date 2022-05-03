
using System.Collections.Generic;
using UnityEngine;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoomWidth = 10;
    public static int nbRoomHeight = 5; 

    public static int nbRoom = (nbRoomHeight * nbRoomWidth) / 4;
    public static int idRoom = 0;


    public static float probUp = 0.66f;
    private Room initRoom;
    

    public static List<GameObject> processRooms = new List<GameObject>();
    public static Dictionary<GameObject,List<Room.side>> validSideOfRoom = new Dictionary<GameObject, List<Room.side>>();
    
    public static List<GameObject> roomsPrefabs = new List<GameObject>();
    
    public static GameObject[,] gridMap = new GameObject[nbRoomHeight, nbRoomWidth];

    private string gamePath = "Prefabs/Game/Game";
    private string roomPath = "Prefabs/Donjon/Rooms";
    public static string moldRoomPath = "Prefabs/Donjon/MoldRoom"; 


    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            generate();
        }
    }


    public void initGenerate(){
        GameObject gameManager = Resources.Load(gamePath, typeof(GameObject)) as GameObject;
        gameManager = Instantiate(gameManager);
        gameManager.name = "Game";
        DontDestroyOnLoad(gameManager);

        foreach(Object o in Resources.LoadAll(roomPath, typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }

        GameObject anteroom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        anteroom.name = anteroom.name.Substring(0,anteroom.name.IndexOf('(')) + '-' + idRoom++;
        anteroom.transform.SetParent(gameManager.transform);
        anteroom.GetComponent<Room>().infoRoom = Resources.Load("Prefabs/Donjon/BossRoom/Anteroom") as GameObject;
        processRooms.Add(anteroom);
        validSideOfRoom.Add(anteroom,new List<Room.side>(){Room.side.Down,Room.side.Left,Room.side.Right,Room.side.Up});
        
        gridMap[nbRoomHeight - 3, nbRoomWidth / 2] = anteroom;

        anteroom.GetComponent<Room>().generateSpecificRoomOnSide(Room.side.Down,Resources.Load("Prefabs/Donjon/BossRoom/BossRoom") as GameObject);
    }

    public void generate(){
        while (nbRoom > 0){
            GameObject processRoom = processRooms[Game.random.Next(0,processRooms.Count)];
            double random = Game.random.NextDouble();
            Room.side side;
            if (random < probUp && validSideOfRoom[processRoom].Contains(Room.side.Up)){
                side = Room.side.Up;
            }
            else if(random < probUp && !validSideOfRoom[processRoom].Contains(Room.side.Up) && validSideOfRoom[processRoom].Count > 0){
                side = validSideOfRoom[processRoom][Game.random.Next(0,validSideOfRoom[processRoom].Count)];
            }
            else{
                List<Room.side> otherSide = new List<Room.side>();

                foreach(Room.side valideSide in validSideOfRoom[processRoom]){
                    if(valideSide != Room.side.Up)
                        otherSide.Add(valideSide);
                }

                if(otherSide.Count == 0)
                    side = Room.side.Up;
                else
                    side = otherSide[Game.random.Next(0,otherSide.Count)];
            }

            if (processRoom.GetComponent<Room>().generateRoomOnSide(side)){
                nbRoom -= 1;
            }
            
        }


        /*for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0 ; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null){
                    Vector2 coordinates = Room.getCoordinates(gridMap[i,j]);
                    Room room = gridMap[i,j].GetComponent<Room>();
                    if(!room.doorUp && coordinates.y + 1 < nbRoomWidth - 1 && gridMap[(int) coordinates.x, (int) coordinates.y + 1] != null && Game.random.Next(0,4) == 0){
                        print("x " + (coordinates.y + 1));
                        print(gridMap[(int) coordinates.x, (int) coordinates.y + 1]);
                        gridMap[i,j].GetComponent<Room>().doorUp = true;
                        gridMap[(int) coordinates.x, (int) coordinates.y + 1].GetComponent<Room>().doorDown = true;
                    }

                    if(!room.doorDown && coordinates.y - 1 > 0 && gridMap[(int) coordinates.x, (int) coordinates.y - 1] != null && Game.random.Next(0,4) == 0){
                        print("y " + (coordinates.y - 1));
                        print(gridMap[(int) coordinates.x, (int) coordinates.y - 1]);
                        gridMap[i,j].GetComponent<Room>().doorUp = true;
                        gridMap[(int) coordinates.x, (int) coordinates.y - 1].GetComponent<Room>().doorDown = true;
                    }

                    if(!room.doorRight && coordinates.x + 1 < nbRoomHeight - 1 && gridMap[(int) coordinates.x + 1 ,(int) coordinates.y] != null && Game.random.Next(0,4) == 0){
                        print("x " + (coordinates.x + 1));
                        print(gridMap[(int) coordinates.x + 1 ,(int) coordinates.y]);
                        gridMap[i,j].GetComponent<Room>().doorUp = true;
                        gridMap[(int) coordinates.x + 1, (int) coordinates.y].GetComponent<Room>().doorDown = true;
                    }

                    if(!room.doorLeft && coordinates.x - 1 > 0 && gridMap[(int) coordinates.x - 1 ,(int) coordinates.y] != null && Game.random.Next(0,4) == 0){
                        print("x " + (coordinates.x - 1));
                        print(gridMap[(int) coordinates.x - 1 ,(int) coordinates.y]);
                        gridMap[i,j].GetComponent<Room>().doorUp = true;
                        gridMap[(int) coordinates.x - 1, (int) coordinates.y].GetComponent<Room>().doorDown = true;
                    }
                }
            }
        }*/

        Map.generateMap();
    }

    public GameObject getNextRoom(){
        return null;
    }

/*
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

        GameObject gameManager = Resources.Load(gamePath, typeof(GameObject)) as GameObject;
        gameManager = Instantiate(gameManager);
        gameManager.name = "Game";
        DontDestroyOnLoad(gameManager);

        foreach(Object o in Resources.LoadAll(roomPath, typeof(GameObject))){
            roomsPrefabs.Add((GameObject) o);
        }
        
        GameObject bossRoom = Resources.Load("Prefabs/Donjon/BossRoom/BossRoom",typeof(GameObject)) as GameObject;
        GameObject anteroom = Resources.Load("Prefabs/Donjon/BossRoom/Anteroom",typeof(GameObject)) as GameObject;
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
        GameObject.FindWithTag("Player").transform.position = initRoom.spawnPlayer.transform.position;
        
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
    }*/
}
