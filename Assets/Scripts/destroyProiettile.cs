using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProiettile : MonoBehaviour
{
    private float level = 4.54f;
    
    void Start () {
		
	}

	void Update () {
        // Il proiettile viene distrutto quando tocca terra
        if(transform.position.y < level){
           Destroy(gameObject);
        }
	}
}
