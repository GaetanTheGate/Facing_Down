using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static InventoryDisplay inventoryDisplay;
    public static HealthBar healthBar;
    public static SpecialBar specialBar;
    public static DashBar dashBar;
    public static Console console;
    public static ItemPreview itemPreview;

    public static MapDisplay map;

    private void Start() {
        Init();
    }

    public static void Init()
    {
        GameObject gameObject = UnityEngine.Object.FindObjectsOfType<UI>()[0].gameObject;
        if (inventoryDisplay == null)
            inventoryDisplay = gameObject.GetComponentInChildren<InventoryDisplay>();
        if (healthBar == null)
            healthBar = gameObject.GetComponentInChildren<HealthBar>();
        if (specialBar == null)
            specialBar = gameObject.GetComponentInChildren<SpecialBar>();
        if (dashBar == null)
            dashBar = gameObject.GetComponentInChildren<DashBar>();
        if (console == null)
            console = gameObject.GetComponentInChildren<Console>();
        if (map == null)
            map = gameObject.GetComponentInChildren<MapDisplay>();
        if (itemPreview == null)
            itemPreview = gameObject.GetComponentInChildren<ItemPreview>();


        inventoryDisplay.Init();
        inventoryDisplay.Disable();
        healthBar.Init();
        specialBar.Init();
        dashBar.Init();
        map.Init();
        map.Disable();

        itemPreview.Init();
        itemPreview.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enables/Disables UI elements depending on user input
    /// </summary>
	private void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
            if (console.IsToggled()) {
                if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["closeUI"]) {
                    console.Toggle();
                }
            }
            else if (inventoryDisplay.IsEnabled()) {
                if (Event.current.keyCode == KeyCode.Escape) {
                    inventoryDisplay.Disable();
                    map.Disable();
                    LockCursor();
                    Game.time.SetGameSpeedInstant(1);
				}
            }
            else {
                if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["openConsole"]) {
                    console.Toggle();
                }
                else if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["openInventoryMap"]) {
                    inventoryDisplay.Enable();
                    map.Enable();
                    UnlockCursor();
                    Game.time.SetGameSpeedInstant(0);
                }
            }
		}
	}

    private void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    private void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
