using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat
{
    public float HPMult;
    public float accelerationMult;
    public int addMaxDashes;
    public int addMaxSpecial;
    public float specialDurationMult;
    public float specialCooldownMult;

    public WeaponStat () {
        this.HPMult = 1;
        this.accelerationMult = 1;
        this.addMaxDashes = 1;
        this.addMaxSpecial = 3;
        this.specialDurationMult = 1;
        this.specialCooldownMult = 1;
	}
}
