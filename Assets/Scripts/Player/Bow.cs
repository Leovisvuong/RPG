using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Animations;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfoManager weaponInfoManager;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;
    private AudioSource bowAttack;

    private void Awake(){
        myAnimator = GetComponent<Animator>();
        bowAttack = GameObject.Find("Bow Attack").GetComponent<AudioSource>();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            bowAttack.Play();
            Stamina.Instance.UseStamina(weaponInfoManager.weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger(FIRE_HASH); 
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfoManager.weaponInfo.weaponRange);
        }
        else{
            Warning.Instance.DoWarn("Run Out Of Stamina!", Color.yellow);
        }
    }
    public WeaponInfoManager GetWeaponInfoManager()
    {
        return weaponInfoManager;
    }
}
