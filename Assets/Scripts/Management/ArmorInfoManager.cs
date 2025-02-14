using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInfoManager : MonoBehaviour
{
    [SerializeField] public ArmorInfo armorInfo;
    [SerializeField] public ArmorInfoManager oppositeArmorKind;

    public bool isEquipped = false;
    public bool isGained = false;

    public void ChangeOppositeArmorEquip(){
        oppositeArmorKind.isEquipped = false;
    }
}
