using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKinematicBlocker : MonoBehaviour
{
    public Collider2D dynamicCollider;
    public Collider2D kinematicCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(dynamicCollider, kinematicCollider, true);
    }
}
