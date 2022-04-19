using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Console : MonoBehaviour
{
	bool alreadyPressed = false;
	List<KeyCode> noRepeatKeys = new List<KeyCode> { KeyCode.Quote };
    bool toggled = false;

	string input = "";
	string output = "";

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
		lastInputs = new List<string>();
	}

	private bool PreventKeyRepeat() {
		if (!noRepeatKeys.Contains(Event.current.keyCode)) return false;
		if (Event.current.type == EventType.KeyDown) {
			if (alreadyPressed) return true;
			alreadyPressed = true;
		}
		if (Event.current.type == EventType.KeyUp) alreadyPressed = false;
		return false;
	}

	void HandleSpecialKeys() {
		if (Event.current.type == EventType.KeyDown) {
			if (Event.current.keyCode == KeyCode.Return) {
				HandleInput();
				input = "";
			}
			else if (Event.current.keyCode == KeyCode.UpArrow) {
				scrollIndex = Utility.mod(scrollIndex - 1, lastInputs.Count + 1);
				if (scrollIndex == lastInputs.Count) input = "";
				else input = lastInputs[scrollIndex];
			}
			else if (Event.current.keyCode == KeyCode.DownArrow) {
				scrollIndex = Utility.mod(scrollIndex + 1, lastInputs.Count + 1);
				if (scrollIndex == lastInputs.Count) input = "";
				else input = lastInputs[scrollIndex];
			}
		}
	}

	void HandleOutputArea() {
		if (output != "") {
			GUI.Box(new Rect(0, Screen.height - 60f, Screen.width, 30f), "");
			GUI.Label(new Rect(5f, Screen.height - 55f, Screen.width - 10f, 20f), output);
		}
	}

	void HandleInputArea() {
		GUI.Box(new Rect(0, Screen.height - 30f, Screen.width, 30f), "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		GUI.SetNextControlName("Console");
		input = GUI.TextField(new Rect(10f, Screen.height - 25f, Screen.width - 20f, 20f), input);
		input = Regex.Replace(input, @"[^a-zA-Z0-9 _]", "");
		GUI.FocusControl("Console");
	}
	public void OnGUI() {
		if (PreventKeyRepeat()) return;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Quote) {
			Toggle();
			alreadyPressed = true;
		}
		if (!toggled) return;

		HandleSpecialKeys();
		HandleOutputArea();
		HandleInputArea();
	}

	public void HandleInput() {
		output = "";
		lastInputs.Add(input);
		if (lastInputs.Count > 20) lastInputs.RemoveAt(0);
		scrollIndex = lastInputs.Count;

		string[] splitInput = input.Split(' ');
		AbstractConsoleCommand command = CommandList.getCommand(splitInput[0], splitInput.Length - 1);
		if (command == null) {
			output = CommandList.getErrorMessage();
			return;
		}
		if (splitInput.Length == 1) {
			if ((command as ConsoleCommand) != null) {
				(command as ConsoleCommand).Invoke();
				return;
			}
		}
		if (splitInput.Length == 2) {
			if ((command as ConsoleCommand<string>) != null) {
				(command as ConsoleCommand<string>).Invoke(splitInput[1]);
				return;
			}
			if ((command as ConsoleCommand<int>) != null) {
				int arg;
				if (!int.TryParse(splitInput[1], out arg)) {
					output = "Format invalid : \"" + splitInput[1] + "\" does not seem to be an integer.";
					return;
				}
				(command as ConsoleCommand<int>).Invoke(arg);
				return;
			}
		}
	}
}
