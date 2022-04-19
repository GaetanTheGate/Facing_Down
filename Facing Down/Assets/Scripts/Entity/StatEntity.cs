using UnityEngine;

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

    [HideInInspector]
    public delegate void hitEvent(int damage);
    [HideInInspector]
    public static event hitEvent onHit;

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
        if (onHit != null) onHit(damage);
    }
}
