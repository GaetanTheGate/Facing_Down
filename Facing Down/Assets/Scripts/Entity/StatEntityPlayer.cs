using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEntityPlayer : StatEntity
{
    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;
}
