using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour for the UI's ItemPreview
/// </summary>
public class ItemPreview : MonoBehaviour
{
    private Image itemIcon;
    private Text itemDescription;
    private Text itemName;
    private Item currentItem;

    public void Init()
    {
        print("1");
        itemIcon = transform.Find("Icon").GetComponent<Image>();
        itemDescription = transform.Find("Description").GetComponent<Text>();
        itemName = transform.Find("Name").GetComponent<Text>();
    }

    /// <summary>
    /// Sets the icon, name and description according to the items
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item) {
        print("2");
        currentItem = item;
        itemIcon.sprite = item.GetSprite(); 
        itemDescription.text = item.GetDescription();
        itemName.text = item.GetName();
	}

    public Item GetCurrentItem() {
        return currentItem;
	}
}
