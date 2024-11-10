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
}
