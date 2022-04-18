using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPlayer : MonoBehaviour
{
    public StatEntity statEntity;

    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;
}
