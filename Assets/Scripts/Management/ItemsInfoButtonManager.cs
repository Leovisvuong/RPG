using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInfoButtonManager : MonoBehaviour
{
    [SerializeField] private WeaponInfoManager weaponInfoManager;
    [SerializeField] private MaterialInfo materialInfo;
    [SerializeField] private ArmorInfoManager armorInfoManager;
    [SerializeField] private GameObject infoUIWithAttributes;
    [SerializeField] private GameObject infoUINoAttributes;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private Image infoImage;
    [SerializeField] private Image materialImage;
    [SerializeField] private Button quitInfo;
    [SerializeField] private Button equip;
    [SerializeField] private Button reroll;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI attributeText;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private bool isInventoryItem;

    private Button enterInfoUI;

    private void Awake()
    {
        enterInfoUI = GetComponentInChildren<Button>();
        if(weaponInfoManager){
            SetWeaponInfoManager();
        }
    }

    private void Start()
    {
        enterInfoUI.onClick.AddListener(ActiveInfoUI);
        if(quitInfo){
            quitInfo.onClick.AddListener(DeactiveInfoUI);
        }

        if(armorInfoManager && isInventoryItem){
            equip.onClick.AddListener(EquipArmor);
        }

        if(weaponInfoManager){
            reroll.onClick.AddListener(RerollWeapon);
        }
    }

    private void SetWeaponInfoManager(){
        string weaponName = weaponInfoManager.weaponInfo.weaponName;

        GameObject weaponInfoContainer = GameObject.Find(weaponName + " Info Container");

        if(!weaponInfoContainer){
            weaponInfoContainer = Instantiate(weaponInfoManager.gameObject);
            weaponInfoContainer.name = weaponName + " Info Container";
            weaponInfoContainer.transform.parent = GameObject.Find("Managers").transform;
        }
    
        weaponInfoManager = weaponInfoContainer.GetComponent<WeaponInfoManager>();
    }

    private void EquipArmor(){
        if(armorInfoManager.isGained && !armorInfoManager.isEquipped && GetComponentInChildren<Button>().gameObject.GetComponent<Image>().sprite == infoImage.sprite){
            string armorKind;
            armorInfoManager.isEquipped = true;
            armorInfoManager.ChangeOppositeArmorEquip();
            
            if(armorInfoManager.armorInfo.armorAttackUp > 0) armorKind = "Physic";
            else armorKind = "Magic";

            EquipmentManager.Instance.ActivateArmor(armorKind, armorInfoManager.armorInfo.armorType);
            ArmorManager.Instance.UpdateArmorAttribute();
        }
    }

    private void RerollWeapon(){
        string weaponRerollName = weaponInfoManager.weaponInfo.weaponName;

        if(GetComponentInChildren<Button>().gameObject.GetComponent<Image>().sprite != infoImage.sprite) return;

        switch(weaponRerollName){
            case "Sword":
                if(InventoryManager.Instance.GetItemNumer("Iron") > 0){
                    InventoryManager.Instance.ChangeItemNumber("Iron",-1);
                }
                else{
                    return;
                }
                break;
            case "Staff":
                if(InventoryManager.Instance.GetItemNumer("Magic Stone") > 0){
                    InventoryManager.Instance.ChangeItemNumber("Magic Stone",-1);
                }
                else{
                    return;
                }
                break;
            case "Bow":
                if(InventoryManager.Instance.GetItemNumer("Wood") > 0){
                    InventoryManager.Instance.ChangeItemNumber("Wood",-1);
                }
                else{
                    return;
                }
                break;
        }

        int rerollPercentNumber = Random.Range(0,1050);
        int newWeaponDamage = weaponInfoManager.currentWeaponMaxDamage * rerollPercentNumber / 1000;
        if(newWeaponDamage == 0) newWeaponDamage = 1;
        if(newWeaponDamage == weaponInfoManager.currentWeaponMaxDamage){
            if(weaponRerollName == "Staff"){
                if(weaponInfoManager.currentWeaponMaxDamage < 150){
                    weaponInfoManager.currentWeaponMaxDamage += 15;
                }
            }
            else if(weaponInfoManager.currentWeaponMaxDamage < 100){
                weaponInfoManager.currentWeaponMaxDamage += 10;
            }
        }
        weaponInfoManager.currentWeaponDamage = newWeaponDamage;
        ActiveInfoUI();
    
    }

    private void ActiveInfoUI(){
        infoImage.sprite = GetComponentInChildren<Button>().gameObject.GetComponent<Image>().sprite;

        if(materialInfo){
            infoUI.SetActive(true);
            equip.gameObject.SetActive(false);
            reroll.gameObject.SetActive(false);
            infoUINoAttributes.SetActive(true);
            infoUIWithAttributes.SetActive(false);
            attributeText.gameObject.SetActive(false);

            itemName.text = materialInfo.materialName;
            descriptionText.text = materialInfo.materialDescription;
        }

        if(armorInfoManager){
            infoUI.SetActive(true);
            
            if(isInventoryItem){
                equip.gameObject.SetActive(true);
            }
            else{
                equip.gameObject.SetActive(false);
            }

            reroll.gameObject.SetActive(false);
            infoUIWithAttributes.SetActive(true);
            infoUINoAttributes.SetActive(false);
            attributeText.gameObject.SetActive(true);

            itemName.text = armorInfoManager.armorInfo.armorName + " Armor";
            descriptionText.text = armorInfoManager.armorInfo.armorDescription;
            attributeText.text = "+" + armorInfoManager.armorInfo.armorHealthUp.ToString() + "\n+" + armorInfoManager.armorInfo.armorStaminaUp.ToString() + "\n+" + armorInfoManager.armorInfo.armorAttackUp.ToString() + "\n+" + armorInfoManager.armorInfo.armorMagicUp.ToString();
        }

        if(weaponInfoManager){
            infoUI.SetActive(true);
            equip.gameObject.SetActive(false);
            reroll.gameObject.SetActive(true);
            infoUIWithAttributes.SetActive(true);
            infoUINoAttributes.SetActive(false);
            attributeText.gameObject.SetActive(true);

            materialImage.sprite = weaponInfoManager.weaponInfo.rerollMaterial.materialSprite;

            itemName.text = weaponInfoManager.weaponInfo.weaponName;
            descriptionText.text = weaponInfoManager.weaponInfo.weaponDescription;
            if(weaponInfoManager.weaponInfo.weaponName == "Staff") attributeText.text = "+0\n+0\n+0\n(+" + weaponInfoManager.currentWeaponDamage.ToString() + ")/" + weaponInfoManager.currentWeaponMaxDamage;
            else attributeText.text = "+0\n+0\n(+" + weaponInfoManager.currentWeaponDamage.ToString() + ")/" + weaponInfoManager.currentWeaponMaxDamage + "\n+0";
        }
    }

    private void DeactiveInfoUI(){
        infoUI.SetActive(false);
    }
}
