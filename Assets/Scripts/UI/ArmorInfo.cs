using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Armor")]
public class ArmorInfo : ScriptableObject
{

    public int armorHealthUp;
    public int armorStaminaUp;
    public int armorAttackUp;
    public int armorMagicUp;
    public string armorDescription;
    public string armorType;
}
