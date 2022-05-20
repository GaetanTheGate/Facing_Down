using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{


    [Range(0.0f, 2.0f)] public float sensibility = 0.8f;

    private Vector2 pointer = new Vector2(0.0f, 0.0f);

    public bool lowSensitivity = false;

    private List<KeyCode> listenedKeys = new List<KeyCode>();

    private Dictionary<KeyCode, List<InputListener>> listeners = new Dictionary<KeyCode, List<InputListener>>();
    private Dictionary<KeyCode, bool> keyPress = new Dictionary<KeyCode, bool>();
    private Dictionary<KeyCode, bool> keyHold = new Dictionary<KeyCode, bool>();
    private Dictionary<KeyCode, bool> keyRelease = new Dictionary<KeyCode, bool>();

    public void Init() {
        listenedKeys.Add(Options.Get().dicoCommand["dash"]);
        listenedKeys.Add(Options.Get().dicoCommand["bulletTime"]);
        listenedKeys.Add(Options.Get().dicoCommand["attack"]);

        foreach (KeyCode key in listenedKeys) {
            keyPress.Add(key, false);
            keyHold.Add(key, false);
            keyRelease.Add(key, false);
            listeners.Add(key, new List<InputListener>());
		}
	}

    public void Subscribe(KeyCode key, InputListener listener) {
        listeners[key].Add(listener);
	}

    // Update is called once per frame
    void Update()
    {
        ComputePress();
        ComputeReleased();


        pointer.x = Input.GetAxis("Mouse X") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
        pointer.y = Input.GetAxis("Mouse Y") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
    }

	private void FixedUpdate() {
        foreach (KeyCode key in listenedKeys) {
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
        foreach (KeyCode key in listenedKeys) {
            if (Input.GetKeyDown(key)) {
                keyPress[key] = true;
                keyHold[key] = true;
			}
		}
    }

    private void ComputeReleased()
    {
        foreach (KeyCode key in listenedKeys) {
            if (Input.GetKeyUp(key)) {
                keyRelease[key] = true;
                keyHold[key] = false;
            }
        }
    }

    public Vector2 getPointer()
    {
        return pointer;
    }
}
