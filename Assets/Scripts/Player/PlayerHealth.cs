using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health=5;
    private int startHealth;
    private float healthPercent = 1.0f;
    private float decreaseAmount;
    [SerializeField]
    private Animator anim;
    private bool isDead = false;
    private bool isDodging = false;
    PlayerMove playerMove;
    BowAim bowAim;
    private EnemySounds sounds;
    private GameObject potionObject;
    private bool canPickHealth = false;
    public bool IsDodging { get => isDodging; set => isDodging = value; }

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        decreaseAmount = 1f / health;
        healthPercent = 1.0f;
        GameManager.Instance.DecreaseHealth(healthPercent);
      
        playerMove = GetComponent<PlayerMove>();
        sounds = GetComponent<EnemySounds>();
        bowAim = GetComponent<BowAim>();
       
    }

    public void ApplyDamage()
    {
        if(!isDodging)
        {
            anim.Play("aloy_DamageAnim");
            health--;
            healthPercent -= decreaseAmount;
            GameManager.Instance.DecreaseHealth(healthPercent);
            sounds.PlayHurtSound();
            CheckIfDead();

        }
    
    }
    private void CheckIfDead()
    {
        if(health<1)
        {
            bowAim.PlayerDied();
            bowAim.enabled = false;
            isDead = true;
            Debug.Log("PlayerIsDead");
            GameManager.Instance.PlayerIsDead();
            playerMove.enabled = false;
            anim.Play("Death");
           // sounds.PlayDeathSound();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Herb"))
        {
            potionObject = collision.gameObject;
            canPickHealth = true;
            GameManager.Instance.ActivateUseHealthPanel(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Herb"))
        {
          
            canPickHealth = false;
            GameManager.Instance.ActivateUseHealthPanel(false);
        }
    }
    public void InteractionPressed()
    {
        Debug.Log("pressed Triangle");
        if(canPickHealth && !isDead)
        {
            health = startHealth;
            healthPercent = 1.0f;
            GameManager.Instance.DecreaseHealth(healthPercent);
            sounds.PlayHealUpSound();
            Destroy(potionObject);
        }
    }
}
