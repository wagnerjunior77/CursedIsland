using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : Collidable
{
    
    //Damage struct
    public int[] damagePoint = {1, 2, 5, 7, 10, 15, 20, 27, 40};
    public float[] pushForce = {2.0f, 2.0f, 2.0f, 2.0f, 2.5f, 2.5f, 3.0f, 3.0f, 3.5f};

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)){
            if (Time.time - lastSwing > cooldown){
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
                    if (coll.name == "Player")
                        return;
            
               // Criando um damage object, para mandar pro fighter que levou hit
               Damage dmg = new Damage()
               {
                   damageAmount = damagePoint[weaponLevel],
                   origin = transform.position,
                   pushForce = pushForce[weaponLevel]

               };

               coll.SendMessage("ReceiveDamage", dmg);
        
            
        }
    }

    private void Swing () {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
