using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
     public abstract void Init();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
