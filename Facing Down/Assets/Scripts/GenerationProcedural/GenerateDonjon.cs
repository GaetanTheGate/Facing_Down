
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoomWidth = 5;
    public static int nbRoomHeight = 10; 

    public static int nbRoom = (nbRoomHeight * nbRoomWidth) / 4;
    public static int idRoom = 0;


    public static float probDown = 0.66f;
    private GameObject initRoom;
    

    public static List<GameObject> processRooms = new List<GameObject>();
    public static Dictionary<GameObject,List<Room.side>> validSideOfRoom = new Dictionary<GameObject, List<Room.side>>();
    
    
    public static GameObject[,] gridMap = new GameObject[nbRoomHeight, nbRoomWidth];

    private string gamePath = "Prefabs/Game/Game";
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

        initRoom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        initRoom.name = initRoom.name.Substring(0,initRoom.name.IndexOf('(')) + '-' + idRoom++;
        initRoom.transform.SetParent(gameManager.transform);
        processRooms.Add(initRoom);
        validSideOfRoom.Add(initRoom,new List<Room.side>(){Room.side.Down,Room.side.Left,Room.side.Right});
        gridMap[0, nbRoomWidth / 2] = initRoom;


        GameObject anteroom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        anteroom.name = anteroom.name.Substring(0,anteroom.name.IndexOf('(')) + '-' + idRoom++;
        anteroom.transform.SetParent(gameManager.transform);
        
        gridMap[nbRoomHeight - 3, nbRoomWidth / 2] = anteroom;

        anteroom.GetComponent<Room>().generateSpecificRoomOnSide(Room.side.Down,"BossRoom");

        Game.currentRoom = initRoom.GetComponent<RoomHandler>();
        
    }

    public void generate(){
        while (!checkIfRoomOnLineBeforeAnteroom() && nbRoom > 0){
            if(processRooms.Count == 0){
                print("plus de possibilité de générer une salle");
                break;
            }

            GameObject processRoom = processRooms[Game.random.Next(0,processRooms.Count)];

            double random = Game.random.NextDouble();
            Room.side side;
            if (random < probDown && validSideOfRoom[processRoom].Contains(Room.side.Down)){
                side = Room.side.Down;
            }

            else if(random < probDown && !validSideOfRoom[processRoom].Contains(Room.side.Down) && validSideOfRoom[processRoom].Count > 0){
                side = validSideOfRoom[processRoom][Game.random.Next(0,validSideOfRoom[processRoom].Count)];
            }

            else{
                List<Room.side> otherSide = new List<Room.side>();

                foreach(Room.side valideSide in validSideOfRoom[processRoom]){
                    if(valideSide != Room.side.Down)
                        otherSide.Add(valideSide);
                }

                if(otherSide.Count == 0)
                    side = Room.side.Down;
                else
                    side = otherSide[Game.random.Next(0,otherSide.Count)];
            }

            if(processRoom.GetComponent<Room>().generateRoomOnSide(side)){
                nbRoom -= 1;
            }
            
        }

        linkRoomToAnteroom();

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
        
        Game.currentRoom.OnEnterRoom();
        Game.player.transform.position = initRoom.GetComponent<RoomHandler>().transform.position;
        Map.generateMap();
              
    }

    public bool checkIfRoomOnLineBeforeAnteroom(){
        for (int i = 0; i < nbRoomWidth; i++){
            if(gridMap[nbRoomHeight - 4,i] != null)
                return true;
        }
        return false;
    }

    public void linkRoomToAnteroom(){
        for(int i = 0; i < nbRoomHeight - 3; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null){
                    Room room = gridMap[i,j].GetComponent<Room>();
                    if(!room.doorDown && !room.doorLeft && !room.doorRight && room.doorUp){
                        if((j + 1 < nbRoomWidth && gridMap[i,j+1] != null))
                            room.setDoorsOn(Room.side.Right, gridMap[i,j+1]);

                        else if(j - 1 > 0 && gridMap[i,j-1] != null)
                            room.setDoorsOn(Room.side.Left, gridMap[i,j-1]);

                        else if(gridMap[i+1,j] != null)
                            room.setDoorsOn(Room.side.Down, gridMap[i+1,j]);
                    
                        else
                            room.generateSpecificRoomOnSide(Room.side.Down);
                    }

                    if(!room.doorDown && !room.doorLeft && room.doorRight && !room.doorUp){
                        if(gridMap[i+1,j] != null)
                            room.setDoorsOn(Room.side.Down, gridMap[i+1,j]);

                        else if(j - 1 > 0 && gridMap[i,j-1] != null)
                            room.setDoorsOn(Room.side.Left, gridMap[i,j-1]);

                        else
                            room.generateSpecificRoomOnSide(Room.side.Down);
                    }

                    if(!room.doorDown && room.doorLeft && !room.doorRight && !room.doorUp){
                        if(gridMap[i+1,j] != null)
                            room.setDoorsOn(Room.side.Down, gridMap[i+1,j]);

                        else if(j + 1 < nbRoomWidth && gridMap[i,j+1] != null)
                            room.setDoorsOn(Room.side.Right, gridMap[i,j+1]);

                        else
                            room.generateSpecificRoomOnSide(Room.side.Down);
                    }
                    if(!room.doorDown && room.doorLeft && !room.doorRight && room.doorUp)
                        checkIfRoomIsLinkToRoomWithDoorDown(Room.side.Left,new Vector2(i,j));

                    if(!room.doorDown && !room.doorLeft && room.doorRight && room.doorUp)
                        checkIfRoomIsLinkToRoomWithDoorDown(Room.side.Right, new Vector2(i,j));    
                }
            }
        }


        
        for(int i = 0; i < nbRoomWidth/2; i +=1){
            if(gridMap[nbRoomHeight-3, i] != null){
                Room room = gridMap[nbRoomHeight-3,i].GetComponent<Room>();
                if(gridMap[nbRoomHeight-3, i + 1] != null)
                    room.setDoorsOn(Room.side.Right, gridMap[nbRoomHeight-3,i + 1]);
                else
                    room.generateSpecificRoomOnSide(Room.side.Right);
            }
        }

        for(int i = nbRoomWidth - 1; i > nbRoomWidth/2; i -=1){
            if(gridMap[nbRoomHeight-3, i] != null){
                Room room = gridMap[nbRoomHeight-3,i].GetComponent<Room>();
                    if(gridMap[nbRoomHeight-3, i - 1] != null)
                        room.setDoorsOn(Room.side.Left, gridMap[nbRoomHeight-3,i - 1]);
                    else
                        room.generateSpecificRoomOnSide(Room.side.Left);
            }
        }
    }


    public void checkIfRoomIsLinkToRoomWithDoorDown(Room.side sideToGo, Vector2 coordinates){
        switch(sideToGo){
            case Room.side.Left:
                if(gridMap[(int) coordinates.x, (int) coordinates.y - 1].GetComponent<Room>().doorDown)
                    return;
                else if (gridMap[(int) coordinates.x, (int) coordinates.y -1].GetComponent<Room>().doorLeft)
                    checkIfRoomIsLinkToRoomWithDoorDown(Room.side.Left, new Vector2(coordinates.x, coordinates.y -1));
                else{
                    if(gridMap[(int) coordinates.x + 1, (int) coordinates.y] == null){
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<Room>().generateSpecificRoomOnSide(Room.side.Down);
                        return;                    }

                    else{
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<Room>().setDoorsOn(Room.side.Down, gridMap[(int) coordinates.x + 1, (int) coordinates.y]);
                        return;
                    }
                }
                break;
            
            case Room.side.Right:
                if(gridMap[(int) coordinates.x, (int) coordinates.y + 1].GetComponent<Room>().doorDown)
                    return;
                else if (gridMap[(int) coordinates.x, (int) coordinates.y +1].GetComponent<Room>().doorRight)
                    checkIfRoomIsLinkToRoomWithDoorDown(Room.side.Right, new Vector2(coordinates.x, coordinates.y + 1));
                else{
                    if(gridMap[(int) coordinates.x + 1, (int) coordinates.y] == null){
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<Room>().generateSpecificRoomOnSide(Room.side.Down);
                        return;                    }

                    else{
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<Room>().setDoorsOn(Room.side.Down, gridMap[(int) coordinates.x + 1, (int) coordinates.y]);
                        return;
                    }
                }
                break;
        }
    } 

}
