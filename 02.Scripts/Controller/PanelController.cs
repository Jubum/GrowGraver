using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour {

    public UpgradeController upgradeController;
    public GraverController graverController;
    public GameObject upgradePanelObject;
    public DeliveryController deliveryController;
    UpgradeInfo upgradeInfo;

    GameObject slidePanel;
    GameObject slidePanel2;
    GameObject slidePanelParent2;

    public Scrollbar scrollbar1;
    public Scrollbar scrollbar2;

    int count;


    List<GameObject> upgradePanel = new List<GameObject>();

    private void Start()
    {
        count = 0;
    }

    // 0: price, 1:upgradeValue, 3:LevelText, 5:totalValue
    public void Upload()
    {
        if(!(count > 0)) {
        
        upgradeInfo = upgradeController.UpgradeInfo;

        slidePanel = GameObject.Find("SlidePanel");
        slidePanel2 = GameObject.Find("SlidePanel2");
        slidePanelParent2 = GameObject.Find("SlidePanelParent2");
            
            for(int i =0; i < 13; i++)
            {
                upgradePanel.Add(Instantiate(upgradePanelObject));
            }

            
            upgradePanel[0].GetComponent<UpgradePanel>().SetValue("섬세함 증가", upgradeInfo.SculptDamageUpgradePrice, upgradeInfo.SculptDamageUpgradeIncrease, graverController.graver.SculptDamage);
            upgradePanel[0].transform.SetParent(slidePanel.transform, false);
            upgradePanel[0].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.DamageUpgrade);
            upgradePanel[0].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[1].GetComponent<UpgradePanel>().SetValue("과감한 터치 강화", upgradeInfo.CriticalDamageUpgradePrice, upgradeInfo.CriticalDamageUpgradeIncrease,graverController.graver.CriticalDamage);
            upgradePanel[1].transform.SetParent(slidePanel.transform, false);
            upgradePanel[1].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.CriticalDamageUpgrade);
            upgradePanel[1].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[2].GetComponent<UpgradePanel>().SetValue("과감한 터치 확률 증가", upgradeInfo.CriticalProbabilityUpgradePrice, upgradeInfo.CriticalProbabilityUpgradeIncrease, graverController.graver.CriticalProbability);
            upgradePanel[2].transform.SetParent(slidePanel.transform, false);
            upgradePanel[2].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.CriticalProbabilityUpgrade);
            upgradePanel[2].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[3].GetComponent<UpgradePanel>().SetValue("자동 공격 속도", upgradeInfo.AutoSpeedUpgradePrice, Mathf.Round(upgradeInfo.AutoSpeedUpgradeIncrease * 100) / 100, Mathf.Round(graverController.graver.AutoSpeed * 100) / 100);
            upgradePanel[3].transform.SetParent(slidePanel.transform, false);
            upgradePanel[3].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.AutoSpeedUpgrade);
            upgradePanel[3].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[4].GetComponent<UpgradePanel>().SetValue("최대 체력", upgradeInfo.MaxSteminaUpgradePrice, upgradeInfo.MaxSteminaUpgradeIncrease, graverController.graver.MaxStemina);
            upgradePanel[4].transform.SetParent(slidePanel.transform, false);
            upgradePanel[4].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.MaxSteminaUpgrade);
            upgradePanel[4].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[5].GetComponent<UpgradePanel>().SetValue("체력 회복량", upgradeInfo.IncreaseSteminaUpgradePrice, upgradeInfo.IncreaseSteminaUpgradeIncrease, graverController.graver.IncreaseStemina);
            upgradePanel[5].transform.SetParent(slidePanel.transform, false);
            upgradePanel[5].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.HealingSteminaUpgrade);
            upgradePanel[5].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);



            //2번째
            upgradePanel[6].GetComponent<UpgradePanel>().SetValue("배달원 무게 증가", upgradeInfo.DeliveryWeightUpgrade, upgradeInfo.DeliveryWeightUpgradeIncrease, deliveryController.delivery.weight);
            upgradePanel[6].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[6].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.DeliveryWeightUpgrade);
            upgradePanel[6].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[7].GetComponent<UpgradePanel>().SetValue("보관함 늘리기", upgradeInfo.MaxStackUpgradePrice, upgradeInfo.MaxStackUpgradeIncrease, upgradeInfo.MaxStack);
            upgradePanel[7].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[7].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.MaxStackUpgrade);
            upgradePanel[7].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[8].GetComponent<UpgradePanel>().SetValue("진열대 늘리기", upgradeInfo.MaxSlotUpgradePrice, upgradeInfo.MaxSlotUpgradeIncrease, upgradeInfo.MaxSlot);
            upgradePanel[8].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[8].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.MaxStorageUpgrade);
            upgradePanel[8].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[9].GetComponent<UpgradePanel>().SetValue("배달원 등장 속도 증가", upgradeInfo.DeliveryDelayUpgrade, upgradeInfo.DeliveryDelayUpgradeIncrease, deliveryController.delivery.delay);
            upgradePanel[9].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[9].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.DeliveryDelayUpgrade);
            upgradePanel[9].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[10].GetComponent<UpgradePanel>().SetValue("손님 증가", upgradeInfo.RegenTimeUpgradePrice, upgradeInfo.RegenTimeUpgradeIncrease, upgradeInfo.RegenTime);
            upgradePanel[10].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[10].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.CustomerUpgrade);
            upgradePanel[10].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[11].GetComponent<UpgradePanel>().SetValue("배달원 증가", upgradeInfo.MaxDeliveryManUpgrade, upgradeInfo.MaxDeliveryManUpgradeIncrease, upgradeInfo.MaxDeliveryManUpgrade);
            upgradePanel[11].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[11].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.MaxDeliveryManUpgrade);
            upgradePanel[11].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);

            upgradePanel[12].GetComponent<UpgradePanel>().SetValue("가게 구입", upgradeInfo.SlotLevelUpgradePrice, upgradeInfo.SlotLevelUpgradeIncrease, upgradeInfo.SlotLevelUpgrade);
            upgradePanel[12].transform.SetParent(slidePanel2.transform, false);
            upgradePanel[12].GetComponentInChildren<Button>().onClick.AddListener(upgradeController.StoreUpgrade);
            upgradePanel[12].GetComponentInChildren<Button>().onClick.AddListener(RefreshAll);


            count++;
            scrollbar1.value = 1;
            scrollbar2.value = 1;
            Invoke("temp", 0.01f);
        }

        Invoke("RefreshAll", 0.01f);
    }
    public void temp()
    {
        upgradePanel[6].transform.SetAsFirstSibling();
        slidePanelParent2.SetActive(false);
    }

    public void RefreshAll()
    {

        upgradePanel[0].GetComponent<UpgradePanel>().SetValue("섬세함 증가", upgradeInfo.SculptDamageUpgradePrice, upgradeInfo.SculptDamageUpgradeIncrease, graverController.graver.SculptDamage);

        upgradePanel[1].GetComponent<UpgradePanel>().SetValue("과감한 터치 증가", upgradeInfo.CriticalDamageUpgradePrice, upgradeInfo.CriticalDamageUpgradeIncrease, graverController.graver.CriticalDamage);
        upgradePanel[2].GetComponent<UpgradePanel>().SetValue("과감한 터치 확률 증가", upgradeInfo.CriticalProbabilityUpgradePrice, upgradeInfo.CriticalProbabilityUpgradeIncrease, graverController.graver.CriticalProbability);

        upgradePanel[3].GetComponent<UpgradePanel>().SetValue("자동 공격 속도", upgradeInfo.AutoSpeedUpgradePrice, Mathf.Round(upgradeInfo.AutoSpeedUpgradeIncrease * 100) / 100, Mathf.Round(graverController.graver.AutoSpeed * 100) / 100);

        upgradePanel[4].GetComponent<UpgradePanel>().SetValue("최대 체력", upgradeInfo.MaxSteminaUpgradePrice, upgradeInfo.MaxSteminaUpgradeIncrease, graverController.graver.MaxStemina);

        upgradePanel[5].GetComponent<UpgradePanel>().SetValue("체력 회복량", upgradeInfo.IncreaseSteminaUpgradePrice, upgradeInfo.IncreaseSteminaUpgradeIncrease, graverController.graver.IncreaseStemina);

        //2번째
        upgradePanel[6].GetComponent<UpgradePanel>().SetValue("배달원 무게 증가", upgradeInfo.DeliveryWeightUpgradePrice, upgradeInfo.DeliveryWeightUpgradeIncrease, deliveryController.delivery.weight);

        upgradePanel[7].GetComponent<UpgradePanel>().SetValue("보관함 늘리기", upgradeInfo.MaxStackUpgradePrice, upgradeInfo.MaxStackUpgradeIncrease, upgradeInfo.MaxStack);

        upgradePanel[8].GetComponent<UpgradePanel>().SetValue("진열대 늘리기", upgradeInfo.MaxSlotUpgradePrice, upgradeInfo.MaxSlotUpgradeIncrease, upgradeInfo.MaxSlot);

        upgradePanel[9].GetComponent<UpgradePanel>().SetValue("배달원 등장 속도 증가", upgradeInfo.DeliveryDelayUpgradePrice, upgradeInfo.DeliveryDelayUpgradeIncrease, deliveryController.delivery.delay);

        upgradePanel[10].GetComponent<UpgradePanel>().SetValue("손님 방문 속도 증가", upgradeInfo.RegenTimeUpgradePrice, Mathf.Round(upgradeInfo.RegenTimeUpgradeIncrease * 100) / 100, Mathf.Round(upgradeInfo.RegenTime * 100) / 100);

        upgradePanel[11].GetComponent<UpgradePanel>().SetValue("배달원 증가", upgradeInfo.MaxDeliveryManUpgradePrice, upgradeInfo.MaxDeliveryManUpgradeIncrease, upgradeInfo.MaxDeliveryManUpgrade);


        upgradePanel[12].GetComponent<UpgradePanel>().SetValue("가게 구입", upgradeInfo.SlotLevelUpgradePrice, upgradeInfo.SlotLevelUpgradeIncrease, upgradeInfo.SlotLevelUpgrade);

        for (int i = 0; upgradePanel.Count > i; i++)
        {
            upgradePanel[i].GetComponent<UpgradePanel>().Refresh();
        }
    }

}
