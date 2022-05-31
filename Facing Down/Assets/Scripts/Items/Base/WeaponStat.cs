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
        this.addMaxDashes = 0;
        this.addMaxSpecial = 0;
        this.specialDurationMult = 1;
        this.specialCooldownMult = 1;
	}
}
