using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class StatEntity : MonoBehaviour
{
    [SerializeField] protected int maxHitPoints = 10;
    protected int currentHitPoints;

    public float atk;

    protected float critRate = 0;
    protected float critDmg = 150;

    public UnityEvent<DamageInfo> onHit;
    public UnityEvent onDeath;
    private Animator animator;

    private EnemyAttack enemyAttack;
    private EnemyMovement enemyMovement;
    
    protected bool isDead = false;

    public bool canTakeKnockBack = true;
    public bool canTakeDamage = true;

    public List<int> activeEffects;

    private bool hasHpAnimatorParameter = false;

    public void InitStats(int maxHP, float atk, float critRate = 0, float critDamage = 150) {
        this.maxHitPoints = maxHP;
        this.atk = atk;
        this.critRate = critRate;
        this.critDmg = critDamage;
	}

    public virtual void Start()
    {
        currentHitPoints = GetMaxHP();
        //UI.healthBar.UpdateHP();
        animator = gameObject.GetComponent<Animator>();
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == "hp") hasHpAnimatorParameter = true;
        }
        if (animator != null && hasHpAnimatorParameter) animator.SetFloat("hp", currentHitPoints);

        enemyAttack = gameObject.GetComponent<EnemyAttack>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();

        activeEffects = new List<int>();
    }

    public void ModifyCritRate(float amount) {
        critRate += amount;
	}

    public float GetCritRate() {
        return Mathf.Min(critRate, 100);
	}

    public void ModifyCritDamage(float amount) {
        critDmg += amount;
	}

    public float GetCritDamage() {
        return critDmg;
	}

    public virtual void Heal(float amount) {
        currentHitPoints = Mathf.Min(GetMaxHP(), currentHitPoints + Mathf.CeilToInt(amount));
	}

    public virtual void TakeDamage(DamageInfo dmgInfo)
    {
        if (isDead || (int)dmgInfo.amount == 0) return;

        if (dmgInfo.source == Game.player.self) dmgInfo = Game.player.inventory.OnDealDamage(dmgInfo);

        if (canTakeDamage)
        {
            currentHitPoints -= (int)dmgInfo.amount;
            Game.player.gameCamera.GetComponent<CameraManager>().Shake(0.1f, 0.1f);
            foreach (Effect effect in dmgInfo.effects)
            {
                if (activeEffects.Contains(effect.id)) continue;
                print("doesn't contain effect");
                effect.OnHit(dmgInfo);
            }
        }
        if(canTakeKnockBack) GetComponent<Rigidbody2D>().velocity += dmgInfo.knockback.GetAsVector2();
        if (animator != null && hasHpAnimatorParameter) animator.SetFloat("hp", currentHitPoints);
        if(canTakeDamage && onHit != null && currentHitPoints > 0) onHit.Invoke(dmgInfo);
        checkIfDead(dmgInfo);
    }

    public virtual void checkIfDead(DamageInfo lastDamageTaken) {
        if (onDeath != null && currentHitPoints <= 0) {
            if (lastDamageTaken != null && lastDamageTaken.source == Game.player.self) {
                Game.player.inventory.OnEnemyKill(gameObject.GetComponent<Entity>());
                if (Random.value > 0.5) {
                    Game.player.stat.Heal(Game.player.stat.GetRawMaxHP() / 100);
				}
                else {
                    Game.player.stat.ModifySpecialLeft(0.5f);
				}
            }
            onDeath.Invoke();
            isDead = true;
        }
    }

    public void ModifyAtk(float amount) {
        this.atk += amount;
	}

    public float getAtk() {
        return atk;
    }

    public virtual void ModifyMaxHP(int amount) {
        maxHitPoints += amount;
        if (GetMaxHP() <= 0) maxHitPoints = 1;
        if (GetMaxHP() < currentHitPoints) currentHitPoints = GetMaxHP();
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

    public virtual void Stun(bool shouldStun)
    {
        if (shouldStun)
        {
            if (enemyAttack != null) enemyAttack.canAttack = false;
            if (enemyMovement != null) enemyMovement.canMove = false;
        }
        else
        {
            if (enemyAttack != null) enemyAttack.canAttack = true;
            if (enemyMovement != null) enemyMovement.canMove = true;
        }
    }

    public float GetComputedDmg()
    {
        if (Random.Range(0f, 100f) <= critRate)
            return atk * critDmg / 100;
        else
            return atk;
    }
}
