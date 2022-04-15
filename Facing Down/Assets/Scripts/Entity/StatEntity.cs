using UnityEngine;

public class StatEntity : AbstractEntity
{
    [Min(0.0f)] public float baseAtk = 100;
    [Min(0.0f)] public float atkMultipler = 1;
    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    [Min(0.0f)] public float acceleration = 1;
    [Min(0.0f)] public float maxSpeed = 10;

    public override void Init()
    {
        computeAtk();
    }

    public void computeAtk()
    {
        atk = baseAtk * atkMultipler;
    }
}