using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float turnSpeed = 20f;
    
    void Start()
    {
  
    }

    void Update()
    {
        // Rotazione continua delle monete
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);  
    }

}
