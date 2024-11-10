using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    [ContextMenu("Quit")]
         public void QuitGamelol() 
        { 
            Application.Quit();
            Debug.Log("Quitted");
         }

    // Update is called once per frame
    void Update()
    {
        
    }
}
