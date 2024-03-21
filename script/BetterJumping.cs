using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour
{
    private WallMovement b;
    private BoxCollider2D myFeet;
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool isGround;

    void Start()
    {
        myFeet = GetComponent<BoxCollider2D> ();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();

        if (Input .GetKeyDown(KeyCode.Space) && isGround ) 
        {
            rb.velocity = Vector2.up * 10;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier  - 1) * Time.deltaTime;
        }
        
    }
    void GroundCheck()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}