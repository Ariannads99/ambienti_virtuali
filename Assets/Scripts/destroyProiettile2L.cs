using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProiettile2L : MonoBehaviour
{
    private float level = 0f;
    
    void Start () {
		
	}

	void Update () {
        // Il proiettile viene distrutto quando tocca terra
        if(transform.position.y <= level){
           Destroy(gameObject);
        }
	}
}
