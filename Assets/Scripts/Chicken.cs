using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime = 2;
                     private float idleTimecd;

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    private int facingDirection = -1;
    private bool wallDetected;
    private bool groundDetected;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        CollisionCheck();

        idleTimecd -= Time.deltaTime;

        if(idleTimecd < 0){
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
        }else
            rb.velocity = new Vector2(0, 0);

        if(wallDetected || !groundDetected){
            Flip();
            idleTimecd = idleTime;
        }
    }
    private void Flip(){
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }
    private void CollisionCheck(){
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatisGround);
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
    }
    // check dis
    // private void OnDrawGizmos() {
    //     Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    //     Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
    // }
}
