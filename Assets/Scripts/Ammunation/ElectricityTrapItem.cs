using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ElectricityTrapItem : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;
    [SerializeField]
    private float dissapearTime = 5f;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        StartCoroutine("DestroyDelay");
        int random = Random.Range(0, clips.Count);
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.clip = clips[random];
        audio.Play();
    }
    
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(dissapearTime);
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.PlayerhasDied==false)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                playerHealth.ApplyDamage();

            }
        }
        
    }
}
