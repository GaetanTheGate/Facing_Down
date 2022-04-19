using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugCommand 
{
    private string id;
    private string description;
    private string format;
    private Action<string> command;

    public string getID() {
        return id;
	}

    public string getDescription() {
        return description;
	}

    public string getFormat() {
        return format;
	}

    public DebugCommand(string id, string description, string format, Action<string> command) {
        this.id = id;
        this.description = description;
        this.format = format;
        this.command = command;
	}

    public void Invoke(string str) {
        command.Invoke(str);
	}
}
