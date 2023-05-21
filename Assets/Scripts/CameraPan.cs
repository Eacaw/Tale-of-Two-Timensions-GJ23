using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{

    private float rotationSpeed = 5.0f;
    void Update()
    {
        float rotation = transform.rotation.y * 180;
        if(rotation >= 175){
            rotationSpeed = rotationSpeed * -1f;
        }
        if(rotation <= 110){
            rotationSpeed = rotationSpeed * -1f;
        }
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
