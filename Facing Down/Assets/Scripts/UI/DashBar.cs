using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public int maxIcons;
    public int xOffset;

    private List<GameObject> dashIcons;
    private int currentAvailableDashes;

    private Sprite dashSprite;
    private Sprite dashPlusSprite;
    private GameObject dashIconPrefab;

    /// <summary>
    /// Initializes the prefab, icons and sprites;
    /// </summary>
    void Start()
    {
        dashIconPrefab = Resources.Load<GameObject>("Prefabs/UI/DashIcon");
        dashSprite = Resources.Load<Sprite>("UI/DashIcon");
        dashPlusSprite = Resources.Load<Sprite>("UI/DashPlusIcon");

        dashIcons = new List<GameObject>();
        for (int i = 0; i < maxIcons; ++i) {
            GameObject dashIcon = Instantiate<GameObject>(dashIconPrefab, transform);
            dashIcon.transform.localPosition = new Vector2(xOffset * i, 0);
            dashIcons.Add(dashIcon);
		}
    }

    void UpdateDashes()
    {
        int newAvailableDashes = Game.player.stat.maxDashes - Game.player.stat.numberOfDashes;
        if (newAvailableDashes > currentAvailableDashes) {
            for (int i = currentAvailableDashes; i < Mathf.Min(newAvailableDashes, dashIcons.Count); ++i) {
                dashIcons[i].SetActive(true);
			}
            if (newAvailableDashes > maxIcons)
                dashIcons[dashIcons.Count - 1].GetComponent<Image>().sprite = dashPlusSprite;

        }
        else if (newAvailableDashes < currentAvailableDashes) {
            for (int i = newAvailableDashes; i < Mathf.Min(currentAvailableDashes, dashIcons.Count); ++i) {
                dashIcons[i].SetActive(false);
			}
            if (newAvailableDashes <= maxIcons)
                dashIcons[dashIcons.Count - 1].GetComponent<Image>().sprite = dashSprite;
        }
        currentAvailableDashes = newAvailableDashes;
    }

	private void Update() {
        UpdateDashes();
	}
}
