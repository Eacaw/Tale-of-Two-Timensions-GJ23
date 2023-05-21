using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference FootstepsEventPath;
    [SerializeField] private FMODUnity.EventReference JumpEventPath;
    [SerializeField] private string SpeedParameterName;
    [SerializeField] private string JumpOrLandParameterName;

    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.2f;
    [SerializeField] private float StartRunningTime = 0.3f;
    [SerializeField] private string JumpInputName;
    [SerializeField] private GameObject gameObject;

    private float StepRandom;
    private Vector3 PrevPos;
    private float DistanceTravelled;

    private RaycastHit hit;
    private bool isJumping = false;
    private float TimeTakenSinceStep;
    // Used to set the parameter in FMOD, if 0 then walking, if 1 then running
    private int F_PlayerRunning;

    // Start is called before the first frame update
    void Start()
    {
        StepRandom = Random.Range(0f, 0.5f);
        PrevPos = transform.position;
        Debug.Log(PrevPos);
    }

    // Update is called once per frame
    void Update()
    {
        TimeTakenSinceStep += Time.deltaTime;
        DistanceTravelled += (transform.position - PrevPos).magnitude;

        // Only want to play a footstep sound if player is grounded
        if (!JumpCheck())
        {
            if (DistanceTravelled >= StepDistance + StepRandom)
            {
                SpeedCheck();
                PlayFootstep();
                StepRandom = Random.Range(0f, 0.5f);
                DistanceTravelled = 0f;
            }
        }

        PrevPos = transform.position;
    }

    bool JumpCheck() {
        Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, RayDistance);

        if (hit.collider)
        {
            if (isJumping) {
                // Just landed
                PlayJumpOrLand(true);
                DistanceTravelled = 0f;
            }

            isJumping = false;

            return isJumping;
        }
        else {
            // If is jumping is false, player has only just jumped so play jump sound
            if (!isJumping) {
                PlayJumpOrLand(false);
                isJumping = true;
            }

            return isJumping;
        }
    }

    void PlayJumpOrLand(bool F_JumpLandCalc) {
        FMOD.Studio.EventInstance jumpLand = FMODUnity.RuntimeManager.CreateInstance(JumpEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(jumpLand, transform, GetComponent<Rigidbody>());

        jumpLand.setParameterByName(JumpOrLandParameterName, F_JumpLandCalc ? 0f : 1f);
        jumpLand.start();
        jumpLand.release();
    }

    void SpeedCheck() {
        if (TimeTakenSinceStep < StartRunningTime)
        {
            F_PlayerRunning = 1;
        }
        else {
            F_PlayerRunning = 0;    
        }

        TimeTakenSinceStep = 0f;
    }

    void PlayFootstep() {
        FMOD.Studio.EventInstance footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, GetComponent<Rigidbody>());

        footstep.setParameterByName(SpeedParameterName, F_PlayerRunning);
        footstep.start();
        footstep.release();
    }
}
