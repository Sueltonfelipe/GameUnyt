using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

     public int vida = 3;
     public float speed;
     public float jumpForce;

     public GameObject kunai;
     public Transform firePoint;
     
     private bool isJumping;
     private bool doubleJump;
     private bool isFire;
     
     private Rigidbody2D rig;
     private Animator anim;

     private float movement;
     
     
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameController.instance.UpdateLives(vida);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        KunaiFire();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if (movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
           
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("transition", 0);
        }
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                { 
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
        
    }

     void KunaiFire()
     {
         StartCoroutine ("Fire");
     }

     IEnumerator Fire()
     {
         if (Input.GetKeyDown(KeyCode.Z))
         {
             isFire = true;
             anim.SetInteger("transition", 3);
             GameObject Kunai = Instantiate(kunai, firePoint.position, firePoint.rotation);

             if (transform.rotation.y == 0)
             {
                 Kunai.GetComponent<kunai>().isRight = true;
             }

             if (transform.rotation.y == 180)
             {
                 Kunai.GetComponent<kunai>().isRight = false;
             }
             yield return new WaitForSeconds(0.2f);
             anim.SetInteger("transition", 0);
         }
     }

     public void Damage(int dmg)
     {
         vida -= dmg;
         GameController.instance.UpdateLives(vida);
         anim.SetTrigger("hit");
         
         
         if (transform.rotation.y == 0)
         {
             transform.position += new Vector3(-1, 0, 0);
         }

         if (transform.rotation.y == 180)
         {
             transform.position += new Vector3(1, 0, 0);
         }
         
         if (vida <= 0)
         {
             GameController.instance.GameOver();
         }
     }

     public void IncreaseLife(int value)
     {
         vida += value;
         GameController.instance.UpdateLives(vida);
     }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 6)
        {
            isJumping = false;
        }
        if (coll.gameObject.layer == 7)
        {
            GameController.instance.GameOver();
        }
    }


}
