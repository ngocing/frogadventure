using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // [SerializeField] float time;
    // Coroutine waitDeactive_C;
    // void OnEnable(){
    //     waitDeactive_C = StartCoroutine(waitDeactive());
    // }
    // void OnDisable(){
    //     StopCoroutine(waitDeactive_C);
    // }
    // IEnumerator waitDeactive(){
    //     yield return new WaitForSeconds(time);
    //     this.gameObject.SetActive(false);
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall")){
            gameObject.SetActive(false);
        }
    }
}
