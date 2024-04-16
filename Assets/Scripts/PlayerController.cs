using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  

public class PlayerController : MonoBehaviour
{
    Camera cameraChild;
    CharacterController controller;
    Animator animator;
    private bool isThirdPerson = false;
    
    // Variabili per controllare il movimento 
    public float speed = 8;
    public float turnSpeed = 60;
    public float horizontalInput;
    public float forwardInput;

    // Variabili per limitare il movimento
    public float rightBoundary;
    public float leftBoundary;
    public float forwardBoundary;
    public float backBoundary;
    public float ceilingBoundary = 5.33f;
    public float floorBoundary = 5.33f;
    
    // Variabili per il livello di salute
    public int maxHealth = 50; 
    public int currentHealth;
    public TextMeshProUGUI testo;
    public Slider slider;

    // Variabili per lo score
    public int currentScore = 0;
    public TextMeshProUGUI testoS;
    public AudioClip audio;
    public AudioSource audiosource;
    public TextMeshProUGUI testoDC;
   
    // Variabili per testo death coin
    float textTime = 2f;
    float textTimer = 0;

    // Variabili per le collisioni
    float _hitTimeEnemy = 2f;
    float _hitTimerEnemy = 0;
    bool _canHitEnemy = true;
    float _hitTimeCoin = 2f;
    float _hitTimerCoin = 0;
    bool _canHitCoin = true;

    // Variabili per il countdown
    public float timeleft = 300f;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI testoT;

    // Variabili per i controller dell'animator
    public RuntimeAnimatorController controller1;
    public RuntimeAnimatorController controller2;
    public RuntimeAnimatorController controller3;

    void Start()
    {
        cameraChild = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        Transform child = transform.Find("BasicMotionsDummy");
        animator = child.GetComponent<Animator>();
        audio = GetComponent<AudioSource>().clip;
        audiosource = GetComponent<AudioSource>();
        audiosource.playOnAwake = false;
        testo.text = "Health: 50";
        testoS.text = "Score: 0";
        testoT.text = "";
        currentHealth = maxHealth;
        slider.value = 50;
        animator.runtimeAnimatorController = controller1;
    }

    void Update()
    {
        // Movimento del personaggio 
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * forwardInput; 
        controller.Move(move * speed * Time.deltaTime); // Move per Character controller
        
        // Rotazione del personaggio
        if(Input.GetKey("c")){
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);  
        }
        if(Input.GetKey("z")){
            transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);  
        }
        // Cambiare modalità gioco prima e terza persona
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPerspective();
        }
        // Cambio controller
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            animator.runtimeAnimatorController = controller2;
            animator.Play("BasicMotions@Run");
        }
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ){
            animator.runtimeAnimatorController = controller1;
            animator.Play("BasicMotions@idle");
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)){
            animator.runtimeAnimatorController = controller3;
            animator.Play("BasicMotions@Walk");
        }

        // Limitare area di movimento
        if(transform.position.y >= ceilingBoundary){
            transform.position = new Vector3(transform.position.x, 5.33f, transform.position.z);
        } 
        else if(transform.position.y <= floorBoundary){
            transform.position = new Vector3(transform.position.x, 5.33f, transform.position.z);
        }   
        // Area 1
        if(transform.position.z >= 6.5 && transform.position.z < 17.5 && transform.position.x < 11 && transform.position.x >= 1){
            rightBoundary = 10f;
            leftBoundary = -7f;
            forwardBoundary = 17f;
            backBoundary = 10f;   
        }
        // Area 2
        else if(transform.position.z >= -0.5 && transform.position.z < 7.5 && transform.position.x < 1 && transform.position.x >= -7){
            rightBoundary = -0.1f;
            leftBoundary = -7f;
            forwardBoundary = 64f;
            backBoundary = -1f;   
        }
        // Area 3
        else if(transform.position.z >= 7.5 && transform.position.z < 17.5 && transform.position.x < 1 && transform.position.x >= -7){
            rightBoundary = 10f;
            leftBoundary = -7f;
            forwardBoundary = 64f;
            backBoundary = -1f;   
        }
        // Area 4
        else if(transform.position.z >= 17.5 && transform.position.z < 37 && transform.position.x < 1 && transform.position.x >= -7){
            rightBoundary = 0.1f;
            leftBoundary = -7f;
            forwardBoundary = 64f;
            backBoundary = -1f;   
        }
        // Area 5
        else if(transform.position.z >= 37 && transform.position.z <= 44  && transform.position.x < 10 && transform.position.x > -7){
            rightBoundary = 0.8f;
            leftBoundary = -40f;
            forwardBoundary = 62f;
            backBoundary = -1f;   
        }
        // Area 6
        else if(transform.position.z > 44 && transform.position.z < 64  && transform.position.x < 16 && transform.position.x >= -7){
            rightBoundary = 50f;
            leftBoundary = -20f;
            forwardBoundary = 100f;
            backBoundary = -1f;   
        }
        // Area 7 (rientranza)
        else if(transform.position.z > 30 && transform.position.z < 37  && transform.position.x < 0.3 && transform.position.x >= -10){
            leftBoundary = -20f;
        }
        playerBoundaries();

        // Incremento del tempo per evitare collisioni multiple con lo stesso oggetto
        _hitTimerEnemy += Time.deltaTime;
        if(_hitTimerEnemy > _hitTimeEnemy){
            _canHitEnemy = true;
        } else {
            _canHitEnemy = false;
        }

        _hitTimerCoin += Time.deltaTime;
        if(_hitTimerCoin > _hitTimeCoin){
            _canHitCoin = true;
        } else {
            _canHitCoin = false;
        }
        
        // Tempo per scritta death coin
        textTimer += Time.deltaTime;
        if(textTimer > textTime){
            testoDC.text = ""; 
        }

        // Attivazione funzione per passare al secondo livello
        if (currentScore >= 10){
            secondLevel();
        }

        // Countdown
        timeleft -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timeleft / 60);  
        seconds = Mathf.FloorToInt(timeleft % 60);
        testoT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
         if(timeleft <= 0)
         {
            SceneManager.LoadScene("gameover");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
         }
                
    }

    // Funzione per limitare il movimento del personaggio 
    void  playerBoundaries(){
        if(transform.position.x >= rightBoundary){
            transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);
        } 
        else if(transform.position.x <= leftBoundary){
            transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);
        }     
        if(transform.position.z <= backBoundary){
            transform.position = new Vector3(transform.position.x, transform.position.y, backBoundary);
        }  
        else if(transform.position.z >= forwardBoundary){
            transform.position = new Vector3(transform.position.x, transform.position.y, forwardBoundary);
        }  
        
    }

    // Cambio prospettiva
    void SwitchPerspective(){
        isThirdPerson = !isThirdPerson;
        if(isThirdPerson == true){
            cameraChild.transform.localPosition = new Vector3(0f, 1.5f, -3f); // regolare la posizione
            cameraChild.transform.localRotation = Quaternion.Euler(10f, 0f, 0f); // regolare la rotazione
        } else {
            cameraChild.transform.localPosition = new Vector3(0.05f, 0.38f, -0.06f); 
            cameraChild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); 
        }
    }

    // Collisioni
    void OnCollisionEnter (Collision collision ){
        print ("Hero tanker just hit gameobject name :  "+collision.collider.gameObject.name);
        if(collision.gameObject.tag == "coin") {
            if (!_canHitCoin){
                return;
            } else {
                _hitTimerCoin = 0; 
                currentScore++;
                testoS.text = "Score: " + currentScore.ToString();
                Destroy(collision.gameObject);
                audiosource.Play();
            }
            
        }
        else if (collision.gameObject.name == "HealthKit"){
            currentHealth = 50;
            testo.text = "Health: " + currentHealth.ToString();
            slider.value = currentHealth;
            audiosource.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "death coin"){
            GameObject[] items = GameObject.FindGameObjectsWithTag("Nemico");
            foreach (var item in items) {
                GameObject.Destroy(item);
            }
            textTimer = 0f;
            testoDC.text = "I nemici sono stati uccisi";
            audiosource.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Nemico"){
            if (!_canHitEnemy) {
                return;
            } else {
                currentHealth--;
                slider.value--;
                testo.text = "Health: " + currentHealth.ToString();
            }
            _hitTimerEnemy = 0;            
        }
        if(currentHealth <=0){
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene("gameover");
        }
               
        
    }

    // Secondo livello
    void secondLevel(){
        SceneManager.LoadScene("Level2");
        SceneManager.UnloadSceneAsync("Dock Thing");
    }

}
