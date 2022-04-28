using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public DamageInfo dmgInfo;

    public List<string> layersToHit = new List<string>();
    private Dictionary<GameObject, bool> entitiesHit = new Dictionary<GameObject, bool>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ComputeAttack(collision, 1.0f);
    }

    public void ComputeAttack(Collider2D collision, float dmgMultiplier)
    {
        foreach (string tag in layersToHit)
        {
            if (collision.CompareTag(tag))
            {
                if (!entitiesHit.ContainsKey(collision.gameObject)) entitiesHit.Add(collision.gameObject, false);
                else if (entitiesHit[collision.gameObject]) entitiesHit[collision.gameObject] = false;
                else continue;
                StatEntity statEntity = collision.GetComponent<StatEntity>();
                
                DamageInfo damage = new DamageInfo(dmgInfo);
                damage.amount *= dmgMultiplier;
                damage.target = collision.GetComponent<Entity>();
                statEntity.TakeDamage(damage);
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
