using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void ReturnToMain() {
        Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}