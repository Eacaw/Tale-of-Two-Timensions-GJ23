using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject ChatBubbleController;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [SerializeField] private FMODUnity.EventReference TeleportEventPath;

    private Queue<string> sentences;

    void Start()
    {
        ChatBubbleController.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ChatBubbleController.SetActive(true);
        nameText.text = dialogue.name;

        GameObject introUI = GameObject.Find("Intro");
        if (introUI != null)
        {
            introUI.SetActive(false);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                PlayTeleport();
                // get object with tag introUI and set it to inactive

                SceneManager.LoadScene(1);
                // get player instance and move them to 000
                GameObject player = GameObject.Find("Player");
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.currentYear = "1570";
                playerController.currentCheckpoint = 0;
                player.transform.position = new Vector3(-6.0f, 1.6f, 81.33f);
            }
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        ChatBubbleController.SetActive(false);
    }

    void PlayTeleport()
    {
        FMOD.Studio.EventInstance teleport = FMODUnity.RuntimeManager.CreateInstance(TeleportEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(teleport, transform, GetComponent<Rigidbody>());

        teleport.start();
        teleport.release();
    }
}
