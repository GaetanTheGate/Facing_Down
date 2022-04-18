using UnityEngine;

public class PlayerMaterial : AbstractPlayer
{
    private StatEntity stat;
    private Entity self;

    [Range(0.0f, 1.0f)] public float highSpeedFriction = 0.1f;
    [Range(0.0f, 1.0f)] public float slowSpeedFriction = 0.6f;
    public override void Init()
    {
        self = gameObject.GetComponent<Player>().self;

        stat = self.gameObject.GetComponent<StatEntity>();
        if (stat == null)
            stat = self.gameObject.AddComponent<StatEntity>(); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeMaterial();
    }

    private void ComputeMaterial()
    {
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0;
        if (new Velocity(self.GetComponent<Rigidbody2D>().velocity).getSpeed() > stat.maxSpeed / 3)
            material.friction = highSpeedFriction;
        else
            material.friction = slowSpeedFriction;

        self.GetComponent<Collider2D>().sharedMaterial = material;
    }
}
