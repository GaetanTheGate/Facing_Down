using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the in-game command console.
/// </summary>
public class Console : MonoBehaviour
{
	bool alreadyPressed = false;
	List<KeyCode> noRepeatKeys;
    bool toggled;

	List<string> lastInputs;
	int scrollIndex;

	List<string> previews;
	int previewIndex;

	public InputField input;
	public Text preview;
	public Text output;

	private bool inputChangedByScrolling;

	/// <summary>
	/// Initializes values.
	/// </summary>
	private void Awake() {
		toggled = false;

		noRepeatKeys = new List<KeyCode> { KeyCode.Period, KeyCode.Tab };

		lastInputs = new List<string>();
		scrollIndex = -1;
		previews = new List<string>();
		previewIndex = 0;
		
		input.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<string>((str) => OnInputChange()));
		CommandList.setConsole(this);
	}

	/// <summary>
	/// Enables / Disables the console, and pauses the game.
	/// </summary>
	private void Toggle() {
		if (toggled) {
			for (int i = 0; i < gameObject.transform.childCount; ++i) {
				gameObject.transform.GetChild(i).gameObject.SetActive(false);
			}
			toggled = false;
			Game.time.SetGameSpeedInstant(1f);
			EventSystem.current.SetSelectedGameObject(null);
		}
		else {
			for (int i = 0; i < gameObject.transform.childCount; ++i) {
				gameObject.transform.GetChild(i).gameObject.SetActive(true);
			}
			toggled = true;
			Game.time.SetGameSpeedInstant(0f);
			input.Select();
		}
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
				input.text = "";
				ClearPreview();
			}
			else if (Event.current.keyCode == KeyCode.UpArrow) {
				scrollIndex = Utility.mod(scrollIndex - 1, lastInputs.Count + 1);
				SetTextFromScrolling(scrollIndex != lastInputs.Count ? lastInputs[scrollIndex] : "");
			}
			else if (Event.current.keyCode == KeyCode.DownArrow) {
				scrollIndex = Utility.mod(scrollIndex + 1, lastInputs.Count + 1);
				SetTextFromScrolling(scrollIndex != lastInputs.Count ? lastInputs[scrollIndex] : "");
			}
			else if (Event.current.keyCode == KeyCode.LeftArrow) {
				if (input.text.LastIndexOf(' ') == -1) input.text = "";
				else input.text = input.text.Remove(input.text.LastIndexOf(' '));
			}
			else if (Event.current.keyCode == KeyCode.RightArrow) {
				if (input.text.Split(' ').Length == 1)
					input.text = preview.text.Split(' ')[0];
			}
			else if (Event.current.keyCode == KeyCode.Tab) {
				if (previews.Count == 0) return;
				previewIndex = Utility.mod(previewIndex + 1, previews.Count);
				UpdatePreview();
			}
			else if (Event.current.keyCode == KeyCode.Escape) {
				input.text = "";
				EventSystem.current.SetSelectedGameObject(null); //Doit être utilisé, sinon il faut appuyer sur Entrée pour re-sélectionner input
				input.Select();
			}
		}
	}

	private void SetTextFromScrolling(string newText) {
		inputChangedByScrolling = input.text != newText;
		if (inputChangedByScrolling)
			input.text = newText;
	}

	/// <summary>
	/// Sets the output message.
	/// </summary>
	/// <param name="output">The new output message.</param>
	public void SetOutput(string output) {
		this.output.text = output;
	}

	/// <summary>
	/// Clears the preview
	/// </summary>
	void ClearPreview() {
		previewIndex = 0;
		if (input.text == "") previews.Clear();
		else previews = CommandList.getCommandPreview(input.text);
		UpdatePreview();
	}

	/// <summary>
	/// Updates the preview string from current index and the preview list.
	/// </summary>
	void UpdatePreview() {
		if (previews.Count == 0) preview.text = "";
		else preview.text = previews[previewIndex];
	}

	/// <summary>
	/// Triggered on input change from the keyboard. Clears scroll and preview informations.
	/// </summary>
	void OnInputChange() {
		input.text = Regex.Replace(input.text, @"[^a-zA-Z0-9 ,\.-]", "");
		input.text = Regex.Replace(input.text, @" +", " ");
		input.text = Regex.Replace(input.text, @"^ ", "");
		ClearPreview();
		if (inputChangedByScrolling) {
			inputChangedByScrolling = false;
		}
		else {
			scrollIndex = lastInputs.Count;
		}
		
	}

	/// <summary>
	/// Listens to events and acts accordingly.
	/// </summary>
	public void OnGUI() {
		if (PreventKeyRepeat()) return;
		input.caretPosition = input.text.Length;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Period) {
			Toggle();
		}
		if (!toggled) return;

		HandleSpecialKeys();

	}

	/// <summary>
	/// Handles the Return keypress.
	/// </summary>
	public void HandleInput() {
		output.text = "";
		lastInputs.Add(input.text);
		if (lastInputs.Count > 20) lastInputs.RemoveAt(0);
		scrollIndex = lastInputs.Count;
		try {
			CommandHandler.ExecuteCommand(input.text);
		} catch(CommandRuntimeException e) {
			output.text = e.Message;
		}
		ClearPreview();
		EventSystem.current.SetSelectedGameObject(null); //Doit être utilisé, sinon il faut appuyer sur Entrée pour re-sélectionner input
		input.Select();
	}
}
