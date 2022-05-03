using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.IO;

public class Room : MonoBehaviour
{
    public bool doorRight = false;
    public bool doorLeft  = false;
    public bool doorUp  = false;
    public bool doorDown  = false;

    public enum side{
        Right,
        Left,
        Up, 
        Down
    }

    public GameObject infoRoom;

    public bool generateRoomOnSide(side onSide){

        Vector2 coordinates = getCoordinates(gameObject);

        bool canGenerate = true;
        switch(onSide){
            case side.Right :
                if(coordinates.y + 1 > GenerateDonjon.nbRoomWidth - 1 || GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y + 1] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Left :
                if(coordinates.y - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x, (int) coordinates.y - 1] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break;

            case side.Down :
                if(coordinates.x + 1 > GenerateDonjon.nbRoomHeight - 1 || GenerateDonjon.gridMap[(int) coordinates.x + 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }
                break; 

            case side.Up :
                if(coordinates.x - 1 < 0 || GenerateDonjon.gridMap[(int) coordinates.x - 1 ,(int) coordinates.y] != null){
                    canGenerate = false;
                    GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide);
                }                
                break; 
        }

        if (GenerateDonjon.validSideOfRoom[gameObject].Count == 0){
            GenerateDonjon.processRooms.Remove(gameObject);
        } 

        if(canGenerate){
            print("génération");
            GameObject newMoldRoom = instantiateNewMoldRoom();

            if (getCoordinates(newMoldRoom).x == -1){
                addRoomToGridMap(newMoldRoom,coordinates,onSide);
                GenerateDonjon.processRooms.Add(newMoldRoom);
                GenerateDonjon.validSideOfRoom.Add(newMoldRoom,new List<side>(){side.Right,side.Left,side.Up});
                GenerateDonjon.validSideOfRoom[newMoldRoom].Remove(onSide);
            }
            setDoorsOn(onSide, newMoldRoom);
            GenerateDonjon.validSideOfRoom[gameObject].Remove(onSide); 
        }
        else{
            print("génération impossible");
        }
        return canGenerate;
            
    }

    public void generateSpecificRoomOnSide(side side, GameObject room){
        GameObject newMoldRoom = instantiateNewMoldRoom();
        newMoldRoom.GetComponent<Room>().infoRoom = room;
        addRoomToGridMap(newMoldRoom, getCoordinates(gameObject), side);
        setDoorsOn(side,newMoldRoom);
        GenerateDonjon.validSideOfRoom[gameObject].Remove(side);
    }

    public static Vector2 getCoordinates(GameObject moldRoom){
        Vector2 coordinates = new Vector2(-1,-1);
        for(int i = 0 ; i < GenerateDonjon.nbRoomHeight ; i += 1){
            for(int j = 0 ; j < GenerateDonjon.nbRoomWidth ; j += 1){
                if (GenerateDonjon.gridMap[i,j] != null && GenerateDonjon.gridMap[i,j].name == moldRoom.name){
                    coordinates.x = i;
                    coordinates.y = j;
                }
            }
        }
        return coordinates;
    }

    public GameObject instantiateNewMoldRoom(){
        GameObject newMoldRoom = Instantiate(Resources.Load(GenerateDonjon.moldRoomPath, typeof(GameObject)) as GameObject);
        newMoldRoom.SetActive(false);
        newMoldRoom.name = newMoldRoom.name.Substring(0,newMoldRoom.name.IndexOf('(')) + '-' + GenerateDonjon.idRoom++;
        newMoldRoom.transform.SetParent(GameObject.Find("Game").transform);

        return newMoldRoom;  
    }

    public static void addRoomToGridMap(GameObject moldRoomToAdd, Vector2 coordinates , side side){
        switch(side){
            case side.Right :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y + 1] = moldRoomToAdd;
                break;
            case side.Left :
                GenerateDonjon.gridMap[(int) coordinates.x , (int) coordinates.y - 1] = moldRoomToAdd;                
                break;
            case side.Down : 
                GenerateDonjon.gridMap[(int) coordinates.x + 1, (int) coordinates.y] = moldRoomToAdd;
                break;
            case side.Up :
                GenerateDonjon.gridMap[(int) coordinates.x - 1, (int) coordinates.y] = moldRoomToAdd;
                break;
        }
    }

    public void setDoorsOn(side onSide, GameObject newMoldRoom){
        switch(onSide){
            case side.Right :
                doorRight = true;
                newMoldRoom.GetComponent<Room>().doorLeft = true;
                break;
            case side.Left :
                doorLeft = true;
                newMoldRoom.GetComponent<Room>().doorRight = true;
                break;
            case side.Down :
                doorDown = true;
                newMoldRoom.GetComponent<Room>().doorUp = true;
                break;
            case side.Up :
                doorUp = true;
                newMoldRoom.GetComponent<Room>().doorDown = true;
                break;
        }
    }


}
