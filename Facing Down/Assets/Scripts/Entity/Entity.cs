using UnityEngine;

public class Entity : MonoBehaviour
{

    public Velocity gravity = new Velocity(9.8f, 270);

    private Quaternion rotation = new Quaternion();
    private Rigidbody2D rb;

    public void Init()
    {
        Component component;
        
        component = gameObject.GetComponent<StatEntity>();
        if (component == null)
        {
            gameObject.AddComponent<StatEntity>();
            gameObject.GetComponent<StatEntity>().Init();
        }

        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.freezeRotation = true;

        component = gameObject.GetComponent<GravityEntity>();
        if (component == null)
        {
            gameObject.AddComponent<GravityEntity>();
            gameObject.GetComponent<GravityEntity>().Init();
        }

        component = gameObject.GetComponent<SpeedEntity>();
        if (component == null)
        {
            gameObject.AddComponent<SpeedEntity>();
            gameObject.GetComponent<SpeedEntity>().Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (!rb.simulated)
            return;

        ComputeGameSpeed();

        SetRotationTowardGravity();
        ComputeRotation(Time.fixedDeltaTime);
    }

    private void ComputeGameSpeed()
    {
        //Velocity rbVelo = new Velocity(rb.velocity).DivToSpeed(Game.GetGameSpeed());
        //rb.velocity = new Velocity(velocity).MulToSpeed(Game.GetGameSpeed()).GetAsVector2();
        //velocity = rbVelo;
    }

    private void SetRotationTowardGravity()
    {
        rotation = Quaternion.Euler(0.0f, 0.0f, new Velocity(gravity).SubToAngle(270).getAngle());
    }

    private void ComputeRotation(float deltaTime)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * deltaTime);
    }

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }
}
