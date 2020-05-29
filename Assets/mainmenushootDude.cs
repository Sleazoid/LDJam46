using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenushootDude : MonoBehaviour
{
    [SerializeField]

    private GameObject arrow;
    [SerializeField]

    private Animator watcherAnim;
    public void ShootArrow()
    {
        arrow.SetActive(true);
        StartCoroutine("WaitToDeath");
    }
    IEnumerator WaitToDeath()
    {
        yield return new WaitForSeconds(0.2f);
        watcherAnim.Play("todeath");
        yield return null;
    }
}
