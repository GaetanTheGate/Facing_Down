using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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
        commandsKeyBoard.Add(new KeyBinding("toogleInventoryMap", KeyCode.E));
        commandsKeyBoard.Add(new KeyBinding("closeConsole", KeyCode.Escape));

        commandsController = new List<KeyBinding>();
        commandsController.Add(new KeyBinding("dash", KeyCode.JoystickButton2));
        commandsController.Add(new KeyBinding("attack", KeyCode.JoystickButton0));
        commandsController.Add(new KeyBinding("bulletTime", KeyCode.JoystickButton11));
        commandsController.Add(new KeyBinding("openConsole", KeyCode.JoystickButton7));
        commandsController.Add(new KeyBinding("toggleInventoryMap", KeyCode.JoystickButton1));
        commandsController.Add(new KeyBinding("closeConsole", KeyCode.JoystickButton3));    

        dicoCommandsKeyBoard = commandsKeyBoardToDictionary();
        dicoCommandsController = commandsControllerToDictionary();
    }

    public static Options Get() {
        if (options == null) {
            if (File.Exists(fullPath)){
                options = JsonUtility.FromJson<Options>(File.ReadAllText(fullPath));
                options.dicoCommandsController = options.commandsControllerToDictionary();
                options.dicoCommandsKeyBoard = options.commandsKeyBoardToDictionary();
                verifyJsonOptionsFile();
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

    private static void verifyJsonOptionsFile(){
        bool reSave = false;
        if(options.langue == null){
            options.langue = "En";
            reSave = true;
        }


        List<string> actionsDefault = new List<string>();
        actionsDefault.Add("dash");
        actionsDefault.Add("attack");
        actionsDefault.Add("bulletTime");
        actionsDefault.Add("openConsole");
        actionsDefault.Add("toggleInventoryMap");
        actionsDefault.Add("closeConsole");
       

        List<KeyBinding> commandsKeyBoardDefault = new List<KeyBinding>();
        commandsKeyBoardDefault.Add(new KeyBinding("dash", KeyCode.Mouse0));
        commandsKeyBoardDefault.Add(new KeyBinding("attack", KeyCode.Mouse1));
        commandsKeyBoardDefault.Add(new KeyBinding("bulletTime", KeyCode.Space));
        commandsKeyBoardDefault.Add(new KeyBinding("openConsole", KeyCode.C));
        commandsKeyBoardDefault.Add(new KeyBinding("toggleInventoryMap", KeyCode.E));
        commandsKeyBoardDefault.Add(new KeyBinding("closeConsole", KeyCode.Escape));

        if (options.commandsKeyBoard == null){
            options.commandsKeyBoard = commandsKeyBoardDefault;
            options.dicoCommandsKeyBoard = options.commandsKeyBoardToDictionary();
            reSave = true; 
        }
        else if(options.commandsKeyBoard.Count < 6){

            List<string> actionsInKeyBoard = new List<string>();
            foreach(KeyBinding key in options.commandsKeyBoard){
                actionsInKeyBoard.Add(key.action);
            }

            IEnumerable<string> keyToAdd = actionsDefault.Except(actionsInKeyBoard);
            foreach(string action in keyToAdd){
                foreach(KeyBinding keyBinding in commandsKeyBoardDefault){
                    if(keyBinding.action == action){
                        options.commandsKeyBoard.Add(keyBinding);
                        break;
                    }
                }
            }
            options.dicoCommandsKeyBoard = options.commandsKeyBoardToDictionary();
            reSave = true; 
        }

        List<KeyBinding> commandsControllerDefault = new List<KeyBinding>();
        commandsControllerDefault.Add(new KeyBinding("dash", KeyCode.JoystickButton2));
        commandsControllerDefault.Add(new KeyBinding("attack", KeyCode.JoystickButton0));
        commandsControllerDefault.Add(new KeyBinding("bulletTime", KeyCode.JoystickButton11));
        commandsControllerDefault.Add(new KeyBinding("openConsole", KeyCode.JoystickButton7));
        commandsControllerDefault.Add(new KeyBinding("toggleInventoryMap", KeyCode.JoystickButton1));
        commandsControllerDefault.Add(new KeyBinding("closeConsole", KeyCode.JoystickButton3));  

        
                
        if (options.commandsController == null){
            options.commandsController = commandsControllerDefault;
            options.dicoCommandsController = options.commandsControllerToDictionary();
            reSave = true; 
        }
        else if(options.commandsController.Count < 6){
            List<string> actionsInController = new List<string>();
            foreach(KeyBinding key in options.commandsController){
                actionsInController.Add(key.action);
            }

            IEnumerable<string> keyToAdd = actionsDefault.Except(actionsInController);
            foreach(string action in keyToAdd){
                foreach(KeyBinding keyBinding in commandsControllerDefault){
                    if(keyBinding.action == action){
                        options.commandsController.Add(keyBinding);
                        break;
                    }
                }
            }
            options.dicoCommandsController = options.commandsControllerToDictionary();
            reSave = true; 
        } 

        if(reSave)
            Save();
    }
}
