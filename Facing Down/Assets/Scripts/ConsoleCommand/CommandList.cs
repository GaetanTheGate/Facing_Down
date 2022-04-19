using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
