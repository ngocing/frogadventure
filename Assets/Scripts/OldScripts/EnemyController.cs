using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    Collider2D coli;
    private void Start() {
        coli = GetComponent<Collider2D>();
    }
    private void Update() {
        coli.offset = new Vector2(0, 0);
    }
}