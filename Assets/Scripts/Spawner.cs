using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
       
    void Start()
    {
        // Il primo nemico viene creato dopo 8s, i restanti dopo 10s
        InvokeRepeating("SpawnEnemy", 8f, 10f);
    }

    void Update()
    {
        
    }

    // I nemici vengono generati in prossimità del player ogni 10s
    void SpawnEnemy(){
        var position = new Vector3(transform.position.x + Random.Range(5f, 8.0f), 4.5f, transform.position.z + Random.Range(5f, 8.0f));
        Instantiate(enemy, position, transform.rotation);
    }
}
