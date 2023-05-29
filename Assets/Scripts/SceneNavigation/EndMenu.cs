using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
    }

    public void ReturnToMain() 
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}