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
    public Dictionary<string,KeyCode> dicoCommandsKeyBoard = new Dictionary<string, KeyCode>();
    [System.NonSerialized]
    public Dictionary<string,KeyCode> dicoCommandsController = new Dictionary<string, KeyCode>();

    public string langue;
    public float masterVolumeValue;
    public float musicVolumeValue;
    public float soundVolumeValue;


    public List<KeyBinding> commandsKeyBoard = new List<KeyBinding>();
    public List<KeyBinding> commandsController = new List<KeyBinding>();

    public void getDefaultOption() {
        langue = "En";
        masterVolumeValue = 100f;
        musicVolumeValue = 100f;
        soundVolumeValue = 100f;

        commandsKeyBoard = new List<KeyBinding>();
        commandsKeyBoard.Add(new KeyBinding("dash", KeyCode.Mouse0));
        commandsKeyBoard.Add(new KeyBinding("attack", KeyCode.Mouse1));
        commandsKeyBoard.Add(new KeyBinding("bulletTime", KeyCode.Space));
        commandsKeyBoard.Add(new KeyBinding("openConsole", KeyCode.C));
        commandsKeyBoard.Add(new KeyBinding("openInventoryMap", KeyCode.E));
        commandsKeyBoard.Add(new KeyBinding("closeUI", KeyCode.Escape));

        commandsController = new List<KeyBinding>();
        commandsController.Add(new KeyBinding("dash", KeyCode.A));
        commandsController.Add(new KeyBinding("attack", KeyCode.Z));
        commandsController.Add(new KeyBinding("bulletTime", KeyCode.E));
        commandsController.Add(new KeyBinding("openConsole", KeyCode.R));
        commandsController.Add(new KeyBinding("openInventoryMap", KeyCode.T));
        commandsController.Add(new KeyBinding("closeUI", KeyCode.Y));    

        dicoCommandsKeyBoard = commandsKeyBoardToDictionary();
        dicoCommandsController = commandsControllerToDictionary();
    }

    public static Options Get() {
        if (options == null) {
            if (File.Exists(fullPath)){
                options = JsonUtility.FromJson<Options>(File.ReadAllText(fullPath));
                options.dicoCommandsController = options.commandsControllerToDictionary();
                options.dicoCommandsKeyBoard = options.commandsKeyBoardToDictionary();
            } 
            else {
                options = new Options();
                options.getDefaultOption();
                Save();
			}
        }
        return options;
    }

    public static void Save() {
        Options.Get().dicoCommandsController = Options.Get().commandsControllerToDictionary();
        Options.Get().dicoCommandsKeyBoard = Options.Get().commandsKeyBoardToDictionary();
        string jsonStringOptions = JsonUtility.ToJson(Options.Get());

        File.WriteAllText(Options.fullPath, jsonStringOptions);
    }

    public Dictionary<string,KeyCode> commandsKeyBoardToDictionary(){
        Dictionary<string,KeyCode> dicCommandKeyBoard = new Dictionary<string, KeyCode>();
        foreach(KeyBinding keyBinding in commandsKeyBoard){
            dicCommandKeyBoard.Add(keyBinding.action, keyBinding.key);
        }
        return dicCommandKeyBoard;
    }

    public Dictionary<string,KeyCode> commandsControllerToDictionary(){
        Dictionary<string,KeyCode> dicCommandController = new Dictionary<string, KeyCode>();
        foreach(KeyBinding keyBinding in commandsController){
            dicCommandController.Add(keyBinding.action, keyBinding.key);
        }
        return dicCommandController;
    }
}
