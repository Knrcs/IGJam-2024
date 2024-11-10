using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_GameUIButtons : MonoBehaviour
{

public void StartAgain()
{
    SceneManager.LoadScene(1);
}
public void MainMenu()
{
    SceneManager.LoadScene(0);
}

    public void QuitGamelol()
    {
        Application.Quit();
        Debug.Log("Quitted");
    }

    public void Credits()
    {
        Application.OpenURL("https://knrc.itch.io/battleneck");
        Debug.Log("Credits Are Happening");
    }
    
    public void LoadScene(int sceneInteger)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
