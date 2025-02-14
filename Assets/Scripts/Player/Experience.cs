using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Experience : Singleton<Experience>
{
    private Slider expSlider;
    private TextMeshProUGUI expText;
    private int currentExp;
    private int levelUpExp;
    private int currentLevel;

    const string EXP_NUMBER_TEXT = "Exp Number Text";
    const string EXP_SLIDER_TEXT = "Exp Slider";

    protected override void Awake()
    {
        base.Awake();

        currentExp = 0;
        currentLevel = 1;
        levelUpExp = Convert.ToInt32(currentLevel * 5 / 2);
    }

    public void AddExp(int amount){
        currentExp += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp(){
        if(currentExp >= levelUpExp && currentLevel <= 100){
            currentExp -= levelUpExp;
            currentLevel++;
            levelUpExp = Convert.ToInt32(currentLevel * 5 / 2);
            PlayerAttribute.Instance.LevelUp();
        }
        UpdateExpOutput();
    }

    private void UpdateExpOutput(){
        if(expSlider == null){
            expSlider = GameObject.Find(EXP_SLIDER_TEXT).GetComponent<Slider>();
        }
        if(expText == null){
            expText = GameObject.Find(EXP_NUMBER_TEXT).GetComponent<TextMeshProUGUI>();
        }

        expSlider.maxValue = levelUpExp;
        expSlider.value = currentExp;
        expText.text = "level " + currentLevel + " (" + currentExp + "/" + levelUpExp + ")";        
        Debug.Log(currentExp);
    }
}
