using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    private List<GameObject> poolObjects = new List<GameObject>();
    private int amountToPool = 1;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToPool; i++){
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPoolObject(){
        for(int i = 0; i < poolObjects.Count; i++){
            if(!poolObjects[i].activeInHierarchy){
                return poolObjects[i];
            }
        }
        return null;
    }
}
