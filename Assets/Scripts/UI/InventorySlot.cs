using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo(){
        if(weaponInfo == null) return null;
        return weaponInfo;
    }
}
