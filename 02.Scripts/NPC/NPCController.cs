using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public GameObject npc;
    public GraverController gc;
    public float regenTime;
    public float randomTemp;

    public bool npcBuff;

	// Use this for initialization
	void Start () {
        StartCoroutine(RegenNPC());
	}

    IEnumerator RegenNPC()
    {
        while (true) {
            GameObject tempNPC = Instantiate(npc);
            tempNPC.transform.SetParent(this.transform);
            randomTemp = Random.Range(regenTime - 1, regenTime);
            if (npcBuff) yield return new WaitForSeconds(randomTemp/10);
            else { yield return new WaitForSeconds(randomTemp); }
        }
    }

    public void OnNpcBuff()
    {
        if (gc.graver.Diamond > 0 && !npcBuff)
        {
            npcBuff = true;
            Invoke("OffNpcBuff", 30);
            gc.graver.Diamond -= 1;
        }
    }

    public void OffNpcBuff()
    {
        npcBuff = false;
    }

}
