using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorManager : Singleton<ArmorManager>
{
    [SerializeField] private List<ArmorInfoManager> armorInfoManagers;

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
        foreach(var i in armorInfoManagers){
            if(i.isEquipped){
                int tmp = 1;
                if(i.armorInfo.armorType == "Arm" || i.armorInfo.armorType == "Leg") tmp++;
                AddAttribute(i.armorInfo.armorHealthUp, i.armorInfo.armorStaminaUp, i.armorInfo.armorAttackUp * tmp, i.armorInfo.armorMagicUp * tmp);
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
