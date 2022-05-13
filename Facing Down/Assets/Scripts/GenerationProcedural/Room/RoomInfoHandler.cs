using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoHandler : MonoBehaviour
{
    private bool isOver = false;
    private int nextWaveRank = 0;
    private int maxWaveRankReached = 0;

    public bool IsOver() => isOver;

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

        if (!isOver)
        {
            nextWaveRank = 0;
            NoMoreEnemy();
        }
        else
        {
            for (int i = 0; i <= maxWaveRankReached; ++i)
                SummonNextWave<PedestalHandler>(i);

            SummonNextWave<PedestalHandler>(-1);
        }
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
            maxWaveRankReached = (nextWaveRank > maxWaveRankReached ? nextWaveRank : nextWaveRank);
            SummonNextWave<PedestalHandler>(nextWaveRank);
            int n = SummonNextWave<EnemyHandler>(ref nextWaveRank).GetEnemyLeft();

            if (nextWaveRank == -1)
                FinishRoom();
            else if (n == 0)
                NoMoreEnemy();
        }
    }

    private T SummonNextWave<T>(ref int rank) where T : SpawnPoint
    {

        Wave nextWave = null;
        foreach(Wave wave in GetComponentsInChildren<Wave>())
            if (wave.rank == rank)
            {
                nextWave = wave;
                break;
            }

        rank = rank != -1 ? rank + 1 : -1;
        if (nextWave == null)
        {
            foreach (Wave wave in GetComponentsInChildren<Wave>())
                if (wave.rank == -1)
                {
                    nextWave = wave;
                    rank = -1;
                    break;
                }
        }
        nextWave.GetComponentInChildren<T>().Spawn();

        return nextWave.GetComponentInChildren<T>();
    }

    private int SummonNextWave<T>(int rank) where T : SpawnPoint
    {
        int r = rank;
        SummonNextWave<T>(ref r);
        return r;
    }
}
