using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Vector3 startPos = new Vector3();
    protected float timePassed = 0.0f;
    protected bool isAttacking = false;

    public Color color = Color.white;
    public float angle = 0.0f;
    public float timeSpan = 1.0f;
    public float startDelay = 0.0f;
    public float endDelay = 0.0f;
    public Entity src;

    public float acceleration = 2.0f;

    public delegate void endAttackEvent(Entity self, float angle);
    public event endAttackEvent onEndAttack;

    void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    public void startAttack()
    {
        startPos = transform.position;
        timePassed = 0.0f;
        isAttacking = true;
        onStart();
        ComputeAttack(0.0f);


        GetComponent<SpriteRenderer>().color = color;
    }

    protected abstract void onStart();

    void FixedUpdate()
    {
        if (!isAttacking)
            return;

        timePassed += Time.fixedDeltaTime;
        if (timePassed - startDelay >= timeSpan + endDelay)
        {
            if (onEndAttack != null) onEndAttack(src, angle);
            Destroy(gameObject);
        }

        float percentageTime = (timePassed - startDelay) / timeSpan;
        percentageTime = percentageTime > 1 ? 1 : percentageTime;
        percentageTime = percentageTime < 0 ? 0 : percentageTime;

        ComputeAttack(Mathf.Pow(percentageTime, acceleration));
    }


    protected abstract void ComputeAttack(float percentageTime);
}
