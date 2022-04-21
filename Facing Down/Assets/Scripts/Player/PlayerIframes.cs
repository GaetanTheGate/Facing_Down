using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerIframes : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    private SpriteRenderer sp;
    [HideInInspector]
    public bool isIframe = false;
    public Color naturalColor = new Color(1, 1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    public void getIframe(float duration)
    {
        isIframe = true;
        StartCoroutine(startIframeRoutine(duration));
    }

    private IEnumerator startIframeRoutine(float duration)
    {
        //if (isTouchingTrap) isIframeTrap = true;
        //Physics2D.IgnoreLayerCollision(10, 11, true);
        int numberOfFlashes = (int)duration + 1;
        float totalWait = duration;
        //Color oldColor = sp.color;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            if (totalWait >= 1.0f)
            {
                sp.color = new Color(1f, 0f, 0f, 0.5f);
                yield return new WaitForSeconds(0.5f);
                sp.color = naturalColor;
                yield return new WaitForSeconds(0.5f);
                totalWait -= 1.0f;
            }
            else if (totalWait > 0.0f)
            {
                sp.color = new Color(1f, 0f, 0f, 0.5f);
                yield return new WaitForSeconds(totalWait);
                sp.color = naturalColor;
            }
        }
        //Physics2D.IgnoreLayerCollision(10, 11, false);
        /*if (isTouchingTrap) 
        {
            isIframeTrap = false;
            isTouchingTrap = false;
        } */

        if (isIframe)
        {
            isIframe = false;
        }
    }
}
