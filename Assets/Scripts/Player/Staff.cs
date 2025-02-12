using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    private void Update(){
        MouseFollowWithOffset();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            Stamina.Instance.UseStamina(weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger(ATTACK_HASH);
        }
        else{
            Warning.Instance.warnText.text = "Run Out Of Stamina!";
            Warning.Instance.DoWarn();
        }
    }

    public void SpawnStaffProjectileAnimationEvent(){
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }

    private void MouseFollowWithOffset(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x){
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
        }
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}