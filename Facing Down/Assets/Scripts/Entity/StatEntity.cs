using UnityEngine;
using UnityEngine.Events;

public class StatEntity : MonoBehaviour
{
    [Min(0.0f)] public float maxHitPoints = 10;
    [HideInInspector] public float currentHitPoints;

    [Min(0.0f)] public float baseAtk = 100;
    [Min(0.0f)] public float atkMultipler = 1;
    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    [Min(0.0f)] public float acceleration = 1;
    [Min(0.0f)] public float maxSpeed = 10;

    public UnityEvent onHit;
    public UnityEvent onDeath;
    private Animator animator;

    public void Start()
    {
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

    public void takeDamage(float damage)
    {
        damage = Game.player.inventory.OnTakeDamage(damage);
        currentHitPoints -= damage;
        Debug.Log("entité : " + this.name + " hp = " + currentHitPoints);
        if (animator != null) animator.SetFloat("hp", currentHitPoints);
        if(onHit != null && currentHitPoints > 0) onHit.Invoke();
        if (onDeath != null && currentHitPoints <= 0) onDeath.Invoke();
    }
}
