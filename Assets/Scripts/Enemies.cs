using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Variabili del giocatore
    GameObject giocatore;
    CharacterController player;
    
    // Variabili per movimento nemico
    Vector3 newPos;
    Vector3 playerPos;
    public float followSpeed;
    public bool canMove = true;

    public GameObject explosion;
    
   
    void Start()
    {
        giocatore = GameObject.Find("Giocatore ");
        if (giocatore) {
            Debug.Log(giocatore.name);
        } else {
            Debug.Log("No game object called giocatore found");
        }
        player = giocatore.GetComponent<CharacterController>();
    }

    
    void Update()
    {
        // Il nemico si muove e ruota verso il giocatore       
        playerPos = player.transform.position; // Posizione del player
        followSpeed = 0.008f;   
        newPos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        if(!canMove){
            return;
        } else {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, newPos, followSpeed);
        }        
    }

    // Collisione con il proiettile
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "proiettile") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    // Collisione con giocatore
    void OnCollisionStay(Collision collision){
        if(collision.gameObject.tag == "Player"){
            canMove = false;
        } 
    }

    void OnCollisionExit(Collision collision){
        canMove = true;
    }
}
