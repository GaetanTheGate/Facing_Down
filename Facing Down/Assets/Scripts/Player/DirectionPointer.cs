using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
    public bool toMouse = false;
    public float maxRadius = 1.2f;
    public Transform target;

    private float baseMaxRadius;
    private float angle = 0;
    private Vector2 nextPosition = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetScale = target.localScale;
        baseMaxRadius = Mathf.Max(targetScale.x, targetScale.y, targetScale.z);
        transform.localScale = new Vector3(baseMaxRadius, baseMaxRadius, baseMaxRadius);

        SetCursorState(false);

    }

    public void SetCursorState(bool state)
    {
        Cursor.visible = state;
        toMouse = state;
        if (state)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputePosition();
        ComputeRotation();
    }

    private void ComputeRotation()
    {
        angle = Mathf.Atan2(nextPosition.y, nextPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle);
    }

    private void ComputePosition()
    {
        if (toMouse)
            posToMouse();
        else
            posAsJoystick();

        ComputeMaxDistance();
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z - 1) + new Vector3(nextPosition.x, nextPosition.y, 0);
    }

    private void posToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        nextPosition = mousePos;
    }

    private void posAsJoystick()
    {
        nextPosition += Game.controller.getPointer();
    }

    private void ComputeMaxDistance()
    {

        float distance = Mathf.Sqrt(Mathf.Pow(nextPosition.x, 2) + Mathf.Pow(nextPosition.y, 2));
        if (distance > baseMaxRadius * maxRadius)
        {
            Vector2 vect = nextPosition.normalized;
            vect *= baseMaxRadius * maxRadius;
            nextPosition = vect;
        }
    }

    public float getAngle()
    {
        return angle;
    }
}
