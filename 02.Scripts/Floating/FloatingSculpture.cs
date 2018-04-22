using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingSculpture : MonoBehaviour {

    public Animator animator;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = animator.GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start()
    {

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        Destroy(gameObject, clipInfo[0].clip.length);

    }
    public void SetSprite(Item item)
    {
        renderer.sprite = item.sprite;
        
    }
}
