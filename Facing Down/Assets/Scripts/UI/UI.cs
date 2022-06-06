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
    /*private void OnGUI() {
            if (console.IsToggled()) {
                if (GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsKeyBoard["closeUI"]) || 
                    GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsController["closeUI"]) ) {
                        console.Toggle();
                }
            }
            else if (inventoryDisplay.IsEnabled()) {
                if (GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsKeyBoard["closeUI"]) || 
                    GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsController["closeUI"])) {
                        inventoryDisplay.Disable();
                        map.Disable();
                        LockCursor();
                        Game.time.SetGameSpeedInstant(1);
				}
            }
            else {
                if (GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsKeyBoard["openConsole"]) || 
                    GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsController["openConsole"]) ) {
                        console.Toggle();
                }
                else if (GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsKeyBoard["openInventoryMap"]) || 
                        GameController.checkIfkeyCodeIsPressedOnGUI(Options.Get().dicoCommandsController["openInventoryMap"] )){
                            inventoryDisplay.Enable();
                            map.Enable();
                            UnlockCursor();
                            Game.time.SetGameSpeedInstant(0);
                }
            }
	}*/

    private void Update()
    {
        if (console.IsToggled())
        {
            if (GameController.checkIfkeyCodeIsPressed("Escape"))
            {
                console.Toggle();
            }
        }
        else if (inventoryDisplay.IsEnabled())
        {
            if (GameController.checkIfkeyCodeIsPressed("Escape"))
            {
                inventoryDisplay.Disable();
                map.Disable();
                LockCursor();
                Game.time.SetGameSpeedInstant(1);
            }
        }
        else
        {
            if (GameController.checkIfkeyCodeIsPressed("Console"))
            {
                console.Toggle();
            }
            else if (GameController.checkIfkeyCodeIsPressed("Inventory"))
            {
                inventoryDisplay.Enable();
                map.Enable();
                UnlockCursor();
                Game.time.SetGameSpeedInstant(0);
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
