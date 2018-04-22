using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Text text;
    private Text damageText;
    public int type = 1;
    private Transform location;

    int x = 0;
    int y = 0;
    float speed = 0;
    
    private void Awake()
    {
        damageText = text.GetComponent<Text>();

    }
    public void SetLocation(Transform location)
    {
        this.location = location;
    }

    void Update()
    {
        if (type == 1)
        {
            damageText.transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);
        }
        if (type == 2)
        {
            this.gameObject.transform.Translate(new Vector3(location.position.x, location.position.y + 10) * speed * Time.deltaTime);
        }
    }
    // Use this for initialization
    void Start()
    {

        if (type == 1)
        {
            x = Random.Range(-400, -100);
            y = Random.Range(-150, 150);
        }
        speed = Random.Range(1f, 2f);
        Destroy(gameObject, 2);
    }
    public void SetText(string text)
    {
        damageText.text = text;
    }
	
	
}
