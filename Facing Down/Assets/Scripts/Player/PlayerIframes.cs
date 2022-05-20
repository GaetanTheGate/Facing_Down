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

    public void getIframeItem(float duration)
    {
        isIframe = true;
        StartCoroutine(startIframeItemRoutine(duration));
    }

    private IEnumerator startIframeRoutine(float duration)
    {
        int numberOfFlashes = (int)duration + 1;
        float totalWait = duration;
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

        if (isIframe)
        {
            isIframe = false;
        }
    }

    private IEnumerator startIframeItemRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isIframe = false;
    }
}
