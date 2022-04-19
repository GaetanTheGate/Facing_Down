using UnityEngine;

public class Attack : MonoBehaviour
{
    private Vector3 startPos;
    public bool followEntity = true;

    public Entity src;
    public float angle;
    private float timePassed = 0.0f;
    public float timeSpan = 1.0f;
    public float lenght = 1.0f;

    public float range = 1.0f;
    public Way behaviour = Way.Direct;

    public Color color = Color.white;
    [SerializeField]
    private bool isAttacking = false;

    private float radius;

    void FixedUpdate()
    {
        if ( !isAttacking)
            return;
        timePassed += Time.fixedDeltaTime;
        if (timePassed >= timeSpan)
        {
            Destroy(gameObject);
        }



        float percentageTime = timePassed / timeSpan ;
        percentageTime = percentageTime > 1 ? 1 : percentageTime;

        Vector3 pos;
        if (followEntity)
            pos = new Vector3(src.transform.position.x, src.transform.position.y, src.transform.position.z);
        else
            pos = startPos;

        if (behaviour == Way.Direct)
        {
            float relativeRange = range * percentageTime;
            transform.localScale = new Vector3(relativeRange, transform.localScale.y, transform.localScale.z);

            radius = lenght/360 * percentageTime * 2 + relativeRange / 2;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            pos.x += radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            pos.y += radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        }
        else
        {
            percentageTime -= 0.5f;

            float relativeRange = range * 2.0f * ( 0.5f - Mathf.Abs(percentageTime) );

            radius = Mathf.Max(src.transform.localScale.x, src.transform.localScale.y, src.transform.localScale.z) / 2 + relativeRange / 2.0f;
            transform.localScale = new Vector3(relativeRange, transform.localScale.y, transform.localScale.z);

            float relativeAngle;
            if(behaviour == Way.CounterClockwise)
                relativeAngle = lenght * percentageTime ;
            else
                relativeAngle = lenght * - percentageTime;

            relativeAngle += angle;

            transform.rotation = Quaternion.Euler(0, 0, relativeAngle);
            pos.x += radius * Mathf.Cos(relativeAngle * Mathf.Deg2Rad);
            pos.y += radius * Mathf.Sin(relativeAngle * Mathf.Deg2Rad);
        }

        transform.position = pos;
    }

    public void startAttack()
    {
        startPos = transform.position;
        GetComponent<SpriteRenderer>().color = color;
        GetComponent<TrailEffect>().timeSpan = 0.2f;
        transform.localScale = new Vector3(range, transform.localScale.y, transform.localScale.z);
        radius = Mathf.Max(src.transform.localScale.x, src.transform.localScale.y, src.transform.localScale.z) / 2 + range / 2.0f;

        isAttacking = true;
    }


    public enum Way
    {
        CounterClockwise, Clockwise, Direct
    }
}
