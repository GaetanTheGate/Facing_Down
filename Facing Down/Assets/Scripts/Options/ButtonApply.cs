using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Luminosity.IO;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    public static bool onDisplayCommands = false;
    public static bool onContentVolume = false;
    public void apply(){

        ButtonDisplayCommand.contentDisplayCommands.SetActive(true);
        ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard.SetActive(true);
        ButtonDisplayCommand.scrollRectContentDisplayCommandController.SetActive(true);
        ButtonAdjustVolume.contentVolume.SetActive(true);

        Options.Get().langue = GameObject.Find("DropdownLangue").GetComponent<Dropdown>().captionText.text;

        Options.Get().masterVolumeValue = GameObject.Find("SliderMasterVolume").GetComponent<Slider>().value;
        Options.Get().musicVolumeValue = GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value;
        Options.Get().soundVolumeValue = GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value;
        
        GameObject commandsKeyBoard = GameObject.Find("ContentDisplayCommandKeyBoard").gameObject;
        for (int i = 0; i < commandsKeyBoard.transform.childCount; ++i) {
            GameObject command = commandsKeyBoard.transform.GetChild(i).gameObject;
            string stringKeyCode = command.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
            Options.Get().keyInput.GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), stringKeyCode);
            InputManager.GetControlScheme("Player_KeyBoard").GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), stringKeyCode);
        }

        /*GameObject commandsController = GameObject.Find("ContentDisplayCommandController").gameObject;
        GenericGamepadProfileSelector inputManager = GameObject.Find("InputManager").GetComponent<GenericGamepadProfileSelector>();
        for (int i = 0; i < commandsController.transform.childCount; ++i) {
            GameObject command = commandsController.transform.GetChild(i).gameObject;
            string stringKeyCode = command.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
            if(stringKeyCode == "Bouton LT"){
                Options.Get().keyInput.GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadAxis = GenericGamepadProfileAxisToGamePadAxis(MenuManager.profile.GamepadProfile.LeftTriggerAxis);
                InputManager.GetControlScheme("PLayer_Controller").GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadAxis = GenericGamepadProfileAxisToGamePadAxis(MenuManager.profile.GamepadProfile.LeftTriggerAxis);
            }
            else if (stringKeyCode == "Bouton RT"){
                Options.Get().keyInput.GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadAxis = GenericGamepadProfileAxisToGamePadAxis(MenuManager.profile.GamepadProfile.RightTriggerAxis);
                InputManager.GetControlScheme("Player_Controller").GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadAxis = GenericGamepadProfileAxisToGamePadAxis(MenuManager.profile.GamepadProfile.RightTriggerAxis);
            }
            else{
                Options.Get().keyInput.GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadButton = ButtonChangeCommand.stringControllerToKeyCode(stringKeyCode);
                InputManager.GetControlScheme("Player_Controller").GetAction(command.transform.Find("Action").GetComponent<InfoAction>().idAction).GetBinding(0).GamepadButton = ButtonChangeCommand.stringControllerToKeyCode(stringKeyCode);
            }
        }*/

        
        foreach(GameObject go in ControllerManager.typeController){
            go.SetActive(false);
        }
        ControllerManager.currentControl.SetActive(true);

        
        
        if(!onDisplayCommands){
            ButtonDisplayCommand.contentDisplayCommands.SetActive(false);
            onDisplayCommands = false;
        }

        if(!onContentVolume){
            ButtonAdjustVolume.contentVolume.SetActive(false);
            onContentVolume = false;
        }

        Options.SetControlToPlayer();
        Options.Save();

        print("options sauvegardées");

        gameObject.SetActive(false);

    }

    public static GamepadAxis GenericGamepadProfileAxisToGamePadAxis(int indexAxis){
        if(indexAxis == 2 || indexAxis == 4 || indexAxis == 8)
            return GamepadAxis.LeftTrigger;
        else if (indexAxis == 9 || indexAxis == 5 )
            return GamepadAxis.RightTrigger;
        return GamepadAxis.LeftThumbstickX;
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonApply").TEXT;
    }
}
