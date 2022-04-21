using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbstractConsoleCommand
{
    private string id;
    private string description;
    private string format;

    public string getID() {
        return id;
	}

    public string getDescription() {
        return description;
	}

    public string getFormat() {
        return format;
	}

    public AbstractConsoleCommand(string id, string description, string format) {
        this.id = id;
        this.description = description;
        this.format = format;
	}
}

public class ConsoleCommand : AbstractConsoleCommand
{
    private Action command;
    public ConsoleCommand(string id, string description, string format, Action command) : base(id, description, format) {
        this.command = command;
	}

    public void Invoke() {
        command.Invoke();
	}
}

public class ConsoleCommand<T> : AbstractConsoleCommand {
    private Action<T> command;
	public ConsoleCommand(string id, string description, string format, Action<T> command) : base(id, description, format) {
        this.command = command;
    }
    
    public void Invoke(T value) {
        command.Invoke(value);
	}
}

public class ConsoleCommand<T1, T2> : AbstractConsoleCommand {
    private Action<T1, T2> command;
	public ConsoleCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format) {
        this.command = command;
    }
    
    public void Invoke(T1 v1, T2 v2) {
        command.Invoke(v1, v2);
	}
}
