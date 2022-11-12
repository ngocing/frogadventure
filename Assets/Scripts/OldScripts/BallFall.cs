using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFall : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float secondSpawn = 5f;
    //float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ballSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        // time -= Time.deltaTime;
        // if(time <= 0){
        //     spawnBall();
        //     time = UnityEngine.Random.Range(1,3);
        // }
    }

    // void spawnBall(){
    //     Vector2 pos = new Vector2(UnityEngine.Random.Range(-13, 13), 16);
    //     Instantiate(ballPrefab, pos, Quaternion.identity);
    // }

    IEnumerator ballSpawn(){
        while(true){
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-13, 13), 10);
            GameObject gameobject = Instantiate(ballPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameobject, 1f);
        }
    }
}
