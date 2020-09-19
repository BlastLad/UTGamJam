using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    public GameObject playerGFX;

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);



        if (Input.GetKey(KeyCode.D))
        {
            FlipRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            FlipLeft();
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            playerGFX.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && GetComponent<SeansTestPlayerController>().isBarrierActive == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true && GetComponent<SeansTestPlayerController>().isBarrierActive == false)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void FlipRight()
    {
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
        playerGFX.GetComponent<SpriteRenderer>().flipX = false;
    }

    void FlipLeft()
    {
        playerGFX.GetComponent<SpriteRenderer>().flipX = true;
    }
}
