using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float gameSpeed = 1;
    private float targetGameSpeed = 1;

    public float coeffSpeedChange = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ComputeTimeSpeed();
    }

    private void ComputeTimeSpeed()
    {
        gameSpeed = Mathf.Lerp(gameSpeed, targetGameSpeed, coeffSpeedChange * Time.deltaTime);
        Time.timeScale = gameSpeed;
        Time.fixedDeltaTime = gameSpeed * 0.02f;
    }


    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void SetGameSpeed(float speed)
    {
        targetGameSpeed = speed;
    }

    public void SetGameSpeedInstant(float speed)
    {
        targetGameSpeed = speed;
        gameSpeed = targetGameSpeed;
    }
}
