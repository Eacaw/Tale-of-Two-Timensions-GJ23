using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference ClickEventPath;

    public void StartClick() {
        PlayButtonSound();

        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.textMesh.gameObject.SetActive(true);
        playerController.yearIndicator.gameObject.SetActive(true);
        playerController.yearIndicatorBackground.gameObject.SetActive(true);
    }

    public void QuitGame() {
        PlayButtonSound();

        Application.Quit();
    }

    void PlayButtonSound()
    {
        FMOD.Studio.EventInstance click = FMODUnity.RuntimeManager.CreateInstance(ClickEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(click, transform, GetComponent<Rigidbody>());

        click.start();
        click.release();
    }
}