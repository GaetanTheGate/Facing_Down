
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoomWidth = 5;
    public static int nbRoomHeight = 10; 

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
    public static string moldRoomPath = "Prefabs/Rooms/BaseRooms/BaseRoom"; 


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
        processRooms.Add(anteroom);
        validSideOfRoom.Add(anteroom,new List<Room.side>(){Room.side.Down,Room.side.Left,Room.side.Right,Room.side.Up});
        
        gridMap[nbRoomHeight - 3, nbRoomWidth / 2] = anteroom;

        anteroom.GetComponent<Room>().generateSpecificRoomOnSide(Room.side.Down,Resources.Load("Prefabs/Donjon/BossRoom/BossRoom") as GameObject);
    }

    public void generate(){
        while (nbRoom > 0){
            if(processRooms.Count == 0){
                print("plus de possibilité de générer une salle");
                break;
            }

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

        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if (gridMap[i,j] != null){
                    gridMap[i,j].GetComponent<RoomHandler>().leftDoor = gridMap[i,j].GetComponent<Room>().doorLeft;
                    gridMap[i,j].GetComponent<RoomHandler>().rightDoor = gridMap[i,j].GetComponent<Room>().doorRight;
                    gridMap[i,j].GetComponent<RoomHandler>().topDoor = gridMap[i,j].GetComponent<Room>().doorUp;
                    gridMap[i,j].GetComponent<RoomHandler>().botDoor = gridMap[i,j].GetComponent<Room>().doorDown;

                    gridMap[i,j].transform.position = new Vector2(36 * j, 36 * -i);
                }
            }
        }
        
        Map.generateMap();
        
        setInitRoom();
        Game.currentRoom.OnEnterRoom();
        

        
    }


    public void setInitRoom(){
        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if (gridMap[i,j] != null){
                    Game.currentRoom = gridMap[i,j].GetComponent<RoomHandler>();
                    Game.player.transform.position = gridMap[i,j].GetComponent<RoomHandler>().transform.position;
                    GameObject.Find("MapIcon" + gridMap[i,j].name).GetComponent<Image>().color = Color.blue;
                    return;
                }
            }
        }
        
    }
}
