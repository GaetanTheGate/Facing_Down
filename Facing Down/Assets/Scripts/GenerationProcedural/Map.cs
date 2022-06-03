using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    private static string mapIconPath = "Sprites/IconMap/moldRoom";

    private static string oneDoor = "1Door";
    private static string twoDoorsConfig1 = "2Doors1";
    private static string twoDoorsConfig2 = "2Doors2";
    private static string threeDoors = "3Doors";
    private static string fourDoors = "4Doors";

    private static int sizeImage = 50;



    public static void generateMap(){
        for(int i = 0; i < Floor.nbRoomHeight; i += 1){
            for(int j = 0; j < Floor.nbRoomWidth; j += 1){
                if (Floor.gridMap[i, j] != null){
                    GameObject mapIcon = chooseMapIcon(Floor.gridMap[i, j].GetComponent<RoomHandler>());
                    mapIcon.transform.localPosition = new Vector2(sizeImage * j, sizeImage * -i);
                    if(Floor.gridMap[i, j].name == Game.currentRoom.name)
                        mapIcon.GetComponent<Image>().color = Color.blue;
                    if(Floor.gridMap[i, j].name == "BossRoom")
                        mapIcon.GetComponent<Image>().color = Color.red;
                    if(Floor.gridMap[i, j].name == "TreasureRoom")
                        mapIcon.GetComponent<Image>().color = Color.green;
                    if(Floor.gridMap[i, j].name.Contains("BonusRoom"))
                        mapIcon.GetComponent<Image>().color = Color.yellow;
                }
            }
        }
        
    }
  
    public static GameObject chooseMapIcon(RoomHandler room){

        GameObject imageGo = new GameObject("MapIcon" + room.name);
        imageGo.tag = "MapIcon"; 
        imageGo.AddComponent<Image>();
        UI.map.AddRoomDisplay(imageGo);

        //4 doors not null
        if (room.botDoor && room.topDoor && room.leftDoor && room.rightDoor)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + fourDoors);

        //1 door not null
        else if(room.leftDoor && !room.topDoor && !room.rightDoor && !room.botDoor)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);

        else if(!room.leftDoor && room.topDoor && !room.rightDoor && !room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(!room.leftDoor && !room.topDoor && room.rightDoor && !room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(!room.leftDoor && !room.topDoor && !room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,90f);
        }

        //2 doors not null config 1 (2 doors on the opposite side)
        else if(room.leftDoor && !room.topDoor && room.rightDoor && !room.botDoor)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig1);

        else if(!room.leftDoor && room.topDoor && !room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig1);
            imageGo.transform.Rotate(0,0,90f);
        }

        //2 doors not null config 2 (2 doors which follow each other)
        else if(room.leftDoor && room.topDoor && !room.rightDoor && !room.botDoor)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);

        else if(!room.leftDoor && room.topDoor && room.rightDoor && !room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(!room.leftDoor && !room.topDoor && room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(room.leftDoor && !room.topDoor && !room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,90f);
        }

        //3 doors not null
        else if(room.leftDoor && room.topDoor && room.rightDoor && !room.botDoor)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);   
        
        else if(!room.leftDoor && room.topDoor && room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(room.leftDoor && !room.topDoor && room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(room.leftDoor && room.topDoor && !room.rightDoor && room.botDoor){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,90f);
        }

        imageGo.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(sizeImage,sizeImage);
        return imageGo;
    }

    public static void changeColorMapicon(GameObject roomToHide, GameObject roomToDisplay){
        onColorMapicon(roomToDisplay);
        offColorMapicon(roomToHide);
    }

    public static void onColorMapicon(GameObject roomToDisplay)
    {
        foreach (Object o in GameObject.FindObjectsOfType(typeof(GameObject), true))
        {
            if ((((GameObject)o).CompareTag("MapIcon") && o.name == "MapIcon" + roomToDisplay.name))
                ((GameObject)o).GetComponent<Image>().color = Color.blue;
        }
    }

    public static void offColorMapicon(GameObject roomToHide)
    {
        foreach (Object o in GameObject.FindObjectsOfType(typeof(GameObject), true))
        {
            if ((((GameObject)o).CompareTag("MapIcon") && o.name == "MapIcon" + roomToHide.name))
                ((GameObject)o).GetComponent<Image>().color = Color.white;
        }
    }

}
