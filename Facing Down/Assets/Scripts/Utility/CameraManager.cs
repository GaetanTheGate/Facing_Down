using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float zoomPercent = 100;
    public float baseZoom = 10;
    public float actualZoom;

    void Start()
    {
        baseZoom = gameObject.GetComponent<Camera>().orthographicSize;
        ComputeZoom();
    }

    private void ComputeZoom()
    {
        actualZoom = baseZoom * zoomPercent / 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, actualZoom, 10 * Time.fixedDeltaTime);
    }

    public void SetZoomPercent(float zoom)
    {
        zoomPercent = zoom;
        ComputeZoom();
    }

    public void SetBaseZoom(float zoom)
    {
        baseZoom = zoom;
        ComputeZoom();
    }

    public void Shake(float duration, float intensity) => StartCoroutine(ShakeCamera(duration, intensity));

    private IEnumerator ShakeCamera(float duration, float intensity)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-intensity, intensity);
            float y = Random.Range(-intensity, intensity);

            transform.position += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    public void Propulse(float angle, float duration, float intensity) => StartCoroutine(PropulseCamera(angle, duration, intensity));

    private IEnumerator PropulseCamera(float angle, float duration, float intensity)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float relativeDuration = (duration - Mathf.Abs(elapsed - duration / 2) * 2) / duration;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * intensity * relativeDuration;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * intensity * relativeDuration;

            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(x, y, 0), 2 * Time.deltaTime);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}
