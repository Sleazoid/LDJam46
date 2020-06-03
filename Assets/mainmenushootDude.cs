using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenushootDude : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]

    private Animator watcherAnim;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void ShootArrow()
    {
        arrow.SetActive(true);
        StartCoroutine("WaitToDeath");
    }
    IEnumerator WaitToDeath()
    {
        yield return new WaitForSeconds(0.2f);
        watcherAnim.Play("todeath");
        audio.Play();
        yield return null;
    }
}
