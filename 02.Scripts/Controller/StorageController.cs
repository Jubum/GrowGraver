using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageController : MonoBehaviour {

    public GameObject exhibit;
    public GraverController gc;
    public float regenTime;
    public List<GameObject> exhibits = new List<GameObject>();
    public Storage storage;
    public MakingController makingController;

    public bool moneyBuff;

    // Use this for initialization
    void Start()
    {
        
        regenTime = 1;

        if (System.IO.File.Exists(DataSaver.storageFilePath))
        {
            LoadExhibit();
        }
        else
        {
            storage = new Storage();
        }

    }
    public void LoadExhibit()
    {
        storage = DataSaver.LoadData<Storage>(DataSaver.storageFilePath);

        for (int i = 0; i < storage.items.Count; i++)
        {
            storage.items[i].sprite = ItemDatabase.sprites[storage.items[i].Id];
            GameObject tempNPC = Instantiate(exhibit);
            tempNPC.GetComponent<SpriteRenderer>().sprite = storage.items[i].sprite;
            tempNPC.transform.SetParent(this.transform);
            tempNPC.transform.localScale = new Vector3(0.16f / (this.transform.localScale.x / 0.4f), 0.16f / (this.transform.localScale.y / 0.4f), 0.16f);
            tempNPC.transform.localPosition = new Vector2(-1.7f + 0.5f * exhibits.Count - 4.0f * (exhibits.Count / 8), 0.9f - (exhibits.Count / 8) * 0.8f);

            if (storage.items[i].Rank == 0)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(0, 0, 0, 0);
            }
            else if (storage.items[i].Rank <3)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 0);
            }
            else if (storage.items[i].Rank <4)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0.5f, 0);
            }
            else if (storage.items[i].Rank == 5)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0, 0);
            }

            exhibits.Add(tempNPC);
        }
    }

    public void FillExhibit(List<Item> item)
    {
        
        for (int i = 0; i < item.Count; i++) {
            if (storage.maxSlot <= exhibits.Count)
            {
                gc.graver.Money += item[i].Price * makingController.makingInfo[item[i].Id].PriceUp;
            }
            else { 
                GameObject tempNPC = Instantiate(exhibit);
                tempNPC.GetComponent<SpriteRenderer>().sprite = item[i].sprite;
                tempNPC.transform.SetParent(this.transform);
                tempNPC.transform.localScale = new Vector3(0.16f / (this.transform.localScale.x / 0.4f), 0.16f / (this.transform.localScale.y / 0.4f), 0.16f);
                tempNPC.transform.localPosition = new Vector2(-1.7f + 0.5f * exhibits.Count - 4.0f * (exhibits.Count / 8), 0.9f - (exhibits.Count / 8) * 0.8f);

                if (item[i].Rank == 0)
                {
                    tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(0, 0, 0, 0);
                }
                else if (item[i].Rank < 3)
                {
                    tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 0);
                }
                else if (item[i].Rank < 5)
                {
                    tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0.5f, 0);
                }
                else if (item[i].Rank == 5)
                {
                    tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0, 0);
                }

                exhibits.Add(tempNPC);
                storage.items.Add(item[i]);
            }
        }
    }

    public void DeleteExhibit(int num, Transform NPCtransform)
    {
        int count = 0;
        float money =0;
        for (int i = exhibits.Count - 1; i >= 0; i--)
        {
            Debug.Log("storage.items[i].Id" + storage.items[i].Id);
            if(makingController.makingInfo[storage.items[i].Id].Rank != 0) {
                money += storage.items[i].Price * makingController.makingInfo[storage.items[i].Id].PriceUp;
            }
            else
            {
                money += storage.items[i].Price;
            }
            Destroy(exhibits[i]);
            exhibits.RemoveAt(i);
            storage.items.RemoveAt(i);
            count++;
            if (count == num) break;
        }
        if(moneyBuff) gc.graver.Money += money * 2;
        gc.graver.Money += money;
        FloatingTextController.CreateFloatingMoneyText(money.ToString(), NPCtransform);
    }
    public void OnMoneyBuff()
    {
        if (gc.graver.Diamond > 0 && !moneyBuff)
        {
            moneyBuff = true;
            Invoke("OffMoneyBuff", 30);
            gc.graver.Diamond -= 1;
        }
    }

    public void OffMoneyBuff()
    {
        moneyBuff = false;
    }

    public int GetItemInfoMoney(int num)
    {
        if (num < 0) return 0;
        return storage.items[num].Price;
    }

}
[Serializable]
public class Storage
{
    public List<Item> items = new List<Item>();
    public int maxSlot;
    public int level;

    public Storage()
    {
        level = 1;
        maxSlot = 24;
    }
}