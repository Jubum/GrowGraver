using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogamController : MonoBehaviour {

    public GameObject dogamSlotObject;
    public MakingController mc;

    GameObject dogamSlotPanel;
    public GameObject dogamPanelObject;

    public Image dogamBar;
    public Image currentDogamImage;
    public Item currentDogamItem;

    List<GameObject> dogamPanel = new List<GameObject>();

    int count;

    // 화면 텍스트 변환을ㅇ ㅟ함
    static GameObject dogamTopPanel;

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
        dogamPanelObject.SetActive(true);
        if (count != 0) return;

        dogamSlotPanel = GameObject.Find("DogamSlotPanel");
        dogamTopPanel = GameObject.Find("DogamTopPanel");

        text = dogamTopPanel.GetComponentsInChildren<Text>();
        image = dogamTopPanel.GetComponentsInChildren<Image>();
        if (System.IO.File.Exists(DataSaver.makingFilePath))
        {
            UpdateItemList();
        }
        SelectItem(Inventory.database.FetchSculptureItemById(0));

        dogamPanelObject.SetActive(false);
        count++;

    }

    public void UpdateItemList()
    {
        int count = 0;
        foreach (KeyValuePair<int, MakingInfo> making in mc.makingInfo)
        {
            dogamPanel.Add(Instantiate(dogamSlotObject));
            dogamPanel[count].GetComponent<UpgradePanel>().SetValue(making.Key);
            dogamPanel[count].GetComponentInChildren<DogamData>().item = making.Value.Item;
            dogamPanel[count].transform.SetParent(dogamSlotPanel.transform, false);

            Image[] imageTemp;
            imageTemp = dogamPanel[count].GetComponentsInChildren<Image>();

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
            imageTemp[3].color = new Color(0, 0, 0, 0);
            imageTemp[3].rectTransform.position = new Vector2(-300, 0);

           count++;
        }
        for(int i = dogamPanel.Count; i < 33; i++)
        {
            dogamPanel.Add(Instantiate(dogamSlotObject));
            dogamPanel[i].GetComponent<UpgradePanel>().SetValue(true);
            dogamPanel[i].transform.SetParent(dogamSlotPanel.transform, false);
        }

    }

    public void SelectItem(Item item)
    {
        currentDogamItem = item;
        image[0].sprite = item.sprite;
        text[0].text = item.Title;
        text[1].text = item.Price.ToString("#,###");
        text[3].text = mc.makingInfo[item.Id].Rjfwkr.ToString() + "%";
        text[5].text = mc.makingInfo[item.Id].Audwkr.ToString() + "%";
        text[7].text = mc.makingInfo[item.Id].Eowkr.ToString() + "%";
        text[8].text = "총 " + mc.makingInfo[item.Id].MadeCount.ToString() + "개 제작";
        text[9].text = "LV. " + mc.makingInfo[item.Id].Rank;
        currentDogamImage.sprite = item.sprite;
        dogamBar.fillAmount = (float)(mc.makingInfo[item.Id].MadeCount - mc.makingInfo[item.Id].PreviousRequireExp) / mc.makingInfo[item.Id].RequireExp;
    }


    public void WaitRefresh()
    {
        Invoke("RefreshAll", 0.1f);

        Item item = currentDogamItem;
        image[0].sprite = item.sprite;
        text[0].text = item.Title;
        text[1].text = item.Price.ToString("#,###");
        text[3].text = mc.makingInfo[item.Id].Rjfwkr.ToString() + "%";
        text[5].text = mc.makingInfo[item.Id].Audwkr.ToString() + "%";
        text[7].text = mc.makingInfo[item.Id].Eowkr.ToString() + "%";
        text[8].text = "총 " + mc.makingInfo[item.Id].MadeCount.ToString() + "개 제작";
        text[9].text = "LV. " + mc.makingInfo[item.Id].Rank;
    }
    public void RefreshAll()
    {
        for (int i = 0; dogamPanel.Count > i; i++)
        {
            dogamPanel[i].GetComponent<UpgradePanel>().Refresh3();
        }

    }
}
