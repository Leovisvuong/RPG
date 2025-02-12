using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button attr_Equip;
    [SerializeField] private Button inventory;
    [SerializeField] private Button setting;
    [SerializeField] private Button quit;
    [SerializeField] private GameObject attr_EquipObj;

    private void Start()
    {
        Time.timeScale = 1;
        attr_Equip.onClick.AddListener(ActiveAttributeAndEquip);
        inventory.onClick.AddListener(ActiveInventory);
        setting.onClick.AddListener(ActiveSetting);
        quit.onClick.AddListener(DeactiveAll);
    }
    private void ActiveAttributeAndEquip(){
        attr_EquipObj.SetActive(true);
        quit.gameObject.SetActive(true);
        FreezeManager.Instance.DoFreeze();
    }

    private void ActiveInventory(){

    }

    private void ActiveSetting(){

    }

    private void DeactiveAll(){
        attr_EquipObj.SetActive(false);
        quit.gameObject.SetActive(false);
        FreezeManager.Instance.StopFreeze();
    }
}
