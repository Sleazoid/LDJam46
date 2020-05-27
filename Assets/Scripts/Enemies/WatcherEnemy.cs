using System.Collections;
using System.Collections.Generic;
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
    private float runSpeed;
    private SpriteRenderer rend;
    private Vector3 runToPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = GetComponent<SpriteRenderer>();
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
            Debug.Log(rb.velocity);
            Vector3 thisPos = transform.position;
            Vector3 position = Vector3.MoveTowards(rb.position, runToPos, runSpeed * Time.fixedDeltaTime);
            thisPos = new Vector2(position.x, thisPos.y);
            rb.MovePosition(thisPos);
            if(this.transform.position.x- runToPos.x<0 && rend.flipX ==true)
            {
                rend.flipX = false;
            }
            else if(this.transform.position.x - runToPos.x > 0 && rend.flipX == false)
            {
                rend.flipX = true;
            }

        }
    }
    private void StartAttack()
    {
        anim.SetBool("PlayerDetected", true);

    }
    private void UpdateRunToPos()
    {
        runToPos = player.position;
    }
    public void ArrowHit()
    {
        playerNoticed = true;
        runToPos = player.position;
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
