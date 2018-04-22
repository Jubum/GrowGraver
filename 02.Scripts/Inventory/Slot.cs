using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    //초기 슬롯 생성시 id 입력받음.
    public int sort;
    public int id;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    //자신이 드랍당했을때 실행됨
    public void OnDrop(PointerEventData eventData)
    {
        //드래그 하고 있던 아이템의 정보를 받아온다
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        //만약 자신의 자리가 비어있다면.
        if (inv.inventoryList[sort].Items[id].Id == -1)
        {
            //가지고있던 아이템을 아무것도 없는것으로 바꾼다
            inv.inventoryList[sort].Items[droppedItem.Slot] = new Item();

            //드랍당한 자리에 가지고있던 아이템으로 바꾼다
            inv.inventoryList[sort].Items[id] = droppedItem.Item;

            //아이템 슬롯의 정보를 바꿔준다.
            droppedItem.Slot = id;

        }
        //다른자리로 이동, 해당자리 비어있지않음
        else if(droppedItem.Slot != id)
        {
            //드랍당한 자리에 있던 아이템 정보를 가져온다
            Transform item = this.transform.GetChild(0);
            //드랍당한 자리에 있던 아이템의 슬롯 정보를 드랍한 아이템 슬롯 정보와 바꿔준다
            item.GetComponent<ItemData>().Slot = droppedItem.Slot;

            //드랍당한 자리에 있던 아이템의 부모를 드랍한 아이템의 부모와 바꾼다
            item.transform.SetParent(inv.inventoryList[sort].Slots[droppedItem.Slot].transform);
            //드랍당한 자리에 있던 아이템을 드랍한 아이템의 원래 자리로 이동시킨다.
            item.transform.position = inv.inventoryList[sort].Slots[droppedItem.Slot].transform.position;

            //드랍한 아이템슬롯의 아이디를 드랍한 곳의 아이디로 설정해준다.
            droppedItem.Slot = id;

            //드랍한 아이템 슬롯의 부모를 드랍한 곳의 부모로 설정해준다
            droppedItem.transform.SetParent(this.transform);
            //드랍한 아이템 슬롯의 위치를 드랍한 곳으로 바꾼다
            droppedItem.transform.position = this.transform.position;

            //아이템 슬롯 번호에 맞게 인벤토리의 items 배열에 넣어준다.
            inv.inventoryList[sort].Items[droppedItem.Slot] = item.GetComponent<ItemData>().Item;
            //아이템 슬롯에 있던 내 자리에는 드랍한 아이템이 들어간닷
            inv.inventoryList[sort].Items[id] = droppedItem.Item;
        }
    }
}

