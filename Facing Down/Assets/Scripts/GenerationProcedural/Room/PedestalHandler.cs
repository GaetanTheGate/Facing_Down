using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalHandler : MonoBehaviour
{
    public List<GameObject> spawnPointPedestals = new List<GameObject>();

    public void spawnPedestals(){
        foreach(GameObject spawnPointPedestal in spawnPointPedestals){
            ItemPedestal.SpawnRandomItemPedestal(gameObject,spawnPointPedestal.transform.position);   
        }
    }
    
}
