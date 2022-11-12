using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator anim;
    private bool isWorking;
    Collider2D coli;
    [SerializeField] private float cd;
                     private float cdTimer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cdTimer -= Time.deltaTime;
        if(cdTimer < 0){
            isWorking = !isWorking;
            cdTimer = cd;
        }
        if (!isWorking){
            coli.isTrigger = false;
        }else{
            coli.isTrigger = true;
        }
        

        anim.SetBool("isWorking", isWorking);
    }
}
