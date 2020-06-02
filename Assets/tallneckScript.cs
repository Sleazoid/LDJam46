using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tallneckScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(this.transform.position.x < playerTransform.position.x+20)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (this.transform.position.x < playerTransform.position.x - 13)
        {
            transform.Translate(Vector2.right * (moveSpeed*10) * Time.deltaTime);
        }

        //this.transform.position = new Vector2(transform.position.x*(transform.right*moveSpeed),transform.position.y);
    }
}
