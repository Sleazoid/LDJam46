using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    InputControls inputAction;
    float yMove;
    float xMove;
    Vector2 moveDir;
    [SerializeField]
    private float moveSpeed = 5f;
    Rigidbody2D rb;
    //player movement
    bool facingRight = true;
    Vector2 movementInput;
    [SerializeField]
    private Animator anim;
    private bool isJumping;
    [SerializeField]
    private float RunSpeed = 5f;
    bool canJump = false;
    [SerializeField]
    bool isGrounded = false;
    // Jump variables
    [SerializeField]
    private float jumpCheckInterval = 0.1f;
    private float jumpCheckForce = 3f;
    private int jumpIntervalLimit = 3;
    private int curJumpInterval = 0;
    bool landedAfterJump = true;
    [SerializeField]
    private Transform groundCheck;
    bool canMove = true;
    [SerializeField]
    private Transform bowTransform;
    private BowAim bowAim;
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool Rolling { get => rolling; set => rolling = value; }

    // roll variables 
    [SerializeField]
    private float rollForce = 2f;
    [SerializeField]
    private float rollJumpForce = 2f;
    [SerializeField]
    private float rollAngle = 45f;
    private bool rolling = false;
    [SerializeField]
    private float rollDeadTime = 0.5f;
    private bool rollIsDead = false;
    DashTrail dashTrail;

    PlayerHealth playerHealth;
    private void Awake()
    {
        inputAction = new InputControls();
        //inputAction.Gamepad.Jump.performed += ctx => Jump();
        inputAction.Gamepad.LeftStick.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.Gamepad.Jump.performed += ctx => AddJumpPressForce();
        inputAction.Gamepad.Jump.canceled += ctx => JumpReleased();
        inputAction.Gamepad.Roll.performed += ctx => RollDodge();
        Debug.Log("awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        //anim.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bowAim = GetComponent<BowAim>();
        dashTrail = GetComponentInChildren<DashTrail>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        yMove = Input.GetAxis("Vertical");
        xMove = Input.GetAxis("Horizontal");

        if (/*!CanMove &&*/ (xMove > 0.1f || xMove < -0.1f))
        {
            if (xMove < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); //bowTransform.localScale = new Vector3(-1f, 1f, 1f);
                facingRight = false;
                bowAim.FacingRight = facingRight;
            }
            else if (xMove > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);// bowTransform.localScale = new Vector3(1f, 1f, 1f);
                facingRight = true;
                bowAim.FacingRight = facingRight;
            }

        }

    }

    public void RollEnded()
    {
        dashTrail.CancelInvoke("SpawnTrailPart");
        rolling = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        rollIsDead = true;
        Invoke("EnableRollAction", rollDeadTime);
        playerHealth.IsDodging = false;
    }
    private void EnableRollAction()
    {
        rollIsDead = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("RunSpeed", rb.velocity.x);

        moveDir = new Vector2(xMove, yMove);
        moveDir.Normalize();

        if (CanMove && (xMove > 0.1f || xMove < -0.1f) && !rolling)// && isGrounded)
        {
            Move();
        }

        if (rb.velocity.y < 0 && rb.drag != 1)// && !isGrounded)
        {
            anim.SetBool("Falling", true);
            rb.drag = 1;
        }
        else if (rb.velocity.y >= 0 && rb.drag != 0)
        {
            rb.drag = 0;
        }

        if (Physics2D.Raycast(groundCheck.position, -this.transform.up, 0.1f))
        {
            isGrounded = true;
            DisableFallingAnim();
        }
        else
        {
            isGrounded = false;
        }

    }
    private void RollDodge()
    {
        if (!rollIsDead && !rolling && IsGrounded)
        {
            rolling = true;
            playerHealth.IsDodging = true;
            anim.SetBool("Roll", true);
            Vector2 dirForce = this.transform.right * rollForce;// * moveDir ;
            if (!facingRight)
            {
                dirForce *= -1;
            }
            Vector2 jumpForce = this.transform.up * rollJumpForce;
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            rb.AddForce(dirForce, ForceMode2D.Impulse);
            dashTrail.InvokeRepeating("SpawnTrailPart", 0, 0.03f);
            dashTrail.FlipTrail();
        }

    }
    public void DisableFallingAnim()
    {
        if(anim.GetBool("Falling"))
            anim.SetBool("Falling", false);

    }
    private void AddJumpPressForce()
    {
        if (isGrounded && !anim.GetBool("Roll"))
        {
            anim.Play("aloyJump");
            InvokeRepeating("JumpCheckInterval", 0f, jumpCheckInterval);

        }

    }
    public void SetOnKneesFalse()
    {
        anim.SetBool("OnKnees", false);
    }
    private void JumpCheckInterval()
    {

        if (curJumpInterval < jumpIntervalLimit)
        {
            rb.velocity = new Vector2(0, 0);
            // Vector2 jumpDir = new Vector2(moveDir.x, this.transform.up.y);
            Vector2 dirForce = this.transform.up * jumpCheckForce;// * moveDir ;
            rb.AddForce(dirForce, ForceMode2D.Impulse);
            curJumpInterval++;
            Debug.Log(curJumpInterval);
        }

    }
    private void JumpReleased()
    {
        CancelInvoke("JumpCheckInterval");
        curJumpInterval = 0;
    }
    private void Move()
    {
        rb.velocity = new Vector2(xMove * RunSpeed, rb.velocity.y);
    }
    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }

}
