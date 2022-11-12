using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(transform.parent.gameObject);
        }
    }
}
