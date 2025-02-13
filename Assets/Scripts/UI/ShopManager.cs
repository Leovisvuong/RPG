using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> goods;

    private void Start()
    {
        foreach(var i in goods){
            i.GetComponentInChildren<Button>().onClick.AddListener(i.GetComponent<GoodsManager>().Buy);
        }
    }
}
