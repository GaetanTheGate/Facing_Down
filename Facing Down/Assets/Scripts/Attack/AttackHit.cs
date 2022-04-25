using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public float damage = 1;
    public List<string> tagsToHit = new List<string>();
    private Dictionary<GameObject, bool> entitiesHit = new Dictionary<GameObject, bool>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ComputeAttack(collision, 1.0f);
    }

    public void ComputeAttack(Collider2D collision, float dmgMultiplier)
    {
        foreach (string tag in tagsToHit)
        {
            if (collision.CompareTag(tag))
            {
                StatEntity statEntity = collision.GetComponent<StatEntity>();
                statEntity.takeDamage(damage * dmgMultiplier);
                if (!entitiesHit.ContainsKey(collision.gameObject)) entitiesHit.Add(collision.gameObject, false);
                else if (entitiesHit[collision.gameObject]) entitiesHit[collision.gameObject] = false;
                waitForAttack(2.0f, collision.gameObject);
            }
        }
    }

    public void waitForAttack(float duration, GameObject gameObject)
    {
        StartCoroutine(startAttackWaitingRoutine(duration, gameObject));
    }

    private IEnumerator startAttackWaitingRoutine(float duration, GameObject gameObject)
    {
        yield return new WaitForSeconds(duration);
        entitiesHit[gameObject] = true;
    }
}
