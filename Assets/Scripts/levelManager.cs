using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class levelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(next());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator next(){
        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("Level2");
        SceneManager.LoadScene("Demo Blue");
    }
}
