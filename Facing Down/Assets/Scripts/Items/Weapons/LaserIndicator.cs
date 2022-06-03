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

        attackPath = "Prefabs/Weapons/Laser";
        specialPath = "Prefabs/Weapons/Laser";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/light_blast_1");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/light_blast_1");
    }
}
