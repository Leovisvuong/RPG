using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickUpPrefabs;
    [SerializeField] private List<int> maxPrefabAmount;
    public void DropItem(){
        int randomNum = UnityEngine.Random.Range(0, pickUpPrefabs.Count);
        if(randomNum < pickUpPrefabs.Count){
            int randomAmountOfPrefab = UnityEngine.Random.Range(Convert.ToInt32(maxPrefabAmount[randomNum] / 10), maxPrefabAmount[randomNum] + 1);

            if(randomAmountOfPrefab == 0) randomAmountOfPrefab++;
            
            GameObject spawnPrefab = pickUpPrefabs[randomNum];
            for(int i = 0; i < randomAmountOfPrefab; i++){
                Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
