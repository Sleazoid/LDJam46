using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject trapPrefab;
    PlayerHealth playerHealth;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitDirAndForce(float force,Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb.AddForce(dir*force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            Vector2 colpoint = collision.contacts[0].point;
            GameObject trap = Instantiate(trapPrefab, colpoint, Quaternion.identity);
            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerHealth.ApplyDamage();         
            Destroy(this.gameObject);

        }
    }
}
