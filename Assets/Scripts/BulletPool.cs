using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instant;
    public static BulletPool instant => _instant;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
    [SerializeField] GameObject bulletBee;
    // Start is called before the first frame update
    private void Awake() {
        _instant = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject getBullet(){
        foreach (GameObject g in bullets){
            if(g.activeSelf)
                continue;
            return g;
        }
        GameObject g2 = Instantiate(bulletBee, this.transform.position, Quaternion.identity);
        bullets.Add(g2);
        return g2;
    }
}
