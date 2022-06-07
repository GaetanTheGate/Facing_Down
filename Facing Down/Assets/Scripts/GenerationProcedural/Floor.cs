using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pathfinding;

public class Floor : MonoBehaviour
{

    public static int nbRoomWidth = 7;
    public static int nbRoomHeight = 15; 

    public static int nbRoom;


    public static int nbTreasureRoom;
    public static int nbBonusRoom;
    public static float probaGenerateBonusRoom; 

    public static int idRoom;


    public static float probDown = 0.66f;
    private static GameObject initRoom;
    

    public static List<GameObject> processRooms;
    public static Dictionary<GameObject,List<RoomHandler.side>> validSideOfRoom;

    public static List<GameObject> processTreasureRooms;
    public static Dictionary<GameObject,RoomHandler.side> sideToGenerateTreasureRoom;
    
    
    public static GameObject[,] gridMap = new GameObject[nbRoomHeight, nbRoomWidth];
    public static string moldRoomPath = "Prefabs/Rooms/BaseRooms/BaseRoom"; 

    public static void generateFloor(){
        initGenerate();
        generate();
    }



    public static void initGenerate(){
        
        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(GameObject.Find("Game").transform);
        floor.AddComponent<AstarPath>();


        initRoom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        initRoom.name = initRoom.name.Substring(0,initRoom.name.IndexOf('(')) + '-' + idRoom++;
        initRoom.transform.SetParent(floor.transform);
        processRooms.Add(initRoom);
        validSideOfRoom.Add(initRoom,new List<RoomHandler.side>(){RoomHandler.side.Down,RoomHandler.side.Left,RoomHandler.side.Right});
        gridMap[0, nbRoomWidth / 2] = initRoom;


        
       
    }

    public static void generate(){
        while (nbRoom > 0){
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

        generateBossRoom();

        addTreasureRoom();

        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if (gridMap[i,j] != null){
                    gridMap[i,j].transform.position = new Vector2(36 * j, 36 * -i);

                    //gridMap[i,j].GetComponent<RoomHandler>().InitRoom("basic");

                    if(gridMap[i,j].name == initRoom.name){
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("spawn");}
                    else if(gridMap[i,j].name == "Anteroom")
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("antichamber");
                    else if(gridMap[i,j].name == "BossRoom")
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("boss");
                    else if (gridMap[i,j].name == "TreasureRoom"){
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("treasure");
                    }
                    else if(gridMap[i,j].name == "NextFloor")
                        gridMap[i,j].GetComponent<RoomHandler>().InitRoom("nextFloor");
                    else{
                        if(Game.random.NextDouble() < probaGenerateBonusRoom){
                            nbBonusRoom += 1;
                            probaGenerateBonusRoom = Mathf.Pow((float) nbTreasureRoom/(nbTreasureRoom+2),nbBonusRoom);
                            gridMap[i,j].name = "BonusRoom" + '-' + idRoom++;
                            gridMap[i,j].GetComponent<RoomHandler>().InitRoom("treasure");
                        }
                        else
                            gridMap[i,j].GetComponent<RoomHandler>().InitRoom("basic");
                    }
                        
                    addGraphPathfinding(i,j);
                }
            }
        }

        Game.coroutineStarter.LaunchCoroutine(WaitForAstarScan());
        initRoom.GetComponent<RoomHandler>().SetAsStart();
        Game.player.self.transform.position = initRoom.GetComponent<RoomHandler>().transform.position;
    }

    private static IEnumerator WaitForAstarScan()
    {
        yield return new WaitForFixedUpdate();
        AstarPath.active.Scan();
    }

    public static void generateBossRoom(){
        int anteroomLine = 0;
        for(int i = 0; i < nbRoomHeight; i += 1){
            bool isRoomOnLine = false;
            for(int j = 0; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null){
                    isRoomOnLine = true;
                    break;
                }
                    
            }
            if(!isRoomOnLine){
                anteroomLine = i;
                break;
            }
        }


        GameObject anteroom = Instantiate(Resources.Load(moldRoomPath, typeof(GameObject)) as GameObject);
        anteroom.name = "Anteroom";
        anteroom.transform.SetParent(GameObject.Find("Floor").transform);
        
        gridMap[anteroomLine, nbRoomWidth / 2] = anteroom;

        anteroom.GetComponent<RoomHandler>().generateSpecificRoomOnSide(RoomHandler.side.Down,"BossRoom");
        gridMap[anteroomLine + 1, nbRoomWidth / 2].GetComponent<RoomHandler>().generateSpecificRoomOnSide(RoomHandler.side.Down,"NextFloor");

        linkRoomToAnteroom(anteroomLine);

    } 

    public static void linkRoomToAnteroom(int anteroomLine){
        for(int i = 0; i < anteroomLine; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null){
                    RoomHandler room = gridMap[i,j].GetComponent<RoomHandler>();
                    if(!room.botDoor && !room.leftDoor && !room.rightDoor && room.topDoor){
                        if((j + 1 < nbRoomWidth && gridMap[i,j+1] != null) && gridMap[i,j+1].name != "TreasureRoom")
                            room.setDoorsOn(RoomHandler.side.Right, gridMap[i,j+1]);

                        else if(j - 1 > 0 && gridMap[i,j-1] != null && gridMap[i,j-1].name != "TreasureRoom")
                            room.setDoorsOn(RoomHandler.side.Left, gridMap[i,j-1]);

                        else if(gridMap[i+1,j] != null)
                            room.setDoorsOn(RoomHandler.side.Down, gridMap[i+1,j]);
                    
                        else
                            room.generateSpecificRoomOnSide(RoomHandler.side.Down);
                    }

                    if(!room.botDoor && !room.leftDoor && room.rightDoor && !room.topDoor)
                        checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Right, new Vector2(i,j));

                    if(!room.botDoor && room.leftDoor && !room.rightDoor && !room.topDoor)
                        checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Left,new Vector2(i,j));
                        
                    if(!room.botDoor && room.leftDoor && !room.rightDoor && room.topDoor)
                        checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Left,new Vector2(i,j));

                    if(!room.botDoor && !room.leftDoor && room.rightDoor && room.topDoor)
                        checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Right, new Vector2(i,j));    
                }
            }
        }


        
        for(int i = 0; i < nbRoomWidth/2; i +=1){
            if(gridMap[anteroomLine, i] != null){
                RoomHandler room = gridMap[anteroomLine,i].GetComponent<RoomHandler>();
                if(gridMap[anteroomLine, i + 1] != null)
                    room.setDoorsOn(RoomHandler.side.Right, gridMap[anteroomLine,i + 1]);
                else
                    room.generateSpecificRoomOnSide(RoomHandler.side.Right);
            }
        }

        for(int i = nbRoomWidth - 1; i > nbRoomWidth/2; i -=1){
            if(gridMap[anteroomLine, i] != null){
                RoomHandler room = gridMap[anteroomLine,i].GetComponent<RoomHandler>();
                    if(gridMap[anteroomLine, i - 1] != null)
                        room.setDoorsOn(RoomHandler.side.Left, gridMap[anteroomLine,i - 1]);
                    else
                        room.generateSpecificRoomOnSide(RoomHandler.side.Left);
            }
        }
    }


    public static void checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side sideToGo, Vector2 coordinates){
        switch(sideToGo){
            case RoomHandler.side.Left:
                if(gridMap[(int) coordinates.x, (int) coordinates.y - 1].GetComponent<RoomHandler>().botDoor)
                    return;
                else if (gridMap[(int) coordinates.x, (int) coordinates.y -1].GetComponent<RoomHandler>().leftDoor)
                    checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Left, new Vector2(coordinates.x, coordinates.y -1));
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
                    checkIfRoomIsLinkToRoomWithBotDoor(RoomHandler.side.Right, new Vector2(coordinates.x, coordinates.y + 1));
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

    public static void addGraphPathfinding(int i, int j){

        // This holds all graph data
        AstarData data = AstarPath.active.data;

        // This creates a Grid Graph
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        // Setup a grid graph with some values
        int width = 80;
        int depth = 80;
        float nodeSize = 0.4f;

        gg.center = new Vector3(36 * j, 36 * -i, 0);

        // Updates internal size from the above values
        gg.SetDimensions(width, depth, nodeSize);

        gg.is2D = true;
        gg.collision.use2D = true;
        gg.collision.mask = removeAllLayerInLayerMask(gg.collision.mask);
        gg.collision.mask = addLayerToLayerMask(gg.collision.mask, LayerMask.NameToLayer("Terrain"));
    }

    public static KeyValuePair<List<GameObject>,Dictionary<GameObject,List<RoomHandler.side>>> getSpotTreasureRoom(){
        List<GameObject> potentialRoomToGenerateTreasureRoom = new List<GameObject>();
        Dictionary<GameObject,List<RoomHandler.side>> sideToGenerateTreasureRoom = new Dictionary<GameObject, List<RoomHandler.side>>();

        for(int i = 0; i < nbRoomHeight; i += 1){
            for(int j = 0; j < nbRoomWidth; j += 1){
                if(gridMap[i,j] != null && gridMap[i,j].name != "BossRoom" && gridMap[i,j].name != "NextFloor"){

                    List<RoomHandler.side> validSides = new List<RoomHandler.side>();
                    bool canAdd = false;

                    if(j + 1 < nbRoomWidth && gridMap[i, j+1] == null){
                        canAdd = true;
                        validSides.Add(RoomHandler.side.Right);
                    }

                    if(j - 1 >= 0 && gridMap[i, j-1] == null){
                        canAdd = true;
                        validSides.Add(RoomHandler.side.Left);
                    }

                    if(canAdd){
                        potentialRoomToGenerateTreasureRoom.Add(gridMap[i,j]);
                        sideToGenerateTreasureRoom.Add(gridMap[i,j],validSides);
                    }
                }
            }
        }

        return new KeyValuePair<List<GameObject>, Dictionary<GameObject, List<RoomHandler.side>>>(potentialRoomToGenerateTreasureRoom,sideToGenerateTreasureRoom);
    }

    public static void addTreasureRoom(){
        KeyValuePair<List<GameObject>,Dictionary<GameObject,List<RoomHandler.side>>> valuePair = getSpotTreasureRoom();
        List<GameObject> potentialRoomToGenerateTreasureRoom = valuePair.Key;
        Dictionary<GameObject,List<RoomHandler.side>> sideToGenerateTreasureRoom = valuePair.Value;

        int toIndex = (potentialRoomToGenerateTreasureRoom.Count < nbTreasureRoom ? potentialRoomToGenerateTreasureRoom.Count : nbTreasureRoom);
        for(int i = 0; i < toIndex; i += 1){
            GameObject processRoomToGenerateTreasureRoom = potentialRoomToGenerateTreasureRoom[Game.random.Next(0,potentialRoomToGenerateTreasureRoom.Count)];
            List<RoomHandler.side> sides = sideToGenerateTreasureRoom[processRoomToGenerateTreasureRoom];

            RoomHandler.side side = sides[Game.random.Next(0,sides.Count)];

            Vector2 coordinates = RoomHandler.getCoordinates(processRoomToGenerateTreasureRoom);

            if ((side == RoomHandler.side.Right && gridMap[(int) coordinates.x,(int) coordinates.y + 1] == null) || (side == RoomHandler.side.Left && gridMap[(int) coordinates.x,(int) coordinates.y - 1] == null))
                processRoomToGenerateTreasureRoom.GetComponent<RoomHandler>().generateSpecificRoomOnSide(side ,"TreasureRoom");
            sides.Remove(side);
            if(sides.Count == 0)
                potentialRoomToGenerateTreasureRoom.Remove(processRoomToGenerateTreasureRoom);
            else
                sideToGenerateTreasureRoom[processRoomToGenerateTreasureRoom] = sides;

        }
    }


 
    public static void destroyFloor(){
        Destroy(GameObject.Find("Floor"));
        UI.map.ResetMapDisplay();
    }

    public static void resetVar(){
        nbRoom = 15;
        idRoom = 0;
        nbTreasureRoom = nbRoom / 3;
        processRooms = new List<GameObject>();
        validSideOfRoom = new Dictionary<GameObject, List<RoomHandler.side>>();
        processTreasureRooms = new List<GameObject>();
        gridMap = new GameObject[nbRoomHeight, nbRoomWidth];
        initRoom = null;
        nbBonusRoom = 1;
        probaGenerateBonusRoom = Mathf.Pow((float) nbTreasureRoom/(nbTreasureRoom+2),nbBonusRoom);
        sideToGenerateTreasureRoom = new Dictionary<GameObject, RoomHandler.side>();
    }

    


    public static LayerMask removeAllLayerInLayerMask(LayerMask layerMask){
        for (int i = 0; i < 32; i++){
            if (layerMask == (layerMask | (1 << i)))
                layerMask |= ~(1 << i);
        }
        return layerMask;
    }

    public static LayerMask addLayerToLayerMask(LayerMask layerMask, int layerToAdd){
        layerMask |= (1 << layerToAdd);
        return layerMask;
    }

}
