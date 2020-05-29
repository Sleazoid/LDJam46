using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class BowAim : MonoBehaviour
{
    [SerializeField]
    private GameObject bowGO;
    private PlayerMove playerMove;
    Animator bowAnim;
    [SerializeField]
    Animator playerAnim;
    [SerializeField]
    private GameObject trajectoryGO;
    [SerializeField]
    private GameObject bowPrefab;
    private bool isShooting = false;
    //input actions
    InputControls inputAction;

    //player movement
    Vector2 movementInput;

    // Shoot btn values
    [SerializeField]
    private float initFireValue = 500f;
    [SerializeField]
    private float prepareBtnValue = 0;
    [SerializeField]
    private float fireBtnValue = 500f;
   // float curfireBtnValue = 500f;
    [SerializeField]
    float maxfireBtnValue = 500f;



    [SerializeField]
    private float fireStrength;
    [SerializeField]
    private float fireCheckInterval = 0.05f;
    private int curFireCheckInterval = 0;
    private int fireIntervalLimit = 3;
    bool aiming = false;
    bool facingRight = false;
    [SerializeField]
    private Color sightColor;
    public float FireStrength { get => fireStrength; set => fireStrength = value; }
    public Color SightColor { get => sightColor; set => sightColor = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        inputAction = new InputControls();
        inputAction.Gamepad.LeftStick.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
       // inputAction.Gamepad.PrepareAim.performed += ctx => prepareBtnValue = ctx.ReadValue<float>();
        inputAction.Gamepad.PrepareAim.performed += ctx => Aiming();
        inputAction.Gamepad.PrepareAim.canceled += ctx => CancelAim();
        inputAction.Gamepad.Shoot.canceled += ctx => FireReleased();
        inputAction.Gamepad.Shoot.performed += ctx => fireBtnValue = ctx.ReadValue<float>();// Shoot();
    
    }
    void Start()
    {
        bowAnim = bowGO.GetComponent<Animator>();
        
        bowGO.SetActive(false);
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CancelAim()
    {
        StartCoroutine("WaitTillCancelAim");
    }
    IEnumerator WaitTillCancelAim()
    {
        yield return new WaitForSeconds(0.15f);
        aiming = false;
        playerAnim.SetBool("isAiming", aiming);
        DisableBow();
        yield return null;
    }
    private void Aiming()
    {
        aiming = true;
        playerAnim.SetBool("isAiming", aiming);
        EnableBow();
    }
    private void FireReleased()
    {
        isShooting = false;
       

        CancelInvoke("FireCheckInterval");
        trajectoryGO.SetActive(false);

        bowAnim.SetBool("isFiring", false);
    }
    private void FixedUpdate()
    {
        if(aiming)
        {
    
            float h = Input.GetAxis("Vertical");
            float v = Input.GetAxis("Horizontal");
            //  Debug.Log(h + " " + v);
            Aim();
            if (fireBtnValue.Equals(1) && !isShooting)
            {
                isShooting = true;
                Shoot();
            }
           
        }
       
    }
    private void EnableBow()
    {
        if(bowGO.activeSelf==false)
        {
            playerMove.CanMove = false;
            bowGO.SetActive(true);
            bowAnim.SetBool("isFiring", false);
            bowAnim.SetBool("cancelFire", false);
        }
    }
    private void DisableBow()
    {
        bowAnim.SetBool("cancelFire", true);
        if (bowGO.activeSelf == true)
        {
            playerMove.CanMove = true;
            bowGO.SetActive(false);
        }
    }
    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }
    public void ShootArrow()
    {
        
        fireStrength = fireBtnValue + initFireValue;
        GameObject newArrow = Instantiate(bowPrefab, this.transform.position, bowGO.transform.rotation);
        Vector3[] positions = new Vector3[trajectoryGO.GetComponent<LineRenderer>().positionCount];// = trajectoryGO.GetComponent<LineRenderer>().
        trajectoryGO.GetComponent<LineRenderer>().GetPositions(positions);
        newArrow.GetComponent<ArrowScript>().SetPath(positions, fireStrength, facingRight);
       
    }
    void Shoot()
    {
        bowAnim.SetBool("isFiring", true);
        trajectoryGO.SetActive(true);
        //Debug.Log("sdfads");
        InvokeRepeating("FireCheckInterval", 0f, fireCheckInterval);
    }
    //IEnumerator EnableShooting()
    //{

    //}
    void Aim()
    {

        playerAnim.SetFloat("YLookValue", movementInput.y);
        float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
        if(FacingRight)
        {
            bowGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            bowGO.transform.rotation = Quaternion.AngleAxis(angle-180f, Vector3.forward);
        }
     

    }
    private void FireCheckInterval()
    {
        if (fireStrength < maxfireBtnValue)
        {
            fireStrength+=100;

        }

    }

}
