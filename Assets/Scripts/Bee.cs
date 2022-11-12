using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] private Transform gun;
    Collider2D coli;
    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coli = this.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            GameObject bullet = ObjPool.instance.GetPoolObject();
            if(bullet != null){
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
            anim.SetBool("atk", true);
        }else{
            anim.SetBool("atk", false);
        }
        checkPlayer();
    }
    void checkPlayer(){
        RaycastHit2D[] hits = new RaycastHit2D[1];
        // Debug.DrawRay(gun.transform.position, gun.transform.up.normalized * 10, Color.red);
        coli.Cast(gun.transform.up.normalized * 10, hits);
        foreach(RaycastHit2D hit in hits){
            if(hit.collider != null && hit.collider.gameObject.tag.Equals("Player")){
                target = hit.collider.gameObject;
                return;
            }
        }
        target = null;
    }
}
