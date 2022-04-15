using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    private SpriteRenderer sp;
    private bool isTouchingTrap = false;
    [HideInInspector]
    public bool isIframeTrap = false;
    public Color naturalColor = new Color(1, 1, 1, 1);

    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        hpText.text = PlayerStatics.hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getIframe(float duration)
    {
        StartCoroutine(startIframeRoutine(duration));
    }

    public void getIframeTrap(float duration)
    {
        StartCoroutine(startIframeRoutineTrap(duration));
    }

    public void getHit(int damage, float iframeDuration = 2.0f)
    {
        PlayerStatics.hp -= damage;
        hpText.text = PlayerStatics.hp.ToString();
        getIframe(iframeDuration);
    }

    public void getHitTrap(int damage, float iframeDuration = 2.0f)
    {
        if (!isIframeTrap)
        {
            isIframeTrap = true;
            PlayerStatics.hp -= damage;
            Debug.Log("Hit! hp = " + PlayerStatics.hp);
            hpText.text = PlayerStatics.hp.ToString();
            isTouchingTrap = true;
            //getIframe(iframeDuration);
            getIframeTrap(iframeDuration);
        }
    }

    private IEnumerator startIframeRoutine(float duration)
    {
        //if (isTouchingTrap) isIframeTrap = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        int numberOfFlashes = (int)duration+1;
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
        Physics2D.IgnoreLayerCollision(10, 11, false);
        /*if (isTouchingTrap) 
        {
            isIframeTrap = false;
            isTouchingTrap = false;
        } */
    }

    private IEnumerator startIframeRoutineTrap(float duration)
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
        if (isTouchingTrap)
        {
            isIframeTrap = false;
            isTouchingTrap = false;
        }
    }

}
