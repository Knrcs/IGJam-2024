using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void QuitGamelol()
    {
        Application.Quit();
        Debug.Log("Quitted");
    }

    public void Credits()
    {
        Application.OpenURL("");
        Debug.Log("Credits Are Happening");
    }
    
    public void LoadScene(int sceneInteger)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
        


    // Update is called once per frame
    void Update()
    {
        
    }
}
