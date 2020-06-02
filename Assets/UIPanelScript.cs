using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelScript : MonoBehaviour
{
    [SerializeField]
    private bool allowNextLevel = false;
    private AudioSource audioSource;
    InputControls inputAction;
    private void Awake()
    {
        inputAction = new InputControls();

        inputAction.Gamepad.Jump.canceled += ctx => NextLevel();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        if(allowNextLevel)
        {
            inputAction.Enable();
        }
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }
    private void NextLevel()
    {
        GameManager.Instance.ToNextLevel(0.5f);
    }
}
