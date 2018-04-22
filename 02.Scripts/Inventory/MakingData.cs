using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MakingData : MonoBehaviour
{
    public Item item;

    MakingController makingController;
    Button button;

    private void Start()
    {
        makingController = GameObject.Find("MakingController").GetComponent<MakingController>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(SelectItem);
    }

    public void SelectItem()
    {
        if (item != null)
        {
            makingController.SelectItem(item);
        }
    }
}
