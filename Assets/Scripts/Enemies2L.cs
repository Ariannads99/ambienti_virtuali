using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies2L : MonoBehaviour
{
    // Variabili del giocatore
    GameObject giocatore;
    CharacterController player;

    // Variabili per movimento nemico
    Vector3 newPos;
    Vector3 playerPos;
    public bool canMove = true;

    public GameObject explosion;

    void Start()
    {
        giocatore = GameObject.Find("Giocatore2L");
        player = giocatore.GetComponent<CharacterController>();
    }

    void Update()
    {
        playerPos = player.transform.position;
        float followSpeed = 0.01f; // velocità maggiore
        
        newPos = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        if(!canMove){
            return;
        } else {
        transform.position = Vector3.MoveTowards(transform.position, newPos, followSpeed);
        transform.LookAt(player.transform);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "proiettile") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionStay(Collision collision){
        if(collision.gameObject.tag == "Player"){
            canMove = false;
        } 
    }

    void OnCollisionExit(Collision collision){
        canMove = true;
    }
}
