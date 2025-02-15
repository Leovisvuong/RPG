using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickUpPrefabs;
    [SerializeField] private List<int> maxPrefabAmount;
    public void DropItem(){
        int randomNum = Random.Range(0, pickUpPrefabs.Count);
        if(randomNum < pickUpPrefabs.Count){
            int randomAmountOfPrefab = Random.Range(0, maxPrefabAmount[randomNum] + 1);
            GameObject spawnPrefab = pickUpPrefabs[randomNum];
            for(int i = 0; i < randomAmountOfPrefab; i++){
                Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
