using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Options
{
    public string langue;

    public float volumeValue;

    public List<KeyBinding> commands = new List<KeyBinding>();

    public Dictionary<string,KeyCode> commandsToDictionary(){
        Dictionary<string,KeyCode> dicCommand = new Dictionary<string, KeyCode>();
        foreach(KeyBinding keyBinding in commands){
            dicCommand.Add(keyBinding.action, keyBinding.key);
        }
        return dicCommand;
    }

    
}
