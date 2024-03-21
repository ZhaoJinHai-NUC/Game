using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallMovement : MonoBehaviour
{
    public float wallClimbSpeed;
    public float wallSlideSpeed;
    public float jumpSpeed;
    public float moveSpeed;
    public float DisableMoveTime;
    public float wallJumpSpeed;
    public float dashSpeed;
    public int jumpCount;
    public int dashCount;
    public float dashTime;
    private  float reTime;


    public Vector3 walloffsetRight;
    public Vector3 walloffsetLeft;
    public bool isLeftWall, isRightWall;
    public LayerMask wallLayer;

    public bool isWallMove;
    public bool canMove = true;
    public bool isGround;
    public bool alreadyLeaveWall;
    public bool test = true;
    public bool jumpPress;
    public bool isDash = false ;

    private Rigidbody2D rb;
    private Animator anim;

    private BoxCollider2D myFeet;

    public Vector3 velocityTest;

    

    public enum WallState
    {
        wallGrab,
        wallSlide,
        wallClimb,
        wallJump,
        none

    }
    WallState ws;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
        WallCheck();
        GroundCheck();

        if (isDash && dashCount > 0)
        {
            Dash();
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPress = true;
        }

        velocityTest = rb.velocity;
        float playerInput = Input.GetAxisRaw("Vertical");

        SwitchAnimation();
        if (isGround )
        {
            jumpCount = 1;
            dashCount = 1;
        }
        

        if (isWallMove && jumpPress)
        {
            jumpCount += 2;
            Debug.Log(333);
            WallJump();
        }
        else if (isGround && jumpPress)
        {
            Jump(Vector2.up);
            jumpPress = false;
        }
        else if (!isGround && jumpPress && !isWallMove)
        {
            Jump(Vector2.up);
            jumpPress = false;
        }
        if (Input.GetKey(KeyCode.C) && (isLeftWall || isRightWall))
        {
            isWallMove = true;
        }
        else
        {
            isWallMove = false;
        }
        if (Input .GetKeyUp(KeyCode.C))
        {
            alreadyLeaveWall = true;
        }
        if(!alreadyLeaveWall && Input .GetKey(KeyCode.C) && !(isLeftWall || isRightWall))
        {
            alreadyLeaveWall = true;
        }
        if (alreadyLeaveWall && !Input.GetKey(KeyCode.C) && (isLeftWall || isRightWall))//不按C时撞墙，自由落体。需要test信号来控制只执行一次速度归零
        {
            if (test)
            {
                test = false;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            test = true;
        }

        if (isWallMove && ws != WallState.wallJump)
        {
            Debug .Log(1);
            rb.gravityScale = 0f; 
            if (playerInput > 0)
            {
                WallClimb();
            }
            else if (playerInput < 0)
            {
                WallSlide();
            }
            else
            {
                WallGrab();
            }

        }
        else
        {
            ws = WallState.none;
            rb.gravityScale = 1.5f;
        }

        if (!isWallMove && ! isGround && !(isLeftWall || isRightWall))
        {
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = 2.0f;
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = 5.0f;
            }
        }

        if (Input .GetKeyDown(KeyCode.LeftShift) && !isWallMove)
        {
            reTime = Time.time + dashTime;
            isDash = true;
        }
        if (reTime < Time .time)
        {
            isDash = false;
        }

    }
    void WallCheck()
    {
        isLeftWall = Physics2D.OverlapCircle(transform.position - walloffsetLeft, 0.1f, wallLayer);
        isRightWall = Physics2D.OverlapCircle(transform.position + walloffsetRight, 0.1f, wallLayer);
    }
    void GroundCheck()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position - walloffsetLeft, 0.1f);
        Gizmos.DrawWireSphere(transform.position + walloffsetRight, 0.1f);
    }
    void WallGrab()
    {
        if ((isLeftWall || isRightWall) && alreadyLeaveWall)
        {
            rb.velocity = Vector2.zero;
            alreadyLeaveWall = false;
        }
        if (!(isLeftWall || isRightWall))
        {
            alreadyLeaveWall = true;
        }
        ws = WallState.wallGrab;


    }
    void WallClimb()
    {
        rb.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y + wallClimbSpeed * 0.01f);
        ws = WallState.wallClimb;

    }
    void WallSlide()
    {
        rb.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y + wallSlideSpeed * 0.01f);
        ws = WallState.wallSlide;

    }
    void WallJump()
    {
        StartCoroutine(DisableMovement());
        Vector3 dir = isRightWall ? Vector3.left : Vector3.right;
        Jump(Vector3.up + dir * wallJumpSpeed);
        ws = WallState.wallJump;
        rb.gravityScale = 1.5f;
        if (isRightWall)
        {
            transform.localScale = new Vector3(-6, 6, 6);
        }
        else if (isLeftWall)
        {
            transform.localScale = new Vector3( 6, 6, 6);
        }
        
    }
    IEnumerator DisableMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(DisableMoveTime);
        canMove = true;
    }
    void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(dir * jumpSpeed, ForceMode2D.Impulse);
        jumpCount--;

        anim.SetBool("Jump", true);
    }
    
    public void Movement()
    {
        float playerInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(playerInput * moveSpeed, rb.velocity.y);
        bool PlayerHasXAxiSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Run", PlayerHasXAxiSpeed);
        if (playerInput != 0 && !isWallMove)
        {
            transform.localScale = new Vector3(playerInput * 6 , 6, 6); 
        }
    }
    void Dash()
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.1f, .2f, 13, 90, false, true);
        float playerInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(playerInput * dashSpeed , rb.velocity.y);
        rb.gravityScale = 0f;
        dashCount--;
    }

    void SwitchAnimation()
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jump"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Idle", true);
        }
        else if (anim.GetBool("Run") && rb.velocity.y < 0)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Fall", true);
        }
    }
}
