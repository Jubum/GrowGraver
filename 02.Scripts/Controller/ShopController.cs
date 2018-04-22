using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopController : MonoBehaviour {

    public GameObject shopPanelObject;
    public GraverController graverController;
    public MakingController makingController;

    GameObject shopSlidePanel;
    public GameObject allSalePanel;

    List<GameObject> shopPanel = new List<GameObject>();

    public ShopData shopData;

    int count;

    // Use this for initialization
    void Start () {
        count = 0;

        if (System.IO.File.Exists(DataSaver.shopFilePath))
        {
            shopData = DataSaver.LoadData<ShopData>(DataSaver.shopFilePath);
        }
        else
        {
            shopData = new ShopData();
            shopData.buiedData = new List<bool>();
            for(int i = 0; i < 33; i++)
            {
                shopData.buiedData.Add(false);
            }
            shopData.buiedData[0] = true;
        }
    }

    public void Upload()
    {
        if (count != 0) return;

        shopSlidePanel = GameObject.Find("ShopSlidePanel");

        Item item;

        int ccc = 0;
        for(int i =shopData.count*4+1; i < shopData.count *4+5; i++)
        {
            if(!shopData.buiedData[i] && i < 33)
            {
                item = Inventory.database.FetchSculptureItemById(i);
                shopPanel.Add(Instantiate(shopPanelObject));
                shopPanel[ccc].GetComponent<UpgradePanel>().SetValue(item.Title + " 도안", item.Price2);
                shopPanel[ccc].transform.SetParent(shopSlidePanel.transform, false);
                if(ccc == 0) { 
                    shopPanel[ccc].GetComponentInChildren<Button>().onClick.AddListener(delegate { this.BuyDesign(1); });
                }
                else if (ccc == 1)
                {
                    shopPanel[ccc].GetComponentInChildren<Button>().onClick.AddListener(delegate { this.BuyDesign(2); });
                }
                else if (ccc == 2)
                {
                    shopPanel[ccc].GetComponentInChildren<Button>().onClick.AddListener(delegate { this.BuyDesign(3); });
                }
                else if (ccc == 3)
                {
                    shopPanel[ccc].GetComponentInChildren<Button>().onClick.AddListener(delegate { this.BuyDesign(4); });
                }
                ccc++;
            }

        }
        if (shopData.count == 8)
        {
            allSalePanel.SetActive(true);
        }

        Invoke("RefreshAll", 0.01f);
        count++;
    }
    
    void RefreshAll()
    {
        for (int i = 0; shopPanel.Count > i; i++)
        {
            shopPanel[i].GetComponent<UpgradePanel>().Refresh2(shopData.count * 4 + i+1);
        }
    }
    void BuyDesign(int num)
    {
        Destroy(shopPanel[num-1]);
        shopData.buiedData[shopData.count * 4 + num] = true;

        //buiedData에 들어가있는게 정상적인 숫자


        //4개씩 노출되는것이 0개인지 체크후 아래에서 재업로드를 시킴.
        int descount = 0;
        for (int i = shopData.count * 4 + 1; i < shopData.count * 4 + 5; i++)
        {
            
            if (!shopData.buiedData[i])
            {
                descount++;
            }
        }
        Item item = Inventory.database.FetchSculptureItemById(shopData.count * 4 + num);
        graverController.graver.Money -= item.Price2;


        if (descount == 0)
        {
            if(shopData.count == 7)
            {
                allSalePanel.SetActive(true);
            }
            else
            {
                shopData.count++;
                count = 0;
                shopPanel.Clear();
                Upload();
            }       
        }
        makingController.BuyItem(item);
    }
	
}

[Serializable]
public class ShopData
{
    public List<bool> buiedData;
    public int count;

    public ShopData()
    {
        count = 0;
    }
}

