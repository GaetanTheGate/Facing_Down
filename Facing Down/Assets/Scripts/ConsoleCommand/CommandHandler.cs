using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandHandler
{
	/// <summary>
	/// Calls a console command from its string input.
	/// </summary>
	/// <param name="input">The string input.</param>
	/// <exception cref="CommandRuntimeException">Thrown if there is an error during the parsing or the execution.</exception>
	public static void ExecuteCommand(string input) {
		string[] splitInput = input.Split(' ');
		AbstractConsoleCommand command = CommandList.getCommand(splitInput[0], splitInput.Length - 1);
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
				int arg = getIntFromInput(splitInput[1]);
				(command as ConsoleCommand<int>).Invoke(arg);
				return;
			}
		}
		if (splitInput.Length == 3) {
			if ((command as ConsoleCommand<string, int>) != null) {
				string arg1 = splitInput[1];
				int arg2 = getIntFromInput(splitInput[2]);
				(command as ConsoleCommand<string, int>).Invoke(arg1, arg2);
				return;
			}
		}
	}

	private static int getIntFromInput(string input) {
		if (!int.TryParse(input, out int i)) {
			throw new CommandRuntimeException("Format invalid : \"" + input + "\" does not seem to be an integer.");
		}
		return i;
	}
}
