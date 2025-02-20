using UnityEngine;

public class ArmorInfoManager : MonoBehaviour
{
    [SerializeField] public ArmorInfo armorInfo;
    [SerializeField] public ArmorInfoManager oppositeArmorKind;

    public bool isEquipped;
    public bool isGained;

    private void Awake(){
        isEquipped = false;
        isGained = false;
    }
    public void ChangeOppositeArmorEquip(){
        oppositeArmorKind.isEquipped = false;
    }
}
