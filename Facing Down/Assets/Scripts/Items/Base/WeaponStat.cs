using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat
{
    public float HPMult;
    public float accelerationMult;
    public int maxDashes;
    public int maxSpecial;
    public float specialDurationMult;
    public float specialCooldownMult;

    public WeaponStat () {
        this.HPMult = 1;
        this.accelerationMult = 1;
        this.maxDashes = 4;
        this.maxSpecial = 0;
        this.specialDurationMult = 1;
        this.specialCooldownMult = 1;
	}
}
