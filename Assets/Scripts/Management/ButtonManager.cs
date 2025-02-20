using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button attr_Equip;
    [SerializeField] private Button inv_Shop;
    [SerializeField] private Button setting;
    [SerializeField] private Button quitAttr_Equip;
    [SerializeField] private Button quitInv_Shop;
    [SerializeField] private Button quitSetting;
    [SerializeField] private GameObject attr_EquipObj;
    [SerializeField] private GameObject inv_ShopObj;
    [SerializeField] private GameObject settingObj;

    private void Start()
    {
        Time.timeScale = 1;
        attr_Equip.onClick.AddListener(ActiveAttributeAndEquip);
        inv_Shop.onClick.AddListener(ActiveInventoryAndShop);
        setting.onClick.AddListener(ActiveSetting);
        quitAttr_Equip.onClick.AddListener(DeactiveAll);
        quitInv_Shop.onClick.AddListener(DeactiveAll);
        quitSetting.onClick.AddListener(DeactiveAll);
    }
    private void ActiveAttributeAndEquip(){
        DeactiveAll();
        attr_EquipObj.SetActive(true);
        FreezeManager.Instance.DoFreeze();
    }

    private void ActiveInventoryAndShop(){
        DeactiveAll();
        inv_ShopObj.SetActive(true);
        FreezeManager.Instance.DoFreeze();
    }

    private void ActiveSetting(){
        DeactiveAll();
        settingObj.SetActive(true);
        FreezeManager.Instance.DoFreeze();
    }

    private void DeactiveAll(){
        attr_EquipObj.SetActive(false);
        inv_ShopObj.SetActive(false);
        settingObj.SetActive(false);
        FreezeManager.Instance.StopFreeze();
    }
}
