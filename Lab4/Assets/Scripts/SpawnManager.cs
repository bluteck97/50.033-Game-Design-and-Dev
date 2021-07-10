using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    void Awake()
    {
        
    }
    void Start()
    {
        for (int j = 0; j < 2; j++) 
        {
            spawnFromPooler(ObjectType.goombaEnemy);
        }
    }
    void spawnFromPooler(ObjectType i){
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null){
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.SetActive(true);
        }
        else{
            Debug.Log("Not enuf items in the pool");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Start is called before the first frame update
    
    
    

    
}
