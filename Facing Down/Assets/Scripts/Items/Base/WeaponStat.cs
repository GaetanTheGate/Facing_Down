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

    public WeaponStat (float HPMult = 1, float accelerationMult = 1, int addMaxDashes = 0, int addMaxSpecial = 0, float specialDurationMult = 1, float specialCooldownMult = 1) {
        this.HPMult = HPMult;
        this.accelerationMult = accelerationMult;
        this.addMaxDashes = addMaxDashes;
        this.addMaxSpecial = addMaxSpecial;
        this.specialDurationMult = specialDurationMult;
        this.specialCooldownMult = specialCooldownMult;
	}
}
