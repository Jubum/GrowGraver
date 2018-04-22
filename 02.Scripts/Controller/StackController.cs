using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackController : MonoBehaviour {

    public GameObject stackSprite;
    public Inventory inventory;
    public float fillSecond;
    public List<GameObject> stacks = new List<GameObject>();
    public Stacks stack;

    // Use this for initialization
    void Start()
    {
        fillSecond = 0.2f;
        if (System.IO.File.Exists(DataSaver.stackFilePath))
        {
            LoadStack();
        }
        else
        {
            stack = new Stacks();
        }
        StartCoroutine(FillSculpture());
    }
    public void GetStack(Item item)
    {
         GameObject tempNPC = Instantiate(stackSprite);
         tempNPC.GetComponent<SpriteRenderer>().sprite = item.sprite;
         tempNPC.transform.SetParent(this.transform);
         tempNPC.transform.localScale = new Vector3(0.16f / (this.transform.localScale.x / 0.4f), 0.16f / (this.transform.localScale.y / 0.4f), 0.16f);
         tempNPC.transform.localPosition = new Vector2(-1.7f + 0.2f * stacks.Count - 1.6f * (stacks.Count / 8), -0.9f + (stacks.Count / 8) * 0.3f);


        if (item.Rank == 0)
        {
            tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(0, 0, 0, 0);
        }
        else if (item.Rank < 3)
        {
            tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 0);
        }
        else if (item.Rank < 5)
        {
            tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0.5f, 0);
        }
        else if (item.Rank == 5)
        {
            tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0, 0);
        }

        stacks.Add(tempNPC);
        stack.items.Add(item);
    }

    public void LoadStack()
    {
        stack = DataSaver.LoadData<Stacks>(DataSaver.stackFilePath);

        for (int i = 0; i < stack.items.Count; i++)
        {
            stack.items[i].sprite = ItemDatabase.sprites[stack.items[i].Id];
            GameObject tempNPC = Instantiate(stackSprite);
            tempNPC.GetComponent<SpriteRenderer>().sprite = stack.items[i].sprite;
            tempNPC.transform.SetParent(this.transform);
            tempNPC.transform.localScale = new Vector3(0.16f / (this.transform.localScale.x / 0.4f), 0.16f / (this.transform.localScale.y / 0.4f), 0.16f);
            tempNPC.transform.localPosition = new Vector2(-1.7f + 0.2f * stacks.Count - 1.6f * (stacks.Count / 8), -0.9f + (stacks.Count / 8) * 0.3f);

            if (stack.items[i].Rank <1)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(0, 0, 0, 0);
            }
            else if (stack.items[i].Rank < 3)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 0);
            }
            else if (stack.items[i].Rank < 5)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0.5f, 0);
            }
            else if (stack.items[i].Rank == 5)
            {
                tempNPC.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 0, 0);
            }

            stacks.Add(tempNPC);
        }
    }

    public List<Item> DeleteStacks(int weight)
    {
        List<Item> itemTemp = new List<Item>();
        for(int i = stacks.Count-1 ; i >= 0; i--)
        {
            itemTemp.Add(stack.items[i]);
            Destroy(stacks[i]);
            stacks.RemoveAt(i);
            stack.items.RemoveAt(i);
            if (itemTemp.Count == weight) break;
        }

        return itemTemp;
    }
	
	IEnumerator FillSculpture () {
        while (true) {
            if (stack.maxStack > stacks.Count)
            {
                for (int i = 0; i < inventory.inventoryList[1].Items.Count; i++)
                {
                    if (inventory.inventoryList[1].ItemDataInfos[i].Amount != -1)
                    {
                        while(inventory.inventoryList[1].ItemDataInfos[i].Amount > 0) {
                            if (stack.maxStack == stacks.Count) goto Finish;
                            GetStack(inventory.inventoryList[1].ItemDataInfos[i].Item);
                            inventory.DelOneItem(1, i);
                        }
                    }
                }
            }
            Finish:
            yield return new WaitForSeconds(fillSecond);
        }
    }
}

[Serializable]
public class Stacks
{
    public List<Item> items = new List<Item>();
    public int maxStack;

    public Stacks()
    {
        maxStack = 24;
    }
}
