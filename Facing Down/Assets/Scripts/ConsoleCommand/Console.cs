using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// Manages the in-game command console.
/// </summary>
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

	/// <summary>
	/// Enables / Disables the console, and pauses the game.
	/// </summary>
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

	/// <summary>
	/// Initializes values.
	/// </summary>
	private void Awake() {
		noRepeatKeys = new List<KeyCode> { KeyCode.Period, KeyCode.Tab };
		lastInputs = new List<string>();
		scrollIndex = -1;
		previews = new List<string>();
		previewIndex = 0;
		tabPressed = false;
		CommandList.setConsole(this);
	}

	/// <summary>
	/// Prevents keys that should not be continuously pressed to be handled twice without releasing them.
	/// </summary>
	/// <returns>True if the input should be ignored, else returns false.</returns>
	private bool PreventKeyRepeat() {
		if (!noRepeatKeys.Contains(Event.current.keyCode)) return false;
		if (Event.current.type == EventType.KeyDown) {
			if (alreadyPressed) return true;
			alreadyPressed = true;
		}
		if (Event.current.type == EventType.KeyUp) alreadyPressed = false;
		return false;
	}

	/// <summary>
	/// Handles keys with special effects, such as Tab or Return.
	/// </summary>
	void HandleSpecialKeys() {
		if (Event.current.type == EventType.KeyDown) {
			if (Event.current.keyCode == KeyCode.Return) {
				HandleInput();
				input = "";
				ClearPreview();
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

	/// <summary>
	/// Displays the output area.
	/// </summary>
	void HandleOutputArea() {
		if (output != "") {
			GUI.Box(outputAreaRect, "");
			GUI.Label(outputTextRect, output);
		}
	}

	/// <summary>
	/// Sets the output message.
	/// </summary>
	/// <param name="output">The new output message.</param>
	public void SetOutput(string output) {
		this.output = output;
	}

	/// <summary>
	/// Clears the preview
	/// </summary>
	void ClearPreview() {
		previewIndex = 0;
		tabPressed = false;
		if (input == "") previews.Clear();
		else previews = CommandList.getCommandPreview(input);
		UpdatePreview();
	}

	/// <summary>
	/// Updates the preview string from current index and the preview list.
	/// </summary>
	void UpdatePreview() {
		if (previewIndex >= 0 && previewIndex < previews.Count) preview = previews[previewIndex];
		else preview = "";
	}

	/// <summary>
	/// Triggered on input change from the keyboard. Clears scroll and preview informations.
	/// </summary>
	void OnInputChange() {
		scrollIndex = lastInputs.Count;
		ClearPreview();
	}

	/// <summary>
	/// Displays the input (and preview) area and retrieves the input.
	/// </summary>
	void HandleInputArea() {
		GUI.Box(inputAreaRect, "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);

		GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
		GUI.Label(inputPreviewRect, preview);
		GUI.contentColor = new Color(1, 1, 1);

		string previousInput = input;
		GUI.SetNextControlName("Console");
		input = GUI.TextField(inputTextRect, input);
		input = Regex.Replace(input, @"[^a-zA-Z0-9 _,\.]", "");
		input = Regex.Replace(input, @" +", " ");
		if (previousInput != input) OnInputChange();
		GUI.FocusControl("Console");
	}
	/// <summary>
	/// Listens to events and acts accordingly.
	/// </summary>
	public void OnGUI() {
		if (PreventKeyRepeat()) return;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Period) {
			Toggle();
			alreadyPressed = true;
		}
		if (!toggled) return;

		HandleSpecialKeys();
		HandleOutputArea();
		HandleInputArea();
	}

	/// <summary>
	/// Handles the Return keypress.
	/// </summary>
	public void HandleInput() {
		output = "";
		lastInputs.Add(input);
		if (lastInputs.Count > 20) lastInputs.RemoveAt(0);
		scrollIndex = lastInputs.Count;
		try {
			CommandHandler.ExecuteCommand(input);
		} catch(CommandRuntimeException e) {
			output = e.Message;
		}
	}
}
