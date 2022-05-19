using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    private bool initialized = false;

    public void Init() {
        if (!initialized) {
            Initialize();
            initialized = true;
		}
	}
    protected abstract void Initialize();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
