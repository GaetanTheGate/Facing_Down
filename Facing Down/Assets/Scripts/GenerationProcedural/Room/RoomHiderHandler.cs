using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHiderHandler : MonoBehaviour
{
    public void SetBlurState(bool state)
    {
        FindInChildren("Blur").SetActive(state);
    }

    public void SetDarknessState(bool state)
    {
        FindInChildren("Darkness").SetActive(state);
    }

    private GameObject FindInChildren(string name)
    {
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
            if (child.gameObject.name.Equals(name))
                return child.gameObject;

        return null;
    }
}
