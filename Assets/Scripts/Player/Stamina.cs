using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Stamina : Singleton<Stamina>
{
    public int currentStamina {get; private set;}

    [SerializeField] private float timeBetweenStaminaRefresh = 1.5f;
    
    private Slider staminaSlider;
    private int maxStamina;
    private int startingStamina = 30;

    const string STAMINA_SLIDER_TEXT = "Stamina Slider";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        currentStamina = startingStamina;
    }
    
    public void UseStamina(int amount){
        currentStamina -= amount;
        if(currentStamina <= 0) currentStamina = 0;
        UpdateStaminaSlider();
    }

    public void RefreshStamina(int amount){
        if(currentStamina + amount < maxStamina){
            currentStamina += amount;
        }
        else{
            currentStamina = startingStamina;
        }
        UpdateStaminaSlider();
    }

    private IEnumerator RefreshStaminaRoutine(){
        yield return new WaitForSeconds(timeBetweenStaminaRefresh);
        RefreshStamina(1);
    }

    private void UpdateStaminaSlider(){
        if(staminaSlider == null){
            staminaSlider = GameObject.Find(STAMINA_SLIDER_TEXT).GetComponent<Slider>();
        }

        staminaSlider.maxValue = startingStamina;
        staminaSlider.value = currentStamina;
        
        if(currentStamina < maxStamina){
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
