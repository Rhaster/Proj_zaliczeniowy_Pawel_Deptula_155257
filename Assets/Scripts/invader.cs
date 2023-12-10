using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invader : MonoBehaviour
{
    public Sprite[] animationSprites; // deklaracja tablicy spritów 
    public float animationtime =1.0f;
    private SpriteRenderer spriteRender;
    private int animationframe;
    public System.Action killed;
    // Start is called before the first frame update
    private void Awake()
    {
     spriteRender = GetComponent<SpriteRenderer>();   
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationtime,this.animationtime);
    }
    // Update is called once per frame
    private void AnimateSprite()
    {
        animationframe++;
        if(animationframe >= animationSprites.Length)
        {
            animationframe = 0;
        }
        spriteRender.sprite = animationSprites[animationframe];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.killed.Invoke();
            soundManager.Instance.PlaySound(soundManager.Sound.Zniszczenie);
            this.gameObject.SetActive(false);
        }
    }
}
