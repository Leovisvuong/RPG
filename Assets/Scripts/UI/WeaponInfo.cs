using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public MaterialInfo rerollMaterial;
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public int weaponDamage;
    public int weaponRange;
    public int weaponStaminaCost;
    public int weaponMaxDamage;
    public string weaponDescription;
    public string weaponName;
}
