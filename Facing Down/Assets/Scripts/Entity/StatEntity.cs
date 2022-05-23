using UnityEngine;
using UnityEngine.Events;

public class StatEntity : MonoBehaviour
{
    [SerializeField] protected int maxHitPoints = 10;
    protected int currentHitPoints;

    [Min(0.0f)] public float baseAtk = 100;
    [Min(0.0f)] public float atkMultipler = 1;
    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    public UnityEvent<DamageInfo> onHit;
    public UnityEvent onDeath;
    private Animator animator;

    protected bool isDead = false;

    public bool canTakeKnockBack = true;

    public void InitStats(int maxHP, float atk, float critRate = 0, float critDamage = 150) {
        this.maxHitPoints = maxHP;
        this.baseAtk = atk;
        this.critRate = critRate;
        this.critDmg = critDamage;
	}

    public virtual void Start()
    {
        computeAtk();
        currentHitPoints = GetMaxHP();
        UI.healthBar.UpdateHP();
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
        if (isDead || (int)dmgInfo.amount == 0) return;
        currentHitPoints -= (int)dmgInfo.amount;
        if(canTakeKnockBack) GetComponent<Rigidbody2D>().velocity += dmgInfo.knockback.GetAsVector2();
        //Debug.Log("entité : " + this.name + " hp = " + currentHitPoints);
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
        if(onHit != null && currentHitPoints > 0) onHit.Invoke(dmgInfo);
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

    public virtual void ModifyMaxHP(int amount) {
        maxHitPoints += amount;
        if (maxHitPoints <= 0) maxHitPoints = 1;
        if (maxHitPoints < currentHitPoints) currentHitPoints = maxHitPoints;
	}

    public virtual int GetMaxHP() {
        return maxHitPoints;
	}

    public int GetCurrentHP() {
        return currentHitPoints;
	}

    public virtual void SetCurrentHP(int HP) {
        currentHitPoints = Mathf.Min(HP, GetMaxHP());
    }

    public bool getIsDead() { return isDead; }
}
