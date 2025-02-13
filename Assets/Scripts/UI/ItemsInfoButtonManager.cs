using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInfoButtonManager : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private MaterialInfo materialInfo;
    [SerializeField] private Armor armor;
    [SerializeField] private GameObject infoUIWithAttributes;
    [SerializeField] private GameObject infoUINoAttributes;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private Image infoImage;
    [SerializeField] private Button quitInfo;
    [SerializeField] private Button equip;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI attributeText;
    [SerializeField] private bool isInventoryItem;

    private Button enterInfoUI;

    private void Awake()
    {
        enterInfoUI = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        enterInfoUI.onClick.AddListener(ActiveInfoUI);
        if(quitInfo) quitInfo.onClick.AddListener(DeactiveInfoUI);
        if(armor && isInventoryItem) equip.onClick.AddListener(EquipArmor);
    }

    private void EquipArmor(){
        if(armor.isGained && !armor.isEquipped && GetComponentInChildren<Button>().gameObject.GetComponent<Image>().sprite == infoImage.sprite){
            string armorKind;
            armor.isEquipped = true;
            armor.ChangeOppositeEquipStatus();
            
            if(armor.armorInfo.armorAttackUp > 0) armorKind = "Physic";
            else armorKind = "Magic";

            EquipmentManager.Instance.ActivateArmor(armorKind, armor.armorInfo.armorType);
            ArmorManager.Instance.UpdateArmorAttribute();
        }
    }

    private void ActiveInfoUI(){
        infoImage.sprite = GetComponentInChildren<Button>().gameObject.GetComponent<Image>().sprite;

        if(materialInfo){
            infoUI.SetActive(true);
            equip.gameObject.SetActive(false);
            infoUINoAttributes.SetActive(true);
            infoUIWithAttributes.SetActive(false);
            attributeText.gameObject.SetActive(false);

            descriptionText.text = materialInfo.materialDescription;
        }

        if(armor){
            infoUI.SetActive(true);
            if(isInventoryItem) equip.gameObject.SetActive(true);
            else equip.gameObject.SetActive(false);
            infoUIWithAttributes.SetActive(true);
            infoUINoAttributes.SetActive(false);
            attributeText.gameObject.SetActive(true);

            descriptionText.text = armor.armorInfo.armorDescription;
            attributeText.text = "+" + armor.armorInfo.armorHealthUp.ToString() + "\n+" + armor.armorInfo.armorStaminaUp.ToString() + "\n+" + armor.armorInfo.armorAttackUp.ToString() + "\n+" + armor.armorInfo.armorMagicUp.ToString();
        }

        if(weaponInfo){
            infoUI.SetActive(true);
            equip.gameObject.SetActive(false);
            infoUIWithAttributes.SetActive(true);
            infoUINoAttributes.SetActive(false);
            attributeText.gameObject.SetActive(true);
            descriptionText.text = weaponInfo.weaponDescription;
            if(weaponInfo.weaponName == "Staff") attributeText.text = "+" + weaponInfo.weaponHealthUp.ToString() + "\n+" + weaponInfo.weaponStaminaUp.ToString() + "\n+0\n+" + weaponInfo.weaponDamage.ToString();
            else attributeText.text = "+" + weaponInfo.weaponHealthUp.ToString() + "\n+" + weaponInfo.weaponStaminaUp.ToString() + "\n+" + weaponInfo.weaponDamage.ToString() + "\n+0";
        }
    }

    private void DeactiveInfoUI(){
        infoUI.SetActive(false);
    }
}
