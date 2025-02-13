using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private Armor oppositeArmorKind;
    public ArmorInfo armorInfo;
    public bool isGained;
    public bool isEquipped;

    public void ChangeOppositeEquipStatus(){
        oppositeArmorKind.isEquipped = false;
    }
}
