using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenEnemy : MonoBehaviour
{
    public void SetGameObjectState(bool state)
    {
        gameObject.SetActive(state);
    }
}
