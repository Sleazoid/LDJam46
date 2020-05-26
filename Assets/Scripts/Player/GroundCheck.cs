using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.transform.parent.gameObject.GetComponent<PlayerMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag.Equals("Ground"))
        //{

        //    playerMove.IsGrounded = true;
        //    playerMove.DisableFallingAnim();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //playerMove.IsGrounded = false;
    }

}
