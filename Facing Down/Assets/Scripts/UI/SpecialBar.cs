using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    private List<GameObject> specialCharges;
    private GameObject specialChargePrefab;

    public int maxIcons;
    public int xOffset;

    private int currentMaxSpecial;
    private float currentSpecialLeft;

    void Start()
    {
        specialChargePrefab = Resources.Load<GameObject>("Prefabs/UI/SpecialCharge");
        specialCharges = new List<GameObject>();
        for (int i = 0; i < maxIcons; ++i) {
            GameObject specialCharge = Instantiate<GameObject>(specialChargePrefab, transform);
            specialCharge.transform.localPosition = new Vector2(xOffset * i, 0);
            specialCharges.Add(specialCharge);
        }
        currentMaxSpecial = maxIcons;
        currentSpecialLeft = maxIcons;
        UpdateSpecial();
    }

    public void UpdateSpecial() {
        if (currentMaxSpecial != Game.player.stat.maxSpecial)
            UpdateMaxSpecial();
        if (currentSpecialLeft != Game.player.stat.specialLeft) {
            UpdateSpecialLeft();
		}
	}

    private void UpdateMaxSpecial() {
        if (currentMaxSpecial > Game.player.stat.maxSpecial) {
            for (int i = Mathf.Max(0, Game.player.stat.maxSpecial); i < Mathf.Min(currentMaxSpecial, maxIcons); ++i) {
                specialCharges[i].SetActive(false);
            }
        }
        else {
            for (int i = Mathf.Max(0, currentMaxSpecial); i < Mathf.Min(Game.player.stat.maxSpecial, maxIcons); ++i) {
                specialCharges[i].SetActive(true);
            }
        }
        currentMaxSpecial = Game.player.stat.maxSpecial;
    }

    private void UpdateSpecialLeft() {
        float newSpecialLeft = Mathf.Min(Mathf.Max(Game.player.stat.specialLeft, 0), maxIcons);
        if (currentSpecialLeft > newSpecialLeft) {
            for (int i = Mathf.FloorToInt(newSpecialLeft); i < Mathf.CeilToInt(currentSpecialLeft); ++i) {
                if (i != Mathf.FloorToInt(newSpecialLeft)) {
                    specialCharges[i].transform.Find("SpecialFill").GetComponent<Image>().fillAmount = 0;
                }
                else {
                    specialCharges[i].transform.Find("SpecialFill").GetComponent<Image>().fillAmount = newSpecialLeft % 1;
                }
            }
		}
        else {
            for (int i = Mathf.FloorToInt(currentSpecialLeft); i < Mathf.CeilToInt(newSpecialLeft); ++i) {
                if (i != Mathf.FloorToInt(newSpecialLeft)) {
                    specialCharges[i].transform.Find("SpecialFill").GetComponent<Image>().fillAmount = 1;
                }
                else {
                    specialCharges[i].transform.Find("SpecialFill").GetComponent<Image>().fillAmount = newSpecialLeft % 1;
                }
            }
        }
        currentSpecialLeft = newSpecialLeft;

    }

	private void Update() {
        UpdateSpecial();
	}
}
