using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [Range(0.0f, 2.0f)] public float sensibility = 0.8f;

    private Vector2 pointer = new Vector2(0.0f, 0.0f);

    public bool lowSensitivity = false;

    //private List<Dictionary<string, KeyCode>> controlers;

    private Dictionary<string, List<InputListener>> listeners;
    private Dictionary<string, bool> keyPress;
    private Dictionary<string, bool> keyHold;
    private Dictionary<string, bool> keyRelease;

    private static Dictionary<string, bool> keyAxisState;

    private static bool onAxisButtonLT = false;
    private static bool onAxisButtonRT = false;

    public void Init() {/*
        controlers = new List<Dictionary<string, KeyCode>>();
        controlers.Add(Options.Get().dicoCommandsController);
        controlers.Add(Options.Get().dicoCommandsKeyBoard);
        */
        listeners = new Dictionary<string, List<InputListener>>();
        keyPress = new Dictionary<string, bool>();
        keyHold = new Dictionary<string, bool>();
        keyRelease = new Dictionary<string, bool>();

        keyAxisState = new Dictionary<string, bool>();
    }

    public void Subscribe(string action, InputListener listener) {
        /*foreach (Dictionary<string, KeyCode> controler in controlers) {
            if (!controler.ContainsKey(action)) continue;
            KeyCode key = controler[action];
            if (!listeners.ContainsKey(key)) {
                listeners.Add(key, new List<InputListener>());
                keyPress.Add(key, false);
                keyHold.Add(key, false);
                keyRelease.Add(key, false);
            }
            listeners[key].Add(listener);
        }
        */
        if (!listeners.ContainsKey(action))
        {
            listeners.Add(action, new List<InputListener>());
            keyPress.Add(action, false);
            keyHold.Add(action, false);
            keyRelease.Add(action, false);
            keyAxisState.Add(action, false);
        }
        listeners[action].Add(listener);
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.time.GetGameSpeed() == 0) return;
        ComputePress();
        ComputeReleased();


        pointer.x = Input.GetAxis("Horizontal_Pointeur") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
        pointer.y = Input.GetAxis("Vertical_Pointeur") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
    }

	private void FixedUpdate() {
        foreach (string key in listeners.Keys) {
            if (keyPress[key]) {
                foreach (InputListener listener in listeners[key]) {
                    listener.OnInputPressed();
                }
                keyPress[key] = false;
            }
            if (keyHold[key]) {
                foreach (InputListener listener in listeners[key]) {
                    listener.OnInputHeld();
                }
            }
            if (keyRelease[key]) {
                foreach (InputListener listener in listeners[key]) {
                    listener.OnInputReleased();
                }
                keyRelease[key] = false;
            }
        }
        foreach (List<InputListener> listenerList in listeners.Values) {
            foreach (InputListener listener in listenerList) {
                listener.UpdateAfterInput();
			}
		}
	}
	private void ComputePress()
    {
        foreach (string key in listeners.Keys) {
            if (checkIfkeyCodeIsPressed(key)) {
                keyPress[key] = true;
                keyHold[key] = true;
			}
		}
    }

    private void ComputeReleased()
    {
        foreach (string key in listeners.Keys) {
            if (checkIfkeyCodeIsReleased(key)) {
                keyRelease[key] = true;
                keyHold[key] = false;
            }
        }
    }

    public Vector2 getPointer()
    {
        return pointer;
    }

    private static float triggerLimit = 0.5f;

    public static bool checkIfkeyCodeIsPressed(string key)
    {
        bool asButton = false;
        bool asAxis = false;
        try
        {
            asButton = Input.GetButtonDown(key);
        }
        catch { }
        try
        {
            if ( ! keyAxisState[key])
            {
                asAxis = ! keyAxisState[key] && (Input.GetAxis(key) >= triggerLimit);
                if (asAxis)
                    keyAxisState[key] = true;
            }
        }
        catch { }
        return asButton || asAxis;
    }

    public static bool checkIfkeyCodeIsReleased(string key)
    {
        bool asButton = false;
        bool asAxis = false;
        try
        {
            asButton = Input.GetButtonUp(key);
        }
        catch { }
        try
        {
            if (keyAxisState[key])
            {
                asAxis = keyAxisState[key] && (Input.GetAxis(key) < triggerLimit);
                if(asAxis)
                    keyAxisState[key] = false;
            }
        }
        catch { }

        return asButton || asAxis;
    }

    public static bool checkIfkeyCodeIsPressed(KeyCode kc){
        if(kc == KeyCode.JoystickButton11){
            if (!onAxisButtonLT && Input.GetAxis("Button LT") > 0){
                onAxisButtonLT = true;
                return true;
            }
            else
                return false;
        }
        else if (!onAxisButtonRT && kc == KeyCode.JoystickButton12){
            if(Input.GetAxis("Button RT") > 0){
                onAxisButtonRT = true;
                return true;
            }
                
            else
                return false;
        }
        else
            return Input.GetKeyDown(kc);
    }

    public static bool checkIfkeyCodeIsReleased(KeyCode kc){
        if(kc == KeyCode.JoystickButton11){
            if (onAxisButtonLT && Input.GetAxis("Button LT") == 0){
                onAxisButtonLT = false;
                return true;
            }
                
            else
                return false ;
        }
        else if (kc == KeyCode.JoystickButton12){
            if(onAxisButtonRT && Input.GetAxis("Button RT") == 0){
                onAxisButtonRT = false;
                return true;
            }
                
            else
                return false;
        }
        else
            return Input.GetKeyUp(kc);
    }

    public static bool checkIfkeyCodeIsPressedOnGUI(KeyCode kc){
        if(kc == KeyCode.JoystickButton11){
            if (!onAxisButtonLT && Input.GetAxis("Button LT") > 0){
                onAxisButtonLT = true;
                return true;
            }
            else
                return false;
        }
        else if (!onAxisButtonRT && kc == KeyCode.JoystickButton12){
            if(Input.GetAxis("Button RT") > 0){
                onAxisButtonRT = true;
                return true;
            }
                
            else
                return false;
        }
        else
            return Input.GetKey(kc);
    }


}
