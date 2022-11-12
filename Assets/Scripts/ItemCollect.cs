using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] public GameObject endLv;
    // [SerializeField] public GameObject door;
    [SerializeField] private Text bananaText;
    [SerializeField] private AudioSource collectItem;
    // [SerializeField] GameObject ballPrefab;
    // [SerializeField] float secondSpawn = 1f;
    public int bananas = 0;
    // float time = 0.0f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("3nana")){
            collectItem.Play();
            Destroy(other.gameObject);
            bananas++;
            bananaText.text = "Bananas: " + bananas;
        }
    }
    private void Update() {
        if(bananas == 10){
            endLv.SetActive(true);
            // door.SetActive(false);
        }
        // time -= Time.deltaTime;
        // if(time <= 0 && bananas == 10){
        //     spawnBall();
        //     time = UnityEngine.Random.Range(1,2);
        // }
    }
    // private void Start() {
    //     StartCoroutine(ballSpawn());
    // }
    // IEnumerator ballSpawn(){
    //     while(true) {
    //         Vector2 pos = new Vector2(UnityEngine.Random.Range(-13, 13), 10);
    //         GameObject gameobject = Instantiate(ballPrefab, pos, Quaternion.identity);
    //         yield return new WaitForSeconds(secondSpawn);
    //         Destroy(gameobject, 1f);
    //     }
    // }
    // void spawnBall(){
    //     Vector2 pos = new Vector2(UnityEngine.Random.Range(-13, 13), 10);
    //     Instantiate(ballPrefab, pos, Quaternion.identity);
    // }
}

