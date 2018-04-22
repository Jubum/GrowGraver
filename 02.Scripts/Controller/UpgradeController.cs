using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeController : MonoBehaviour {
    UpgradeInfo upgradeInfo;
    public UpgradeInfo UpgradeInfo { get { return upgradeInfo; } set { upgradeInfo = value; } }
    public GraverController graverController;
    public StackController stackController;
    public StorageController storageController;
    public NPCController npcController;
    public DeliveryController deliveryController;
    public UIController uiController;

    void Start()
    {
        if (System.IO.File.Exists(DataSaver.upgradeFilePath))
        {
            upgradeInfo = DataSaver.LoadData<UpgradeInfo>(DataSaver.upgradeFilePath);
            
        }
        else
        {
            upgradeInfo = new UpgradeInfo();
        }
        npcController.regenTime = upgradeInfo.RegenTime;
    }

    float StatsPriceUp(float level, float price)
    {
        if (level < 10) price += 100;
        else if (level < 30) price += Mathf.Round(level * 11);
        else if (level < 40) price += Mathf.Round(level * 10);
        else if (level < 50) price += Mathf.Round(level * 9);
        else if (level < 60) price += Mathf.Round(level * 13);
        else if (level < 70) price += Mathf.Round(level * 18);
        else if (level < 80) price += Mathf.Round(level * 26);
        else if (level < 90) price += Mathf.Round(level * 34);
        else if (level < 100) price += Mathf.Round(level * 44);
        else if (level < 110) price += Mathf.Round(level * 60);
        else if (level < 130) price += Mathf.Round(level * 89);
        else if (level < 150) price += Mathf.Round(level * 129);
        else if (level < 160) price += Mathf.Round(level * 241);
        else if (level < 170) price += Mathf.Round(level * 481);
        else if (level < 180) price += Mathf.Round(level * 786);
        else if (level < 190) price += Mathf.Round(level * 1057);
        else if (level <= 200) price += Mathf.Round(level * 1241);

        return price;
    }
    float DamageIncrease(float damage, float level)
    {
        float exvalue = damage;
        if (level < 10) damage += 1;
        else if (level < 50) damage += Mathf.Round(level * 0.18f);
        else if (level < 100) damage += Mathf.Round(level * 0.3f);
        else if (level < 200) damage += Mathf.Round(damage * 0.027f);
        return damage - exvalue;
    }
    float MaxSteminaIncrease(float stemina, float level)
    {
        float exvalue = stemina;
        if (level < 10) stemina += 3;
        else if (level < 50) stemina += Mathf.Round(level * 0.36f);
        else if (level < 100) stemina += Mathf.Round(level * 0.6f);
        else if (level < 200) stemina += Mathf.Round(stemina * 0.054f);
        return stemina - exvalue;
    }
    float HealingSteminaIncrease(float stemina, float level)
    {
        float exvalue = stemina;
        if (level < 10) stemina += 0.6f;
        else if (level < 50) stemina += level * 0.07f;
        else if (level < 100) stemina += level * 0.12f;
        else if (level < 200) stemina += stemina * 0.011f;
        return Mathf.Round(stemina - exvalue);
    }
    float DeliveryDelayPriceIncrease(float price, float level)
    {
        if (level < 3) price = Mathf.Round(price * 3);
        else if (level < 5) price = Mathf.Round(price * 2.7f);
        else if (level < 7) price = Mathf.Round(price * 2.4f);
        else if (level < 9) price = Mathf.Round(price * 2.1f);
        else if (level < 11) price = Mathf.Round(price * 2);

        return price;
    }
    float DeliveryWeightPriceIncrease(float price, float level)
    {
        if (level < 10) price = Mathf.Round(price * 1.3f);
        else if (level < 20) price = Mathf.Round(price * 1.2f);
        else if (level < 30) price = Mathf.Round(price * 1.15f);
        else if (level < 40) price = Mathf.Round(price * 1.12f);
        else if (level < 50) price = Mathf.Round(price * 1.1f);

        return price;
    }


    //데미지 업그레이드
    public void DamageUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.SculptDamageUpgradePrice)
        { 
            graverController.graver.SculptDamage += upgradeInfo.SculptDamageUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.SculptDamageUpgradePrice;

            upgradeInfo.SculptDamageUpgrade++;
            upgradeInfo.SculptDamageUpgradePrice = StatsPriceUp(upgradeInfo.SculptDamageUpgrade, upgradeInfo.SculptDamageUpgradePrice);
            upgradeInfo.SculptDamageUpgradeIncrease = DamageIncrease(graverController.graver.SculptDamage, upgradeInfo.SculptDamageUpgrade);
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //크리티컬 데미지 업그레이드
    public void CriticalDamageUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.CriticalDamageUpgradePrice)
        {
            graverController.graver.CriticalDamage += upgradeInfo.CriticalDamageUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.CriticalDamageUpgradePrice;

            upgradeInfo.CriticalDamageUpgrade++;
            upgradeInfo.CriticalDamageUpgradePrice = StatsPriceUp(upgradeInfo.CriticalDamageUpgrade, upgradeInfo.CriticalDamageUpgradePrice);
            upgradeInfo.CriticalDamageUpgradeIncrease = 5;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //크리티컬 확률 업그레이드
    public void CriticalProbabilityUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.CriticalProbabilityUpgradePrice)
        {
            graverController.graver.CriticalProbability += upgradeInfo.CriticalProbabilityUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.CriticalProbabilityUpgradePrice;

            upgradeInfo.CriticalProbabilityUpgrade++;
            upgradeInfo.CriticalProbabilityUpgradePrice = StatsPriceUp(upgradeInfo.CriticalProbabilityUpgrade, upgradeInfo.CriticalProbabilityUpgradePrice);
            upgradeInfo.CriticalProbabilityUpgradeIncrease = 4;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //최대 스테미나 업그레이드
    public void MaxSteminaUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.MaxSteminaUpgradePrice)
        {
            graverController.graver.MaxStemina += upgradeInfo.MaxSteminaUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.MaxSteminaUpgradePrice;

            upgradeInfo.MaxSteminaUpgrade++;
            upgradeInfo.MaxSteminaUpgradePrice = StatsPriceUp(upgradeInfo.MaxSteminaUpgrade, upgradeInfo.MaxSteminaUpgradePrice);
            upgradeInfo.MaxSteminaUpgradeIncrease = MaxSteminaIncrease(graverController.graver.MaxStemina, upgradeInfo.MaxSteminaUpgrade);
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //회복 스테미나 업그레이드
    public void HealingSteminaUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.IncreaseSteminaUpgradePrice)
        {
            graverController.graver.IncreaseStemina += upgradeInfo.IncreaseSteminaUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.IncreaseSteminaUpgradePrice;

            upgradeInfo.IncreaseSteminaUpgrade++;
            upgradeInfo.IncreaseSteminaUpgradePrice = StatsPriceUp(upgradeInfo.IncreaseSteminaUpgrade, upgradeInfo.IncreaseSteminaUpgradePrice);
            upgradeInfo.IncreaseSteminaUpgradeIncrease = HealingSteminaIncrease(graverController.graver.IncreaseStemina, upgradeInfo.IncreaseSteminaUpgrade);
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //자동공격 속도 업그레이드
    public void AutoSpeedUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.AutoSpeedUpgradePrice)
        {
            graverController.graver.AutoSpeed += upgradeInfo.AutoSpeedUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.AutoSpeedUpgradePrice;

            upgradeInfo.AutoSpeedUpgrade++;
            upgradeInfo.AutoSpeedUpgradePrice = StatsPriceUp(upgradeInfo.AutoSpeedUpgrade, upgradeInfo.AutoSpeedUpgradePrice);
            upgradeInfo.AutoSpeedUpgradeIncrease = -0.013f;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }



    //2번째 업그리에드 라인
    /////
    /////

    //보관함 업그레이드
    public void MaxStackUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.MaxStackUpgradePrice)
        {
            stackController.stack.maxStack += (int)upgradeInfo.MaxStackUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.MaxStackUpgradePrice;

            upgradeInfo.MaxStack += (int)upgradeInfo.MaxStackUpgradeIncrease;
            upgradeInfo.MaxStackUpgrade++;
            upgradeInfo.MaxStackUpgradePrice = DeliveryWeightPriceIncrease(upgradeInfo.MaxStackUpgradePrice, upgradeInfo.MaxStackUpgrade);
            upgradeInfo.MaxStackUpgradeIncrease = 1;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //진열대 업그레이드
    public void MaxStorageUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.MaxSlotUpgradePrice)
        {
            storageController.storage.maxSlot += (int)upgradeInfo.MaxSlotUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.MaxSlotUpgradePrice;

            upgradeInfo.MaxSlot += (int)upgradeInfo.MaxSlotUpgradeIncrease;
            upgradeInfo.MaxSlotUpgrade++;
            upgradeInfo.MaxSlotUpgradePrice = DeliveryWeightPriceIncrease(upgradeInfo.MaxSlotUpgradePrice, upgradeInfo.MaxSlotUpgrade);
            upgradeInfo.MaxSlotUpgradeIncrease = 1;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //가게 업그레이드
    public void StoreUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.SlotLevelUpgradePrice)
        {
            storageController.storage.level += (int)upgradeInfo.SlotLevelUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.SlotLevelUpgradePrice;

            upgradeInfo.SlotLevelUpgrade++;
            upgradeInfo.SlotLevelUpgradePrice = upgradeInfo.SlotLevelUpgradePrice * 10;
            upgradeInfo.SlotLevelUpgradeIncrease = 1;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //손님 업그레이드
    public void CustomerUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.RegenTimeUpgradePrice)
        {
            npcController.regenTime += upgradeInfo.RegenTimeUpgradeIncrease;
            upgradeInfo.RegenTime += upgradeInfo.RegenTimeUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.RegenTimeUpgradePrice;

            upgradeInfo.RegenTimeUpgrade++;
            upgradeInfo.RegenTimeUpgradePrice = StatsPriceUp(upgradeInfo.RegenTimeUpgrade, upgradeInfo.RegenTimeUpgradePrice);
            upgradeInfo.RegenTimeUpgradeIncrease = -0.022f;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //최대 배달원 업그레이드
    public void MaxDeliveryManUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.MaxDeliveryManUpgradePrice)
        {
            deliveryController.delivery.maxDeliveryMan += (int)upgradeInfo.MaxDeliveryManUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.MaxDeliveryManUpgradePrice;

            upgradeInfo.MaxDeliveryManUpgrade++;
            upgradeInfo.MaxDeliveryManUpgradePrice = upgradeInfo.MaxDeliveryManUpgradePrice * 3;
            upgradeInfo.MaxDeliveryManUpgradeIncrease = 1;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //배달 딜레이 업그레이드
    public void DeliveryDelayUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.DeliveryDelayUpgradePrice)
        {
            deliveryController.delivery.delay += (int)upgradeInfo.DeliveryDelayUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.DeliveryDelayUpgradePrice;

            upgradeInfo.DeliveryDelayUpgrade++;
            upgradeInfo.DeliveryDelayUpgradePrice = DeliveryDelayPriceIncrease(upgradeInfo.DeliveryDelayUpgradePrice, upgradeInfo.DeliveryDelayUpgrade);

        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
    //배달 무게 업그레이드
    public void DeliveryWeightUpgrade()
    {
        if (graverController.graver.Money > upgradeInfo.DeliveryWeightUpgradePrice)
        {
            deliveryController.delivery.weight += (int)upgradeInfo.DeliveryWeightUpgradeIncrease;
            graverController.graver.Money -= upgradeInfo.DeliveryWeightUpgradePrice;

            upgradeInfo.DeliveryWeightUpgrade++;
            upgradeInfo.DeliveryWeightUpgradePrice = DeliveryWeightPriceIncrease(upgradeInfo.DeliveryWeightUpgradePrice, upgradeInfo.DeliveryWeightUpgrade);
            upgradeInfo.DeliveryWeightUpgradeIncrease = 1;
        }
        else
        {
            uiController.OnWarningPopUp();
        }
    }
}

[Serializable]
public class UpgradeInfo
{
    public UpgradeInfo()
    {
        AutoSpeedUpgrade = 1;
        AutoSpeedUpgradeIncrease = -0.013f;
        AutoSpeedUpgradePrice = 100;

        MaxSteminaUpgrade = 1;
        MaxSteminaUpgradeIncrease = 3;
        MaxSteminaUpgradePrice = 100;

        IncreaseSteminaUpgrade = 1;
        IncreaseSteminaUpgradeIncrease = 1;
        IncreaseSteminaUpgradePrice = 100;

        SculptDamageUpgrade = 1;
        SculptDamageUpgradeIncrease = 1;
        SculptDamageUpgradePrice = 100;

        CriticalDamageUpgrade = 1;
        CriticalDamageUpgradeIncrease = 5;
        CriticalDamageUpgradePrice = 100;

        CriticalProbabilityUpgrade = 1;
        CriticalProbabilityUpgradeIncrease = 4;
        CriticalProbabilityUpgradePrice = 100;

        MaxStack = 24;
        MaxStackUpgrade = 1;
        MaxStackUpgradeIncrease = 1;
        MaxStackUpgradePrice = 3000;

        MaxSlot = 24;
        MaxSlotUpgrade = 1;
        MaxSlotUpgradeIncrease = 1;
        MaxSlotUpgradePrice = 3000;

        SlotLevelUpgrade = 1;
        SlotLevelUpgradeIncrease = 1;
        SlotLevelUpgradePrice = 90000;

        RegenTime = 5;
        RegenTimeUpgrade = 1;
        RegenTimeUpgradeIncrease = -0.022f;
        RegenTimeUpgradePrice = 10000;

        MaxDeliveryManUpgrade = 1;
        MaxDeliveryManUpgradeIncrease = 1;
        MaxDeliveryManUpgradePrice = 20000;

        DeliveryDelayUpgrade = 1;
        DeliveryDelayUpgradeIncrease = -1;
        DeliveryDelayUpgradePrice = 10000;

        DeliveryWeightUpgrade = 1;
        DeliveryWeightUpgradeIncrease = 1;
        DeliveryWeightUpgradePrice = 1000;


    }

    float sculptDamageUpgrade;
    public float SculptDamageUpgrade { get; set; }
    float sculptDamageUpgradeIncrease;
    public float SculptDamageUpgradeIncrease { get; set; }
    float sculptDamageUpgradePrice;
    public float SculptDamageUpgradePrice { get; set; }

    float autoSpeedUpgrade;
    public float AutoSpeedUpgrade { get; set; }
    float autoSpeedUpgradeIncrease;
    public float AutoSpeedUpgradeIncrease { get; set; }
    float autoSpeedUpgradePrice;
    public float AutoSpeedUpgradePrice { get; set; }

    float criticalDamageUpgrade;
    public float CriticalDamageUpgrade { get; set; }
    float criticalDamageUpgradeIncrease;
    public float CriticalDamageUpgradeIncrease { get; set; }
    float criticalDamageUpgradePrice;
    public float CriticalDamageUpgradePrice { get; set; }

    float criticalProbabilityUpgrade;
    public float CriticalProbabilityUpgrade { get; set; }
    float criticalProbabilityUpgradeIncrease;
    public float CriticalProbabilityUpgradeIncrease { get; set; }
    float criticalProbabilityUpgradePrice;
    public float CriticalProbabilityUpgradePrice { get; set; }

    float maxSteminaUpgrade;
    public float MaxSteminaUpgrade { get; set; }
    float maxSteminaUpgradeIncrease;
    public float MaxSteminaUpgradeIncrease { get; set; }
    float maxSteminaUpgradePrice;
    public float MaxSteminaUpgradePrice { get; set; }

    float increaseSteminaUpgrade;
    public float IncreaseSteminaUpgrade { get; set; }
    float increaseSteminaUpgradeIncrease;
    public float IncreaseSteminaUpgradeIncrease { get; set; }
    float increaseSteminaUpgradePrice;
    public float IncreaseSteminaUpgradePrice { get; set; }

    //최고 보관
    float maxStack;
    public float MaxStack { get; set; }
    float maxStackUpgrade;
    public float MaxStackUpgrade { get; set; }
    float maxStackUpgradeIncrease;
    public float MaxStackUpgradeIncrease { get; set; }
    float maxStackUpgradePrice;
    public float MaxStackUpgradePrice { get; set; }

    //최고 판매량
    float maxSlot;
    public float MaxSlot { get; set; }
    float maxSlotUpgrade;
    public float MaxSlotUpgrade { get; set; }
    float maxSlotUpgradeIncrease;
    public float MaxSlotUpgradeIncrease { get; set; }
    float maxSlotUpgradePrice;
    public float MaxSlotUpgradePrice { get; set; }

    //진열대 레벨
    float slotLevelUpgrade;
    public float SlotLevelUpgrade { get; set; }
    float slotLevelUpgradeIncrease;
    public float SlotLevelUpgradeIncrease { get; set; }
    float slotLevelUpgradePrice;
    public float SlotLevelUpgradePrice { get; set; }

    //NPC 리젠타임
    float regenTime;
    public float RegenTime { get; set; }
    float regenTimeUpgrade;
    public float RegenTimeUpgrade { get; set; }
    float regenTimeUpgradeIncrease;
    public float RegenTimeUpgradeIncrease { get; set; }
    float regenTimeUpgradePrice;
    public float RegenTimeUpgradePrice { get; set; }

    //최대 배달원
    float maxDeliveryManUpgrade;
    public float MaxDeliveryManUpgrade { get; set; }
    float maxDeliveryManUpgradeIncrease;
    public float MaxDeliveryManUpgradeIncrease { get; set; }
    float maxDeliveryManUpgradePrice;
    public float MaxDeliveryManUpgradePrice { get; set; }

    //배달 시간
    float deliveryDelayUpgrade;
    public float DeliveryDelayUpgrade { get; set; }
    float deliveryDelayUpgradeIncrease;
    public float DeliveryDelayUpgradeIncrease { get; set; }
    float deliveryDelayUpgradePrice;
    public float DeliveryDelayUpgradePrice { get; set; }

    //배달원 무게
    float deliveryWeightUpgrade;
    public float DeliveryWeightUpgrade { get; set; }
    float deliveryWeightUpgradeIncrease;
    public float DeliveryWeightUpgradeIncrease { get; set; }
    float deliveryWeightUpgradePrice;
    public float DeliveryWeightUpgradePrice { get; set; }
}
