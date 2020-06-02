using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Käytetään myös pelaajalla
public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> hurtSounds;

    [SerializeField]
    private List<AudioClip> attackSounds;
    [SerializeField]
    private AudioClip healUpSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip jumpSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHurtSound()
    {
        int random = Random.Range(0, hurtSounds.Count);
        audioSource.clip = hurtSounds[random];
        audioSource.Play();
        
    }
    public void PlayAttackSound()
    {
        //if (!audioSource.isPlaying)
        {
            int random = Random.Range(0, attackSounds.Count);
            audioSource.clip = attackSounds[random];
            audioSource.Play();

        }
    }
    public void PlayDeathSound()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }
    public void PlayJumpSound()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
       
    }
    public void PlayHealUpSound()
    {
        audioSource.clip = healUpSound;
        audioSource.Play();
    }
}
