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

    public static GenericGamepadProfileSelector.Profile profile;

    void Awake(){
        chooseGameProfileAccordingToOS();
        Localization.Init();

        DontDestroyOnLoad(GameObject.Find("ToogleSelectableObject")); 

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

        
        foreach(InputAction action in Options.Get().keyInput.Actions){
            InputManager.GetControlScheme("Player_KeyBoard").GetAction(action.Name).GetBinding(0).Positive = action.GetBinding(0).Positive;
        }

        /*
        foreach(InputAction action in Options.Get().controllerInput.Actions){
            InputManager.GetControlScheme("Player_Controller").GetAction(action.Name).GetBinding(0).GamepadButton = action.GetBinding(0).GamepadButton;
            InputManager.GetControlScheme("Player_Controller").GetAction(action.Name).GetBinding(0).GamepadAxis = action.GetBinding(0).GamepadAxis;
        }*/
        
    }

    private static void chooseGameProfileAccordingToOS(){
        GenericGamepadProfileSelector inputManager = GameObject.Find("InputManager").GetComponent<GenericGamepadProfileSelector>();
        if(System.Environment.OSVersion.Platform.ToString().Contains("Win"))
            profile = inputManager.GetProfile(0);
        else if (System.Environment.OSVersion.Platform.ToString().Contains("Unix"))
            profile = inputManager.GetProfile(1);
        else
            profile = inputManager.GetProfile(2);
    }


}
