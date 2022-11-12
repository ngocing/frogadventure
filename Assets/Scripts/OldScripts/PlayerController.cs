using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    Collider2D _colli;
    [SerializeField] BaseAnimation _anim;
    [SerializeField] bool isGround = false;
    [SerializeField] bool die = false;
    [SerializeField] float _speed, _jumpForce;
    [SerializeField] bool can2Jump = false;
    private bool canWallSlide;
    // [SerializeField] private bool isWallSliding = false;

    [SerializeField]
    playerState PLAY_STATE = playerState.IDLE;
    public playerState currentState => PLAY_STATE;
    public enum playerState{
        IDLE = 1,
        RUN = 2,
        JUMP = 3,
        FALLING = 4,
        DOUJUMP = 5,
        HIT = 6,
        WALLJUMP = 7,
        DEATH = 8
    }
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _colli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        // checkGround();
        updateState();
        _anim.changeAnim(PLAY_STATE);
        CollisionCheck();
        if(isGround) can2Jump = true;
    }
    void Moving(){
        if(isWallDetected && canWallSlide){
            // isWallSliding = true;
        }else{
            _rb.velocity = new Vector2
            (Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime, _rb.velocity.y);
        }
        Vector3 tmp = this.transform.localScale;
        if(_rb.velocity.x > 0){
            tmp.x = 1;
        }else if(_rb.velocity.x < 0){
            tmp.x = -1;
        }
        this.transform.localScale = tmp;
        

        // if(Input.GetKeyDown(KeyCode.Space)){
        //     if(isGround){
        //         isGround = false;
        //         _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
        //         canJump = true;
        //     }else if(canJump){
        //         _jumpForce = _jumpForce/2f;
        //         _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
        //         canJump = false;
        //         _jumpForce = _jumpForce*2f;

        //     }
            
        // }
        if(Input.GetKeyDown(KeyCode.Space))
            JumpButton();

    }
    private void JumpButton(){
        if(isGround){
            Jump();
        }else if(can2Jump){
            can2Jump = false;
            Jump();
        }
    }
    private void Jump(){
        _rb.velocity =  new Vector2 (_rb.velocity.x, _jumpForce);
    }
    private void CollisionCheck(){
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIGround);
        if(!isGround && _rb.velocity.y < 0)
            canWallSlide = true;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, 
                                                        wallCheck.position.y + wallCheck.position.z));
    }
    // void checkGround() {
    //     RaycastHit2D[] hits = new RaycastHit2D[10];
    //     _colli.Cast(Vector2.down, hits, .05f);
    //     foreach (RaycastHit2D hit in hits)
    //     {
    //         if(hit.collider != null){
    //             isGround = true;
    //             if(hit.collider.gameObject.CompareTag("MovingObj"))
    //                 this.transform.SetParent(hit.collider.transform);
    //             return;
    //         }
    //     }
    //     this.transform.SetParent(null);
    //     isGround = false;
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        isGround = true;
        if(other.gameObject.CompareTag("Trap")){
            die = true;
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }
    // private void OnCollisionExit2D(Collision2D other) {
    //     isGround = false;
    // }
    private void updateState(){
        if(isGround){
            if(_rb.velocity.x != 0)
                PLAY_STATE = playerState.RUN;
            else
                PLAY_STATE = playerState.IDLE;
        }else{
            if(_rb.velocity.y > 0)
                PLAY_STATE = playerState.JUMP;
            else if (_rb.velocity.y < 0)
                PLAY_STATE = playerState.FALLING;
        }
        if(!isGround && !can2Jump){
                PLAY_STATE = playerState.DOUJUMP;
        }
        // if(Input.GetKey(KeyCode.V) && isGround){
        //         PLAY_STATE = playerState.HIT;
        // }
        // if(isWallSliding)
        //     PLAY_STATE = playerState.WALLJUMP;
        // else
        //     PLAY_STATE = playerState.IDLE;
        
        if(die) PLAY_STATE = playerState.DEATH;
    }
    private void RestartLv(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
