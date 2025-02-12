using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : Singleton<PlayerAttribute>
{
    public int health;
    public int stamina;
    public int attack;
    public int magic;
    public int level;
    public int experience;
    public int levelUpExperience;
    public int pointRemain;

    private GameObject attributeObj;

    protected override void Awake()
    {
        base.Awake();

        attributeObj = GameObject.Find("Attribute");
        health = 15;
        stamina = 30;
        attack = 1;
        magic = 1;
        level = 1;
        experience = 0;
        pointRemain = 3;
    }

    public void CheckLevelUp(){
        levelUpExperience = Convert.ToInt32(level * 5 / 2);

        if(experience >= levelUpExperience && level <= 100){
            pointRemain++;
            experience -= levelUpExperience;
            level++;
            attributeObj.GetComponent<AttributeUIManager>().UpdateValue();
        }
    }
}
