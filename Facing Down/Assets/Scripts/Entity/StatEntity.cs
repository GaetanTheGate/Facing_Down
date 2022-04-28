using UnityEngine;
using UnityEngine.Events;

public class StatEntity : MonoBehaviour
{
    [Min(0)] public int maxHitPoints = 10;
    [HideInInspector] public int currentHitPoints;

    [Min(0.0f)] public float baseAtk = 100;
    [Min(0.0f)] public float atkMultipler = 1;
    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    public UnityEvent onHit;
    public UnityEvent onDeath;
    private Animator animator;

    public virtual void Awake()
    {
        computeAtk();
        currentHitPoints = maxHitPoints;
        animator = gameObject.GetComponent<Animator>();
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
    }

    public void computeAtk()
    {
        atk = baseAtk * atkMultipler;
    }

    public float getAtk() {
        return atk;
	}

    public void Heal(float amount) {
        currentHitPoints = Mathf.Max(maxHitPoints, currentHitPoints + Mathf.CeilToInt(amount));
	}

    public virtual void TakeDamage(DamageInfo dmgInfo)
    {
        currentHitPoints -= (int)dmgInfo.amount;
        GetComponent<Rigidbody2D>().velocity += dmgInfo.knockback.GetAsVector2();

        Debug.Log("entit้ : " + this.name + " hp = " + currentHitPoints);
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
        if(onHit != null && currentHitPoints > 0) onHit.Invoke();
        checkIfDead();
    }

    public virtual void checkIfDead() {
        if (onDeath != null && currentHitPoints <= 0) onDeath.Invoke();
    }
}
