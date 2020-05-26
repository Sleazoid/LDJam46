using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public struct RegisteredArrows
{
    public Arrow real;
    public Arrow hidden;

}



public class ShootTrajectory : MonoBehaviour
{
    public static bool charging;
    public Transform referenceArrow;

    private Scene mainScene;
    private Scene physicsScene;

    public GameObject marker;
    private List<GameObject> markers = new List<GameObject>();
    private Dictionary<string, RegisteredArrows> allArrows = new Dictionary<string, RegisteredArrows>();

    public GameObject objectsToSpawn;

    private void Start()
    {
        Physics.autoSimulation = false;

        mainScene = SceneManager.CreateScene("Physics_Scene", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        PreparePhysicsScene();
    }
    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShowTrajectory();
        }
        mainScene.GetPhysicsScene2D().Simulate(Time.fixedDeltaTime);

    }
    public void RegisterArrow(Arrow arrow)
    {

    }
    public void PreparePhysicsScene()
    {

    }
    public void CreateMovementMarkers()
    {

    }
    public void ShowTrajectory()
    {

    }
    public void SyncArrows()
    {

    }

}
