using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

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
    //Climb variables
    private bool climbBtnPressed = false;
    [SerializeField]
    private Transform climbCheck;
    [SerializeField]
    private float climbColDistance;
    [SerializeField]
    private LayerMask climbLayer;
    public bool isClimbing = false;
    private float defaultGravityScale;
    bool canMove = true;
    [SerializeField]
    private PhysicsMaterial2D onGroundMaterial;
    [SerializeField]
    private PhysicsMaterial2D onAirMaterial;
    [SerializeField]
    private SpriteRenderer playerSpriteRend;


    private BowAim bowAim;
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        inputAction = new InputControls();
        //inputAction.Gamepad.Jump.performed += ctx => Jump();
        inputAction.Gamepad.LeftStick.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.Gamepad.Jump.performed += ctx => AddJumpPressForce();
        inputAction.Gamepad.Jump.canceled += ctx => JumpReleased();
        inputAction.Gamepad.ClimbBtn.performed += ctx => ClimbActivated();
        inputAction.Gamepad.ClimbBtn.canceled += ctx => ClimbCancelled();
        Debug.Log("awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        //anim.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
        bowAim = GetComponent<BowAim>();
    }

    private void Update()
    {
        yMove = Input.GetAxis("Vertical");
        xMove = Input.GetAxis("Horizontal");

        if (!isClimbing && (xMove > 0.1f || xMove < -0.1f))
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

    private void TryToClimb()
    {
        if (!isClimbing)
        {
            Debug.Log("aaaa00");
            RaycastHit2D hit = Physics2D.Raycast((Vector2)climbCheck.position, transform.right, climbColDistance, climbLayer);
            if (hit)
            {
                float distanceToWallEdge = hit.point.x - climbCheck.position.x;
                this.transform.position = new Vector2(this.transform.position.x + distanceToWallEdge, this.transform.position.y);
                playerSpriteRend.flipX = true;
                isClimbing = true;
                anim.SetBool("IsClimbing", isClimbing);
                Debug.Log("safds");
                rb.velocity = new Vector2(0, 0);
                rb.gravityScale = 0f;
            }
        }

    }
    private void ClimbActivated()
    {
        climbBtnPressed = true;
    }
    private void ClimbCancelled()
    {
        climbBtnPressed = false;
        if (isClimbing)
        {
            Debug.Log("nooh");
            playerSpriteRend.flipX = false;
            isClimbing = false;
            anim.SetBool("IsClimbing", isClimbing);
            anim.SetBool("Falling", true);
            rb.gravityScale = defaultGravityScale;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbBtnPressed)
        {
            TryToClimb();
        }
        else
        {
            anim.SetFloat("RunSpeed", rb.velocity.x);
            if (rb.velocity.y < 0 && rb.drag != 1)// && !isGrounded)
            {
                Debug.Log("AAGRG");
                if (!anim.GetBool("Falling"))
                {
                    anim.SetBool("Falling", true);
                }
                
                //anim.SetBool("JumpUp", false);
                rb.drag = 1;
            }
            else if (rb.velocity.y >= 0 && rb.drag != 0)
            {
                rb.drag = 0;
            }
        }


        moveDir = new Vector2(xMove, yMove);
        moveDir.Normalize();
        // Debug.Log(moveDir);
        if (!isClimbing && (xMove > 0.1f || xMove < -0.1f))// && isGrounded)
        {
            Move();
        }


        //if (!isGrounded && rb.velocity.y < 0)
        {
            if (!anim.GetBool("Falling") && Physics2D.Raycast(groundCheck.position, -this.transform.up, 0.5f) && !IsGrounded)
            {
                Debug.Log("disableFalling");
                DisableFallingAnim();
            }
            if (Physics2D.Raycast(groundCheck.position, -this.transform.up, 0.1f))
            {
                rb.sharedMaterial = onGroundMaterial;
                isGrounded = true;

                DisableFallingAnim();
                anim.Play("aloy_idle");
            }
            else
            {
                rb.sharedMaterial = onAirMaterial;
                isGrounded = false;
            }

        }
    }
    public void DisableFallingAnim()
    {
        anim.SetBool("Falling", false);
        //   anim.SetBool("JumpUp", false);
    }
    private void AddJumpPressForce()
    {

        if (isGrounded)
        {
            anim.Play("aloyJump");
            //anim.SetBool("JumpUp", true);
            InvokeRepeating("JumpCheckInterval", 0f, jumpCheckInterval);

        }
        else if (isClimbing)
        {
            Debug.Log("sf");
            //playerSpriteRend.flipX = false;
            anim.SetBool("JumpUp", true);
            ClimbCancelled();
            //isClimbing = false;
            //anim.SetBool("IsClimbing", isClimbing);
            //rb.gravityScale = defaultGravityScale;
            InvokeRepeating("JumpCheckInterval", 0f, jumpCheckInterval);
        }
        //  isGrounded = false;
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
            //Debug.Log(curJumpInterval);
        }

        //  yield return null;
    }
    private void JumpReleased()
    {

        CancelInvoke("JumpCheckInterval");
        curJumpInterval = 0;
    }
    private void Move()
    {
        //Debug.Log(moveDir);
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
    //private void SwapFacing()
    //{
    //    if (facingRight)
    //    {
    //        transform.localScale = new Vector3(-1f, 1f, 1f); //bowTransform.localScale = new Vector3(-1f, 1f, 1f);
    //        facingRight = false;
    //        bowAim.FacingRight = facingRight;
    //    }
    //    else if (!facingRight)
    //    {
    //        transform.localScale = new Vector3(1f, 1f, 1f);// bowTransform.localScale = new Vector3(1f, 1f, 1f);
    //        facingRight = true;
    //        bowAim.FacingRight = facingRight;
    //    }
    //}
}