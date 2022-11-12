using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;
    public Rigidbody2D rb;
    private void FixedUpdate() {
        rb.velocity = Vector2.left * speed;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall")){
            gameObject.SetActive(false);
        }
    }
    private void Start() {
        
    }
}
