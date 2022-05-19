using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputListener
{
    public void OnInputPressed();
    public void OnInputReleased();
    public void OnInputHeld();
    public void UpdateAfterInput();
}
