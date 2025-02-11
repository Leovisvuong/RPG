using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject SlashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnimation;
    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    private void Start(){
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }
    
    void Update(){
        MouseFollowWithOffset();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            Stamina.Instance.UseStamina(weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnimation = Instantiate(SlashAnimationPrefab,slashAnimationSpawnPoint.position,Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
        }
    }

    public void DoneAttackAnimationEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent(){
        slashAnimation.transform.rotation = Quaternion.Euler(-180,0,0);

        if(PlayerController.Instance.FacingLeft){
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimationEvent(){
        slashAnimation.transform.rotation = Quaternion.Euler(0,0,0);

        if(PlayerController.Instance.FacingLeft){
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = 30;

        if(mousePos.x < playerScreenPoint.x){
            ActiveWeapon.Instance.gameObject.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
