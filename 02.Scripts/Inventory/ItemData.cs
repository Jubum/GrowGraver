using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

//슬롯에 있는 아이템 데이타를 저장

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item Item { get { return itemDataInfo.Item; } set { itemDataInfo.Item = value; } }
    public int Amount { get { return itemDataInfo.Amount; } set { itemDataInfo.Amount = value; } }
    public int Slot { get { return itemDataInfo.Slot; } set { itemDataInfo.Slot = value; } }
    public int Rank { get { return itemDataInfo.Rank; } set { itemDataInfo.Rank = value; } }
    public ItemDataInfo ItemDataInfo { get { return itemDataInfo; } set { itemDataInfo = value; } }

    public ItemDataInfo itemDataInfo;

    [NonSerialized]
    private Inventory inv;
    [NonSerialized]
    private Tooltip tooltip;
    [NonSerialized]
    private Vector2 offset;

    public ItemData(){
        itemDataInfo = new ItemDataInfo();
    }

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();

        if (Rank == 5)
        {
            this.GetComponentsInChildren<Image>()[1].color = new Color(1, 0, 0);
        }
        else if (Rank < 1)
        {
            this.GetComponentsInChildren<Image>()[1].color = new Color(0, 0, 0, 0);
        }
        else if (Rank < 3)
        {
            this.GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 0);
        }
        else if (Rank < 5)
        {
            this.GetComponentsInChildren<Image>()[1].color = new Color(1, 0.5f, 0);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemDataInfo.Item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            tooltip.Activate(ItemDataInfo);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemDataInfo.Item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int num = 0;
        if (Item.Type == "weapon") num = 0;
        if (Item.Type == "sculpture") num = 1;
        if (Item.Type == "material") num = 2;
        this.transform.SetParent(inv.inventoryList[num].Slots[itemDataInfo.Slot].transform);
        this.transform.position = inv.inventoryList[num].Slots[itemDataInfo.Slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemDataInfo.Item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemDataInfo.Item != null)
        {
            tooltip.Activate(ItemDataInfo);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}

[Serializable]
public class ItemDataInfo
{
    public Item Item { get { return item; } set { item = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public int Slot { get { return slot; } set { slot = value; } }
    public int Rank { get { return rank; } set { rank = value; } }

    public Item item;
    int amount;
    int slot;
    int rank;

    public ItemDataInfo()
    {
        item = new Item();
        amount = -1;
        slot = -1;
        rank = -1;
    }
    public ItemDataInfo(int slot)
    {
        item = new Item();
        amount = -1;
        this.slot = slot;
        rank = -1;
    }

    public ItemDataInfo(Item item, int amount, int slot)
    {
        this.item = item;
        this.amount = amount;
        this.slot = slot;
        this.rank = item.Rank;
    }
}
