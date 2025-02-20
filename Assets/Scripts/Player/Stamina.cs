using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stamina : Singleton<Stamina>
{
    public int currentStamina;

    [SerializeField] private float timeBetweenStaminaRefresh = 1.5f;
    
    private Slider staminaSlider;
    private int maxStamina;
    private TextMeshProUGUI staminaText;

    const string STAMINA_NUMBER_TEXT = "Stamina Number";
    const string STAMINA_SLIDER_TEXT = "Stamina Slider";

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetMaxStamina();
        currentStamina = maxStamina;
        UpdateStaminaOutput();
    }

    public void SetMaxStamina(){
        maxStamina = PlayerAttribute.Instance.stamina + ArmorManager.Instance.armorStamina;
        if(currentStamina > maxStamina) currentStamina = maxStamina;
        UpdateStaminaOutput();
    }

    public void UseStamina(int amount){
        currentStamina -= amount;
        if(currentStamina < 0) currentStamina = 0;
        UpdateStaminaOutput();
    }

    public void RefreshStamina(int amount){
        if(currentStamina + amount < maxStamina){
            currentStamina += amount;
        }
        else{
            currentStamina = maxStamina;
        }
        UpdateStaminaOutput();
    }

    private IEnumerator RefreshStaminaRoutine(){
        yield return new WaitForSeconds(timeBetweenStaminaRefresh);
        if(!FreezeManager.Instance.gamePause) RefreshStamina(1);
    }

    private void UpdateStaminaOutput(){
        if(staminaSlider == null){
            staminaSlider = GameObject.Find(STAMINA_SLIDER_TEXT).GetComponent<Slider>();
        }
        if(staminaText == null){
            staminaText = GameObject.Find(STAMINA_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        }

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
        staminaText.text = currentStamina.ToString() + "/" + maxStamina.ToString();
        
        if(currentStamina < maxStamina){
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
