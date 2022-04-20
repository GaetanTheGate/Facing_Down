using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private Vector3 startPos = new Vector3();
    private float timePassed = 0.0f;
    private bool isAttacking = false;

    public bool followEntity = true;

    public float timeSpan = 0.2f;
    public float startDelay = 0.0f;
    public float endDelay = 0.0f;
    public Entity src;
    public float angle = 0.0f;
    public float lenght = 1.0f;
    public float range = 1.0f;
    public Color color = Color.white;

    public float acceleration = 2.0f;

    public delegate void endAttackEvent(Entity self, float angle);
    public event endAttackEvent onEndAttack;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    void FixedUpdate()
    {
        if ( !isAttacking)
            return;

        timePassed += Time.fixedDeltaTime;
        if (timePassed - startDelay >= timeSpan + endDelay)
        {
            if (onEndAttack != null) onEndAttack(src, angle);
            Destroy(gameObject);
        }

        float percentageTime = (timePassed - startDelay) / timeSpan ;
        percentageTime = percentageTime > 1 ? 1 : percentageTime;
        percentageTime = percentageTime < 0 ? 0 : percentageTime;

        Vector3 pos;
        if (followEntity)
            pos = new Vector3(src.transform.position.x, src.transform.position.y, src.transform.position.z);
        else
            pos = startPos;
        
        pos += Behaviour(Mathf.Pow(percentageTime, acceleration));

        transform.position = pos;
    }

    public void startAttack()
    {
        startPos = transform.position;
        timePassed = 0.0f;
        isAttacking = true;


        Vector3 pos;
        if (followEntity)
            pos = new Vector3(src.transform.position.x, src.transform.position.y, src.transform.position.z);
        else
            pos = startPos;

        pos += Behaviour(0);

        transform.position = pos;


        GetComponent<SpriteRenderer>().color = color;
    }

    public abstract Vector3 Behaviour(float percentage);
}
