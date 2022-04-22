using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// Static class CommandList registers all the commands recognized by the console.
/// </summary>
public static class CommandList
{
	static Dictionary<string, Dictionary<int, AbstractConsoleCommand>> commandList;

	/// <summary>
	/// Registers all the commands.
	/// </summary>
	/// To add new commands, add a line there following the others' patterns. 
	/// If the command is not recognized, you may need to add a class in ConsoleCommand.cs and / or a handle method in Console.cs -> HandleInput().
	static CommandList() {
		commandList = new Dictionary<string, Dictionary<int, AbstractConsoleCommand>>();
		Add(new ConsoleCommand("printDebug", "Prints \"CONSOLE : DEBUG\" into Debug.Log.", "printDebug", () => { Debug.Log("CONSOLE : DEBUG"); }));
		Add(new ConsoleCommand<string>("printStr", "Prints \"CONSOLE : <str>\" into Debug.Log.", "printStr <str>", (ID) => { Debug.Log("CONSOLE : " + ID);}));
		Add(new ConsoleCommand<string>("addItem", "Adds 1 of the specified item to the inventory.", "addItem <ID>", (ID) => {AdvancedCommandFunctions.AddItem(ID, 1);}));
		Add(new ConsoleCommand<string, int>("addItem", "Adds <amount> of the specified item to the inventory.", "addItem <ID> <amount>", (ID, amount) => {AdvancedCommandFunctions.AddItem(ID, amount);}));
	}

	/// <summary>
	/// Registers a new command.
	/// </summary>
	/// <exception cref="System.Exception">Raised if a command with the same name and number of arguments has already been added.</exception>
	/// <param name="command">The command to register.</param>
	static void Add(AbstractConsoleCommand command) {
		if (!commandList.ContainsKey(command.getID()))
			commandList.Add(command.getID(), new Dictionary<int, AbstractConsoleCommand>());
		if (commandList[command.getID()].ContainsKey(command.getFormat().Split(' ').Length - 1)) throw new System.Exception("Can't register \"" + command.getFormat() + "\" : a command with the same name and number of args already exists.");
		commandList[command.getID()].Add(command.getFormat().Split(' ').Length - 1, command);
	}

	/// <summary>
	/// Gets a command from its id and its number of args. Sets errorMessage to get in case of an error.
	/// </summary>
	/// <param name="id">The command's id.</param>
	/// <param name="argCount">The number of params passed to the command.</param>
	/// <returns>The corresponding command.</returns>
	/// <exception cref="CommandRuntimeException">Thrown if no command is found.</exception>
	public static AbstractConsoleCommand getCommand(string id, int argCount) {
		if (!commandList.ContainsKey(id)) {
			throw new CommandRuntimeException("Command " + id + " not found.");
		}
		if (!commandList[id].ContainsKey(argCount)) {
			throw new CommandRuntimeException("Invalid number of arguments (" + argCount + ") for command " + id + ".");
		}
		return commandList[id][argCount];
	}

	/// <summary>
	/// Gets a list of the function formats starting by the given string.
	/// </summary>
	/// <example><c>GetCommandPreview("addIt");</c> may return a list containing "addItem ID", "addItem ID amount".</example>
	/// <param name="input">The string from which the function formats must be deduced.</param>
	/// <returns>A list of the previews, as strings.</returns>
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

	/// <summary>
	/// static class AdvancedCommandFunctions contains command functions to lighten their registration in CommandList.
	/// </summary>
	private static class AdvancedCommandFunctions {

		/// <summary>
		/// Adds items to the player inventory.
		/// </summary>
		/// <param name="ID">The item's ID.</param>
		/// <param name="amount">The amount of items to add.</param>
		/// <exception cref="CommandRuntimeException">Thrown if there is no item corresponding to the given ID.</exception>
		public static void AddItem(string ID, int amount) {
			Item item;
			if (ID == "PrintItem") item = new PrintItem();
			else item = ItemPool.GetByID(ID);
			if (item == null) throw new CommandRuntimeException("Item " + ID + " not found");
			item.setAmount(amount);
			Game.player.inventory.AddItem(item);
		}
	}
}
