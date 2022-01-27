using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest;
    public int CoinsAmount = 5;

    private AudioSource aud;
    
    public AudioClip getCoin;
    
    
    protected override void OnCollect(){
        
         if (!collected) {
             collected = true;
             GetComponent<SpriteRenderer>().sprite = emptyChest;
             GameManager.instance.coins += CoinsAmount;
             GameManager.instance.ShowText("+" + CoinsAmount + " moedas!", 35, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
             aud = GetComponent<AudioSource>();
             PlaySFX(getCoin);
         }

    }

    void PlaySFX(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
    
}
