using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAddForce : MonoBehaviour
{
    Rigidbody2D _rb;
    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            _rb.AddForce(new Vector2(_rb.velocity.x, 500));
        }
    }
}
