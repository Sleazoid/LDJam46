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
    private float attackSpeed = 4f;
    [SerializeField]
    private float yLoopSpeed = 2f;
    [SerializeField]
    private float yLoopPos = 1f;
    [SerializeField]
    private bool moveSideways = true;
    SpriteRenderer sprite;
    private bool dead = false;
    public bool playerNoticed = false;
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
    private EnemyHealthBarScript healthBar;
    private EnemySounds sounds;
    [SerializeField]
    private float distanceToDetect;
    private Vector2 flyToPos;
    private bool isDying = false;
    private float detectedStartRelativePosX = 0;
    public bool debugPosition;
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
        healthBar = GetComponent<EnemyHealthBarScript>();
        healthBar.InitHealthBarValues(health);
        sounds = GetComponent<EnemySounds>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.DrawRay(this.transform.position, this.transform.right,Color.green,1f);
        if (moveSideways && !playerNoticed)
        {
            
            float sidewayValue = Mathf.PingPong(Time.time * loopSpeed,  xLoopPos);
            if (sidewayValue > xLoopPos - 0.1f && this.transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (sidewayValue < 0.1f && this.transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            this.transform.position = new Vector2(startPos.x + sidewayValue - xLoopPos,
      startPos.y + Mathf.Sin(Time.time * yLoopSpeed) * yLoopPos);

            //if(debugPosition)
            //{
            //    Debug.Log("aaaasdg");
            //    //Debug.Log("sidewayval: "+sidewayValue);
            //    //Debug.Log("to Value: " + startPos.x + xLoopPos);
            //}

        }
        if (!playerNoticed && !isDying)
        {
            if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) < distanceToDetect)
            {
                detectedStartRelativePosX = playerTransform.position.x;
                NoticePlayer();
            }
        }
       
    }
    private void FixedUpdate()
    {
        if (playerNoticed && !isDying)
        {
            //flyToPos = playerTransform.position.x;
            Vector3 thisPos = transform.position;
            Vector3 position = Vector3.MoveTowards(rb.position, flyToPos, attackSpeed * Time.fixedDeltaTime);
            thisPos = new Vector2(position.x, thisPos.y);
            rb.MovePosition(thisPos);
           // Debug.Log("taalla22 "+ flyToPos);
        }
    }
    private void UpdateRunToPos()
    {
        if(!isDying)
        {
           // playerTransform = playerGO.transform;
         //  Debug.Log("taalla");
            Vector2 lastFlyToPos = flyToPos;
            //Vector3 dir = player.position - this.transform.position;
            Vector2 dir = new Vector2( (playerTransform.position.x - this.transform.position.x), this.transform.position.y).normalized * 4;
            flyToPos = playerTransform.position * dir; //new Vector2(playerTransform.position.x + (playerTransform.position.x - this.transform.position.x),this.transform.position.y).normalized * 6;
            if (lastFlyToPos.x < flyToPos.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (lastFlyToPos.x > flyToPos.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
       

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(this.transform.position, distanceToDetect);
    //}
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

        healthBar.DisableHealthBar();
        rb.velocity = new Vector2(0, 0);
        moveSideways = false;
        CancelInvoke();
        StartCoroutine("waitToDeath");
    }
    IEnumerator waitToDeath()
    {
        isDying = true;
        anim.Play("Glide");
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 1;
        groundCollider.SetActive(true);
        GameManager.Instance.EnemyDied();
        yield return null;
    }
    private void NoticePlayer()
    {
        playerNoticed = true;
        anim.SetBool("attacking", true);
        //if (!playerNoticed)
        //{
        //    playerNoticed = true;
        //}
        if (!isShooting)
        {
            InvokeRepeating("ShootElectricity", 1f, shootDelay);
            isShooting = true;
        }
        InvokeRepeating("UpdateRunToPos", 0f, 3f);
    }
    public void ArrowHit()
    {
        if (!dead && !isDying)// && !playerNoticed)
        {
            anim.Play("TakeDamage");

            health -= 1;
            sounds.PlayHurtSound();
            CheckHealth();

            healthBar.UpdateHealthBar();
            if(!playerNoticed)
            {
                NoticePlayer();
            }
          
        }

    }
    private void ShootElectricity()
    {
        Vector2 facing = transform.TransformDirection(Vector2.right);
        Vector3 toPlayer = (playerTransform.position - transform.position).normalized;
        //if (Vector3.Dot(facing, toPlayer) < 0)
        {
            GameObject bulletGO = Instantiate(bullet, this.transform.position, Quaternion.identity);
            bulletGO.GetComponent<ElectricityBullet>().InitDirAndForce(bulletForce, toPlayer);
        }
        sounds.PlayAttackSound();
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
                sounds.PlayDeathSound();
                anim.Play("ToDeath");
                dead = true;
                this.GetComponent<Collider2D>().enabled = false;
            }


        }

    }
    public void PlayerIsDead()
    {
        CancelInvoke();
        this.enabled = false;

    }

}
