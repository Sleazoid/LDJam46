using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContinueScript : MonoBehaviour
{
    InputControls inputAction;
    [SerializeField]
    private float timeToWaitLevel = 2f;

    [SerializeField]
    private Animator arrowAnim;

    private bool tipsOn = false;
    [SerializeField]
    private Text newGame;
    [SerializeField]
    private Text tips;
    private int selected = 0;
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color deselectedColor;
    [SerializeField]
    private GameObject tipsPanel;
    // Start is called before the first frame update
    void Awake()
    {
        inputAction = new InputControls();
        inputAction.Gamepad.Jump.performed += ctx => userPress();
        //inputAction.Gamepad.Roll.performed += ctx => ShowTips();
    }
    private void OnEnable()
    {
        inputAction.Enable();
    }
    void OnDisable()
    {
        inputAction.Disable();
    }
    private void ShowTips()
    {
        Debug.Log("AG¤¤¤");
        newGame.enabled = false;
        tips.enabled = false;
        tipsOn = true;
        tipsPanel.SetActive(true);
    }
    private void TipsDisable()
    {
        Debug.Log("666");
        newGame.enabled = true;
        tips.enabled = true;
        tipsPanel.SetActive(false);
        tipsOn = false;
    }
    private void userPress()
    {
        if (!tipsOn)
        {
            if (selected == 0)
            {
                StartGame();
            }
            else
            {
                ShowTips();
            }
        }
        else
        {
            TipsDisable();
        }
            
    }
    private void StartGame()
    {
        if(!tipsOn)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                arrowAnim.enabled = true;
                GameManager.Instance.ToNextLevel(timeToWaitLevel);
            }
            else
            {

                GameManager.Instance.ToNextLevel(timeToWaitLevel);
            }
        }
        else
        {
            TipsDisable();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if(tipsOn && Input.anyKeyDown)
        //{
        //    TipsDisable();
        //}
        float yMove = Input.GetAxis("Vertical");
        if(yMove!=0)
        {
            if (yMove > 0 && selected != 0)
            {
                newGame.color = selectedColor;
                tips.color = deselectedColor;
                selected = 0;
            }
            else if (yMove < 0 && selected !=1)
            {
                tips.color = selectedColor;
                newGame.color = deselectedColor;
                selected = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Return))
        {
            userPress();
        }
    }
}
