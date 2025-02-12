using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;

    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            Stamina.Instance.UseStamina(weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger(FIRE_HASH); 
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
        }
        else{
            Warning.Instance.warnText.text = "Run Out Of Stamina!";
            Warning.Instance.DoWarn();
        }
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
