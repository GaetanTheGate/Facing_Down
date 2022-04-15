using UnityEngine;

public class Entity : AbstractEntity
{
    public override void Init()
    {
        AbstractEntity entity;

        entity = gameObject.GetComponent<StatEntity>();
        if (entity == null)
        {
            entity = gameObject.AddComponent<StatEntity>();
            entity.Init();
        }

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.freezeRotation = true;

        entity = gameObject.GetComponent<GravityEntity>();
        if (entity == null)
        {
            entity = gameObject.AddComponent<GravityEntity>();
            entity.Init();
        }

        entity = gameObject.GetComponent<SpeedEntity>();
        if (entity == null)
        {
            entity = gameObject.AddComponent<SpeedEntity>();
            entity.Init();
        }

        entity = gameObject.GetComponent<RotationEntity>();
        if (entity == null)
        {
            entity = gameObject.AddComponent<RotationEntity>();
            entity.Init();
        }
    }
}
