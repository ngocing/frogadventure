using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    Animator anim;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private Transform gun;
    Collider2D coli;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        coli = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            GameObject bullet = ObjPool.instance.GetPoolObject();
            if(bullet != null){
                bullet.transform.position = bulletPosition.position;
                // float angle = transform.rotation.y == 0 ? -180 : 0;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
            anim.SetBool("ATK", true);
        }else{
            anim.SetBool("ATK", false);
        }
        checkPlayer();
    }
    void checkPlayer(){
        RaycastHit2D[] hits = new RaycastHit2D[1];
        // Debug.DrawRay(gun.transform.position, gun.transform.up.normalized * 5, Color.red);
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
