using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    private List<GameObject> specialCharges;
    private GameObject specialChargePlus;
    private GameObject specialChargePrefab;

    private Sprite specialFill;
    private Sprite specialPlusFill;

    public int maxIcons;
    public int xOffset;

    private int currentMaxSpecial;
    private float currentSpecialLeft;

    public void Init()
    {
        specialChargePrefab = Resources.Load<GameObject>("Prefabs/UI/Components/SpecialCharge");
        
        specialCharges = new List<GameObject>();
        for (int i = 0; i < maxIcons; ++i)
        {
            GameObject specialCharge = Instantiate<GameObject>(specialChargePrefab, transform);
            specialCharge.transform.localPosition = new Vector2(xOffset * i, 0);
            specialCharges.Add(specialCharge);
        }

        if(specialChargePlus == null)
        {
            specialChargePlus = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Components/SpecialChargePlus"), transform);
            specialChargePlus.transform.localPosition = new Vector2(maxIcons * xOffset, 0);
        }

        currentMaxSpecial = maxIcons;
        currentSpecialLeft = maxIcons;
    }

    public void UpdateSpecial() {
        if (currentMaxSpecial != Game.player.stat.GetMaxSpecial())
            UpdateMaxSpecial();
        if (currentSpecialLeft != Game.player.stat.GetSpecialLeft()) {
            UpdateSpecialLeft();
		}
	}

    private void UpdateMaxSpecial() {
        Debug.Log("MAX SPECIAL UPDATED FROM " + currentMaxSpecial + " TO " + Game.player.stat.GetMaxSpecial());
        int newMaxSpecial = Game.player.stat.GetMaxSpecial();
        if (currentMaxSpecial > newMaxSpecial) {
            for (int i = Mathf.Max(0, newMaxSpecial); i < Mathf.Min(currentMaxSpecial, maxIcons); ++i) {
                specialCharges[i].SetActive(false);
            }
        }
        else {
            for (int i = Mathf.Max(0, currentMaxSpecial); i < Mathf.Min(newMaxSpecial, maxIcons); ++i) {
                specialCharges[i].SetActive(true);
            }
        }

        if (newMaxSpecial > maxIcons) specialChargePlus.SetActive(true);
        else specialChargePlus.SetActive(false);

        currentMaxSpecial = newMaxSpecial;
    }

    private void UpdateSpecialLeft() {
        float newSpecialLeft = Mathf.Min(Mathf.Max(Game.player.stat.GetSpecialLeft(), 0), maxIcons);
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

        if (Game.player.stat.GetSpecialLeft() >= maxIcons + 1)
        specialChargePlus.transform.Find("SpecialPlusFill").gameObject.SetActive(true);
        else specialChargePlus.transform.Find("SpecialPlusFill").gameObject.SetActive(false);

        currentSpecialLeft = newSpecialLeft;
    }
}
