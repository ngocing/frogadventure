using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBee : MonoBehaviour
{
    // private float speed = 5f;
    // public Rigidbody2D rb;
    // private void FixedUpdate() {
    //     rb.velocity = Vector2.down * speed;
    // }
    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.CompareTag("Wall")){
    //         gameObject.SetActive(false);
    //     }
    // }
    [SerializeField] public GameObject bulletBee;
    [SerializeField] float secondSpawn = 1f;

    private void Update() {
        
    }
    private void Start() {
        StartCoroutine(ballSpawn());
    }
    IEnumerator ballSpawn(){
        while(true){
            GameObject gameobject = Instantiate(bulletBee, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameobject, 2f);
        }
    }
}
