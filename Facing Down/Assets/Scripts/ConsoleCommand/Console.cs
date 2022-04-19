using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Console : MonoBehaviour
{
	bool alreadyPressed = false;
	List<KeyCode> usedKeys = new List<KeyCode> { KeyCode.Quote };
    bool toggled = false;

	string input = "";
	string output = "";

	public static ConsoleCommand PRINT_DEBUG;
	public static ConsoleCommand<string> PRINT_STR;
	public List<AbstractConsoleCommand> commandList;

	List<string> lastInputs;
	int scrollIndex = 0;

	private void Toggle() {
		if (toggled) {
			toggled = false;
			Game.time.SetGameSpeedInstant(1f);
		}
		else {
			toggled = true;
			Game.time.SetGameSpeedInstant(0f);
		}
	}

	private void Awake() {
		PRINT_DEBUG = new ConsoleCommand("print_debug", "Prints \"CONSOLE : Print Debug\" in the debug console", "print_debug", () => { Debug.Log("CONSOLE : Print Debug"); });
		PRINT_STR = new ConsoleCommand<string>("print_str", "Prints \"CONSOLE : <str>\" in the debug console", "print_str <string>", (str) => { Debug.Log("CONSOLE : " + str); });

		commandList = new List<AbstractConsoleCommand> {PRINT_DEBUG, PRINT_STR};
	}

	private bool IgnoreIrrelevantEvents() {
		if (!(usedKeys.Contains(Event.current.keyCode))) return false;
		if (Event.current.type == EventType.KeyDown) {
			if (alreadyPressed) return true;
			alreadyPressed = true;
		}
		if (Event.current.type == EventType.KeyUp) alreadyPressed = false;
		return false;
	}

	public void OnGUI() {
		if (IgnoreIrrelevantEvents()) return;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Quote) {
			Toggle();
			alreadyPressed = true;
		}
		if (!toggled) return;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			HandleInput();
			input = "";
		}

		if (output != "") {
			GUI.Box(new Rect(0, Screen.height - 60f, Screen.width, 30f), "");
			GUI.Label(new Rect(5f, Screen.height - 55f, Screen.width - 10f, 20f), output);
		}
		GUI.Box(new Rect(0, Screen.height - 30f, Screen.width, 30f), "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		GUI.SetNextControlName("Console");
		input = GUI.TextField(new Rect(10f, Screen.height - 25f, Screen.width - 20f, 20f), input);
		input = Regex.Replace(input, @"[^a-zA-Z0-9 _]", "");
		GUI.FocusControl("Console");
	}

	public void HandleInput() {
		output = "";
		lastInputs.Add(input);
		if (lastInputs.Count > 20) lastInputs.RemoveAt(0);
		string[] splitInput = input.Split(' ');
		for (int i = 0; i < commandList.Count; ++i) {
			if (splitInput[0] == commandList[i].getID()) {
				if (commandList[i] as ConsoleCommand != null) {
					(commandList[i] as ConsoleCommand).Invoke();
					return;
				}
				else if (commandList[i] as ConsoleCommand<string> != null) {
					if (splitInput.Length < 2) 
						output = "Not enough arguments for " + commandList[i].getFormat();
					else 
						(commandList[i] as ConsoleCommand<string>).Invoke(splitInput[1]);
					return;
				}
			}
		}
		output = "Command " + input + " not found";
	}
}
