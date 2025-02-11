using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;
    [SerializeField] private bool isMelee = false;

    private void Start(){
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.currentActiveWeapon;
        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D other){
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        MagicLaser magicLaser = GetComponent<MagicLaser>();
        enemyHealth?.TakeDamage(damageAmount);

        if((other.gameObject.GetComponent<GrapeProjectile>() || other.gameObject.GetComponent<Projectile>()) && !isMelee){
            if(magicLaser){
                if(magicLaser.GetLaserStrength() > 0) magicLaser.DecreaseLaserStrength(1);
            }
            else Destroy(gameObject);
        }
    } 
}