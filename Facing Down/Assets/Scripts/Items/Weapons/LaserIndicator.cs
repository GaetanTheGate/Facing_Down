using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserIndicator : Laser
{
    public LaserIndicator() : this("Enemy") { }
    public LaserIndicator(string target) : base(target, "LaserIndicator")
    {
        baseAtk = 0;
        baseRange = 50;
        baseLenght = 0.1f;
        baseSpan = 0.1f;
        baseEDelay = 0.2f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Items/Weapons/Laser";
        specialPath = "Prefabs/Items/Weapons/Laser";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/bip_low");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/bip_low");
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay);
        AddHitAttack(laser, dmgInfo);

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().audioClip = null;

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange;
        laser.GetComponent<LaserAttack>().lenght = baseLenght;
        laser.GetComponent<LaserAttack>().startDelay = baseSDelay;
        laser.GetComponent<LaserAttack>().timeSpan = baseSpan;
        laser.GetComponent<LaserAttack>().endDelay = baseEDelay;

        laser.GetComponent<LaserAttack>().followEntity = forceUnFollow;

        laser.GetComponent<LaserAttack>().onStartAttack += startBeepingSound;

        

        return laser.GetComponent<LaserAttack>();
    }

    private void startBeepingSound(Entity self, float angle)
    {
        Game.coroutineStarter.LaunchCoroutine(BipSoundPitch(baseSDelay + baseSpan + baseEDelay, self));
    }

    private IEnumerator BipSoundPitch(float duration, Entity self)
    {
        GameObject laserIndicatorAudio = new GameObject("Laser Indicator Audio");
        laserIndicatorAudio.transform.parent = self.transform;
        laserIndicatorAudio.AddComponent<AudioSource>();

        laserIndicatorAudio.GetComponent<AudioSource>().volume = 0.5f;
        //laserIndicatorAudio.GetComponent<AudioSource>().pitch = 1f;
        laserIndicatorAudio.GetComponent<AudioSource>().pitch = 1.5f;

        float startingTime = Time.time;

        float count = 0f;

        //bool isStep1 = false;
        bool isStep2 = false;

        //float waitDuration = 0.3f;
        float waitDuration = 0.15f;
        while(Time.time - startingTime < duration)
        {
            /*if(count >= duration * (2f/3f) && !isStep2)
            {
                isStep2 = true;
                laserIndicatorAudio.GetComponent<AudioSource>().pitch += 0.5f;
                waitDuration *= 0.5f;
            }
            else if(count >= duration * (1f/3f) && !isStep1 && !isStep2)
            {
                isStep1 = true;
                laserIndicatorAudio.GetComponent<AudioSource>().pitch += 0.5f;
                waitDuration *= 0.5f;
            }*/
            if (count >= duration * (1f / 2f) && !isStep2)
            {
                isStep2 = true;
                laserIndicatorAudio.GetComponent<AudioSource>().pitch += 0.5f;
                waitDuration *= 0.5f;
            }

            laserIndicatorAudio.GetComponent<AudioSource>().PlayOneShot(attackAudio);

            yield return new WaitForSeconds(waitDuration);
            count += waitDuration;
        }
        GameObject.Destroy(laserIndicatorAudio);
    }

}
