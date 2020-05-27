using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimCallbacks : MonoBehaviour
{
    PlayerMove playerMove;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.transform.root.gameObject.GetComponent<PlayerMove>();
        anim = this.GetComponent<Animator>();
    }

    public void SetOnKneesToFalse()
    {
        playerMove.SetOnKneesFalse();
    }
    public void SetRollFalse()
    {
        anim.SetBool("Roll", false);
        playerMove.RollEnded();
    }
    public void Checking()
    {
        Debug.Log("Checking");
    }
}
