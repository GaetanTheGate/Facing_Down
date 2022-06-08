using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgAttack : EnemyAttack
{
    public float bashRange = 3f;
    private StunShot stunShot;
    private Shield shieldForAttack;

    public override void Start()
    {
        base.Start();
        stunShot = new StunShot("Player");
        shieldForAttack = new Shield("Player");
    }

    public override void attackPlayer(Vector2 playerPosition)
    {
        if (timePassed >= delay && !isAttacking && canAttack)
        {
            isAttacking = true;
            if (Vector2.Distance(transform.position, playerPosition) <= bashRange) bash(playerPosition);
            else shoot(playerPosition);
        }
    }

    private void bash(Vector2 targetPosition)
    {
        Transform originalShield = gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").Find("Shield");

        shieldForAttack.startPos = originalShield.position;
        Attack attack = shieldForAttack.GetAttack(gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").localRotation.eulerAngles.z, gameObject.GetComponent<Entity>());
        attack.transform.localScale = new Vector3(attack.transform.localScale.x, Mathf.Abs(attack.transform.localScale.y) * originalShield.localScale.y > 0 ? 1 : -1, attack.transform.localScale.z);

        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(false);
        attack.startAttack();

        StartCoroutine(bashWait(1f));
    }

    private void shoot(Vector2 targetPosition)
    {
        float angle = Angles.AngleBetweenVector2(transform.position, targetPosition);
        Vector3 bulletPos = new Vector3(0, 0, 0);
        float radius = 1.1f;

        if (angle >= 0 && angle <= 90)
        {
            bulletPos.x = radius * Mathf.Cos((angle) * Mathf.Deg2Rad);
            bulletPos.y = radius * Mathf.Sin((angle) * Mathf.Deg2Rad);
        }
        else if (angle > 90 && angle <= 180)
        {
            bulletPos.x = radius * Mathf.Cos((180 - angle) * Mathf.Deg2Rad) * -1;
            bulletPos.y = radius * Mathf.Sin((180 - angle) * Mathf.Deg2Rad);
        }
        else if (angle < 0 && angle >= -90)
        {
            bulletPos.x = radius * Mathf.Cos((angle * -1) * Mathf.Deg2Rad);
            bulletPos.y = radius * Mathf.Sin((angle * -1) * Mathf.Deg2Rad) * -1;
        }
        else if (angle < -90 && angle >= -180)
        {
            bulletPos.x = radius * Mathf.Cos((180 - angle * -1) * Mathf.Deg2Rad) * -1;
            bulletPos.y = radius * Mathf.Sin((180 - angle * -1) * Mathf.Deg2Rad) * -1;
        }
        stunShot.startPos = new Vector3(transform.position.x + bulletPos.x, transform.position.y + bulletPos.y, transform.position.z);
        Attack attack = stunShot.GetAttack(angle, gameObject.GetComponent<Entity>());
        Physics2D.IgnoreCollision(attack.GetComponent<Collider2D>(), transform.Find("ShieldPivot_y").Find("ShieldPivot_z").Find("Shield").GetComponent<Collider2D>(), true);
        attack.startAttack();
        //stunShot.WeaponAttack(angle, gameObject.GetComponent<Entity>());
        StartCoroutine(shootWait(1f));
    }

    protected IEnumerator bashWait(float vulnerableDuration)
    {
        yield return new WaitForSeconds(vulnerableDuration);
        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(true);
        timePassed = 0f;
        isAttacking = false;
    }
    
    protected IEnumerator shootWait(float vulnerableDuration)
    {
        yield return new WaitForSeconds(vulnerableDuration);
        timePassed = 0f;
        isAttacking = false;
    }
}
