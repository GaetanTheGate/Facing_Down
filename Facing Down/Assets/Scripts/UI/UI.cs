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

    public static GameObject map;

    private void Start() {
        inventoryDisplay = gameObject.GetComponentInChildren<InventoryDisplay>();
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        specialBar = gameObject.GetComponentInChildren<SpecialBar>();
        dashBar = gameObject.GetComponentInChildren<DashBar>();
        console = gameObject.GetComponentInChildren<Console>();
        map = transform.Find("Map").gameObject;
        itemPreview = gameObject.GetComponentInChildren<ItemPreview>();

        inventoryDisplay.gameObject.SetActive(false);
        map.SetActive(false);

        itemPreview.SetItem(new PrintItem());
    }

    /// <summary>
    /// Enables/Disables 
    /// </summary>
	private void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
            if (console.IsToggled()) {
                if (Event.current.keyCode == KeyCode.Escape) console.Toggle();
            }
            else if (inventoryDisplay.gameObject.activeSelf) {
                if (Event.current.keyCode == KeyCode.Escape) {
                    inventoryDisplay.gameObject.SetActive(false);
                    map.SetActive(false);
				}
            }
            else {
                if (Event.current.keyCode == KeyCode.C) console.Toggle();
                else if (Event.current.keyCode == KeyCode.E) {
                    inventoryDisplay.gameObject.SetActive(true);
                    map.SetActive(true);
				}
			}
		}
	}
}
