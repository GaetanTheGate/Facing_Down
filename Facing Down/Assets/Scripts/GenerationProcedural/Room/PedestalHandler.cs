using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalHandler : MonoBehaviour, SpawnPoint
{
    public void Chose()
    {
        foreach (PedestalSpawnPoint spawnPoint in GetComponentsInChildren<PedestalSpawnPoint>())
            spawnPoint.Chose();
    }

    public void Spawn()
    {
        foreach (PedestalSpawnPoint spawnPoint in GetComponentsInChildren<PedestalSpawnPoint>())
            spawnPoint.Spawn();
    }

    public void Despawn()
    {
        foreach (PedestalSpawnPoint spawnPoint in GetComponentsInChildren<PedestalSpawnPoint>())
            spawnPoint.Despawn();
    }
}
