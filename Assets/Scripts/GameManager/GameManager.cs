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
    [SerializeField]
    private GameObject LevelClearPanel;
    [SerializeField]
    private GameObject healthUsePanel;
    private List<GameObject> watchers;
    private List<GameObject> stormBirds;
    private bool playerIsDead = false;
    private int deadEnemiesCount = 0;
    private static GameManager instance;
    private bool levelClearPointReached = false;
    [SerializeField]
    private GameObject HpPanel;
    [SerializeField]
    private Image healthImg;
    public static GameManager Instance { get => instance; }
    public bool PlayerhasDied { get => playerIsDead; set => playerIsDead = value; }

    private int currentSceneIndex = 0;
    bool alreadyLoading = false;
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
        stormBirds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hawk"));
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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            HpPanel.SetActive(false);
        }
        else
        {
            HpPanel.SetActive(true);
        }
        canvas.SetActive(true);
        deadTextPanel.SetActive(false);
        LevelClearPanel.SetActive(false);
        watchers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Watcher"));
        stormBirds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hawk"));
        alreadyLoading = false;
        playerIsDead = false;
        levelClearPointReached = false;
        healthUsePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Application.Quit();
            }
            else
            {
                ToMainMenu();
            }
        }

    }
    private void ToMainMenu()
    {
        if (!alreadyLoading)
        {
            Debug.Log("Next level wanted");
            SceneManager.LoadScene(0);
            alreadyLoading = true;
            currentSceneIndex = 0;
        }
    }
    public void ToNextLevel(float timeWait)
    {
        if (!alreadyLoading)
        {
            Debug.Log("Next level wanted");
            StartCoroutine("LoadLevelWithWait", timeWait);
            alreadyLoading = true;
        }
    }
    IEnumerator LoadLevelWithWait(float val)
    {
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex++;
        if (currentSceneIndex >= totalScenes)
        {
            currentSceneIndex = 0;
        }
        yield return new WaitForSeconds(val);
        SceneManager.LoadScene(currentSceneIndex);
        yield return null;

    }
    public void PlayerIsDead()
    {
        playerIsDead = true;
        deadTextPanel.SetActive(true);
        healthImg.fillAmount = 0;
        foreach (GameObject go in watchers)
        {
            go.GetComponent<WatcherEnemy>().PlayerIsDead();
        }
        foreach (GameObject go in stormBirds)
        {
            go.GetComponent<StormBirdEnemy>().PlayerIsDead();
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

        healthImg.fillAmount = value;
        Debug.Log(healthImg.fillAmount);

    }
    public void ActivateUseHealthPanel(bool value)
    {
        healthUsePanel.SetActive(value);
    }
    public void EnemyDied()
    {
        deadEnemiesCount++;
        //Debug.Log(watchers.Count + stormBirds.Count);
        //Debug.Log(deadEnemiesCount);
        if (deadEnemiesCount >= watchers.Count + stormBirds.Count)
        {
            Debug.Log("Nice");
            if (levelClearPointReached)
            {
                Debug.Log("Nice");
                LevelClearPanel.SetActive(true);
                GameObject.Find("CampFire").GetComponent<CampFireSave>().SetFireOn();
            }
        }

    }
    public void SavePointReached()
    {
        levelClearPointReached = true;

        if (deadEnemiesCount >= watchers.Count + stormBirds.Count)
        {
            Debug.Log("Nice");
            LevelClearPanel.SetActive(true);
            GameObject.Find("CampFire").GetComponent<CampFireSave>().SetFireOn();
        }
    }
}
