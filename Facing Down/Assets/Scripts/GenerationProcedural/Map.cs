using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    private static string mapIconPath = "IconMap/moldRoom";

    private static string oneDoor = "1Door";
    private static string twoDoorsConfig1 = "2Doors1";
    private static string twoDoorsConfig2 = "2Doors2";
    private static string threeDoors = "3Doors";
    private static string fourDoors = "4Doors";

    private static int sizeImage = 50;



    public static void generateMap(){
        GameObject map = UI.map;
    
        
        for(int i = 0; i < GenerateDonjon.nbRoomHeight; i += 1){
            for(int j = 0; j < GenerateDonjon.nbRoomWidth; j += 1){
                if (GenerateDonjon.gridMap[i, j] != null){
                    GameObject mapIcon = chooseMapIcon(GenerateDonjon.gridMap[i, j].GetComponent<Room>());
                    mapIcon.transform.localPosition = new Vector2(sizeImage * j, sizeImage * -i);
                    if(GenerateDonjon.gridMap[i, j].name == Game.currentRoom.name)
                        mapIcon.GetComponent<Image>().color = Color.blue;
                    if(GenerateDonjon.gridMap[i, j].name == "BossRoom")
                        mapIcon.GetComponent<Image>().color = Color.red;
                }
            }
        }
        
    }
  
    public static GameObject chooseMapIcon(Room room){

        GameObject imageGo = new GameObject("MapIcon" + room.name);
        imageGo.tag = "MapIcon"; 
        imageGo.AddComponent<Image>();
        imageGo.transform.SetParent(GameObject.Find("Map").transform);

        //4 doors not null
        if (room.doorDown && room.doorUp && room.doorLeft && room.doorRight)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + fourDoors);

        //1 door not null
        else if(room.doorLeft && !room.doorUp && !room.doorRight && !room.doorDown)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);

        else if(!room.doorLeft && room.doorUp && !room.doorRight && !room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(!room.doorLeft && !room.doorUp && room.doorRight && !room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(!room.doorLeft && !room.doorUp && !room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + oneDoor);
            imageGo.transform.Rotate(0,0,90f);
        }

        //2 doors not null config 1 (2 doors on the opposite side)
        else if(room.doorLeft && !room.doorUp && room.doorRight && !room.doorDown)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig1);

        else if(!room.doorLeft && room.doorUp && !room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig1);
            imageGo.transform.Rotate(0,0,90f);
        }

        //2 doors not null config 2 (2 doors which follow each other)
        else if(room.doorLeft && room.doorUp && !room.doorRight && !room.doorDown)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);

        else if(!room.doorLeft && room.doorUp && room.doorRight && !room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(!room.doorLeft && !room.doorUp && room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(room.doorLeft && !room.doorUp && !room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + twoDoorsConfig2);
            imageGo.transform.Rotate(0,0,90f);
        }

        //3 doors not null
        else if(room.doorLeft && room.doorUp && room.doorRight && !room.doorDown)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);   
        
        else if(!room.doorLeft && room.doorUp && room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,-90f);
        }

        else if(room.doorLeft && !room.doorUp && room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,180f);
        }

        else if(room.doorLeft && room.doorUp && !room.doorRight && room.doorDown){
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>(mapIconPath + threeDoors);
            imageGo.transform.Rotate(0,0,90f);
        }

        imageGo.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(sizeImage,sizeImage);
        return imageGo;
    }

    public static void changeColorMapicon(GameObject roomToHide, GameObject roomToDisplay){
        GameObject.Find("MapIcon"+roomToHide.name).GetComponent<Image>().color = Color.white;
        GameObject.Find("MapIcon"+roomToDisplay.name).GetComponent<Image>().color = Color.blue;
    }

}
