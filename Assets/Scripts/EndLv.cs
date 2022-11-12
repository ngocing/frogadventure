using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLv : MonoBehaviour
{
    private bool nextLv = false;
    [SerializeField] private AudioSource endLv;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !nextLv) {
            endLv.Play();
            nextLv = true;
            Invoke("NextLv", 1f);
        }
    }
    private void NextLv(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
