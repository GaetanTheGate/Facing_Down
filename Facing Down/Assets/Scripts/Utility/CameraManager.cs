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
        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, actualZoom, 0.5f);
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
}
