using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    private Image itemIcon;
    private Text itemDescription;

    void Start()
    {
        itemIcon = GetComponentInChildren<Image>();
        itemDescription = GetComponentInChildren<Text>();
    }

    public void SetItem(Item item) {
        itemIcon.sprite = item.GetSprite();
        itemDescription.text = item.GetDescription();
	}
}
