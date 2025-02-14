using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI magicText;
    [SerializeField] private Button healthUp;
    [SerializeField] private Button staminaUp;
    [SerializeField] private Button attackUp;
    [SerializeField] private Button magicUp;

    private int point, health, stamina, attack, magic;

    private void Start()
    {
        UpdateValue();
        healthUp.onClick.AddListener(HealthUp);
        staminaUp.onClick.AddListener(StaminaUp);
        attackUp.onClick.AddListener(AttackUp);
        magicUp.onClick.AddListener(MagicUp);
        if(pointText == null) Debug.Log(1);
    }

    private void OnEnable()
    {
        UpdateAttrText();
    }

    public void UpdateValue(){
        point = PlayerAttribute.Instance.pointRemain;
        health = PlayerAttribute.Instance.health;
        stamina = PlayerAttribute.Instance.stamina;
        attack = PlayerAttribute.Instance.attack;
        magic= PlayerAttribute.Instance.magic;

        UpdateAttrText();

        PlayerHealth.Instance.SetMaxHealth();
        Stamina.Instance.SetMaxStamina();
    }

    private void UpdateAttrText(){
        pointText.text = point.ToString();
        healthText.text = health.ToString() +"<color=red>+" + ArmorManager.Instance.armorHealth;
        staminaText.text = stamina.ToString() +"<color=red>+" + ArmorManager.Instance.armorStamina;
        attackText.text = attack.ToString() +"<color=red>+" + ArmorManager.Instance.armorAttack;
        magicText.text = magic.ToString() +"<color=red>+" + ArmorManager.Instance.armorMagic;
    }

    private void HealthUp(){
        if(point > 0){
            PlayerAttribute.Instance.health++;
            PlayerAttribute.Instance.pointRemain--;
            UpdateValue();
        }
    }
    private void StaminaUp(){
        if(point > 0){
            PlayerAttribute.Instance.stamina++;
            PlayerAttribute.Instance.pointRemain--;
            UpdateValue();
        }
    }

    private void AttackUp(){
        if(point > 0){
            PlayerAttribute.Instance.attack++;
            PlayerAttribute.Instance.pointRemain--;
            UpdateValue();
        }
    }

    private void MagicUp(){
        if(point > 0){
            PlayerAttribute.Instance.magic++;
            PlayerAttribute.Instance.pointRemain--;
            UpdateValue();
        }
    }

}
