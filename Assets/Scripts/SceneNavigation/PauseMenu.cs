using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] private FMODUnity.EventReference ClickEventPath;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            if(GameIsPaused) {
                Resume();
            } else 
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PlayButtonSound();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        PlayButtonSound();

        Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
        SceneManager.LoadScene(0);
    }

    void Pause()
    {
        PlayButtonSound();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void PlayButtonSound()
    {
        FMOD.Studio.EventInstance click = FMODUnity.RuntimeManager.CreateInstance(ClickEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(click, transform, GetComponent<Rigidbody>());

        click.start();
        click.release();
    }
}