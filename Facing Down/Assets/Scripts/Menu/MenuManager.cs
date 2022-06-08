using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using Luminosity.IO;
using UnityEngine.TextCore.LowLevel;
using System.Runtime.InteropServices;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    public AudioMixer audioMixerEditor;
    public static AudioMixer audioMixer;

    public static bool isInputManager = false;

    void Awake(){
        if(!isInputManager)
            createInputManager();

        Localization.Init();

        audioMixer = audioMixerEditor;

        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");

        ToggleSelectableObject.lastSelectableObject = gameObjectActions.GetComponent<InfoSelectButton>().selectButton;

        ButtonDisplayCommand.contentDisplayCommands = GameObject.Find("ContentDisplayCommands");
        ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard = GameObject.Find("ScrollRectCommandsKeyBoard");
        ButtonDisplayCommand.scrollRectContentDisplayCommandController = GameObject.Find("ScrollRectCommandsController");

        ControllerManager.typeController.Add(ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard);
        ControllerManager.typeController.Add(ButtonDisplayCommand.scrollRectContentDisplayCommandController);
        ControllerManager.currentControl = ControllerManager.typeController[0];

        ButtonOptions.buttonApply = GameObject.Find("ButtonApply");
        ButtonAdjustVolume.contentVolume = GameObject.Find("ContentVolume");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);
        ButtonDisplayCommand.contentDisplayCommands.SetActive(false);
        ButtonAdjustVolume.contentVolume.SetActive(false);


        applyOptions();
    }


    public static void applyOptions(){
        audioMixer.SetFloat("masterVolume", Options.Get().masterVolumeValue);
        audioMixer.SetFloat("musicVolume", Options.Get().musicVolumeValue);
        audioMixer.SetFloat("soundVolume", Options.Get().soundVolumeValue);

    
        print("InputManager " + InputManager.GetControlScheme("Player_KeyBoard"));
        
        foreach(InputAction action in Options.Get().keyInput.Actions){
            InputManager.GetControlScheme("Player_KeyBoard").GetAction(action.Name).GetBinding(0).Positive = action.GetBinding(0).Positive;
        }

        /*
        foreach(InputAction action in Options.Get().controllerInput.Actions){
            InputManager.GetControlScheme("Player_Controller").GetAction(action.Name).GetBinding(0).GamepadButton = action.GetBinding(0).GamepadButton;
            InputManager.GetControlScheme("Player_Controller").GetAction(action.Name).GetBinding(0).GamepadAxis = action.GetBinding(0).GamepadAxis;
        }*/
        
    }

    public void createInputManager(){
        GameObject inputManager = Resources.Load("Prefabs/InputManager/InputManager" , typeof(GameObject)) as GameObject;
        inputManager = Instantiate(inputManager);
        inputManager.name = "InputManager";
        isInputManager = true;
    }



}
