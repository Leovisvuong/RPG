using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<GameObject> items;

    public void ChangeItemNumber(string itemName, int amount){
        foreach(var i in items){
            if(i.name == itemName  + " Slot"){
                TextMeshProUGUI itemText = i.GetComponentInChildren<TextMeshProUGUI>();
                int itemNumber = Convert.ToInt32(itemText.text);
                itemNumber += amount;

                if(itemNumber < 0) itemNumber = 0;

                itemText.text = itemNumber.ToString();
                return;
            }
        }
    }

    public int GetItemNumer(string itemName){
        foreach(var i in items){
            if(i.name == itemName  + " Slot"){
                TextMeshProUGUI itemText = i.GetComponentInChildren<TextMeshProUGUI>();
                int itemNumber = Convert.ToInt32(itemText.text);

                return itemNumber;
            }
        }
        return 0;
    }
}
