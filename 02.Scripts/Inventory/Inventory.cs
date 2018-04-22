using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {


    public GameObject inventoryPanel;
    static public ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public Tooltip tooltip;
    public GraverController graverController;
    public UIController uiController;

    public GameObject yesNoPopUp;
    public Text yesNoText;
    public Button yesButton;

    public List<InventoryInfo> inventoryList = new List<InventoryInfo>();

    void Start()
    {
        inventoryPanel.SetActive(true);
        //0
        inventoryList.Add(new InventoryInfo("weaponPanel"));
        //1
        inventoryList.Add(new InventoryInfo("sculpturePanel"));
        //2
        inventoryList.Add(new InventoryInfo("materialPanel"));
        
        database = GetComponent<ItemDatabase>();
        
        for(int i = 0; i < inventoryList.Count; i++)
        {
            //기본적으로 해줘야 하는 작업들
            inventoryList[i].SlotAmount = 30;
            inventoryList[i].SlotPanel = inventoryPanel.transform.Find(inventoryList[i].PanelName+"/SlotPanel").gameObject;
            for (int j = 0; j < inventoryList[i].SlotAmount; j++)
            {
                //ItemDataInfos 에도 빈공간을 생성해중
                inventoryList[i].ItemDataInfos.Add(new ItemDataInfo());
                //슬롯에 빈 아이템 들을 생성해줌
                inventoryList[i].Items.Add(new Item());
                //슬롯을 모두 생성
                inventoryList[i].Slots.Add(Instantiate(inventorySlot));
                //슬롯 아이디 지정
                inventoryList[i].Slots[j].GetComponent<Slot>().id = j;
                inventoryList[i].Slots[j].GetComponent<Slot>().sort = i;
                //슬롯 부모를슬롯 패널로 지정
                inventoryList[i].Slots[j].transform.SetParent(inventoryList[i].SlotPanel.transform, false);
            }
            if (System.IO.File.Exists(DataSaver.inventoryFilePath[i]))
            {
                LoadInventory(i);
            }
        }
        

        inventoryPanel.SetActive(false);
    }

    public void AddItem(Item item)
    {
        int num = CheckCategory(item);
        //이미 인벤에 있으면 개수 추가
        if (item.Stackable && CheckInInventory(num, item))
        {
            for (int i = 0; i < inventoryList[num].Items.Count; i++)
            {
                //아이디가 같은것을 찾는다
                if (inventoryList[num].Items[i].Id == item.Id && inventoryList[num].ItemDataInfos[i].Rank == item.Rank)
                {
                    //슬롯 아래 있는 아이템 데이터를 받아옴
                    ItemData data = inventoryList[num].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.Amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.Amount.ToString();
                    break;
                }
            }
        }
        //없을 시 아이템 인벤 추가
        else
        {
            for (int i = 0; i < inventoryList[num].Items.Count; i++)
            {
                //빈칸을 찾는다
                if (inventoryList[num].Items[i].Id == -1)
                {
                    //해당 아이템을 빈칸에다가 추가한다
                    inventoryList[num].Items[i] = item;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().Item = item;
                    itemObj.GetComponent<ItemData>().Amount = 1;
                    itemObj.transform.GetChild(0).GetComponent<Text>().text = "1";
                    itemObj.GetComponent<ItemData>().Slot = i;
                    itemObj.GetComponent<ItemData>().Rank = item.Rank;
                    itemObj.transform.SetParent(inventoryList[num].Slots[i].transform, false);
                    itemObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = item.sprite;
                    itemObj.name = item.Title;
                    //데이터 저장을 위해 데이터 인포에 주가한다
                    inventoryList[num].ItemDataInfos[i] = itemObj.GetComponent<ItemData>().ItemDataInfo;
                    break;
                }
            }
        }
    }
    public void SaleCurrentSculptureCheck()
    {
        if (tooltip.selected)
        {
            uiController.OnPanel(yesNoPopUp);
            yesNoText.text = "정말 " + tooltip.itemDataInfo.Item.Title+ "("+ tooltip.itemDataInfo.Rank+ " 랭크)전부를 반 값에 판매 하시겠습니까?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(SaleCurrentSculpture);
        }
    }
    public void SaleCurrentSculpture()
    {
        if (tooltip.selected)
        {
            ItemDataInfo itemInfo = tooltip.itemDataInfo;
            graverController.graver.Money += (itemInfo.item.Price / 2) * itemInfo.Amount;
            inventoryList[1].Items[itemInfo.Slot] = new Item();
            inventoryList[1].ItemDataInfos[itemInfo.Slot] = new ItemDataInfo();
            DestroyObject(inventoryList[1].Slots[itemInfo.Slot].transform.GetChild(0).gameObject);
            tooltip.ClearString();
        }
    }
    public void SaleAllSculptureCheck() {
        uiController.OnPanel(yesNoPopUp);
        yesNoText.text = "정말 모든 조각품을 반 값에 판매 하시겠습니까?";
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(SaleAllSculpture);
    }
    public void SaleAllSculpture()
    {

        for (int i = 0; i < inventoryList[1].Items.Count; i++)
        {
            if (inventoryList[1].Items[i].Id != -1)
            {
                ItemData itemData = inventoryList[1].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (itemData.Rank == 0)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * itemData.Amount;
                }
                else if (itemData.Rank < 3)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.2f);
                }
                else if (itemData.Rank < 5)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.4f);
                }
                else if (itemData.Rank == 5)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.8f);
                }

                inventoryList[1].Items[i] = new Item();
                inventoryList[1].ItemDataInfos[i] = new ItemDataInfo();
                DestroyObject(inventoryList[1].Slots[i].transform.GetChild(0).gameObject);
            }
        }
        tooltip.ClearString();
    }
    public void SaleRjfwkrSculptureCheck()
    {
        uiController.OnPanel(yesNoPopUp);
        yesNoText.text = "정말 모든 걸작 조각품을 반 값에 판매 하시겠습니까?";
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(SaleRjfwkrSculpture);
    }
    public void SaleRjfwkrSculpture()
    {

        for (int i = 0; i < inventoryList[1].Items.Count; i++)
        {
            if (inventoryList[1].Items[i].Id != -1)
            {
                ItemData itemData = inventoryList[1].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (itemData.Rank > 0 && itemData.Rank < 3)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.2f);
                    inventoryList[1].Items[i] = new Item();
                    inventoryList[1].ItemDataInfos[i] = new ItemDataInfo();
                    DestroyObject(inventoryList[1].Slots[i].transform.GetChild(0).gameObject);
                }
            }
        }
        tooltip.ClearString();
    }
    public void SaleAudwkrSculptureCheck()
    {
        uiController.OnPanel(yesNoPopUp);
        yesNoText.text = "정말 모든 명작 조각품을 반 값에 판매 하시겠습니까?";
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(SaleAudwkrSculpture);
    }
    public void SaleAudwkrSculpture()
    {

        for (int i = 0; i < inventoryList[1].Items.Count; i++)
        {
            if (inventoryList[1].Items[i].Id != -1)
            {
                ItemData itemData = inventoryList[1].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (itemData.Rank > 2 && itemData.Rank < 5)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.4f);
                    inventoryList[1].Items[i] = new Item();
                    inventoryList[1].ItemDataInfos[i] = new ItemDataInfo();
                    DestroyObject(inventoryList[1].Slots[i].transform.GetChild(0).gameObject);
                }
            }
        }
        tooltip.ClearString();
    }
    public void SaleEowkrSculptureCheck()
    {
        uiController.OnPanel(yesNoPopUp);
        yesNoText.text = "정말 모든 대작 조각품을 반 값에 판매 하시겠습니까?";
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(SaleEowkrSculpture);
    }
    public void SaleEowkrSculpture()
    {

        for (int i = 0; i < inventoryList[1].Items.Count; i++)
        {
            if (inventoryList[1].Items[i].Id != -1)
            {
                ItemData itemData = inventoryList[1].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (itemData.Rank == 5)
                {
                    graverController.graver.Money += (inventoryList[1].Items[i].Price / 2) * (itemData.Amount * 1.8f);
                    inventoryList[1].Items[i] = new Item();
                    inventoryList[1].ItemDataInfos[i] = new ItemDataInfo();
                    DestroyObject(inventoryList[1].Slots[i].transform.GetChild(0).gameObject);
                }
            }
        }
        tooltip.ClearString();
    }

    public void DelOneItem(int sort, int num)
    {
        if(inventoryList[sort].ItemDataInfos[num].Amount > 1)
        {
           ItemData data = inventoryList[sort].Slots[num].transform.GetChild(0).GetComponent<ItemData>();
           data.Amount--;
           data.transform.GetChild(0).GetComponent<Text>().text = data.Amount.ToString();
        }
        else { 
            inventoryList[sort].Items[num] = new Item();
            inventoryList[sort].ItemDataInfos[num] = new ItemDataInfo();
            DestroyObject(inventoryList[sort].Slots[num].transform.GetChild(0).gameObject);
        }
    }

    //0 weapon, 1 sculpture , 2,material
    public void AddItem(int sort, int id)
    {
        Item itemToAdd = null;
        if (sort == 0)
        {
            //받아온 번호로 아이템 코드에서 찾아옴
            itemToAdd = database.FetchWeaponItemById(id);
        }
        else if(sort == 1)
        {
            //받아온 번호로 아이템 코드에서 찾아옴
            itemToAdd = database.FetchSculptureItemById(id);
        }
        else if(sort == 2)
        {
            //받아온 번호로 아이템 코드에서 찾아옴
            itemToAdd = database.FetchMaterialItemById(id);
        }
        else { Debug.Log("제대로 입력해라"); }
        
        //이미 인벤에 있으면 개수 추가
        if (itemToAdd.Stackable && CheckInInventory(sort, itemToAdd))
        {
            for(int i=0; i< inventoryList[sort].Items.Count; i++)
            {
                //아이디가 같은것을 찾는다
                if(inventoryList[sort].Items[i].Id == id && inventoryList[sort].Items[i].Rank == itemToAdd.Rank)
                {
                    //슬롯 아래 있는 아이템 데이터를 받아옴
                    ItemData data = inventoryList[sort].Slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.Amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.Amount.ToString();
                    break;
                }
            }
        }
        //없을 시 아이템 인벤 추가
        else
        {
            for(int i = 0; i < inventoryList[sort].Items.Count; i++)
            {
                //빈칸을 찾는다
                if(inventoryList[sort].Items[i].Id == -1)
                {
                    //해당 아이템을 빈칸에다가 추가한다 
                    inventoryList[sort].Items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().Item = itemToAdd;
                    itemObj.GetComponent<ItemData>().Amount = 1;
                    itemObj.transform.GetChild(0).GetComponent<Text>().text = "1";
                    itemObj.GetComponent<ItemData>().Slot = i;
                    itemObj.transform.SetParent(inventoryList[sort].Slots[i].transform, false);
                    itemObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.sprite;
                    itemObj.name = itemToAdd.Title;
                    //데이터 저장을 위해 데이터 인포에 주가한다
                    inventoryList[sort].ItemDataInfos[i] = itemObj.GetComponent<ItemData>().ItemDataInfo;
                    break;
                }
            }
        }
    }

    //인벤토리 안에 아이템ㅇ ㅣ있는지 확인
    bool CheckInInventory(int sort,Item item)
    {
        for(int i=0; i< inventoryList[sort].Items.Count; i++)
        {
            if (inventoryList[sort].Items[i].Id == item.Id)
            {
                if(inventoryList[sort].ItemDataInfos[i].Rank == item.Rank)
                return true;
            }
                
        }
        return false;
    }

    public void LoadInventory(int sort)
    {
        inventoryList[sort].ItemDataInfos = DataSaver.LoadData<List<ItemDataInfo>>(DataSaver.inventoryFilePath[sort]);
        for (int i = 0; i < inventoryList[sort].ItemDataInfos.Count; i++){
            ItemDataInfo info = inventoryList[sort].ItemDataInfos[i];
            if (info.Slot != -1) {
                
                inventoryList[sort].Items[info.Slot] = info.Item;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().ItemDataInfo = info;
                itemObj.transform.GetChild(0).GetComponent<Text>().text = info.Amount.ToString();
                itemObj.transform.SetParent(inventoryList[sort].Slots[info.Slot].transform, false);
                itemObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                inventoryList[sort].Items[info.Slot].sprite = ItemDatabase.sprites[info.item.Id];
                itemObj.GetComponent<Image>().sprite = ItemDatabase.sprites[info.item.Id];
            }
        }
    }
    
    int CheckCategory(Item item)
    {
        if (item.Type == "weapon") return 0;
        if (item.Type == "sculpture") return 1;
        if (item.Type == "material") return 2;

        Debug.Log(item.Type);

        return -1;
    }
}
public class InventoryInfo
{
    public GameObject SlotPanel { get; set; }
    public int SlotAmount { get; set; }
    public List<Item> Items { get; set; }
    public List<ItemDataInfo> ItemDataInfos { get; set; }
    public List<GameObject> Slots { get; set; }
    public string PanelName { get; set; }

    public InventoryInfo(string name)
    {
        PanelName = name;
        Items = new List<Item>();
        ItemDataInfos = new List<ItemDataInfo>();
        Slots = new List<GameObject>();
    }
}