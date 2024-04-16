using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmaController : MonoBehaviour
{
 
    public int quantity = 50;       // Munizioni totali
    public GameObject proiettile;   // Il prefab del proiettile
    public Transform shotPoint;     // punto di creazione dei proiettili
    
    // Variabili per l'attesa tra uno sparo e l'altro
    float _shootTime = 2f;
    float _shootTimer = 0;
    bool _canShoot = true;

    // Variabili per il testo
    public TextMeshProUGUI testo;

    void Start()
    {        
        testo.text = "Proiettili: 50/50";
    }


    void Update() {
        // Incremento del tempo
        _shootTimer += Time.deltaTime;
        if(_shootTimer > _shootTime){
            _canShoot = true;
        } else {
            _canShoot = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            shoot();
        }        
    } 

    // Funzione di sparo
    void shoot(){
        if (!_canShoot) {
            return;
        } else {  
                quantity--;
                testo.text = "Proiettili: " + quantity.ToString() + "/50";
                // Istanzia l'oggetto proiettile, creando una copia del proiettilePrefab impostando la posizione e la rotazione
                GameObject proiettileInstance = Instantiate(proiettile, shotPoint.transform.position, proiettile.transform.rotation);
                proiettileInstance.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 20, ForceMode.Impulse);
        }
    }
} 


