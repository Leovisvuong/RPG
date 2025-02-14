using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : Singleton<EquipmentManager>
{
    [SerializeField] private List<GameObject> physicArmors;
    [SerializeField] private List<GameObject> magicArmors;

    public void ActivateArmor(string armorKind, string armorType){
        if(armorKind == "Physic"){
            foreach(var i in physicArmors){
                if(i.name == "Physic " + armorType || i.name == "Physic " + armorType + " Left" || i.name == "Physic " + armorType + " Right"){
                    magicArmors[physicArmors.IndexOf(i)].SetActive(false);
                    i.SetActive(true);
                }
            }
        }
        else if(armorKind == "Magic"){
            foreach(var i in magicArmors){
                if(i.name == "Magic " + armorType || i.name == "Magic " + armorType + " Left" || i.name == "Magic " + armorType + " Right"){
                    physicArmors[magicArmors.IndexOf(i)].SetActive(false);
                    i.SetActive(true);
                }
            }
        }
    }
}
