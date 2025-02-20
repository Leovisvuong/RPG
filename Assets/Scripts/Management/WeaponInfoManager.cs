using UnityEngine;

public class WeaponInfoManager : MonoBehaviour
{
    [SerializeField] public WeaponInfo weaponInfo;

    public int currentWeaponDamage;

    public int currentWeaponMaxDamage;
    private void Awake()
    {
        currentWeaponDamage = weaponInfo.weaponDamage;
        currentWeaponMaxDamage = weaponInfo.weaponMaxDamage;
    }
}
