using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    
    //Damage
    public int damage = 1;
    public float pushForce = 4;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            //Create a new damage obeject, before sending it to the player
            Damage dmg = new Damage()
               {
                   damageAmount = damage,
                   origin = transform.position,
                   pushForce = pushForce

               };

               coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
