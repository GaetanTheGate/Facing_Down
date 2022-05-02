using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ItemDisplay manages a single item's display.
/// </summary>
public class ItemDisplay : MonoBehaviour {
    public static readonly string spriteFolderPath = "Items/Sprites/";
    static readonly float imageSize = Screen.width / 20;
    Item item;

    /// <summary>
    /// Initializes the values from an item.
    /// </summary>
    /// <param name="item">The item to be displayed.</param>
    public void Init(Item item) {
        this.item = item;
        transform.position = new Vector3(150, 150, 0);

        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(spriteFolderPath + item.GetID());
        GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
        GetComponentInChildren<Text>().text = item.GetAmount().ToString();
        GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
    }

    /// <summary>
    /// Sets the display's position.
    /// </summary>
    /// <param name="pos">The display's position.</param>
    public void SetPosition(Vector2 pos) {
        GetComponentInChildren<Image>().rectTransform.position = pos;
        GetComponentInChildren<Text>().rectTransform.position = pos;
    }

    /// <summary>
    /// Update the display value from its item.
    /// </summary>
    public void UpdateDisplay() {
        GetComponentInChildren<Text>().text = item.GetAmount().ToString();
    }
}
