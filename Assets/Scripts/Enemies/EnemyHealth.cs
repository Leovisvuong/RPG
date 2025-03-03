using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private int experienceDropAmount;
    private Knockback knockback;
    private int currentHealth;
    private Flash flash;
    private Experience experienceScript;
    private AudioSource enemiesHit;

    private void Start(){
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        currentHealth = maxHealth;
        experienceScript = GameObject.Find("Player").GetComponent<Experience>();
        enemiesHit = GameObject.Find("Enemies Hit").GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage){
        enemiesHit.Play();
        currentHealth -= damage;
        knockback.GetKnockBack(PlayerController.Instance.transform,knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine(){
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath(){
        if(currentHealth <= 0){

            if(experienceScript) experienceScript.AddExp(experienceDropAmount);
            else Debug.Log("noo");
            Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItem();
            Destroy(gameObject);
        }
    }
}
