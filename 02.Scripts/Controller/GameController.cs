using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour {

    public GraverController graverController;
    public Inventory inventory;
    public StorageController storageController;
    public StackController stack;
    public UpgradeController upgrade;
    public DeliveryController delivery;
    public ShopController shop;
    public MakingController making;
    public UIController uiController;
    public NPCController npcController;

    public Text moneyText;
    public Text moneyText2;
    public Text moneyText3;

    public Text diaText;

    public GameObject yesNoPopUp;
    public Text yesNoText;
    public Button yesButton;

    public bool IsOnInventory;

    float expTimer;
    float npcTimer;
    float moneyTimer;
    float deliveryTimer;

    public GameObject expPanel;
    public GameObject npcPanel;
    public GameObject moneyPanel;
    public GameObject deliveryPanel;

    public Text expBuffText;
    public Text npcBuffText;
    public Text moneyBuffText;
    public Text deliveryBuffText;

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 10, true);
        Invoke("LateStart", 1f);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    void LateStart()
    {
        StartCoroutine(AutoSave());
    }


    public void OnExpBuff()
    {
        expPanel.SetActive(true);
        expTimer = 30.0f;
    }
    public void OnNpcBuff()
    {
        npcPanel.SetActive(true);
        npcTimer = 30.0f;
    }
    public void OnMoneyBuff()
    {
        moneyPanel.SetActive(true);
        moneyTimer = 30.0f;
    }
    public void OnDeliveryBuff()
    {
        deliveryPanel.SetActive(true);
        deliveryTimer = 40.0f;
    }

    private void Update()
    {
        moneyText.text = graverController.graver.Money.ToString("#,###");
        moneyText2.text = graverController.graver.Money.ToString("#,###");
        moneyText3.text = graverController.graver.Money.ToString("#,###");
        diaText.text = graverController.graver.Diamond.ToString("#,###");


        if (graverController.expBuff)
        {
            expTimer -= Time.deltaTime;
            if (expTimer < 0)
            {
                expPanel.SetActive(false);
            }
            expBuffText.text = expTimer.ToString("##.#");
        }

        if (npcController.npcBuff)
        {
            npcTimer -= Time.deltaTime;
            if (npcTimer < 0)
            {
                npcPanel.SetActive(false);
            }
            npcBuffText.text = npcTimer.ToString("##.#");
        }

        if (storageController.moneyBuff)
        {
            moneyTimer -= Time.deltaTime;
            if (moneyTimer < 0)
            {
                moneyPanel.SetActive(false);
            }
            moneyBuffText.text = moneyTimer.ToString("##.#");
        }

        if (delivery.deliveryBuff)
        {
            deliveryTimer -= Time.deltaTime;
            if (deliveryTimer < 0)
            {
                deliveryPanel.SetActive(false);
            }
            deliveryBuffText.text = deliveryTimer.ToString("##.#");
        }

    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            SaveAll();
            yield return new WaitForSeconds(300);
        }
    }

    public void ChangeSculpture()
    {
        graverController.SetCurrentSculpture(10);
    }

    public void SaveAll()
    {
        SaveGraver();
        SaveInventory();
        SaveStructor();
        SaveUpgrade();
        SaveDelivery();
        SaveShop();
        SaveMaking();
    }
    public void SaveGraver()
    {
        DataSaver.SaveData<Graver>(graverController.graver, DataSaver.filePath);
        
    }
    public void SaveInventory()
    {
        for(int i =0; i < inventory.inventoryList.Count; i++) { 
            DataSaver.SaveData<List<ItemDataInfo>>(inventory.inventoryList[i].ItemDataInfos, DataSaver.inventoryFilePath[i]);
        }
    }
    public void SaveStructor()
    {
        DataSaver.SaveData<Storage>(storageController.storage, DataSaver.storageFilePath);

        DataSaver.SaveData<Stacks>(stack.stack, DataSaver.stackFilePath);
    }

    public void SaveUpgrade()
    {
        DataSaver.SaveData<UpgradeInfo>(upgrade.UpgradeInfo, DataSaver.upgradeFilePath);
    }
    public void SaveDelivery()
    {
        DataSaver.SaveData<Delivery>(delivery.delivery, DataSaver.deliveryFilePath);
    }
    public void SaveShop()
    {
        DataSaver.SaveData<ShopData>(shop.shopData, DataSaver.shopFilePath);
    }
    public void SaveMaking()
    {
        DataSaver.SaveData<Dictionary<int, MakingInfo>>(making.makingInfo, DataSaver.makingFilePath);
    }

    public void OnInventory()
    {
        IsOnInventory = !IsOnInventory;
    }

    public void DeleteData()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Data");

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        Directory.Delete(Application.persistentDataPath + "/Data");

        Application.Quit();
    }

    public void DeleteYesNoPopUp()
    {
        uiController.OnPanel(yesNoPopUp);
        yesNoText.text = "정말 데이터를 삭제 하시겠습니까?";
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(DeleteData);
    }
}
