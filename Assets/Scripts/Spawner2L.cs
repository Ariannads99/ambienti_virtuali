using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2L : MonoBehaviour
{
    public GameObject enemy;
       
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 8f, 10f);
    }

    void Update()
    {
        
    }

    void SpawnEnemy(){
        var position = new Vector3(transform.position.x + Random.Range(5f, 8.0f), 0f, transform.position.z + Random.Range(5f, 8.0f));
        Instantiate(enemy, position, transform.rotation);
    }
}
