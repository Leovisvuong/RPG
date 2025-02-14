using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    [SerializeField] private ArmorInfoManager armorInfoManager;
    private int price;
    private string goodsName;

    private void Start()
    {
        string goodsPrice = GetComponentInChildren<TextMeshProUGUI>().text;
        price = Convert.ToInt32(goodsPrice.Substring(0, goodsPrice.Length-1));
        goodsName = name.Substring(0, name.Length - 5);
    }

    public void Buy(){
        if(EconomyManager.Instance.currentGold < price){
            return;
        }
        
        EconomyManager.Instance.UpdateCurrentGold(-price);
        InventoryManager.Instance.ChangeItemNumber(goodsName, 1);
        
        if(armorInfoManager){
            armorInfoManager.isGained = true;
        }
    }
}
