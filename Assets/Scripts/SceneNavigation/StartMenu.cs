using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartClick() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}