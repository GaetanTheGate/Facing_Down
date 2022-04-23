using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    public Katana()
    {
        baseAtk = 100;
        baseRange = 3;
        baseLenght = 180;
        baseSpan = 0.2f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Weapons/Katana";
        specialPath = "Prefabs/Weapons/KatanaDash";
    }

    private SlashAttack.Way way = SlashAttack.Way.CounterClockwise;

    public override void Attack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<SlashAttack>();

        swing.GetComponent<SlashAttack>().src = self;
        swing.GetComponent<SlashAttack>().acceleration = 3.0f;
        swing.GetComponent<SlashAttack>().angle = angle;
        swing.GetComponent<SlashAttack>().range = baseRange;
        swing.GetComponent<SlashAttack>().lenght = baseLenght;
        swing.GetComponent<SlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<SlashAttack>().followEntity = true;

        swing.GetComponent<SlashAttack>().way = way;

        swing.GetComponent<SlashAttack>().startAttack();


        if (way == SlashAttack.Way.Clockwise)
            way = SlashAttack.Way.CounterClockwise;
        else if (way == SlashAttack.Way.CounterClockwise)
            way = SlashAttack.Way.Clockwise;
    }

    public override void Special(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        laser.transform.position = self.transform.position;
        laser.AddComponent<LaserAttack>();



        float radius = Mathf.Max(self.transform.localScale.x, self.transform.localScale.y);
        float distanceMax = baseRange * 3;
        Vector3 angleDash = new Velocity(self.GetComponent<Rigidbody2D>().velocity).setAngle(angle).GetAsVector2();
        RaycastHit2D resultHit = Physics2D.Raycast(self.transform.position, angleDash, distanceMax + radius, LayerMask.GetMask("Terrain"));

        Velocity teleportPoint;
        if (resultHit.collider == null)
            teleportPoint = new Velocity(distanceMax, angle);
        else
            teleportPoint = new Velocity(resultHit.distance - radius, angle);

        self.GetComponent<Rigidbody2D>().velocity = angleDash;

        Vector3 teleportPointVector = teleportPoint.GetAsVector2();
        self.transform.position += teleportPointVector;
        self.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);




        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = teleportPoint.getSpeed();
        laser.GetComponent<LaserAttack>().lenght = self.transform.localScale.x;
        laser.GetComponent<LaserAttack>().timeSpan = 0.00f;
        laser.GetComponent<LaserAttack>().endDelay = 0.05f;
        laser.GetComponent<LaserAttack>().followEntity = false;

        laser.GetComponent<LaserAttack>().startAttack();
    }
}
