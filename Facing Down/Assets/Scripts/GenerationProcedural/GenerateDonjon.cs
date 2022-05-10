
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDonjon : MonoBehaviour
{

    public static int nbRoomWidth = 5;
    public static int nbRoomHeight = 10; 

    public static int nbRoom = (nbRoomHeight * nbRoomWidth) / 3;
    public static int idRoom = 0;


    public static float probDown = 0.66f;
    private GameObject initRoom;
    

    public static List<GameObject> processRooms = new List<GameObject>();
    public static Dictionary<GameObject,List<RoomHandler.side>> validSideOfRoom = new Dictionary<GameObject, List<RoomHandler.side>>();
    
    
    public static GameObject[,] gridMap = new GameObject[nbRoomHeight, nbRoomWidth];

    private string gamePath = "Prefabs/Game/Game";
    private string UIPath = "Prefabs/UI/UI";
    public static string moldRoomPath = "Prefabs/Rooms/BaseRooms/BaseRoom"; 

    


    void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            initGenerate();
            generate();
        }
    }


    public void initGenerate(){
        GameObject ui = Resources.Load(UIPath, typeof(GameObject)) as GameObject;
        ui = Instantiate(ui);
        ui.name = "UI";

        GameObject gameManager = Resources.Load(gamePath, typeof(GameObject)) as GameObject;
        gameManager = Instantiate(gameManager);
        gameManager.name = "Game";

        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(gameManager.transform);

        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(ui);

        initRoom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        initRoom.name = initRoom.name.Substring(0,initRoom.name.IndexOf('(')) + '-' + idRoom++;
        initRoom.transform.SetParent(floor.transform);
        processRooms.Add(initRoom);
        validSideOfRoom.Add(initRoom,new List<RoomHandler.side>(){RoomHandler.side.Down,RoomHandler.side.Left,RoomHandler.side.Right});
        gridMap[0, nbRoomWidth / 2] = initRoom;


        GameObject anteroom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        anteroom.name = "Anteroom";
        anteroom.transform.SetParent(floor.transform);
        
        gridMap[nbRoomHeight - 3, nbRoomWidth / 2] = anteroom;

        anteroom.GetComponent<RoomHandler>().generateSpecificRoomOnSide(RoomHandler.side.Down,"BossRoom");

        initRoom.GetComponent<RoomHandler>().SetAsStart();
        
    }

    public void generate(){
        while (!checkIfRoomOnLineBeforeAnteroom() && nbRoom > 0){
            if(processRooms.Count == 0){
                print("plus de possibilité de générer une salle");
                break;
            }

            GameObject processRoom = processRooms[Game.random.Next(0,processRooms.Count)];

            double random = Game.random.NextDouble();
            RoomHandler.side side;
            if (random < probDown && validSideOfRoom[processRoom].Contains(RoomHandler.side.Down)){
                side = RoomHandler.side.Down;
            }

            else if(random < probDown && !validSideOfRoom[processRoom].Contains(RoomHandler.side.Down) && validSideOfRoom[processRoom].Count > 0){
                side = validSideOfRoom[processRoom][Game.random.Next(0,validSideOfRoom[processRoom].Count)];
            }

            else{
                List<RoomHandler.side> otherSide = new List<RoomHandler.side>();

                foreach(RoomHandler.side valideSide in validSideOfRoom[processRoom]){
                    if(valideSide != RoomHandler.side.Down)
                        otherSide.Add(valideSide);
                }

                if(otherSide.Count == 0)
                    side = RoomHandler.side.Down;
                else
                    side = otherSide[Game.random.Next(0,otherSide.Count)];
            }

            if(processRoom.GetComponent<RoomHandler>().generateRoomOnSide(side)){
                nbRoom -= 1;
            }
            
        }

        linkRoomToAnteroom();

        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if (gridMap[i,j] != null){

                    gridMap[i,j].transform.position = new Vector2(36 * j, 36 * -i);

                    gridMap[i,j].GetComponent<RoomHandler>().InitRoom("basic");

                    /*if(gridMap[i,j].name == initRoom.name)
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("spawn");
                    else if(gridMap[i,j].name == "Anteroom")
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("anteroom");
                    else if(gridMap[i,j].name == "BossRoom")
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("boss");
                    else
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("basic");*/
                }
            }
        }
        
        //Game.player.transform.position = initRoom.spawn;
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
                    RoomHandler room = gridMap[i,j].GetComponent<RoomHandler>();
                    if(!room.botDoor && !room.leftDoor && !room.rightDoor && room.topDoor){
                        if((j + 1 < nbRoomWidth && gridMap[i,j+1] != null))
                            room.setDoorsOn(RoomHandler.side.Right, gridMap[i,j+1]);

                        else if(j - 1 > 0 && gridMap[i,j-1] != null)
                            room.setDoorsOn(RoomHandler.side.Left, gridMap[i,j-1]);

                        else if(gridMap[i+1,j] != null)
                            room.setDoorsOn(RoomHandler.side.Down, gridMap[i+1,j]);
                    
                        else
                            room.generateSpecificRoomOnSide(RoomHandler.side.Down);
                    }

                    if(!room.botDoor && !room.leftDoor && room.rightDoor && !room.topDoor){
                        if(gridMap[i+1,j] != null)
                            room.setDoorsOn(RoomHandler.side.Down, gridMap[i+1,j]);

                        else if(j - 1 > 0 && gridMap[i,j-1] != null)
                            room.setDoorsOn(RoomHandler.side.Left, gridMap[i,j-1]);

                        else
                            room.generateSpecificRoomOnSide(RoomHandler.side.Down);
                    }

                    if(!room.botDoor && room.leftDoor && !room.rightDoor && !room.topDoor){
                        if(gridMap[i+1,j] != null)
                            room.setDoorsOn(RoomHandler.side.Down, gridMap[i+1,j]);

                        else if(j + 1 < nbRoomWidth && gridMap[i,j+1] != null)
                            room.setDoorsOn(RoomHandler.side.Right, gridMap[i,j+1]);

                        else
                            room.generateSpecificRoomOnSide(RoomHandler.side.Down);
                    }
                    if(!room.botDoor && room.leftDoor && !room.rightDoor && room.topDoor)
                        checkIfRoomIsLinkToRoomWithbotDoor(RoomHandler.side.Left,new Vector2(i,j));

                    if(!room.botDoor && !room.leftDoor && room.rightDoor && room.topDoor)
                        checkIfRoomIsLinkToRoomWithbotDoor(RoomHandler.side.Right, new Vector2(i,j));    
                }
            }
        }


        
        for(int i = 0; i < nbRoomWidth/2; i +=1){
            if(gridMap[nbRoomHeight-3, i] != null){
                RoomHandler room = gridMap[nbRoomHeight-3,i].GetComponent<RoomHandler>();
                if(gridMap[nbRoomHeight-3, i + 1] != null)
                    room.setDoorsOn(RoomHandler.side.Right, gridMap[nbRoomHeight-3,i + 1]);
                else
                    room.generateSpecificRoomOnSide(RoomHandler.side.Right);
            }
        }

        for(int i = nbRoomWidth - 1; i > nbRoomWidth/2; i -=1){
            if(gridMap[nbRoomHeight-3, i] != null){
                RoomHandler room = gridMap[nbRoomHeight-3,i].GetComponent<RoomHandler>();
                    if(gridMap[nbRoomHeight-3, i - 1] != null)
                        room.setDoorsOn(RoomHandler.side.Left, gridMap[nbRoomHeight-3,i - 1]);
                    else
                        room.generateSpecificRoomOnSide(RoomHandler.side.Left);
            }
        }
    }


    public void checkIfRoomIsLinkToRoomWithbotDoor(RoomHandler.side sideToGo, Vector2 coordinates){
        switch(sideToGo){
            case RoomHandler.side.Left:
                if(gridMap[(int) coordinates.x, (int) coordinates.y - 1].GetComponent<RoomHandler>().botDoor)
                    return;
                else if (gridMap[(int) coordinates.x, (int) coordinates.y -1].GetComponent<RoomHandler>().leftDoor)
                    checkIfRoomIsLinkToRoomWithbotDoor(RoomHandler.side.Left, new Vector2(coordinates.x, coordinates.y -1));
                else{
                    if(gridMap[(int) coordinates.x + 1, (int) coordinates.y] == null){
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<RoomHandler>().generateSpecificRoomOnSide(RoomHandler.side.Down);
                        return;                    }

                    else{
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<RoomHandler>().setDoorsOn(RoomHandler.side.Down, gridMap[(int) coordinates.x + 1, (int) coordinates.y]);
                        return;
                    }
                }
                break;
            
            case RoomHandler.side.Right:
                if(gridMap[(int) coordinates.x, (int) coordinates.y + 1].GetComponent<RoomHandler>().botDoor)
                    return;
                else if (gridMap[(int) coordinates.x, (int) coordinates.y +1].GetComponent<RoomHandler>().rightDoor)
                    checkIfRoomIsLinkToRoomWithbotDoor(RoomHandler.side.Right, new Vector2(coordinates.x, coordinates.y + 1));
                else{
                    if(gridMap[(int) coordinates.x + 1, (int) coordinates.y] == null){
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<RoomHandler>().generateSpecificRoomOnSide(RoomHandler.side.Down);
                        return;                    }

                    else{
                        gridMap[(int) coordinates.x, (int) coordinates.y].GetComponent<RoomHandler>().setDoorsOn(RoomHandler.side.Down, gridMap[(int) coordinates.x + 1, (int) coordinates.y]);
                        return;
                    }
                }
                break;
        }
    }
 

}
