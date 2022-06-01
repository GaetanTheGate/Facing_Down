using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{


    [Range(0.0f, 2.0f)] public float sensibility = 0.8f;

    private Vector2 pointer = new Vector2(0.0f, 0.0f);

    public bool lowSensitivity = false;

    private List<Dictionary<string, KeyCode>> controlers;

    private Dictionary<KeyCode, List<InputListener>> listeners;
    private Dictionary<KeyCode, bool> keyPress;
    private Dictionary<KeyCode, bool> keyHold;
    private Dictionary<KeyCode, bool> keyRelease;

    private static bool onAxisButtonLT = false;
    private static bool onAxisButtonRT = false;

    public void Init() {
        controlers = new List<Dictionary<string, KeyCode>>();
        controlers.Add(Options.Get().dicoCommandsController);
        controlers.Add(Options.Get().dicoCommandsKeyBoard);

        listeners = new Dictionary<KeyCode, List<InputListener>>();
        keyPress = new Dictionary<KeyCode, bool>();
        keyHold = new Dictionary<KeyCode, bool>();
        keyRelease = new Dictionary<KeyCode, bool>();
	}

    public void Subscribe(string action, InputListener listener) {
        foreach (Dictionary<string, KeyCode> controler in controlers) {
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
	}

    // Update is called once per frame
    void Update()
    {
        if (Game.time.GetGameSpeed() == 0) return;
        ComputePress();
        ComputeReleased();


        pointer.x = Input.GetAxis("Mouse X") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
        pointer.y = Input.GetAxis("Mouse Y") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
    }

	private void FixedUpdate() {
        foreach (KeyCode key in listeners.Keys) {
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
        foreach (KeyCode key in listeners.Keys) {
            if (checkIfkeyCodeIsPressed(key)) {
                keyPress[key] = true;
                keyHold[key] = true;
			}
		}
    }

    private void ComputeReleased()
    {
        foreach (KeyCode key in listeners.Keys) {
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
        else{
            return Input.GetKeyDown(kc);
        }
            
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

}
