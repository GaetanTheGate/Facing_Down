using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static void generateMap(){
        GameObject map = new GameObject("Map");
        map.transform.SetParent(GameObject.Find("GameManager").transform);
        map.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        map.GetComponent<Canvas>().transform.position = new Vector2(0,0);

        for(int i = 0; i < GenerateDonjon.nbRoomHeight; i += 1){
            for(int j = 0; j < GenerateDonjon.nbRoomWidth; j += 1){
                if (GenerateDonjon.gridMap[i, j] != null){
                    Room room = GenerateDonjon.gridMap[i, j];
                    GameObject imageGo = chooseMapIcon(room);
                    if(i == 0 && j == 0)
                        imageGo.GetComponent<Image>().color = Color.blue;
                    imageGo.transform.position = new Vector2(-700 + 25 * j,230 + 25 * -i);
                }
                
            }
        }
        
    }

    public static GameObject chooseMapIcon(Room room){

        GameObject imageGo = new GameObject("MapIcon" + room.name);
        imageGo.tag = "MapIcon"; 
        imageGo.AddComponent<Image>();
        imageGo.transform.SetParent(GameObject.Find("Map").transform);

        //renvoie le sprite originale pour les salles avec 1 seule portes
        if (room.doors.Count == 1)
            imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));

        //renvoie le sprite correspondant au nombre de roomBehind non null pour la Room2
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

            //4 portes non nulles
            if (hasRoomOnLeft && hasRoomOnRight && hasRoomOnDown && hasRoomOnUp)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));

            //1 porte non nulle
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

            //2 portes non nulles config 1
            else if (hasRoomOnLeft && !hasRoomOnUp && hasRoomOnRight && !hasRoomOnDown)
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_1");

            else if (!hasRoomOnLeft && hasRoomOnUp && !hasRoomOnRight && hasRoomOnDown){
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')) + "Door2_1");
                imageGo.transform.Rotate(0,0,-90f);
            }

            //2 portes non nulles config 2
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

            //3 portes non nulles
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
