using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Close(bool state)
    {
        GetComponentInChildren<DoorTrigger>(true).gameObject.SetActive(!state);
        GetComponentInChildren<Block>(true).gameObject.SetActive(state);
    }
}
