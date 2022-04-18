using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhileFading : MonoBehaviour
{
    public float timeSpan = 1.0f;
    private float startTime = 0.0f;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.realtimeSinceStartup;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float timePassed = Time.realtimeSinceStartup - startTime;
        if (timePassed >= timeSpan)
            Destroy(gameObject);
        Color color = sprite.color;
        sprite.color = new Color(color.a, color.g, color.b, 1.0f - (timePassed / timeSpan > 1 ? 1 : timePassed / timeSpan));

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f);
    }
}
