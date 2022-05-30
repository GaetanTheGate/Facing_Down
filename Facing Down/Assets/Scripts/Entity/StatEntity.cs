using UnityEngine;
using UnityEngine.Events;

public class StatEntity : MonoBehaviour
{
    [SerializeField] protected int maxHitPoints = 10;
    protected int currentHitPoints;

    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    public UnityEvent<DamageInfo> onHit;
    public UnityEvent onDeath;
    private Animator animator;
    
    protected bool isDead = false;

    public bool canTakeKnockBack = true;
    public bool canTakeDamage = true;

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
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
    }

    public void Heal(float amount) {
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
        }
        if(canTakeKnockBack) GetComponent<Rigidbody2D>().velocity += dmgInfo.knockback.GetAsVector2();
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
        if(canTakeDamage && onHit != null && currentHitPoints > 0) onHit.Invoke(dmgInfo);
        checkIfDead(dmgInfo);
    }

    public virtual void checkIfDead(DamageInfo lastDamageTaken) {
        if (onDeath != null && currentHitPoints <= 0) {
            if (lastDamageTaken != null && lastDamageTaken.source == Game.player.self) {
                Game.player.inventory.OnEnemyKill(gameObject.GetComponent<Entity>());
                if (Random.value > 0.5) {
                    Game.player.stat.Heal(Game.player.stat.GetMaxHP() / 100);
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
}
