using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public static InventoryDisplay inventoryDisplay;
    public static HealthBar healthBar;
    public static SpecialBar specialBar;
    public static DashBar dashBar;
    public static Console console;
    public static ItemPreview itemPreview;

    public static MapDisplay map;

    private static bool canToogle = true;

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
	private void Update() {
            if (GameController.checkIfkeyCodeIsReleased(Options.Get().dicoCommandsKeyBoard["toggleInventoryMap"]) || GameController.checkIfkeyCodeIsReleased(Options.Get().dicoCommandsController["toggleInventoryMap"]))
                canToogle = true; 

            if (console.IsToggled()) {
                if (GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsKeyBoard["closeConsole"]) || 
                    GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsController["closeConsole"]) ) {
                        console.Toggle();
                }
            }
            else if (inventoryDisplay.IsEnabled()) {
                if ((GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsKeyBoard["toggleInventoryMap"]) || 
                    GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsController["toggleInventoryMap"])) && canToogle) {
                        inventoryDisplay.Disable();
                        map.Disable();
                        LockCursor();
                        Game.time.SetGameSpeedInstant(1);
                        canToogle = false;
				}
            }
            else {
                
                if (GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsKeyBoard["openConsole"]) || 
                    GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsController["openConsole"]) ) {
                        console.Toggle();
                }
                else if ((GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsKeyBoard["toggleInventoryMap"]) || 
                        GameController.checkIfkeyCodeIsPressed(Options.Get().dicoCommandsController["toggleInventoryMap"] )) && canToogle){ 
                            inventoryDisplay.Enable();
                            map.Enable();
                            UnlockCursor();
                            Game.time.SetGameSpeedInstant(0);
                            canToogle = false;
                            if (ToggleSelectableObject.onController){
                                if(GameObject.Find("Inventory").transform.GetChild(0).transform.childCount > 0){
                                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Inventory").transform.GetChild(0).transform.GetChild(0).gameObject);
                                }
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
