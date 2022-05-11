using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    private Image itemIcon;
    private Text itemDescription;
    private Item currentItem;

    void Start()
    {
        itemIcon = GetComponentInChildren<Image>();
        itemDescription = GetComponentInChildren<Text>();
    }

    public void SetItem(Item item) {
        currentItem = item;
        itemIcon.sprite = item.GetSprite();
        itemDescription.text = item.GetDescription();
	}

    public Item GetCurrentItem() {
        return currentItem;
	}
}
