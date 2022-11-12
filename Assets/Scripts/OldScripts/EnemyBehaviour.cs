using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool moveLeft;
    public Transform groundDetect;

    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void FixedUpdate() {
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);
        if(groundCheck.collider == false){
            if(moveLeft){
                transform.eulerAngles = new Vector2(0, -180);
                moveLeft = false;
            }else{
                transform.eulerAngles = new Vector2(0, 0);
                moveLeft = true;
            }
        }
    }
}
