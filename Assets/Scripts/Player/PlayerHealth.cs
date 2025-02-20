using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {get; private set;}
    public int maxHealth {get; private set;}
    public int currentHealth;
        
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float timeBetweenHealthRefresh = 5;
    [SerializeField] private float timeTakeDamageCoolDown = 0.5f;

    private Slider healthSlider;
    private Knockback knockback;
    private Flash flash;
    private int healingRoutinesNum = 0;
    private TextMeshProUGUI healthText;
    private AudioSource playerHit;
    private bool canTakeDamage;

    const string HEALTH_NUMBER_TEXT = "Health Number";
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Route 1";
    const string DEATH_HASH = "Death";
    const string REVIVE_HASH = "Revive";

    protected override void Awake()
    {
        base.Awake();
        
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        playerHit = GameObject.Find("Player Hit").GetComponent<AudioSource>();
    }

    private void Start(){
        isDead = false;
        canTakeDamage = true;
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
        maxHealth = PlayerAttribute.Instance.health + ArmorManager.Instance.armorHealth;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
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
        if(!canTakeDamage) return;

        playerHit.Play();

        canTakeDamage = false;
        ScreenShakeManager.Instance.ShakeScreeen();
        knockback.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        currentHealth -= damageAmount;
        StartCoroutine(CanTakeDamageCountDown());
        if(currentHealth < 0) currentHealth = 0;
        UpdateHealthOutput();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath(){
        if(currentHealth <= 0 && !isDead){
            isDead = true;
            PlayerController.Instance.playerDied = true;
            ActiveWeapon.Instance.gameObject.SetActive(false);
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine(){
        yield return new WaitForSeconds(2f);
        SceneManagement.Instance.SetTransitionName("Enter");
        isDead = false;
        PlayerController.Instance.playerDied = false;
        currentHealth = maxHealth;
        Stamina.Instance.currentStamina = PlayerAttribute.Instance.stamina + ArmorManager.Instance.armorStamina;
        UpdateHealthOutput();
        Stamina.Instance.SetMaxStamina();
        GetComponent<Animator>().SetTrigger(REVIVE_HASH);
        ActiveWeapon.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator HealPlayerRoutine(){
        yield return new WaitForSeconds(timeBetweenHealthRefresh);
        healingRoutinesNum = 0;
        if(!FreezeManager.Instance.gamePause) HealPlayer(1);
    }

    private IEnumerator CanTakeDamageCountDown(){
        yield return new WaitForSeconds(timeTakeDamageCoolDown);
        canTakeDamage = true;
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
