using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRuntimeException : System.Exception {
	public CommandRuntimeException(string message) : base(message) {

	}
}