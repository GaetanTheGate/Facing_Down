using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Options
{
    public static string fullPath = Application.persistentDataPath + "/options.json";
    private static Options options;

    [System.NonSerialized]
    public Dictionary<string,KeyCode> dicoCommand;

    public string langue;
    public float volumeValue;
    public List<KeyBinding> commands = new List<KeyBinding>();

    private Options() {
        langue = "En";
        volumeValue = 100f;
        commands = new List<KeyBinding>();
        commands.Add(new KeyBinding("dash", KeyCode.Mouse0));
        commands.Add(new KeyBinding("attack", KeyCode.Mouse1));
        commands.Add(new KeyBinding("bulletTime", KeyCode.Space));
        commands.Add(new KeyBinding("openConsole", KeyCode.C));
        commands.Add(new KeyBinding("openInventoryMap", KeyCode.E));
        commands.Add(new KeyBinding("closeUI", KeyCode.Escape));

        dicoCommand = commandsToDictionary();
    }

    public static Options Get() {
        if (options == null) {
            if (File.Exists(fullPath)) options = JsonUtility.FromJson<Options>(File.ReadAllText(fullPath));
            else {
                options = new Options();
                Save();
			}
        }
        return options;
    }

    public static void Save() {
        Options.Get().dicoCommand = Options.Get().commandsToDictionary();
        string jsonStringOptions = JsonUtility.ToJson(Options.Get());

        File.WriteAllText(Options.fullPath, jsonStringOptions);
    }

    public Dictionary<string,KeyCode> commandsToDictionary(){
        Dictionary<string,KeyCode> dicCommand = new Dictionary<string, KeyCode>();
        foreach(KeyBinding keyBinding in commands){
            dicCommand.Add(keyBinding.action, keyBinding.key);
        }
        return dicCommand;
    }
}
