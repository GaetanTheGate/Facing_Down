using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    protected SpriteRenderer sp;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public virtual void getHit(DamageInfo dmgInfo)
    {
        StartCoroutine(startBlinkRoutine());
    }

    protected IEnumerator startBlinkRoutine()
    {
        sp.color = new Color(1f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(1, 1, 1, 1);
    }
}
