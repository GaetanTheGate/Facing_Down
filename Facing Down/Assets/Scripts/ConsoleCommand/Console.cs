using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Console : MonoBehaviour
{
	bool alreadyPressed = false;
	List<KeyCode> noRepeatKeys;
    bool toggled = false;

	string input = "";
	string output = "";

	List<string> lastInputs;
	int scrollIndex;

	List<string> previews;
	int previewIndex;
	string preview;
	bool tabPressed;

	readonly Rect inputTextRect = new Rect(10f, Screen.height - 25f, Screen.width - 20f, 20f);
	readonly Rect inputPreviewRect = new Rect(12f, Screen.height - 25f, Screen.width - 20f, 20f);
	readonly Rect inputAreaRect = new Rect(0, Screen.height - 30f, Screen.width, 30f);
	readonly Rect outputTextRect = new Rect(5f, Screen.height - 55f, Screen.width - 10f, 20f);
	readonly Rect outputAreaRect = new Rect(0, Screen.height - 60f, Screen.width, 30f);

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
		noRepeatKeys = new List<KeyCode> { KeyCode.Comma, KeyCode.Tab };
		lastInputs = new List<string>();
		scrollIndex = -1;
		previews = new List<string>();
		previewIndex = 0;
		tabPressed = false;
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
				ClearPreview();
			}
			else if (Event.current.keyCode == KeyCode.DownArrow) {
				scrollIndex = Utility.mod(scrollIndex + 1, lastInputs.Count + 1);
				if (scrollIndex == lastInputs.Count) input = "";
				else input = lastInputs[scrollIndex];
				ClearPreview();
			}
			else if (Event.current.keyCode == KeyCode.Tab) {
				if (previews.Count == 0) return;
				if (!tabPressed) tabPressed = true;
				else previewIndex = Utility.mod(previewIndex + 1, previews.Count);
				if (input.Split(' ').Length <= 1) 
					input = previews[previewIndex].Split(' ')[0];
				UpdatePreview();
			}
		}
	}

	void HandleOutputArea() {
		if (output != "") {
			GUI.Box(outputAreaRect, "");
			GUI.Label(outputTextRect, output);
		}
	}

	void ClearPreview() {
		previewIndex = 0;
		tabPressed = false;
		if (input == "") previews.Clear();
		else previews = CommandList.getCommandPreview(input);
		UpdatePreview();
	}

	void UpdatePreview() {
		if (previewIndex >= 0 && previewIndex < previews.Count) preview = previews[previewIndex];
		else preview = "";
	}

	void OnInputChange() {
		scrollIndex = lastInputs.Count;
		ClearPreview();
	}

	void HandleInputArea() {
		GUI.Box(inputAreaRect, "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);

		GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
		GUI.Label(inputPreviewRect, preview);
		GUI.contentColor = new Color(1, 1, 1);

		string previousInput = input;
		GUI.SetNextControlName("Console");
		input = GUI.TextField(inputTextRect, input);
		input = Regex.Replace(input, @"[^a-zA-Z0-9 _]", "");
		input = Regex.Replace(input, @" +", " ");
		if (previousInput != input) OnInputChange();
		GUI.FocusControl("Console");
	}
	public void OnGUI() {
		if (PreventKeyRepeat()) return;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Comma) {
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
		try {
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
			if (splitInput.Length == 3) {
				if ((command as ConsoleCommand<string, int>) != null) {
					string arg1 = splitInput[1];
					int arg2;
					if (!int.TryParse(splitInput[2], out arg2)) {
						output = "Format invalid : \"" + splitInput[2] + "\" does not seem to be an integer.";
						return;
					}
					(command as ConsoleCommand<string, int>).Invoke(arg1, arg2);
					return;
				}
			}
		} catch(CommandRuntimeException e) {
			output = e.Message;
		}
		if (splitInput.Length == 3) {
			if ((command as ConsoleCommand<string, int>) != null) {
				string arg1 = splitInput[1];
				int arg2;
				if (!int.TryParse(splitInput[2], out arg2)) {
					output = "Format invalid : \"" + splitInput[2] + "\" does not seem to be an integer.";
					return;
				}
				(command as ConsoleCommand<string, int>).Invoke(arg1, arg2);
				return;
			}
		}


		ClearPreview();
	}
}
