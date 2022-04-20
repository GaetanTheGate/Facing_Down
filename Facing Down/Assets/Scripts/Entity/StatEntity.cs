using UnityEngine;
using UnityEngine.Events;

public class StatEntity : MonoBehaviour
{
    [Min(0.0f)] public int hitPoints = 10;

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
        animator = gameObject.GetComponent<Animator>();
        if (animator != null) animator.SetFloat("hp", hitPoints);
    }

    public void computeAtk()
    {
        atk = baseAtk * atkMultipler;
    }

    public float getAtk() {
        return atk;
	}

    public void takeDamage(int damage)
    {
        hitPoints -= damage;
        Debug.Log("entité : " + this.name + " hp = " + hitPoints);
        if (animator != null) animator.SetFloat("hp", hitPoints);
        if(onHit != null) onHit.Invoke();
        if (onDeath != null && hitPoints <= 0) onDeath.Invoke();
    }
}
