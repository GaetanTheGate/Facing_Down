using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Hit : MonoBehaviour
{
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void getHit()
    {
        StartCoroutine(startBlinkRoutine());
    }

    private IEnumerator startBlinkRoutine()
    {
        sp.color = new Color(1f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sp.color = new Color(1, 1, 1, 1);
    }
}
