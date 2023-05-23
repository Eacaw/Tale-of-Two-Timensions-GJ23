using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference FootstepsEventPath;
    [SerializeField] private FMODUnity.EventReference JumpEventPath;
    [SerializeField] private FMODUnity.EventReference TeleportEventPath;
    [SerializeField] private string SpeedParameterName;
    [SerializeField] private string JumpOrLandParameterName;

    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.2f;
    [SerializeField] private float StartRunningTime = 0.3f;
    [SerializeField] private string JumpInputName;
    [SerializeField] private GameObject ghostPoint;

    private float StepRandom;
    private Vector3 PrevPos;
    private float DistanceTravelled;

    private RaycastHit hit;
    private bool isJumping = false;
    private float TimeTakenSinceStep;

    private FMOD.Studio.Bus masterBus;

    // Start is called before the first frame update
    void Start()
    {
        StepRandom = Random.Range(0f, 0.5f);
        PrevPos = transform.position;
        masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    // Update is called once per frame
    void Update()
    {
        TimeTakenSinceStep += Time.deltaTime;
        DistanceTravelled += (transform.position - PrevPos).magnitude;

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayTeleport();
        }
        else {
            if (!JumpCheck())
            {
                if (DistanceTravelled >= StepDistance + StepRandom)
                {
                    SpeedCheck();
                    StepRandom = Random.Range(0f, 0.5f);
                    DistanceTravelled = 0f;
                }
            }
        }

        PrevPos = transform.position;
    }

    void PlayTeleport() {
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        FMOD.Studio.EventInstance teleport = FMODUnity.RuntimeManager.CreateInstance(TeleportEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(teleport, transform, GetComponent<Rigidbody>());

        teleport.start();
        teleport.release();
    }

    bool JumpCheck() {
        Physics.Raycast(ghostPoint.transform.position, Vector3.down, out hit, RayDistance);

        if (hit.collider)
        {
            if (isJumping) {
                // Just landed
                PlayJumpOrLand(false);
                DistanceTravelled = 0f;
            }

            isJumping = false;

            return isJumping;
        }
        else {
            // If is jumping is false, player has only just jumped so play jump sound
            if (!isJumping) {
                PlayJumpOrLand(true);
                isJumping = true;
            }

            return isJumping;
        }
    }

    void PlayJumpOrLand(bool F_JumpLandCalc) {
        if (F_JumpLandCalc)
        {
            masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }


        FMOD.Studio.EventInstance jumpLand = FMODUnity.RuntimeManager.CreateInstance(JumpEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(jumpLand, transform, GetComponent<Rigidbody>());

        jumpLand.setParameterByName(JumpOrLandParameterName, F_JumpLandCalc ? 0f : 1f);
        jumpLand.start();
        jumpLand.release();
    }

    void SpeedCheck() {
        if (TimeTakenSinceStep < StartRunningTime)
        {
            PlayFootstep(false);
        }
        else {
            PlayFootstep(true);
        }

        TimeTakenSinceStep = 0f;
    }

    void PlayFootstep(bool F_PlayerRunning) {
        FMOD.Studio.EventInstance footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, GetComponent<Rigidbody>());


        footstep.setParameterByName(SpeedParameterName, F_PlayerRunning ? 0f : 1f);
        footstep.start();
        footstep.release();
    }
}
