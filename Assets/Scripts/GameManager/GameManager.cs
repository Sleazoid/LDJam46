using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    InputControls inputAction;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject deadTextPanel;
    private List<GameObject> watchers;
    private static GameManager instance;
    [SerializeField]
    private Image healthImg;
    public static GameManager Instance { get => instance; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        inputAction = new InputControls();
        inputAction.Gamepad.Restart.performed += ctx => RestartGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        watchers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Watcher"));
    }
    private void OnEnable()
    {
        inputAction.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        //inputAction.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.SetActive(true);
        deadTextPanel.SetActive(false);
        watchers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Watcher"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayerIsDead()
    {
        deadTextPanel.SetActive(true);
        healthImg.fillAmount = 0;
        foreach(GameObject go in watchers)
        {
            go.GetComponent<WatcherEnemy>().PlayerIsDead();
        }
    }
    private void RestartGame()
    {
        Debug.Log("Restart");    
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex);
    }
    public void DecreaseHealth(float value)
    {
        Debug.Log(value);
        healthImg.fillAmount = value;
        Debug.Log(healthImg.fillAmount);

    }
}
