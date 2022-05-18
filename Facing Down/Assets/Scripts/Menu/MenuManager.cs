using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    public AudioMixer audioMixerEditor;
    public static AudioMixer audioMixer;

    void Awake(){
        Localization.Init();

        audioMixer = audioMixerEditor;

        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");

        ButtonDisplayCommand.contentDisplayCommand = GameObject.Find("ContentDisplayCommand");
        ButtonOptions.buttonApply = GameObject.Find("ButtonApply");
        ButtonAdjustVolume.contentVolume = GameObject.Find("ContentVolume");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);
        ButtonAdjustVolume.contentVolume.SetActive(false);

        applyOptions();
    }

    public static void applyOptions(){

        audioMixer.SetFloat("masterVolume", Options.Get().masterVolumeValue);
        audioMixer.SetFloat("musicVolume", Options.Get().musicVolumeValue);
        audioMixer.SetFloat("soundVolume", Options.Get().soundVolumeValue);
        
    }
}
