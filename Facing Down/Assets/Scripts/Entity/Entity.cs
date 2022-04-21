using UnityEngine;

public class Entity : AbstractEntity
{

    public static Rigidbody2D initRigidBody(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            if (rb.CompareTag("Player"))
            {
                rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
            else if (rb.CompareTag("Enemy"))
            {
                rb.mass = 10;
            }
        }
        return rb;
    }

    public override void Init()
    {
        AbstractEntity entity;

        StatEntity statEntity = gameObject.GetComponent<StatEntity>();
        if (statEntity == null)
        {
            statEntity = gameObject.AddComponent<StatEntity>();
        }

        initRigidBody(gameObject);


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

        entity = gameObject.GetComponent<EntityCollisionStructure>();
        if (entity == null)
        {
            entity = gameObject.AddComponent<EntityCollisionStructure>();
            entity.Init();
        }
    }
}
