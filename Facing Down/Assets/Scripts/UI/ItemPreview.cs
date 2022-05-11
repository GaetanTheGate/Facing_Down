using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    private Image itemIcon;
    private Text itemDescription;
    private Text itemName;
    private Item currentItem;

    void Start()
    {
        itemIcon = transform.Find("Icon").GetComponent<Image>();
        itemDescription = transform.Find("Description").GetComponent<Text>();
        itemName = transform.Find("Name").GetComponent<Text>();
    }

    public void SetItem(Item item) {
        currentItem = item;
        itemIcon.sprite = item.GetSprite();
        itemDescription.text = item.GetDescription();
        itemName.text = item.GetName();
	}

    public Item GetCurrentItem() {
        return currentItem;
	}
}
