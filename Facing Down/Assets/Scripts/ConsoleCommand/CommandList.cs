using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class CommandList
{
	static Dictionary<string, Dictionary<int, AbstractConsoleCommand>> commandList;
	static string errorMessage;
	static CommandList() {
		commandList = new Dictionary<string, Dictionary<int, AbstractConsoleCommand>>();
		add(new ConsoleCommand("print_debug", "Prints \"CONSOLE : DEBUG\" into Debug.Log.", "print_debug", () => { Debug.Log("CONSOLE : DEBUG"); }));
		add(new ConsoleCommand<string>("print_str", "Prints \"CONSOLE : <str>\" into Debug.Log.", "print_str <str>", (str) => { Debug.Log("CONSOLE : " + str);}));
	}

	static void add(AbstractConsoleCommand command) {
		if (!commandList.ContainsKey(command.getID()))
			commandList.Add(command.getID(), new Dictionary<int, AbstractConsoleCommand>());
		commandList[command.getID()].Add(command.getFormat().Split(' ').Length - 1, command);
	}

	public static AbstractConsoleCommand getCommand(string id, int argCount) {
		if (!commandList.ContainsKey(id)) {
			errorMessage = "Command " + id + " not found.";
			return null;
		}
		if (!commandList[id].ContainsKey(argCount)) {
			errorMessage = "Invalid number of arguments (" + argCount + ") for command " + id + ".";
			return null;
		}
		errorMessage = "No error, this should never appear.";
		return commandList[id][argCount];
	}

	public static string getErrorMessage() {
		return errorMessage;
	}

	public static List<string> getCommandPreview(string input) {
		List<string> previews = new List<string>();
		string[] splitInput = input.Split(' ');
		string inputID = splitInput[0];
		int inputArgCount = splitInput.Length - 1;
		//Checking all command IDs to find potential previews
		foreach (string commandId in commandList.Keys) {
			if (Regex.Match(commandId, @"^" + inputID + ".*").Success) {
				//Checking all command formats to find those with more args than the input
				foreach(int argCount in commandList[commandId].Keys) {
					if (argCount >= inputArgCount) {
						string preview = input;
						//Handling case where the function id must be previewed
						if (inputArgCount == 0) preview += commandList[commandId][argCount].getFormat().Substring(input.Length);
						//Handling case where arguments must be previewed
						else {
							int argNum = inputArgCount + 1;
							if (splitInput[inputArgCount] == "") argNum--;
							else preview += " ";
							for (; argNum <= argCount; ++argNum) {
								preview += commandList[commandId][argCount].getFormat().Split(' ')[argNum] + " ";
							}
						}
						previews.Add(preview);
					}
				}
			}
		}
		return previews;
	}
}
