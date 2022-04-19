using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    bool toggled = false;
	string input = "";

	public static DebugCommand PRINT_DEBUG;
	public static DebugCommand PRINT_STR;
	public List<DebugCommand> commandList;

	private void toggle() {
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
		PRINT_DEBUG = new DebugCommand("print_debug", "Prints \"CONSOLE : Print Debug\" in the debug console", "print_debug", (str) => { Debug.Log("CONSOLE : Print Debug"); });
		PRINT_STR = new DebugCommand("print_str", "Prints \"CONSOLE : <str>\" in the debug console", "print_str <int>", (str) => { Debug.Log("CONSOLE : " + str); });

		commandList = new List<DebugCommand> { PRINT_DEBUG, PRINT_STR};
	}

	public void OnGUI() {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			if (toggled) HandleInput();
			toggle();
		}
		if (!toggled) return;

		GUI.Box(new Rect(0, Screen.height - 30f, Screen.width, 30f), "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		GUI.SetNextControlName("Console");
		input = GUI.TextField(new Rect(10f, Screen.height - 25f, Screen.width - 20f, 20f), input);
		GUI.FocusControl("Console");
	}

	public void HandleInput() {
		string[] command = input.Split(' ');
		for (int i = 0; i < commandList.Count; ++i) {
			if (command[0] == commandList[i].getID()) {
				if (command.Length == 1)
					commandList[i].Invoke("");
				if (command.Length == 2)
					commandList[i].Invoke(command[1]);
			}
		}
	}
}
