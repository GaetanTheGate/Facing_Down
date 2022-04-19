using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public int damage = 1;
    public List<string> tagsToHit = new List<string>();
    private Dictionary<GameObject, bool> entitiesHit = new Dictionary<GameObject, bool>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tagsToHit)
        {
            if (collision.CompareTag(tag))
            {
                if (!entitiesHit.ContainsKey(collision.gameObject))
                {
                    StatEntity statEntity = collision.GetComponent<StatEntity>();
                    statEntity.takeDamage(damage);
                    entitiesHit.Add(collision.gameObject, false);
                    waitForAttack(2.0f, collision.gameObject);
                }
                else if (entitiesHit[collision.gameObject])
                {
                    StatEntity statEntity = collision.GetComponent<StatEntity>();
                    statEntity.takeDamage(damage);
                    entitiesHit[collision.gameObject] = false;
                    waitForAttack(2.0f, collision.gameObject);
                }
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
