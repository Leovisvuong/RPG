using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D other){
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(other.gameObject.GetComponent<DamageSource>()){
            GetComponent<PickUpSpawner>().DropItem();
            Instantiate(destroyVFX, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

        if(projectile){
            if(projectile.GetIsEnemyProjecttile()) return;
            GetComponent<PickUpSpawner>().DropItem();
            Instantiate(destroyVFX, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
