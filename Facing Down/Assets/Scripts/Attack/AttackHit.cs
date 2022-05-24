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
        if (gameObject.GetComponent<Collider2D>().IsTouching(collision))
            ComputeAttack(collision, 1.0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.GetComponent<Collider2D>().IsTouching(collision))
            ComputeAttack(collision, 1.0f);
    }

    public void ComputeAttack(Collider2D collision, float dmgMultiplier)
    {
        foreach (string layer in layersToHit)
        {
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(layer)))
            {
                if (dmgInfo.isMelee && IsBlocked(dmgInfo.source.gameObject.transform.position, collision.transform.position))
                    continue;

                if (!entitiesHit.ContainsKey(collision.gameObject)) entitiesHit.Add(collision.gameObject, false);
                else if (entitiesHit[collision.gameObject]) entitiesHit[collision.gameObject] = false;
                else continue;
                StatEntity statEntity = collision.GetComponent<StatEntity>();
                
                DamageInfo damage = new DamageInfo(dmgInfo);
                damage.amount *= dmgMultiplier;
                damage.target = collision.GetComponent<Entity>();
                statEntity.TakeDamage(damage);
                waitForAttack(damage.hitCooldown, collision.gameObject);
            }
        }
    }

    public bool IsBlocked(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        Vector2 angle = end - start;

        RaycastHit2D result = Physics2D.Raycast(start, angle, distance, LayerMask.GetMask("Terrain"));
        Debug.DrawRay(start, angle, Color.red, 2.0f);
        if (result.collider == null)
            return false;

        return true;
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
