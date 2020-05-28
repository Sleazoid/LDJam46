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
    private bool playerIsDead = false;
    [SerializeField]
    private float distanceToDetect = 4;
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
        


    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    void FixedUpdate()
    {
       // Debug.Log(rb.position.x - lastPos.x);

        if (!playerIsDead)
        {
            anim.SetFloat("velocityX", (rb.position.x - lastPos.x)*100f);
        }
        lastPos = rb.position;
        if (playerNoticed && !dead)
        {


            if (Vector2.Distance(this.transform.position, player.position) < 2f && !attack)
            {
                CheckDirection();
                anim.SetBool("Attack", true);
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
        if (!playerNoticed && !dead && !playerIsDead)
        {
            if (Vector2.Distance(player.position, transform.position) < distanceToDetect)
            {
                DetectPlayer();
            }
        }
    }
    private void GiveDamage()
    {
        if (canGiveDamage)
        {
            playerHealth.ApplyDamage();
            canGiveDamage = false;
            StartCoroutine("EnableDamaging");
        }

    }

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


    }
    public void CheckDirection()
    {

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
        if (Vector2.Distance(this.transform.position, player.position) <= 2.3f && attack) //TODO: ihan hirveää hotfix koodia, tee uusiksi kun on aikaa
        {

            attack = false;
        }
        yield return null;
    }
    private void StartAttack()
    {
        anim.SetBool("PlayerDetected", true);

    }
    private void UpdateRunToPos()
    {
        if (!attack)
        {
            Vector3 dir = player.position - this.transform.position;
            runToPos = player.position + ((player.position - this.transform.position).normalized * 5);
        }


    }
    public void ArrowHit()
    {
        if (!dead)// && !playerNoticed)
        {
            anim.Play("TakeDamage");
            playerNoticed = true;
            runToPos = player.position + ((player.position - this.transform.position).normalized * 2);
            InvokeRepeating("UpdateRunToPos", 0f, 1.5f);
            // if()
            health -= 10;
            CheckHealth();
        }

    }
    private void DetectPlayer()
    {
        if (!dead)// && !playerNoticed)
        {
            anim.Play("TakeDamage");
            playerNoticed = true;
            runToPos = player.position + ((player.position - this.transform.position).normalized * 5);
            InvokeRepeating("UpdateRunToPos", 0f, 1.5f);
            // if()
            health -= 0;
            CheckHealth();
        }

    }
    private void CheckHealth()
    {
        if (health < 1 && !dead)
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
    public void PlayerIsDead()
    {
        anim.SetFloat("velocityX", 0);
        playerIsDead = true;
        playerNoticed = false;
        rb.velocity = new Vector2(0, 0);
        this.enabled = false;
        SetAttackToFalse();
    }
}
