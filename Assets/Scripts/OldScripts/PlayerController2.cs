using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerNEW : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Move info")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private bool canMove = true;
    private bool die = false;

    private bool canDoubleJump;
    private bool canWallJump = true;
    private bool canWallSlide;
    private bool isWallSliding;

    private bool facingRight = true;
    private float movingInput;
    private int facingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGround;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CollisionCheck();
        FlipController();
        AnimatorController();
    }


    private void FixedUpdate()
    {
        if (isGround)
        {
            canMove = true;
            canDoubleJump = true;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            canWallSlide = false;
        }

        if (isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y * 0.1f);
        }
        else if(!isWallDetected)
        {
            isWallSliding = false;
            Move();
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpButton();

        if(canMove)
            movingInput = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        if(canMove)
            rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
    }

    private void JumpButton()
    {
        
        if (isWallSliding && canWallJump)
        {
            WallJump();
        }
        else if (isGround)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            Jump();
        }

        canWallSlide = false;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        canMove = false;
        canDoubleJump = true;

        
        Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {

        if (isGround && isWallDetected)
        {
            if (facingRight && movingInput < 0)
                Flip();
            else if (!facingRight && movingInput > 0)
                Flip();
        }

        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isGround", isGround);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isWallSliding", isWallSliding);
        
        if(die)
            anim.SetBool("die", true);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        isGround = true;
        if(other.gameObject.CompareTag("Trap")){
            die = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void CollisionCheck()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if (!isGround && rb.velocity.y < 0)
            canWallSlide = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
    private void RestartLv(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}