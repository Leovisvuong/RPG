using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private bool isMelee = false;
    private int damageAmount;

    private void OnTriggerEnter2D(Collider2D other){
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        MagicLaser magicLaser = GetComponent<MagicLaser>();
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.currentActiveWeapon;
        
        if(!currentActiveWeapon) Debug.Log(9);

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