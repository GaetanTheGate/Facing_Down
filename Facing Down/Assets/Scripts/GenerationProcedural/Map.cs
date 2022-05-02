using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static void generateMap(){
        GameObject map = new GameObject("Map");
        map.transform.SetParent(GameObject.Find("Game").transform);
        map.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        map.GetComponent<Canvas>().transform.position = new Vector2(0,0);

        for(int i = 0; i < GenerateDonjon.nbRoomHeight; i += 1){
            for(int j = 0; j < GenerateDonjon.nbRoomWidth; j += 1){
                if (GenerateDonjon.gridMap[i, j] != null){
                    Room room = GenerateDonjon.gridMap[i, j];
                    GameObject imageGo = chooseMapIcon(room);
                    imageGo.transform.position = new Vector2(-650 + 25 * j,250 + 25 * -i);
                }
                
            }
        }
        
    }

    public static GameObject chooseMapIcon(Room room){

        GameObject imageGo = new GameObject("MapIcon" + room.name);
        imageGo.tag = "MapIcon"; 
        imageGo.AddComponent<Image>();
        imageGo.transform.SetParent(GameObject.Find("Map").transform);

        //get initial sprite from room which have 1 doors or from anteroom
        if (room.doors.Count == 1 || room.name.Contains("Anteroom"))
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));

        //get sprite corresponding to number of door not null in Room2
        if (room.name.Contains("Room2")){
            bool hasRoomOnRight = false;
            bool hasRoomOnLeft = false;

            foreach(Door door in room.doors){
                if (door.roomBehind != null){
                    switch(door.onSide){
                        case Door.side.Right:
                            hasRoomOnRight = true;
                            break;
                        case Door.side.Left :
                            hasRoomOnLeft = true;
                            break;
                    }
                }
            }

            if(hasRoomOnLeft && hasRoomOnRight)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));

            else if(hasRoomOnLeft)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");

            else{
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");
                imageGo.transform.Rotate(0,0,180f);
            }

        }

        //get sprite corresponding to number of door not null and configuration of these doors in Room5
        if (room.name.Contains("Room5")){
            bool hasRoomOnRight = false;
            bool hasRoomOnLeft = false;
            bool hasRoomOnDown = false;
            bool hasRoomOnUp = false;

            foreach(Door door in room.doors){
                if (door.roomBehind != null){
                    switch(door.onSide){
                        case Door.side.Right:
                            hasRoomOnRight = true;
                            break;
                        case Door.side.Left :
                            hasRoomOnLeft = true;
                            break;
                        case Door.side.Down:
                            hasRoomOnDown = true;
                            break;
                        case Door.side.Up :
                            hasRoomOnUp = true;
                            break;
                    }
                }
            }
            
            //4 doors not null
            if (hasRoomOnLeft && hasRoomOnRight && hasRoomOnDown && hasRoomOnUp)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));

            //1 door not null
            else if (hasRoomOnLeft && !hasRoomOnUp && !hasRoomOnRight && !hasRoomOnDown)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");
            
            else if (hasRoomOnUp && !hasRoomOnRight && !hasRoomOnDown && !hasRoomOnLeft){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");
                imageGo.transform.Rotate(0,0,-90f);
            }

            else if(hasRoomOnRight && !hasRoomOnDown && !hasRoomOnLeft && !hasRoomOnUp){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");
                imageGo.transform.Rotate(0,0,180f);
            }

            else if(hasRoomOnDown && !hasRoomOnLeft && !hasRoomOnUp && !hasRoomOnRight){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door1");
                imageGo.transform.Rotate(0,0,90f);
            }

            //2 doors not null config 1 (2 doors on the opposite side)

            else if (hasRoomOnLeft && !hasRoomOnUp && hasRoomOnRight && !hasRoomOnDown)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_1");

            else if (!hasRoomOnLeft && hasRoomOnUp && !hasRoomOnRight && hasRoomOnDown){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_1");
                imageGo.transform.Rotate(0,0,-90f);
            }
            
            //2 doors not null config 2 (2 doors which follow each other)
            else if (hasRoomOnLeft && hasRoomOnUp && !hasRoomOnRight && !hasRoomOnDown)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_2");

            else if (hasRoomOnUp && hasRoomOnRight && !hasRoomOnDown && !hasRoomOnLeft){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_2");
                imageGo.transform.Rotate(0,0,-90f);
            }

            else if (hasRoomOnRight && hasRoomOnDown && !hasRoomOnLeft && !hasRoomOnUp){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_2");
                imageGo.transform.Rotate(0,0,180f);
            }

            else if (hasRoomOnDown && hasRoomOnLeft && !hasRoomOnUp && !hasRoomOnRight){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_2");
                imageGo.transform.Rotate(0,0,90f);
            }

            //3 doors not null
            else if (hasRoomOnLeft && hasRoomOnUp && hasRoomOnRight && !hasRoomOnDown)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door3");
            
            else if (hasRoomOnUp && hasRoomOnRight && hasRoomOnDown && !hasRoomOnLeft){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door3");
                imageGo.transform.Rotate(0,0,-90f);
            }

            else if (hasRoomOnRight && hasRoomOnDown && hasRoomOnLeft && !hasRoomOnUp){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door3");
                imageGo.transform.Rotate(0,0,180f);
            }

            else{
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door3");
                imageGo.transform.Rotate(0,0,90f);
            }
        }

        imageGo.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(25,25);
        return imageGo;
    }

}
