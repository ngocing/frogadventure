using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController4 : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheck;
    public float footOffest = 0.4f;
    public float groundDistance = 0.1f;
    public LayerMask groundLayer;
    public bool onGround;

    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 12;
    public float horizontalJumpForce = 6;
    public float horizontal;
    public bool jumpPressed;
    public int direction = 1;
    public bool canMove = true;

    [Header("Wall")]
    public bool onWall;
    public Vector3 wallOffset;
    public float wallRadius;
    public float maxFallSpeed = -1;
    public float wallJumpDuration = 0.25f;
    public bool jumpFromWall;
    public float jumpFinish;
    public LayerMask wallLayer;

    private bool clearInputs;    
    private bool doubleJump;
    private bool die = false;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource jumpEft;
    [SerializeField] private AudioSource deathEft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        PhysicsCheck();
    }

    private void FixedUpdate()
    {
        GroundMovement();
        AirMovement();
        clearInputs = true;
    }

    void GroundMovement()
    {
        if (!canMove)
            return;

        float x = horizontal * speed;

        rb.velocity = new Vector2(x, rb.velocity.y);

        if (x * direction < 0f)
            Flip();

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    void AirMovement()
    {
        if (jumpPressed && onGround)
        {
            jumpPressed = false;
            doubleJump = true;
            rb.velocity = Vector2.zero;
            Jump();
        }
        else if(jumpPressed && doubleJump){
            doubleJump = false;
            Jump();
        }
        else if(jumpPressed && onWall && !onGround)
        {
            canMove = false;
            jumpFinish = Time.time + wallJumpDuration;
            jumpPressed = false;
            jumpFromWall = true;
            Flip();

            rb.velocity = Vector2.zero;

            rb.AddForce(new Vector2(horizontalJumpForce * direction, jumpForce));
            jumpEft.Play();

        }
    }

    void Jump(){
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        jumpEft.Play();
    }

    void Flip()
    {
        direction *= -1;        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void CheckInputs()
    {
        if (clearInputs)
        {
            jumpPressed = false;
            clearInputs = false;
        }

       
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");

        if(canMove)
            horizontal = Input.GetAxis("Horizontal");

        if (jumpFromWall)
        {
            if(Time.time > jumpFinish)
            {
                jumpFromWall = false;
            }
        }

        if(!jumpFromWall && !canMove)
        {
            if(Input.GetAxis("Horizontal") != 0 || onGround)
            {
                canMove = true;
            }
        }

    }

    void PhysicsCheck()
    {
        onGround = false;
        onWall = false;

        RaycastHit2D leftFoot = Raycast(groundCheck.position + new Vector3(-footOffest, 0), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFoot = Raycast(groundCheck.position + new Vector3(footOffest, 0), Vector2.down, groundDistance, groundLayer);
    
        if(leftFoot || rightFoot)
        {
            onGround = true;
        }

        bool rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset.x, 0), wallRadius, wallLayer);
        bool leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset.x, 0), wallRadius, wallLayer);

        if(rightWall || leftWall)
        {
            onWall = true;
        }

        if (onWall)
        {
            if(rb.velocity.y < maxFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            }
        }

        anim.SetBool("OnGround", onGround);
        anim.SetBool("OnWall", onWall);

        if(die){
            anim.SetBool("die", true);
        }
    }


    public RaycastHit2D Raycast(Vector2 origin, Vector2 rayDirection, float length, LayerMask mask, bool drawRay = true)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, length, mask);

        if (drawRay)
        {
            Color color = hit ? Color.red : Color.green;
            // Debug.DrawRay(origin, rayDirection * length, color);
        }
        return hit;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;

    //     Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffset.x, 0), wallRadius);
    //     Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffset.x, 0), wallRadius);
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Trap")){
            die = true;
            deathEft.Play();
            rb.bodyType = RigidbodyType2D.Static;
            AdManager.instant.ShowInter();
        }
        if(other.gameObject.CompareTag("Bird")){
            rb.AddForce(new Vector2(rb.velocity.x, 6));
        }

    }
    private void RestartLv(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            die = true;
            deathEft.Play();
            rb.bodyType = RigidbodyType2D.Static;
            AdManager.instant.ShowInter();
        }
    }
}