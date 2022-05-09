using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public int maxIcons;
    public int xOffset;

    private List<GameObject> dashIcons;
    private GameObject dashPlusIcon;

    private int currentMaxDashes;
    private int currentAvailableDashes;

    private GameObject dashIconPrefab;
    private GameObject dashPlusIconPrefab;

    private Color dashActiveColor;
    private Color dashInactiveColor;
    private Color dashPlusActiveColor;
    private Color dashPlusInactiveColor;

    /// <summary>
    /// Initializes the prefab, icons and sprites;
    /// </summary>
    void Start()
    {
        dashIconPrefab = Resources.Load<GameObject>("Prefabs/UI/Components/DashIcon");

        dashIcons = new List<GameObject>();
        for (int i = 0; i < maxIcons; ++i) {
            GameObject dashIcon = Instantiate<GameObject>(dashIconPrefab, transform);
            dashIcon.transform.localPosition = new Vector2(xOffset * i, 0);
            dashIcon.GetComponent<Image>().color = dashInactiveColor;
            dashIcon.SetActive(false);
            dashIcons.Add(dashIcon);
		}

        dashPlusIconPrefab = Resources.Load<GameObject>("Prefabs/UI/Components/DashPlusIcon");
        dashPlusIcon = Instantiate<GameObject>(dashPlusIconPrefab, transform);
        dashPlusIcon.transform.localPosition = new Vector2((maxIcons - 1) * xOffset,0);
        dashPlusIcon.GetComponent<Image>().color = dashPlusInactiveColor;
        dashPlusIcon.SetActive(false);

        dashActiveColor = new Color(0f, 0.9f, 0f);
        dashInactiveColor = new Color(0f, 0.4f, 0f);
        dashPlusActiveColor = new Color(1f, 1f, 1f);
        dashPlusInactiveColor = new Color(0.4f, 0.4f, 0.4f);

        currentAvailableDashes = 0;
        currentMaxDashes = 0;
    }

    public void UpdateDashes()
    {
        UpdateMaxDashes();
        UpdateAvailableDashes();
    }

    private void UpdateMaxDashes() {
        int newMaxDashes = Game.player.stat.GetMaxDashes();
        if (newMaxDashes > currentMaxDashes) {
            for (int i = Mathf.Max(currentMaxDashes, 0); i < Mathf.Min(newMaxDashes, maxIcons); ++i) {
                dashIcons[i].SetActive(true);
			}
            if (newMaxDashes > maxIcons)
                dashPlusIcon.SetActive(true);
		}
        else if (newMaxDashes < currentMaxDashes) {
            for (int i = Mathf.Max(newMaxDashes, 0); i < Mathf.Min(currentMaxDashes, maxIcons); ++i) {
                dashIcons[i].SetActive(false);
			}
            if (newMaxDashes <= maxIcons) {
                dashPlusIcon.SetActive(false);
			}
		}
        currentMaxDashes = newMaxDashes;
	}

    private void UpdateAvailableDashes() {
        int newAvailableDashes = Game.player.stat.GetRemainingDashes();
        if (newAvailableDashes > currentAvailableDashes) {
            for (int i = Mathf.Max(currentAvailableDashes, 0); i < Mathf.Min(newAvailableDashes, dashIcons.Count); ++i) {
                dashIcons[i].GetComponent<Image>().color = dashActiveColor;
            }
            if (newAvailableDashes > maxIcons)
                dashPlusIcon.GetComponent<Image>().color = dashPlusActiveColor;

        }
        else if (newAvailableDashes < currentAvailableDashes) {
            for (int i = Mathf.Max(newAvailableDashes, 0); i < Mathf.Min(currentAvailableDashes, dashIcons.Count); ++i) {
                dashIcons[i].GetComponent<Image>().color = dashInactiveColor;
            }
            if (newAvailableDashes <= maxIcons)
                dashPlusIcon.GetComponent<Image>().color = dashPlusInactiveColor;
        }
        currentAvailableDashes = newAvailableDashes;
    }
}
