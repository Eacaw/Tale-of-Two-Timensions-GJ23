using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsUI;

    private CanvasRenderer controlsCanvas;
    private PlayerController player;
    
    [SerializeField] private FMODUnity.EventReference ClickEventPath;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(false);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        CloseControls();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Controls()
    {
        player.HideGUI();
        controlsUI.SetActive(true);
    }

    public void CloseControls()
    {
        player.ShowGUI();
        controlsUI.SetActive(false);
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