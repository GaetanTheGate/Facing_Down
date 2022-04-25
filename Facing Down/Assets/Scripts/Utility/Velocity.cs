using UnityEngine;
public class Velocity
{
    private float speed;
    private float angleDeg;

    public Velocity(float speed, float angle)
    {
        this.speed = speed;
        this.angleDeg = angle;
        ModulateAngle();
    }

    public Velocity(Velocity velocity) : this(velocity.speed, velocity.angleDeg)
    {
    }
    
    public Velocity(Vector2 v)
    {
        FromVector2(v);
    }

    public Velocity() : this(0.0f, 0.0f)
    {
    }


    /// Start calculate with speed
    public Velocity AddToSpeed(float val)
    {
        speed += val;

        return this;
    }

    public Velocity SubToSpeed(float val)
    {
        speed -= val;

        return this;
    }

    public Velocity MulToSpeed(float val)
    {
        speed *= val;

        return this;
    }

    public Velocity DivToSpeed(float val)
    {
        speed /= val;

        return this;
    }
    /// End calculate with speed


    /// Start calculate with angle
    public Velocity AddToAngle(float val)
    {
        angleDeg += val;
        ModulateAngle();

        return this;
    }

    public Velocity SubToAngle(float val)
    {
        angleDeg -= val;
        ModulateAngle();

        return this;
    }

    public Velocity MulToAngle(float val)
    {
        angleDeg *= val;
        ModulateAngle();

        return this;
    }

    public Velocity DivToAngle(float val)
    {
        angleDeg /= val;
        ModulateAngle();

        return this;
    }
    /// End calculate with angle
    
    
    /// Start calculate with Velocity
    public Velocity Add(Velocity velocity)
    {
        FromVector2(velocity.GetAsVector2() + GetAsVector2());

        return this;
    }

    public Velocity Sub(Velocity velocity)
    {
        FromVector2(velocity.GetAsVector2() - GetAsVector2());

        return this;
    }

    public Velocity Mul(Velocity velocity)
    {
        FromVector2(velocity.GetAsVector2() * GetAsVector2());

        return this;
    }

    public Velocity Div(Velocity velocity)
    {
        FromVector2(velocity.GetAsVector2() / GetAsVector2());

        return this;
    }
    /// End calculate with Velocity

    public Vector2 GetAsVector2()
    {
        float x = Mathf.Cos(Mathf.Deg2Rad * angleDeg) * speed;
        float y = Mathf.Sin(Mathf.Deg2Rad * angleDeg) * speed;

        return new Vector2(x, y);
    }

    public Velocity FromVector2(Vector2 v)
    {
        speed = Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2));
        angleDeg = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        ModulateAngle();
        return this;
    }

    private void ModulateAngle()
    {
        angleDeg = (angleDeg % 360 + 360) % 360;
    }


    public float getSpeed()
    {
        return speed;
    }
    public float getAngle()
    {
        return angleDeg;
    }

    public Velocity setSpeed(float speed)
    {
        this.speed = speed;

        return this;
    }

    public Velocity setAngle(float angle)
    {
        this.angleDeg = angle;
        ModulateAngle();

        return this;
    }
}
