using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoHandler : MonoBehaviour
{
    private bool isOver = false;
    private int nextWaveRank = 0;
    private int maxWaveRankReached = 0;

    public void InitRoomInfo()
    {
        foreach (Wave wave in GetComponentsInChildren<Wave>())
        {
            wave.GetComponentInChildren<EnemyHandler>().Chose();
            wave.GetComponentInChildren<PedestalHandler>().Chose();
        }
    }

    public void FinishRoom()
    {
        isOver = true;
        GetComponentInParent<RoomHandler>().OnFinishRoom();
    }

    public void EnterRoom()
    {
        for (int i = 0; i <= maxWaveRankReached; ++i)
            SummonNextWave<PedestalHandler>(i);

        if (!isOver)
        {
            nextWaveRank = 0;
            int rank = SummonNextWave<EnemyHandler>(nextWaveRank);

            maxWaveRankReached = (nextWaveRank > maxWaveRankReached ? nextWaveRank : nextWaveRank);
            nextWaveRank = rank;
            if (nextWaveRank == -1)
                FinishRoom();
        }
        else
            SummonNextWave<PedestalHandler>(-1);
    }

    public void ExitRoom()
    {
        foreach (Wave wave in GetComponentsInChildren<Wave>())
        {
            wave.GetComponentInChildren<EnemyHandler>().Despawn();
            wave.GetComponentInChildren<PedestalHandler>().Despawn();
        }
    }

    public bool checkIfNoEnemy(){
        return GetComponentInChildren<EnemyHandler>().checkIfNoEnemy();
    }

    public void NoMoreEnemy()
    {
        if (!isOver)
        {
            int rank = SummonNextWave<EnemyHandler>(nextWaveRank);
            SummonNextWave<PedestalHandler>(nextWaveRank);

            maxWaveRankReached = (nextWaveRank > maxWaveRankReached ? nextWaveRank : nextWaveRank);
            nextWaveRank = rank;
            if (nextWaveRank == -1)
                FinishRoom();
        }
    }

    private int SummonNextWave<T>(int rank) where T : SpawnPoint
    {
        int nextRank = rank != -1 ? rank + 1 : -1;

        Wave nextWave = null;
        foreach(Wave wave in GetComponentsInChildren<Wave>())
            if (wave.rank == rank)
            {
                nextWave = wave;
                break;
            }

        if(nextWave == null)
        {
            foreach (Wave wave in GetComponentsInChildren<Wave>())
                if (wave.rank == -1)
                {
                    nextWave = wave;
                    nextRank = -1;
                    break;
                }
        }
        nextWave.GetComponentInChildren<T>().Spawn();

        return nextRank;
    }
}
