using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class menuManager : MonoBehaviour
{
    public RectTransform play;
    public RectTransform informazioni;
    public TextMeshProUGUI testo;
    
    void Start()
    {
        testo.text = "";
        SceneManager.UnloadSceneAsync("Dock Thing");
        SceneManager.LoadScene("Menu iniziale", LoadSceneMode.Single);
    }

    void Update()
    {
        

    }

    public void lanciaGioco()
    {
        SceneManager.LoadScene("Dock Thing");
    }

    public void istruzioni()
    {
        testo.text = "Usa le frecce della tastiera per muoverti.\n" +
            "Usa i tasti \"z\" e \"c\" per ruotare il personaggio.\n" +
            "Premi la barra spaziatrice per sparare.\n" +
            "Premi Tab per cambiare la prospettiva.";
    }
}

