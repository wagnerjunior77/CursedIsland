using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{


    private Vector3 originalSize;
    protected BoxCollider2D boxCollider;
    
    protected Vector3 moveDelta;

    protected RaycastHit2D hit;

    public float ySpeed = 0.8f;
    public float xSpeed = 0.8f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame


    protected virtual void UpdateMotor(Vector3 input)
    {
        //Resetar MoveDelta
                moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

                /*Debug.Log(x);
                Debug.Log(y);*/
                
                // Direita ou Esquerda ?
                if (moveDelta.x > 0) {
                    transform.localScale = originalSize;
                } else if (moveDelta.x < 0){
                    transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
                }
                

                //Empurrando o inimigo após levar dano
                moveDelta += pushDirection;

                //Reduzindo a empurrada
                pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

                // Tretas pra resolver colisão (Y)
                hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                    if (hit.collider == null) {
                        //Fazendo o Player se mover
                        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
                    }
                
                // Tretas pra resolver colisão (X)
                hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                    if (hit.collider == null) {
                        //Fazendo o Player se mover
                        transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
                    }
            }
}
