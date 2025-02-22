using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public bool isAttacking;
    public MonoBehaviour currentActiveWeapon {get; private set;}
    
    private PlayerControls playerControls;
    private float timeBetweenAttacks;
    private bool attackButtonDown;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void Start(){
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    private void Update(){
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon){
        currentActiveWeapon = newWeapon;

        AttackCooldown();
        timeBetweenAttacks = (currentActiveWeapon as IWeapon).GetWeaponInfoManager().weaponInfo.weaponCooldown;
    }

    public void WeaponNull(){
        currentActiveWeapon = null;
    }

    private void AttackCooldown(){
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine(){
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    private void StartAttacking(){
        attackButtonDown = true;
    }

    private void StopAttacking(){
        attackButtonDown = false;
    }

    private void Attack(){
        if(attackButtonDown && !isAttacking && currentActiveWeapon){
            AttackCooldown();
            (currentActiveWeapon as IWeapon).Attack();
        }
    }
}
