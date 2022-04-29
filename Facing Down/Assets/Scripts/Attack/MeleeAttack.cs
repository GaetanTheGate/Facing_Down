using UnityEngine;

public abstract class MeleeAttack : Attack
{

    public bool followEntity = true;
    public float lenght = 1.0f;
    public float range = 1.0f;

    protected Vector3 pos;

    protected override void ComputeAttack(float percentageTime)
    {
        if (followEntity)
            pos = new Vector3(src.transform.position.x, src.transform.position.y, src.transform.position.z);
        else
            pos = startPos;

        pos += Behaviour(percentageTime);

        transform.position = pos;
    }

    protected override void onStart()
    {
    }

    public abstract Vector3 Behaviour(float percentage);
}
