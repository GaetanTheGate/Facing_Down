using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StunShotLightFlicker : MonoBehaviour
{
    Light2D shotLight;

    // Start is called before the first frame update
    void Start()
    {
        shotLight = gameObject.GetComponent<Light2D>();
        StartCoroutine(changeLight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator changeLight()
    {
        while (true)
        {
            shotLight.pointLightOuterRadius += 0.1f;
            shotLight.intensity += 0.1f;
            yield return new WaitForSeconds(0.4f);
            shotLight.pointLightOuterRadius -= 0.1f;
            shotLight.intensity -= 0.1f;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
