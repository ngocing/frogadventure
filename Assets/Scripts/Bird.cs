using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private float dirX;
    [SerializeField] public float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private Vector3 localScale;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<WallBird>()){
            dirX *= -1f;
        }
    }
    private void FixedUpdate() {
        rb.velocity = new Vector3(dirX * moveSpeed, rb.velocity.y);
    }
    private void LateUpdate() {
        CheckWheretoFace();
    }
    void CheckWheretoFace(){
        if(dirX < 0)
            facingRight = true;
        else if(dirX > 0)
            facingRight = false;

        if(((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;
        transform.localScale = localScale;
    }
}
