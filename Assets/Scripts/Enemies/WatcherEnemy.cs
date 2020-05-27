using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using UnityEngine;

public class WatcherEnemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    Animator anim;
    private bool dead = false;
    private Transform player;
    [SerializeField]
    private bool playerNoticed = false;
    Rigidbody2D rb;
    [SerializeField]
    private float runDefSpeed;
    [SerializeField]
    private float runAttackSpeed;
    private float runSpeed;
    private SpriteRenderer rend;
    private Vector3 runToPos;
    private bool canGiveDamage = true;
    PlayerHealth playerHealth;
    [SerializeField]
    private float damageSleepTime = 1f;
    private Vector2 lastPos;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = GetComponent<SpriteRenderer>();
        playerHealth = player.GetComponent<PlayerHealth>();
        runSpeed = runDefSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetFloat("velocityX", rb.velocity.x);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    void FixedUpdate()
    {
        if(playerNoticed && !dead)
        {
           
            
        //    Debug.Log(rb.GetPointVelocity(runToPos));
            //Debug.Log(rb.velocity.normalized+"  ase333");
            //Debug.Log(rb.velocity.magnitude + "  as333345");
            // rb.
            //float step = runSpeed * Time.fixedDeltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, runToPos, step);
            // if (Vector3.Distance(transform.position, lastPos) < 0.1f)
            //if (Vector2.Distance(transform.position, lastPos) < 0.1f)
            //{
            //    anim.Play("WatcherIdleLongerAnim");
            //}
            //else
            //{
            //    anim.Play("Run");
            //}
            if (Vector2.Distance(this.transform.position,player.position)<2f && !attack)
            {
                CheckDirection();
                anim.SetBool("Attack",true);
                UpdateRunToPos();
                attack = true;
                runSpeed = runAttackSpeed;
               // Invoke("GiveDamage", 0.2f);
            }
            else if (Vector2.Distance(this.transform.position, player.position) >= 2.3f && attack)
            {
                attack = false;
                anim.SetBool("Attack", false);
                runSpeed = runDefSpeed;
            }
           
            if (Math.Abs(this.transform.position.x - player.position.x) < 0.1f)
            {
                GiveDamage();
            }
           // if (!attack)
            {
                if (this.transform.position.x - runToPos.x < 0 && rend.flipX == true)
                {
                    //rb.velocity = new Vector2(0, 0);
                    rend.flipX = false;
                }
                if (this.transform.position.x - runToPos.x > 0 && rend.flipX == false)
                {
                    rend.flipX = true;
                    //  rb.velocity = new Vector2(0, 0);
                }
                Vector3 thisPos = transform.position;
                Vector3 position = Vector3.MoveTowards(rb.position, runToPos, runSpeed * Time.fixedDeltaTime);
                thisPos = new Vector2(position.x, thisPos.y);
                rb.MovePosition(thisPos);
            }
            //else
            //{
            //    Vector3 thisPos = transform.position;
            //    Vector3 position;
            //    if (rend.flipX)
            //         position = Vector3.MoveTowards(rb.position, rb.position*(transform.right*5), runSpeed * Time.fixedDeltaTime);
            //    else
            //    {
            //        position = Vector3.MoveTowards(rb.position, rb.position * (-transform.right * 5), runSpeed * Time.fixedDeltaTime);
            //    }
            //    thisPos = new Vector2(position.x, thisPos.y);
            //    rb.MovePosition(thisPos);
            //}
        }
    }
    private void GiveDamage()
    {
        if(canGiveDamage )
        {
           // if(!attack)

            playerHealth.ApplyDamage();
            canGiveDamage = false;
            StartCoroutine("EnableDamaging");
        }
      
    }
    //public void SetAttackToTrue()
    //{
    //    anim.SetBool("Attack", true);
    //}
    IEnumerator ieAttackFalse()
    {
        yield return new WaitForSeconds(0.5f);
        attack = false;
        yield return null;
    }
    public void SetAttackToFalse()
    {
        canGiveDamage = true;
        
        runSpeed = runDefSpeed;
        anim.SetBool("Attack", false);
        StartCoroutine("ieAttackFalse");
        //if(Vector2.Distance(lastPos,this.transform.position)<0.06f)
        //{

        //}
        lastPos = runToPos;
        //if (this.transform.position.x - runToPos.x < 0 && rend.flipX == true)
        //{
        //    rend.flipX = false;
        //}
        //else if (this.transform.position.x - runToPos.x > 0 && rend.flipX == false)
        //{
        //    rend.flipX = true;
        //}
    }
    public void CheckDirection()
    {
       // Debug.LogError("AAAA");
        //attack = true;
        if (this.transform.position.x - player.position.x < 0 && rend.flipX == true)
        {
            rend.flipX = false;
        }
        else if (this.transform.position.x - player.position.x > 0 && rend.flipX == false)
        {
            rend.flipX = true;
        }
    }
    IEnumerator EnableDamaging()
    {
        yield return new WaitForSeconds(damageSleepTime);
        canGiveDamage = true;
        yield return null;
    }
    private void StartAttack()
    {
        anim.SetBool("PlayerDetected", true);

    }
    private void UpdateRunToPos()
    {
       if(!attack)
        {
            Vector3 dir = player.position - this.transform.position;
            runToPos = player.position + ((player.position - this.transform.position).normalized * 5);
        }
        
        //if(Vector2.Distance(lastPos,runToPos)<0.1f)
        //{
        //    anim.Play("WatcherIdleLongerAnim");
        //}
        //else if (Vector2.Distance(lastPos, runToPos) >0.09f)
        //{
        //    anim.SetBool("PlayerDetected", true);
        //}
        
    }
    public void ArrowHit()
    {
        playerNoticed = true;
        runToPos = player.position+((player.position-this.transform.position).normalized*2);
        InvokeRepeating("UpdateRunToPos", 0f, 1.5f);
        // if()
        health -= 10;
        CheckHealth();
    }
    private void CheckHealth()
    {
        if(health<1 && !dead)
        {
            dead = true;
            playerNoticed = false;
            Death();
        }
    }
    private void Death()
    {
        anim.Play("ToDeath");
    }
}
