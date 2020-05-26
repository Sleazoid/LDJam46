using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimCallbacks : MonoBehaviour
{
    PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.transform.root.gameObject.GetComponent<PlayerMove>();
    }

    public void SetOnKneesToFalse()
    {
        playerMove.SetOnKneesFalse();
    }
}
