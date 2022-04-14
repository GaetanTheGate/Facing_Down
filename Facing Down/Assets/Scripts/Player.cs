using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    readonly Inventory Inventory = new Inventory();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Player = this;
        GameManager.Player.Inventory.AddItem(new PrintItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Inventory GetInventory() {
        return Inventory;
	}

}
