using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private Vector3 startPos = new Vector3();
    private float timePassed = 0.0f;
    private bool isAttacking = false;

    public bool followEntity = true;

    public float timeSpan = 0.1f;
    public Entity src;
    public float angle = 0.0f;
    public float lenght = 1.0f;
    public float range = 1.0f;
    public Color color = Color.white;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    void FixedUpdate()
    {
        if ( !isAttacking)
            return;
        timePassed += Time.fixedDeltaTime;
        if (timePassed >= timeSpan)
            Destroy(gameObject);


        float percentageTime = timePassed / timeSpan ;
        percentageTime = percentageTime > 1 ? 1 : percentageTime;

        Vector3 pos;
        if (followEntity)
            pos = new Vector3(src.transform.position.x, src.transform.position.y, src.transform.position.z);
        else
            pos = startPos;
        
        pos += Behaviour(percentageTime);

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
