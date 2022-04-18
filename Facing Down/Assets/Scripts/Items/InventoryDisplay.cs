using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemDisplay itemDisplay;
    private Vector2 rootPosition = new Vector2(Screen.width / 20, Screen.height * 7 / 8);
    private int xOffset = Screen.width / 20;
    private int yOffset = Screen.width / 20;

    void Start()
    {
        Item a = new AttackMultUpItem();
        ItemDisplay i = Instantiate<ItemDisplay>(itemDisplay);
        i.transform.SetParent(this.transform);
        i.Init(new AttackMultUpItem());
        i.setPosition(rootPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
