using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBinding 
{
    public string action;
    public KeyCode key;

    public KeyBinding() { }
    public KeyBinding(string action, KeyCode key) {
        this.action = action;
        this.key = key;
	}
}
