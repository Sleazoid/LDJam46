using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health=5;
    [SerializeField]
    private Animator anim;

    private bool isDodging = false;

    public bool IsDodging { get => isDodging; set => isDodging = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ApplyDamage()
    {
        if(!isDodging)
        {
            anim.Play("aloy_DamageAnim");
            health--;
            CheckIfDead();
        }
    
    }
    private void CheckIfDead()
    {
        if(health<1)
        {
            Debug.Log("PlayerIsDead");
        }
    }
}
