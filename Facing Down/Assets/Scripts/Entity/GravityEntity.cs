using UnityEngine;

public class GravityEntity : MonoBehaviour
{
    private Rigidbody2D rb;
    private Entity self;

    // Test
    [Range(0.0f, 360.0f)] public float gravity_direction = 270;
    [Range(0.0f, 20.0f)] public float gravity_speed = 9.8f;

    public void Init()
    {

        self = gameObject.GetComponent<Entity>();
        if (self == null)
            self = gameObject.AddComponent<Entity>();

        self.gravity.setAngle(gravity_direction);
        gravity_direction = self.gravity.getAngle();

        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( ! rb.simulated)
            return;

        self.gravity.setAngle(gravity_direction);
        self.gravity.setSpeed(gravity_speed);

        ComputeGravity(Time.fixedDeltaTime);
    }

    private void ComputeGravity(float deltaTime)
    {
        rb.velocity += new Velocity(self.gravity).MulToSpeed(deltaTime).MulToSpeed(2).GetAsVector2();
    }
}
