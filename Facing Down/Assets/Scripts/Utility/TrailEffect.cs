using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public float timeSpan = 1.0f;
    public float timeGap = 0.0f;
    private float timePassed = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed += Time.fixedDeltaTime;
        if (timePassed >= timeGap)
        {
            GameObject self = Instantiate(gameObject);
            self.SetActive(false);
            foreach (Component component in self.GetComponents(typeof(Component))){
                if (component.GetType() == typeof(Transform))
                    continue;
                if (component.GetType() == typeof(SpriteRenderer))
                    continue;
                
                Destroy(component);
            }
            foreach (Component child in self.GetComponentsInChildren(typeof(Transform)))
            {
                if (child.gameObject.Equals(self.gameObject))
                    continue;
                /*if (child.GetType() == typeof(Transform))
                    continue;
                if (child.GetType() == typeof(SpriteRenderer))
                    continue;*/
                Destroy(child.gameObject);
            }
            self.AddComponent<DestroyWhileFading>();
            self.GetComponent<DestroyWhileFading>().timeSpan = timeSpan;

            self.transform.SetParent(transform.parent);
            timePassed = 0.0f;

            self.SetActive(true);
        }
    }
}
