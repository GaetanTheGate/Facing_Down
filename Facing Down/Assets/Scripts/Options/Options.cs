using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Luminosity.IO;

[System.Serializable]
public class Options
{
    public static string fullPath = Application.persistentDataPath + "/options.json";
    public static string fullPathKeyBoard = Application.persistentDataPath + "/input_keyboard.xml";
    public static string fullPathController = Application.persistentDataPath + "/input_controller.xml";

    private static Options options;

    //[System.NonSerialized]
    //public Dictionary<string,KeyCode> dicoCommandsKeyBoard = new Dictionary<string, KeyCode>();
    //[System.NonSerialized]
    //public Dictionary<string,KeyCode> dicoCommandsController = new Dictionary<string, KeyCode>();

    public string langue;
    public float masterVolumeValue;
    public float musicVolumeValue;
    public float soundVolumeValue;

    public ControlScheme keyInput;
    public ControlScheme controllerInput;

    //public List<KeyBinding> commandsKeyBoard = new List<KeyBinding>();
    //public List<KeyBinding> commandsController = new List<KeyBinding>();

    public void setOptionToDefault()
    {
        SetLanguageToDefault();
        SetMasterVolumeToDefault();
        SetMusicVolumeToDefault();
        SetSoundVolumeToDefault();
        SetPlayerContolToDefault();
    }

    private void SetLanguageToDefault() => langue = "En";
    private void SetMasterVolumeToDefault() => masterVolumeValue = 100f;
    private void SetMusicVolumeToDefault() => musicVolumeValue = 100f;
    private void SetSoundVolumeToDefault() => soundVolumeValue = 100f;
    private void SetPlayerContolToDefault()
    {
        keyInput = CreateCopyOfControl("Default_KeyBoard", "Player_KeyBoard");
        controllerInput = CreateCopyOfControl("Default_Controller", "Player_Controller");

        SetControlToPlayer("Player");
    }

    private static ControlScheme CreateCopyOfControl(string string_source, string string_target)
    {
        ControlScheme source = InputManager.GetControlScheme(string_source);

        ControlScheme target = InputManager.GetControlScheme(string_target);
        if (target != null)
            while (target.Actions.Count > 0)
                target.DeleteAction(0);
        else
            target = InputManager.CreateControlScheme(string_target);

        foreach (InputAction action in source.Actions)
        {
            target.CreateNewAction(action.Name).Copy(action);
        }

        target.Initialize();
        return target;
    }

    private static ControlScheme CreateCopyOfControl(ControlScheme source, string string_target)
    {
        ControlScheme target = InputManager.GetControlScheme(string_target);
        if (target != null)
            while (target.Actions.Count > 0)
                target.DeleteAction(0);
        else
            target = InputManager.CreateControlScheme(string_target);

        foreach (InputAction action in source.Actions)
        {
            target.CreateNewAction(action.Name).Copy(action);
        }

        target.Initialize();
        return target;
    }

    private static ControlScheme FuseControls(string string_keyboard, string string_controller, string string_target)
    {
        ControlScheme keyboard = InputManager.GetControlScheme(string_keyboard);
        ControlScheme controller = InputManager.GetControlScheme(string_controller);

        ControlScheme target = InputManager.GetControlScheme(string_target);
        if (target != null)
            while (target.Actions.Count > 0)
                target.DeleteAction(0);
        else
            target = InputManager.CreateControlScheme(string_target);


        foreach (InputAction action in keyboard.Actions)
        {
            target.CreateNewAction(action.Name).Copy(action);
        }


        foreach (InputAction action in controller.Actions)
        {
            if (target.GetAction(action.Name) == null)
                target.CreateNewAction(action.Name).Copy(action);
            else
                foreach (InputBinding binding in action.Bindings)
                    target.GetAction(action.Name).CreateNewBinding().Copy(binding);
        }

        target.Initialize();
        return target;
    }

    private static void SetControlToPlayer(string target)
    {
        ControlScheme control = FuseControls(target + "_KeyBoard", target + "_Controller", target);

        InputManager.SetControlScheme(control.Name, PlayerID.One);
        
        control.Initialize();
    }

    public static void SetControlToPlayer()
    {
        SetControlToPlayer("Player");
    }

    public static Options Get()
    {
        if (options == null)
        {
            if (File.Exists(fullPath))
            {
                try
                {
                    options = JsonUtility.FromJson<Options>(File.ReadAllText(fullPath));
                }
                catch { }
                if (options == null)
                {
                    options = new Options();
                    options.setOptionToDefault();
                    Save();
                }
                else if (options.VerifyOptionsValidity())
                {
                    Save();
                }
                    
            }
            else
            {
                options = new Options();
                options.setOptionToDefault();
                Save();
			}
        }
        return options;
    }

    public static void Save()
    {
        string jsonStringOptions = JsonUtility.ToJson(Get());
        File.WriteAllText(fullPath, jsonStringOptions);
    }

    private bool VerifyOptionsValidity()
    {
        bool hasBeenChanged = false;

        hasBeenChanged = VerifyLanguage() || hasBeenChanged;

        if(options.masterVolumeValue < 0 || masterVolumeValue > 100)
        {
            SetMasterVolumeToDefault();
            hasBeenChanged = true;
        }

        if (options.musicVolumeValue < 0 || musicVolumeValue > 100)
        {
            SetMusicVolumeToDefault();
            hasBeenChanged = true;
        }

        if (soundVolumeValue < 0 || soundVolumeValue > 100)
        {
            SetSoundVolumeToDefault();
            hasBeenChanged = true;
        }

        hasBeenChanged = VerifyPlayerControl() || hasBeenChanged;


        CreateCopyOfControl(keyInput, "Player_KeyBoard");
        CreateCopyOfControl(controllerInput, "Player_Controller");
        SetControlToPlayer("Player");

        return hasBeenChanged;
    }

    private bool VerifyLanguage()
    {
        switch (langue)
        {
            case "En":
            case "Fr":
                return false;
            default:
                SetLanguageToDefault();
                return true;
        }
    }

    private bool VerifyPlayerControl()
    {
        bool hasBeenChanged = false;

        if (keyInput == null)
        {
            keyInput = CreateCopyOfControl("Default_KeyBoard", "Player_KeyBoard");
            hasBeenChanged = true;
        }
        else
        {
            try
            {
                if (!keyInput.Name.Equals("Player_KeyBoard"))
                {
                    keyInput.Name = "Player_KeyBoard";
                    hasBeenChanged = true;
                }
                hasBeenChanged = VerifyValidityOfControl(keyInput, InputManager.GetControlScheme("Default_KeyBoard")) || hasBeenChanged;
            }
            catch
            {
                keyInput = CreateCopyOfControl("Default_KeyBoard", "Player_KeyBoard");
                hasBeenChanged = true;
            }
        }

        if (controllerInput == null)
        {
            controllerInput = CreateCopyOfControl("Default_Controller", "Player_Controller");
            hasBeenChanged = true;
        }
        else
        {
            try
            {
                if (!controllerInput.Name.Equals("Player_Controller"))
                {
                    controllerInput.Name = "Player_Controller";
                    hasBeenChanged = true;
                }

                hasBeenChanged = VerifyValidityOfControl(controllerInput, InputManager.GetControlScheme("Default_Controller")) || hasBeenChanged;
            }
            catch
            {
                controllerInput = CreateCopyOfControl("Default_Controller", "Player_Controller");
                hasBeenChanged = true;
            }
        }


        return hasBeenChanged;
    }

    private static bool VerifyValidityOfControl(ControlScheme control, ControlScheme defaultControl)
    {
        bool hasBeenChanged = false;

        foreach (InputAction action in defaultControl.Actions)
        {
            InputAction playerAction = control.GetAction(action.Name);

            if (playerAction == null)
            {
                control.CreateNewAction(action.Name);
                hasBeenChanged = true;
            }
            if (playerAction.Bindings.Count.Equals(0))
            {
                foreach (InputBinding binding in action.Bindings)
                    playerAction.CreateNewBinding(binding);
                hasBeenChanged = true;
            }
        }
        return hasBeenChanged;
    }
}
