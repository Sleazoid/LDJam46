using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StormBirdEnemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    private Animator anim;
    [SerializeField]
    private float xLoopPos = 5.3f;

    private Vector2 startPos;
    [SerializeField]
    private float loopSpeed = 2f;
    [SerializeField]
    private float yLoopSpeed = 2f;
    [SerializeField]
    private float yLoopPos = 1f;
    [SerializeField]
    private bool moveSideways = true;
    SpriteRenderer sprite;
    private bool dead = false;
    private bool playerNoticed = false;
    Rigidbody2D rb;
    private GameObject playerGO;
    private PlayerHealth playerHealth;
    [SerializeField]
    private float shootDelay = 3f;
    [SerializeField]
    private GameObject groundCollider;
    private bool isShooting = false;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletForce;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerGO.GetComponent<PlayerHealth>();
        playerTransform = playerGO.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSideways)
        {
            float sidewayValue = Mathf.PingPong(Time.time * loopSpeed, startPos.x + xLoopPos);
            if (sidewayValue > startPos.x + xLoopPos - 0.1f && this.transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (sidewayValue < 0.1f && this.transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            this.transform.position = new Vector2(startPos.x + sidewayValue - xLoopPos,
            startPos.y + Mathf.Sin(Time.time * yLoopSpeed) * yLoopPos);
        }
    }

    private void CheckHealth()
    {
        if (health < 1 && !dead)
        {
          
            playerNoticed = false;
            Death();
        }
    }
    private void Death()
    {
        rb.velocity = new Vector2(0,0);
        moveSideways = false;
        CancelInvoke();
        StartCoroutine("waitToDeath");
    }
    IEnumerator waitToDeath()
    {
        anim.Play("Glide");
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 1;
        groundCollider.SetActive(true);
        yield return null;
    }
    public void ArrowHit()
    {
        if (!dead)// && !playerNoticed)
        {
            anim.Play("TakeDamage");
            playerNoticed = true;
            anim.SetBool("attacking", true);
            health -= 10;
            CheckHealth();
            if(!isShooting)
            {
                InvokeRepeating("ShootElectricity", 1f, shootDelay);
                isShooting = true;
            }
         
        }

    }
    private void ShootElectricity()
    {
        Vector2 facing = transform.TransformDirection(Vector2.right);
        Vector3 toPlayer = playerTransform.position - transform.position;
        //if (Vector3.Dot(facing, toPlayer) < 0)
        {
            GameObject bulletGO = Instantiate(bullet,this.transform.position,Quaternion.identity);
            bulletGO.GetComponent<ElectricityBullet>().InitDirAndForce(bulletForce, toPlayer);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead)// && !playerNoticed)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                playerHealth.ApplyDamage();
            }
            else if (collision.gameObject.tag.Equals("Ground"))
            {
                anim.Play("ToDeath");
                dead = true;
                this.GetComponent<Collider2D>().enabled = false;
            }


        }
            
    }

}
