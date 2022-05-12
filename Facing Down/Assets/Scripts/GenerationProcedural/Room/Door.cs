using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Close(bool state)
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if(child.GetComponent<DoorTrigger>() == null)
                child.gameObject.SetActive(state);
            else
                child.gameObject.SetActive( ! state);
        }
    }
}
