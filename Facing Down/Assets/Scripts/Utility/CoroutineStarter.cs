using UnityEngine;
using System.Collections;

public class CoroutineStarter : MonoBehaviour
{
    public void LaunchCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }
}