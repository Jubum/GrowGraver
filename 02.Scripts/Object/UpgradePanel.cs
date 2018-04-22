using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour {

    int id;
    float price;
    float increase;
    float currentValue;

    string title;

    bool noRefresh;

    Image[] image;

    Text[] text;

    private void Start()
    {
        text = gameObject.GetComponentsInChildren<Text>();
        image = gameObject.GetComponentsInChildren<Image>();
    }

    public void SetValue(string title, float price, float increase, float currentValue)
    {
        this.title = title;
        this.price = price;
        this.increase = increase;
        this.currentValue = currentValue;
    }
    public void SetValue(string title, float price)
    {
        this.title = title;
        this.price = price;
    }
    //Making에 쓰는 거
    public void SetValue(int id)
    {
        this.id = id;
    }
    //도감에 쓰는 거
    public void SetValue(bool no)
    {
        this.noRefresh = no;
    }

    public void Refresh()
    {
        text[0].text = price.ToString("#,###");
        text[1].text = title;
        text[2].text = currentValue + " -> " + (currentValue + increase);
    }
    public void Refresh2(int i)
    {
        text[0].text = price.ToString("#,###");
        text[1].text = title;
        text[2].text = "";
        image[3].sprite = Inventory.database.FetchSculptureItemById(i).sprite;
    }

    public void Refresh3()
    {
        if (!noRefresh)image[2].sprite = ItemDatabase.sprites[id];

    }
}
