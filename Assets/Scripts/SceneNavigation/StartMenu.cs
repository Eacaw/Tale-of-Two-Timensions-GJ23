using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartClick() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.textMesh.gameObject.SetActive(true);
         playerController.yearIndicator.gameObject.SetActive(true);
        playerController.yearIndicatorBackground.gameObject.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}