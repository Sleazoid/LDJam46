using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    InputControls input;
    float yMove;
    float xMove;


    private void Awake()
    {
        input = new InputControls();
        input.Gamepad.Jump.performed += ctx => XPressed();
        Debug.Log("awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yMove = Input.GetAxis("Vertical");
        xMove = Input.GetAxis("Horizontal");
        if(xMove!=0)
        {
            Debug.Log("x: "+xMove);
        }
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void XPressed()
    {
        Debug.Log("pressed x");
    }
}
