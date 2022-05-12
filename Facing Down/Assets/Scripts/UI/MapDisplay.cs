using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    private GameObject display;

    public void AddRoomDisplay(GameObject room) {
        room.transform.SetParent(display.transform);
	}

    public void Init() {
        display = transform.Find("Display").gameObject;
        Map.generateMap();
    }

    public void ResetMapDisplay() {
        for (int i = 0; i < transform.childCount; ++i) {
            Destroy(transform.GetChild(i));
		}
	}

    public void Enable() {
        display.SetActive(true);
    }

    public void Disable() {
        display.SetActive(false);
    }

    public bool IsEnabled() {
        return display.activeSelf;
    }
}
