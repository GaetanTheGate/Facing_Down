using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    public void SetLightsState(bool state)
    {
        gameObject.SetActive(state);
    }
}
