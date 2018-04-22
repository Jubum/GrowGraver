using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MakingController : MonoBehaviour {

    public GameObject MakingSlotObject;
    public GraverController graverController;

    GameObject makingSlotPanel;
    public GameObject makingPanelObject;

    public Image makingBar;
    public Image currentMakingImage;

    List<GameObject> makingPanel = new List<GameObject>();

    public ShopController shopController;

    int count;

    public Dictionary<int, MakingInfo> makingInfo = new Dictionary<int, MakingInfo>();

    public Dictionary<int, Image> bandImage = new Dictionary<int, Image>();
    // 화면 텍스트 변환을ㅇ ㅟ함
    static GameObject makingTopPanel;

    static Image[] image;
    static Text[] text;

    // Use this for initialization
    void Start()
    {
        count = 0;
        Invoke("Upload", 0.1f);
    }
    public void Upload()
    {
        makingPanelObject.SetActive(true);
        if (count != 0) return;

        makingSlotPanel = GameObject.Find("MakingSlotPanel");
        makingTopPanel = GameObject.Find("MakingTopPanel");

        text = makingTopPanel.GetComponentsInChildren<Text>();
        image = makingTopPanel.GetComponentsInChildren<Image>();

        //List 노출
        if (System.IO.File.Exists(DataSaver.makingFilePath))
        {
            makingInfo = DataSaver.LoadData<Dictionary<int, MakingInfo>>(DataSaver.makingFilePath);
            UpdateItemList();
        }
        else
        {
            BuyItem(Inventory.database.FetchSculptureItemById(0));
        }

        //graver이 가지고 있는 아이템 불러오기
        if (System.IO.File.Exists(DataSaver.filePath))
        {
            SelectItem(graverController.graver.CurrentSculpture);
        }
        else
        {
            SelectItem(Inventory.database.FetchSculptureItemById(0));
        }
        makingPanelObject.SetActive(false);
        count++;
        
    }

    public void UpdateItemList()
    {
        int count = 0;
        foreach (KeyValuePair<int, MakingInfo> making in makingInfo)
        {
            makingInfo[making.Key].Item.sprite = ItemDatabase.sprites[making.Key];
            makingPanel.Add(Instantiate(MakingSlotObject));
            makingPanel[count].GetComponent<UpgradePanel>().SetValue(making.Key);
            makingPanel[count].GetComponentInChildren<MakingData>().item = making.Value.Item;
            makingPanel[count].transform.SetParent(makingSlotPanel.transform, false);

            Image[] imageTemp;
            imageTemp = makingPanel[count].GetComponentsInChildren<Image>();
            bandImage.Add(making.Key, imageTemp[1]);

            if (making.Value.Rank == 5)
            {
                imageTemp[1].color = new Color(1, 0, 0);
            }
            else if (making.Value.Rank >= 3)
            {
                imageTemp[1].color = new Color(1, 0.5f, 0);
            }
            else if (making.Value.Rank >= 1)
            {
                imageTemp[1].color = new Color(1, 1, 0);
            }
            else
            {
                imageTemp[1].color = new Color(0, 0, 0, 0);
            }

            count++;
        }

    }

    public void BuyItem(Item item)
    {
        makingPanel.Add(Instantiate(MakingSlotObject));
        makingInfo.Add(item.Id, new MakingInfo(item));
        makingPanel[makingPanel.Count - 1].GetComponent<UpgradePanel>().SetValue(item.Id);
        makingPanel[makingPanel.Count - 1].GetComponentInChildren<MakingData>().item = makingInfo[item.Id].item;
        makingPanel[makingPanel.Count - 1].transform.SetParent(makingSlotPanel.transform, false);
        Image[] imageTemp = makingPanel[makingPanel.Count - 1].GetComponentsInChildren<Image>();
        imageTemp[1].color = new Color(0, 0, 0, 0.0f);
        bandImage.Add(item.Id, imageTemp[1]);
    }

    public void SelectItem(Item item)
    {
        image[0].sprite = item.sprite;
        text[0].text = item.Title;
        text[1].text = item.Price.ToString("#,###");
        text[3].text = makingInfo[item.Id].Rjfwkr.ToString()+"%";
        text[5].text = makingInfo[item.Id].Audwkr.ToString() + "%";
        text[7].text = makingInfo[item.Id].Eowkr.ToString() + "%";
        text[8].text = "총 " + makingInfo[item.Id].MadeCount.ToString() + "개 제작";
        text[9].text = "LV. " + makingInfo[item.Id].Rank;
        currentMakingImage.sprite = item.sprite;
        graverController.SetCurrentSculpture(item);
        makingBar.fillAmount = (float)(makingInfo[item.Id].MadeCount - makingInfo[item.Id].PreviousRequireExp) / makingInfo[item.Id].RequireExp;
    }


    public void CountUp(Item item)
    {
        makingInfo[item.Id].MadeCount++;
        if (makingInfo[item.Id].Rank < 5)
        {
            makingBar.fillAmount = (float)(makingInfo[item.Id].MadeCount - makingInfo[item.Id].PreviousRequireExp) / makingInfo[item.Id].RequireExp;
            if (makingBar.fillAmount == 1)
            {
                RankUp(item.Id);
                makingBar.fillAmount = 0;
            }
            
        }
        else
        {
            makingBar.fillAmount = 1;
        }
    }
    public void RankUp(int id)
    {
        if (makingInfo[id].Rank == 0)
        {
            makingInfo[id].Rjfwkr = 10;
            bandImage[id].color = new Color(1, 1, 0,1);
            makingInfo[id].PriceUp = 1.2f;
            graverController.graver.CurrentSculpture.Rank = 1;
        }
        else if (makingInfo[id].Rank == 1)
        {
            makingInfo[id].Rjfwkr = 19;
            graverController.graver.CurrentSculpture.Rank = 2;
        }
        else if (makingInfo[id].Rank == 2)
        {
            makingInfo[id].Rjfwkr = 26;
            makingInfo[id].Audwkr = 1;
            bandImage[id].color = new Color(1, 0.5f, 0);
            makingInfo[id].PriceUp = 1.4f;
            graverController.graver.CurrentSculpture.Rank = 3;
        }
        else if (makingInfo[id].Rank == 3)
        {
            makingInfo[id].Rjfwkr = 35;
            makingInfo[id].Audwkr = 2;
            graverController.graver.CurrentSculpture.Rank = 4;
        }
        else if (makingInfo[id].Rank == 4)
        {
            makingInfo[id].Rjfwkr = 43;
            makingInfo[id].Audwkr = 3;
            makingInfo[id].Eowkr = 1;
            bandImage[id].color = new Color(1, 0, 0);
            makingInfo[id].PriceUp = 1.8f;
            graverController.graver.CurrentSculpture.Rank =5;
        }
        makingInfo[id].Rank++;
        makingInfo[id].PreviousRequireExp = makingInfo[id].RequireExp;
        //makingInfo[id].RequireExp = (float)((makingInfo[id].Rank + 1) * 100 +((makingInfo[id].Rank+1) * makingInfo[id].Rank * 25));
        makingInfo[id].RequireExp = (float)(makingInfo[id].Rank + 1) * 10;
    }


    public void WaitRefresh()
    {
        Invoke("RefreshAll", 0.1f);

        Item item = graverController.graver.CurrentSculpture;
        image[0].sprite = item.sprite;
        text[0].text = item.Title;
        text[1].text = item.Price.ToString("#,###");
        text[3].text = makingInfo[item.Id].Rjfwkr.ToString() + "%";
        text[5].text = makingInfo[item.Id].Audwkr.ToString() + "%";
        text[7].text = makingInfo[item.Id].Eowkr.ToString() + "%";
        text[8].text = "총 " + makingInfo[item.Id].MadeCount.ToString() + "개 제작";
        text[9].text = "LV. " + makingInfo[item.Id].Rank;
    }
    public void RefreshAll()
    {
        for (int i = 0; makingPanel.Count > i; i++)
        {
            makingPanel[i].GetComponent<UpgradePanel>().Refresh3();
        }

    }
}

[Serializable]
public class MakingInfo
{
    public Item Item { get { return item; } set { item = value; } }
    public int Rank { get { return rank; } set { rank = value; } }
    public int Rjfwkr { get { return rjfwkr; } set { rjfwkr = value; } }
    public int Audwkr { get { return audwkr; } set { audwkr = value; } }
    public int Eowkr { get { return eowkr; } set { eowkr = value; } }
    public int MadeCount { get { return madeCount; } set { madeCount = value; } }
    public float PriceUp { get { return priceUp; } set { priceUp = value; } }
    public float RequireExp { get { return requireExp; } set { requireExp = value; } }
    public float PreviousRequireExp { get { return previousRrequireExp; } set { previousRrequireExp = value; } }

    public Item item;
    int rank;
    int rjfwkr;
    int audwkr;
    int eowkr;
    int madeCount;
    float priceUp;
    float requireExp;
    float previousRrequireExp;

    public MakingInfo(Item item)
    {
        Item = item;
        rank = 0;
        Rjfwkr = 0;
        Audwkr = 0;
        Eowkr = 0;
        priceUp = 1;
        MadeCount = 0;
        RequireExp = 10;
        PreviousRequireExp = 0;
    }
}
