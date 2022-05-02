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

    protected bool isDead = false;

    public virtual void Start()
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
        if (isDead) return;
        currentHitPoints -= (int)dmgInfo.amount;
        GetComponent<Rigidbody2D>().velocity += dmgInfo.knockback.GetAsVector2();
        Debug.Log("entité : " + this.name + " hp = " + currentHitPoints);
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
        if(onHit != null && currentHitPoints > 0) onHit.Invoke();
        checkIfDead(dmgInfo);
    }

    public virtual void checkIfDead(DamageInfo lastDamageTaken) {
        if (onDeath != null && currentHitPoints <= 0) {
            if (lastDamageTaken != null && lastDamageTaken.source == Game.player.self) {
                Game.player.inventory.OnEnemyKill(gameObject.GetComponent<Entity>());
            }
            onDeath.Invoke();
            isDead = true;
        }
    }

    public bool getIsDead() { return isDead; }
}
