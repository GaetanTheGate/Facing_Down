using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static void generateMap(Room room){
        GameObject map = new GameObject("Map");
        map.transform.SetParent(GameObject.Find("GameManager").transform);
        map.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        map.GetComponent<Canvas>().transform.position = new Vector2(0,0);

        GameObject imageGo = new GameObject("MapIcon" + room.name);
        imageGo.tag = "MapIcon"; 
        imageGo.AddComponent<Image>();
        imageGo.transform.SetParent(GameObject.Find("Map").transform);
        imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + room.name.Substring(0,room.name.IndexOf('-')));
        imageGo.GetComponent<Image>().color = Color.blue;
        imageGo.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50,50);

        generateIconMap(room, null, new Vector2(0,0));
        
    }


    public static void generateIconMap(Room processRoom, Room lastRoom, Vector2 coordinates){

        foreach(Door door in processRoom.doors){
            if (door.roomBehind != lastRoom && door.roomBehind != null){
                GameObject imageGo = new GameObject("MapIcon" + door.roomBehind.name);
                imageGo.tag = "MapIcon";  
                imageGo.AddComponent<Image>();
                imageGo.transform.SetParent(GameObject.Find("Map").transform);
                imageGo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Donjon/IconMap/" + door.roomBehind.name.Substring(0,door.roomBehind.name.IndexOf('-')));
                imageGo.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50,50);

                Vector2 newCoordinates;

                switch(door.onSide){
                    case Door.side.Right :
                        newCoordinates = new Vector2(coordinates.x + imageGo.GetComponent<Image>().rectTransform.sizeDelta.x, coordinates.y);
                        imageGo.transform.position = newCoordinates;
                        generateIconMap(door.roomBehind, door.currentRoom, newCoordinates);
                        break;
                    case Door.side.Left :
                        newCoordinates = new Vector2(coordinates.x - imageGo.GetComponent<Image>().rectTransform.sizeDelta.x, coordinates.y);
                        imageGo.transform.position = newCoordinates;
                        generateIconMap(door.roomBehind, door.currentRoom, newCoordinates);
                        break;
                    case Door.side.Down :
                        newCoordinates = new Vector2(coordinates.x, coordinates.y - imageGo.GetComponent<Image>().rectTransform.sizeDelta.y);
                        imageGo.transform.position = newCoordinates;
                        generateIconMap(door.roomBehind, door.currentRoom, newCoordinates);
                        break;
                    case Door.side.Up :
                        newCoordinates = new Vector2(coordinates.x, coordinates.y + imageGo.GetComponent<Image>().rectTransform.sizeDelta.y);
                        imageGo.transform.position = newCoordinates;
                        generateIconMap(door.roomBehind, door.currentRoom, newCoordinates);
                        break;
                    default :
                        print("unknown side");
                        break;

                }
            }
        }

    }

}
