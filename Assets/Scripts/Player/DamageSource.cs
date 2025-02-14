using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public MonoBehaviour currentActiveWeapon;
    [SerializeField] private bool isMelee = false;
    private int damageAmount;

    private void Start()
    {
        currentActiveWeapon = ActiveWeapon.Instance.currentActiveWeapon;
    }

    private void OnTriggerEnter2D(Collider2D other){
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        MagicLaser magicLaser = GetComponent<MagicLaser>();
        
        if(magicLaser){
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfoManager().currentWeaponDamage + PlayerAttribute.Instance.magic + ArmorManager.Instance.armorMagic;
        }
        else{
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfoManager().currentWeaponDamage + PlayerAttribute.Instance.attack + ArmorManager.Instance.armorAttack;
        }

        enemyHealth?.TakeDamage(damageAmount);

        if((other.gameObject.GetComponent<GrapeProjectile>() || other.gameObject.GetComponent<Projectile>()) && !isMelee){
            if(magicLaser){
                if(magicLaser.GetLaserStrength() > 0) magicLaser.DecreaseLaserStrength(1);
            }
            else Destroy(gameObject);
        }
    } 
}