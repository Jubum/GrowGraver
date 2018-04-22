using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    public ItemDataInfo itemDataInfo;
    private string data;
    private string data2;
    private string data3;

    public bool selected;

    public Text text;
    public Text text2;
    public Text text3;

    public void Activate(ItemDataInfo itemDataInfo)
    {
        this.itemDataInfo = itemDataInfo;
        ConstructDataString();
    }

    public void ConstructDataString()
    {
        Item item = itemDataInfo.item;

        if (item.Type.Equals("sculpture")) { 
            data = "<color=#0473f0><b>" + item.Title + "</b></color>\n" + item.Description + "\n희귀도: " + item.RaritySlug + "\n재질: " + item.MaterialSlug;
            data2 = "<color=#FFA400FF>판매가: " + item.Price + "</color>" + "\n<color=#25F941FF>경험치: " + item.Exp + "</color>"
                + "\n소모 체력: " + item.RequireHealth + "\n총 체력 : " + item.RequireWorkValue;
            data3 = "<color=#FFF920FF> 랭크: " + itemDataInfo.Rank+ "\n 명성도: " + item.Fame + "</color>";
        }

        selected = true;

        text.text = data;
        text2.text = data2;
        text3.text = data3;
    }

    public void ClearString()
    {
        text.text = "";
        text2.text = "";
        text3.text = "";
        itemDataInfo = null;
        selected = false;
    }
}
