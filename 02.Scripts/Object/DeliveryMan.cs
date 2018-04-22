using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour {

    Animator animator;
    public GameObject sprite;

    public StackController stackController;
    public StorageController storage;

    Vector2 stackPosition;
    Vector2 originallyPosition;

    bool getItem;
    public int weight;

    public List<Item> items = new List<Item>();

    int mode;

    public int speed;



    // Use this for initialization
    void Start () {
        animator = sprite.GetComponent<Animator>();

        originallyPosition = new Vector2(-4f, 2.5f);
        this.transform.position = originallyPosition;
        stackPosition = new Vector2(-1.12f, 2.5f);
        mode = 0;

        stackController = GameObject.Find("Stack").GetComponent<StackController>();
        storage = GameObject.Find("Storage").GetComponent<StorageController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!getItem)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, stackPosition, 1 * speed * Time.deltaTime);
            if (mode == 0) { 
                if (Mathf.Round(transform.position.x * 100.0f) >= Mathf.Round((stackPosition.x - 0.1f) * 100.0f))
                {
                    items = stackController.DeleteStacks(weight);
                    Invoke("Leave", 2 / speed);
                    Invoke("ChangePosition", 5 /speed);
                    mode++;
                    animator.SetBool("stop", true);
                }
            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, stackPosition, 1 * speed * Time.deltaTime);
            if(mode == 1) { 
                if (Mathf.Round(transform.position.x * 100.0f) >= Mathf.Round((stackPosition.x - 0.1f) * 100.0f))
                {
                    storage.FillExhibit(items);
                    items.RemoveRange(0, items.Count);
                    Invoke("Leave", 2 / speed);
                    mode++;
                    animator.SetBool("stop", true);
                }
            }
        }

    }

    //화면밖에 등장했던 위치로 돌아간다
    void Leave()
    {
        stackPosition = originallyPosition;
        animator.SetBool("stop", false);

        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

    }

    // 목적지를 바꾼다
    void ChangePosition()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;

        originallyPosition = new Vector2(-4f, 0.51f);
        stackPosition = new Vector2(-0.5f, 0.51f);
        this.transform.position = originallyPosition;
        getItem = true;
    }

}
