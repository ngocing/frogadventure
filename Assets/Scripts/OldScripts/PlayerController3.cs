using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] public float moveSpeed, jumpForce;
    public Transform groundCheckPoint;
    public LayerMask whatisGround;
    private bool isGrounded;
    public Animator anim;
    public Transform wallGrabPoint;
    private bool canGrap, isGrabbing;
    private float gravityStore;
    public float wallJumpTime = .2f;
    private float wallJumpCounter;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        gravityStore = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(wallJumpCounter <= 0){
        rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatisGround);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(new Vector2(moveSpeed, jumpForce));
        }
        
        if(rb.velocity.x > 0){
            transform.localScale = Vector3.one;
        }else if(rb.velocity.x < 0){
            transform.localScale = new Vector3(-1f, 1, 1f);
        }

        canGrap = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGround);
        isGrabbing = false;
        if(canGrap && !isGrounded){
            if((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0)
                || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
            {
                isGrabbing = true;
            }
        }
        if(isGrabbing){
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;

            if(Input.GetKeyDown(KeyCode.Space)){
                wallJumpCounter = wallJumpTime;
                rb.AddForce(new Vector2(-Input.GetAxisRaw("Horizontal") * moveSpeed, jumpForce));
                rb.gravityScale = gravityStore;
                isGrabbing = false;
            }
        }else{
            rb.gravityScale = gravityStore;
        }
    }else{
        wallJumpCounter -= Time.deltaTime;
    }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isGrabbing", isGrabbing);
    }
}
