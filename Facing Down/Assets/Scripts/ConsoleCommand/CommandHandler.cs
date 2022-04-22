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
				int arg;
				if (!int.TryParse(splitInput[1], out arg)) {
					throw new CommandRuntimeException("Invalid format : \"" + splitInput[1] + "\" does not seem to be an integer.");
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
					throw new CommandRuntimeException("Format invalid : \"" + splitInput[2] + "\" does not seem to be an integer.");
				}
				(command as ConsoleCommand<string, int>).Invoke(arg1, arg2);
				return;
			}
		}
	}
}
