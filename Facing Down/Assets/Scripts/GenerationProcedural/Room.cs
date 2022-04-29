using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.IO;

public class Room : MonoBehaviour
{
    public bool hasDoorOnRight;
    public bool hasDoorOnLeft;
    public bool hasDoorOnUp;
    public bool hasDoorOnDown;

    public List<Door> doors = new List<Door>();
    

}
