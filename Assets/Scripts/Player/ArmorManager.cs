using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorManager : Singleton<ArmorManager>
{
    [SerializeField] private List<Armor> armor;

    public int armorHealth;
    public int armorStamina;
    public int armorAttack;
    public int armorMagic;

    private void Start()
    {
        ResetAttribute();
    }

    public void UpdateArmorAttribute(){
        ResetAttribute();
        foreach(var i in armor){
            ArmorInfo armorInfo = i.armorInfo;

            if(i.isEquipped){
                int tmp = 1;
                if(armorInfo.armorType == "Arm" || armorInfo.armorType == "Leg") tmp++;
                AddAttribute(armorInfo.armorHealthUp, armorInfo.armorStaminaUp, armorInfo.armorAttackUp * tmp, armorInfo.armorMagicUp * tmp);
            }
        }
        PlayerHealth.Instance.SetMaxHealth();
        Stamina.Instance.SetMaxStamina();
    }

    private void ResetAttribute(){
        armorHealth = 0;
        armorStamina = 0;
        armorAttack = 0;
        armorMagic = 0;
    }

    private void AddAttribute(int health, int stamina, int attack, int magic){
        armorHealth += health;
        armorStamina += stamina;
        armorAttack += attack;
        armorMagic += magic;
    }
}
