using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhileFadingImage : MonoBehaviour
{
    public float timeSpan = 1.0f;
    private float timePassed = 0.0f;
    private UnityEngine.UI.Image image;
    public Color baseColor = Color.black;

    // Start is called before the first frame update
    void Awake()
    {
        timePassed = 0.0f;
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeSpan)
            Destroy(gameObject);

        image.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1.0f - (timePassed / timeSpan > 1 ? 1 : timePassed / timeSpan));

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f);
    }
}
