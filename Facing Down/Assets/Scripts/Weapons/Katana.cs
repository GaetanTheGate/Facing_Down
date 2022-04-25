using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MeleeWeapon
{
    public Katana(string target) : base(target)
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

    public override void WeaponAttack(float angle, Entity self)
    {

        GetAttack(angle, self).startAttack();


        if (way == SlashAttack.Way.Clockwise)
            way = SlashAttack.Way.CounterClockwise;
        else if (way == SlashAttack.Way.CounterClockwise)
            way = SlashAttack.Way.Clockwise;
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        LaserAttack laser = (LaserAttack) GetSpecial(angle, self);

        self.GetComponent<Rigidbody2D>().velocity = new Velocity(self.GetComponent<Rigidbody2D>().velocity).setAngle(angle).GetAsVector2();

        Vector3 teleportPointVector = new Velocity(laser.range, angle).GetAsVector2();
        self.transform.position = laser.transform.position + teleportPointVector;
        self.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        laser.startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = startPos;
        swing.AddComponent<SlashAttack>();

        swing.GetComponent<SlashAttack>().src = self;
        swing.GetComponent<SlashAttack>().acceleration = 3.0f;
        swing.GetComponent<SlashAttack>().angle = angle;
        swing.GetComponent<SlashAttack>().range = baseRange;
        swing.GetComponent<SlashAttack>().lenght = baseLenght;
        swing.GetComponent<SlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<SlashAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SlashAttack>().way = way;

        AddHitAttack(swing, baseAtk);

        return swing.GetComponent<SlashAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        float radius = Mathf.Max(self.transform.localScale.x, self.transform.localScale.y);
        float distanceMax = baseRange * 3;
        Vector3 angleDash = new Velocity(1.0f, angle).GetAsVector2();
        RaycastHit2D resultHit = Physics2D.Raycast(startPos, angleDash, distanceMax + radius, LayerMask.GetMask("Terrain"));

        float teleportPoint;
        if (resultHit.collider == null)
            teleportPoint = distanceMax;
        else
            teleportPoint = resultHit.distance - radius;

        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();


        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = teleportPoint;
        laser.GetComponent<LaserAttack>().lenght = self.transform.localScale.x;
        laser.GetComponent<LaserAttack>().timeSpan = 0.00f;
        laser.GetComponent<LaserAttack>().endDelay = 0.05f;
        laser.GetComponent<LaserAttack>().followEntity = false;

        AddHitAttack(laser, baseAtk * 5);

        return laser.GetComponent<LaserAttack>();
    }
}
