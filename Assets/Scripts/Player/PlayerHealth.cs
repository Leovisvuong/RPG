using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {get; private set;}
        
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float timeBetweenHealthRefresh = 5;

    private int maxHealth;
    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    private int healingRoutinesNum = 0;
    private TextMeshProUGUI healthText;

    const string HEALTH_NUMBER_TEXT = "Health Number";
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Route 1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start(){
        isDead = false;
        SetMaxHealth();
        currentHealth = maxHealth;
        UpdateHealthOutput();
    }

    private void OnCollisionStay2D(Collision2D other){
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if(enemy){
            TakeDamage(1, other.transform);
        }
    }

    public void SetMaxHealth(){
        maxHealth = PlayerAttribute.Instance.health;
        UpdateHealthOutput();
    }

    public void HealPlayer(int amount){
        if(currentHealth + amount < maxHealth){
            currentHealth += amount;
        }
        else{
            currentHealth = maxHealth;
        }
        UpdateHealthOutput();
    }

    public void TakeDamage(int damageAmount, Transform hitTransform){
        if(!canTakeDamage){
            return;
        }

        ScreenShakeManager.Instance.ShakeScreeen();
        knockback.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthOutput();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath(){
        if(currentHealth <= 0 && !isDead){
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine(){
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator HealPlayerRoutine(){
        yield return new WaitForSeconds(timeBetweenHealthRefresh);
        healingRoutinesNum = 0;
        HealPlayer(1);
    }

    private void UpdateHealthOutput(){
        if(healthSlider == null){
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }
        if(healthText == null){
            healthText = GameObject.Find(HEALTH_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        if(currentHealth < maxHealth && healingRoutinesNum == 0){
            healingRoutinesNum++;
            StartCoroutine(HealPlayerRoutine());
        }
    }
}
