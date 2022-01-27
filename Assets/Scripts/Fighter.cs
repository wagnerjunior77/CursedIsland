using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    //Public Fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float imumneTime = 1.0f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    //Todos os Fighters podem receber dano / morrer
    protected virtual void ReceiveDamage(Damage dmg)
    {
         if (Time.time - lastImmune > imumneTime)
         {
             lastImmune = Time.time;
             hitpoint -= dmg.damageAmount;
             pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            string msg = dmg.damageAmount.ToString();
            Debug.Log(msg);
            
            GameManager.instance.ShowText(msg, 35, Color.red, transform.position, Vector3.zero, 0.5f);
            
             if (hitpoint <= 0)
             {
                 hitpoint = 0;
                 Death();
             } 
         }
    } 

    

    protected virtual void Death()
    {

    }
}
