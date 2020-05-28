using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health=5;
    private float healthPercent = 1.0f;
    private float decreaseAmount;
    [SerializeField]
    private Animator anim;
    private bool isDead = false;
    private bool isDodging = false;
    PlayerMove playerMove;

    public bool IsDodging { get => isDodging; set => isDodging = value; }

    // Start is called before the first frame update
    void Start()
    {
        
        decreaseAmount = 1f / 5;
        playerMove = GetComponent<PlayerMove>();
    }

    public void ApplyDamage()
    {
        if(!isDodging)
        {
            anim.Play("aloy_DamageAnim");
            health--;
            healthPercent -= decreaseAmount;
            GameManager.Instance.DecreaseHealth(healthPercent);
            CheckIfDead();
        }
    
    }
    private void CheckIfDead()
    {
        if(health<1)
        {
            isDead = true;
            Debug.Log("PlayerIsDead");
            GameManager.Instance.PlayerIsDead();
            playerMove.enabled = false;
            anim.Play("Death");
        }
    }
}
