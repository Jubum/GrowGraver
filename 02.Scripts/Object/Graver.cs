using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Graver {

    public Graver()
    {
        //초기 세팅
        //currentEquipMaterialNumber = 0;
        money = 99999999;
        autoSpeed = 3.0f;
        //autoSpeed = 0.01f;
        increaseStemina = 2f;
        sculptDamage = 1;
        criticalDamage = 150;
        criticalProbability = 0;
        maxStemina = 10;

        currentStemina = maxStemina;
        level = 1;
        maxExp = 15;
        fame = 0;

        currentWorkValue = 0;
        maxWorkValue = 5;

        diamond = 100;


    }

    Item currentSculptMaterial;
    public Item CurrentSculptMaterial { get { return currentSculptMaterial; } set { currentSculptMaterial = value; } }

    Item currentSculpture;
    public Item CurrentSculpture { get { return currentSculpture; } set { currentSculpture = value; } }
    //캐릭터 정보
    float money;
    public float Money { get { return money; } set { money = value; } }
    float diamond;
    public float Diamond { get { return diamond; } set { diamond = value; } }
    float fame;
    public float Fame { get; set; }
    float sculptDamage;
    public float SculptDamage { get { return sculptDamage; } set { sculptDamage = value; } }
    float criticalDamage;
    public float CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }
    float criticalProbability;
    public float CriticalProbability { get { return criticalProbability; } set { criticalProbability = value; } }

    float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }
    float maxExp;
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }
    float currentStemina;
    public float CurrentStemina { get { return currentStemina; } set { currentStemina = value; } }
    float maxStemina;
    public float MaxStemina { get { return maxStemina; } set { maxStemina = value; } }
    float increaseStemina;
    public float IncreaseStemina { get { return increaseStemina; } set { increaseStemina = value; } }
    float level;
    public float Level { get { return level; } set { level = value; } }
    float autoSpeed;
    public float AutoSpeed { get { return autoSpeed; } set { autoSpeed = value; } }
    //제작중인 조각품 정보를 받아 필요한 workvalue 획득을 위한 변수
    public float currentWorkValue;
    public float CurrentWorkValue { get { return currentWorkValue; } set { currentWorkValue = value; } }
    public float maxWorkValue;
    public float MaxWorkValue { get { return maxWorkValue; } set { maxWorkValue = value; } }


    
}
