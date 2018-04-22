using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public GameObject NPCObject;
    GameObject storage;
    StorageController storage2;
    public Vector2 graverPosition;
    public float storageRange { get; set; }

    int mode;
    float y;
    float x;

    // Use this for initialization
    void Start()
    {
        mode = 0;
        y = Random.Range(-1f, -8f);
        x = 0;
        if(y > -4f)
        {
            if (Random.Range(-1, 2) > 0) x = -4;
            else x = 4;
        }
        else { x = Random.Range(-4f, 4f); }

        this.transform.position = new Vector2(x, y);
        storage = GameObject.Find("Storage");
        storage2 = storage.GetComponent<StorageController>();
        storageRange = storage.transform.localScale.x*2;
        graverPosition = storage.transform.position - new Vector3(Random.Range(-storageRange, storageRange), 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, graverPosition, 1 * Time.deltaTime);
        if(mode == 0) Arrival();
    }

    void Arrival()
    {
        if(Mathf.Round(transform.position.x * 100.0f) == Mathf.Round(graverPosition.x * 100.0f) && Mathf.Round(transform.position.y * 100.0f) == Mathf.Round(graverPosition.y * 100.0f))
        {
            if (storage2.exhibits.Count > 0)
            {
                if (UnityEngine.Random.Range(0, 100) < 80)
                {
                    BuyItem();
                }
                else
                {
                    Disapoint();
                }
            }
            else
            {
                Disapoint();
            }
            mode++;
        }
    }
    void Disapoint()
    {
        Invoke("Leave", 2);
        Invoke("DeleteNPC", 7);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void BuyItem()
    {
        if (storage2.exhibits.Count > 0)
        {
            storage2.DeleteExhibit(1, this.transform);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        Invoke("Leave", 2);
        Invoke("DeleteNPC", 7);
    }

    void Leave()
    {
        graverPosition = new Vector2(x, y);
    }

    void DeleteNPC()
    {
        Destroy(NPCObject);
    }
}
