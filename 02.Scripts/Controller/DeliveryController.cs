using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryController : MonoBehaviour {

    public DeliveryMan deliveryMan;
    public GraverController gc;
    public List<DeliveryMan> deliveryMans = new List<DeliveryMan>();
    public Storage storage;

    public Delivery delivery;

    public bool deliveryBuff;
    int speed;


    // Use this for initialization
    void Start () {
        speed = 1;

        if (System.IO.File.Exists(DataSaver.deliveryFilePath))
        {
            delivery = DataSaver.LoadData<Delivery>(DataSaver.deliveryFilePath);
        }
        else
        {
            delivery = new Delivery();
        }
        StartCoroutine(Delivery());


    }

    IEnumerator Delivery()
    {
        while (true) {
            if(delivery.maxDeliveryMan > deliveryMans.Count) {
                deliveryMan.weight = delivery.weight;
                deliveryMan.speed = speed;
                deliveryMans.Add(Instantiate(deliveryMan));
                Invoke("DestroyDeliveryMan",17 / speed);
                yield return new WaitForSeconds(delivery.delay / speed);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
            
        }
    }

    void DestroyDeliveryMan()
    {
        Destroy(deliveryMans[0].gameObject);
        deliveryMans.RemoveAt(0);
    }

    public void OnDeliveryBuff()
    {
        if (gc.graver.Diamond > 0 && !deliveryBuff)
        {
            deliveryBuff = true;
            Invoke("OffDeliveryBuff", 40);
            gc.graver.Diamond -= 1;
            speed =3;
            for (int i = 0; i < deliveryMans.Count; i++) {
                deliveryMans[i].speed = 3;
            }
        }
    }

    public void OffDeliveryBuff()
    {
        deliveryBuff = false;
        speed = 1;
    }

}
[Serializable]
public class Delivery
{
    public int delay;
    public int maxDeliveryMan;
    public int weight;

    public Delivery()
    {
        delay = 11;
        maxDeliveryMan = 1;
        weight = 2;
    }
}
