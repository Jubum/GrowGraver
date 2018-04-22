using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DogamData : MonoBehaviour
{
    public Item item;

    DogamController dogamController;
    Button button;

    private void Start()
    {
        dogamController = GameObject.Find("DogamController").GetComponent<DogamController>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(SelectItem);
    }

    public void SelectItem()
    {
        if (item != null)
        {
            dogamController.SelectItem(item);
        }
    }
}