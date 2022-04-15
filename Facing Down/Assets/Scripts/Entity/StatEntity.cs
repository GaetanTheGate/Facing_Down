using UnityEngine;

public class StatEntity : MonoBehaviour
{
    [Min(0.0f)] public float baseAtk = 100;
    [Min(0.0f)] public float atkMultipler = 1;
    private float atk;

    [Min(0.0f)] public float critRate = 5;
    [Min(100.0f)] public float critDmg = 150;

    [Min(0.0f)] public float acceleration = 1;
    [Min(0.0f)] public float maxSpeed = 10;

    public void Init()
    {
        computeAtk();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void computeAtk()
    {
        atk = baseAtk * atkMultipler;
    }
}
