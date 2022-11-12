using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv3 : MonoBehaviour
{
    [SerializeField] public GameObject endLv;
    [SerializeField] public GameObject door;
    [SerializeField] private Text bananaText;
    [SerializeField] private AudioSource collectItem;
    public int bananas = 0;
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
            door.SetActive(false);
        }
        if(bananas == 20){
            endLv.SetActive(true);
        }
    }
}

