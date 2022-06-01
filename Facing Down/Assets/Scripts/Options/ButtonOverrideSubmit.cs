using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonOverrideSubmit : Button
{
    public override void OnSubmit(BaseEventData eventData)
    {
        GetComponentInParent<ButtonChangeCommand>().submitUp();
    }
}
