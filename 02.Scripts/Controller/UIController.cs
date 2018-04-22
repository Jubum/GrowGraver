using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameController gameController;
    public GameObject backButton;
    public GameObject warningPopUp;
    public List<GameObject> currentPanel = new List<GameObject>();
    public List<GameObject> backButtons = new List<GameObject>();

    public void OnPanel(GameObject panel)
    {
        currentPanel.Add(panel);
        backButtons.Add(Instantiate(backButton));
        backButtons[backButtons.Count-1].transform.SetParent(panel.transform.parent, false);
        backButtons[backButtons.Count-1].transform.SetSiblingIndex(panel.transform.GetSiblingIndex());

        panel.SetActive(true);

    }
    public void OnWarningPopUp()
    {
        OnPanel(warningPopUp);
        warningPopUp.GetComponentInChildren<Text>().text = "소지 금액이 부족합니다";
    }
    public void OnWarningPopUp(string text)
    {
        OnPanel(warningPopUp);
        warningPopUp.GetComponentInChildren<Text>().text = text;
    }
    public void OnToggle(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void OffToggle(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ABB()
    {

        if (currentPanel.Count > 0)
        {
            int count = currentPanel.Count - 1;
            currentPanel[count].SetActive(false);
            currentPanel.Remove(currentPanel[count]);
            Destroy(backButtons[count]);
            backButtons.Remove(backButtons[count]);
        }

    }
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ABB();
            }
        } 

    }
}
