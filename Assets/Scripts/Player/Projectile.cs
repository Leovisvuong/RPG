using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjecttile = false;
    [SerializeField] private float projectileRange = 10;
    
    private int projectileDamage;
    private Vector3 startPosition;

    private void Start(){
        startPosition = transform.position;
        if(isEnemyProjecttile) projectileDamage = gameObject.transform.parent.GetComponent<EnemyAI>().maxAttack;
    }

    private void Update(){
        MoveProjectile();
        DetectFiredDistance();
    }

    public void UpdateProjectileRange(float projectileRange){
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }

    public bool GetIsEnemyProjecttile(){
        return isEnemyProjecttile;
    }

    private void OnTriggerEnter2D(Collider2D other){
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.gameObject.GetComponent<Indestructable>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        DamageSource damageSource = other.gameObject.GetComponent<DamageSource>();
        
        if(!other.isTrigger && (enemyHealth || indestructable || player)){
            if((player && isEnemyProjecttile) || (enemyHealth && !isEnemyProjecttile)){
                player?.TakeDamage(projectileDamage, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if(!other.isTrigger && indestructable){
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if(damageSource && isEnemyProjecttile){
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFiredDistance(){
        if(Vector3.Distance(transform.position, startPosition) > projectileRange){
            Destroy(gameObject);
        }
    }

    private void MoveProjectile(){
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
