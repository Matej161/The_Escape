using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform myCamera;
    //public Transform hand;
    public float cameraSensitivity = 200f;
    public float cameraAcceleration= 5f;

    private float Xrotation;
    private float Yrotation;

    void Start()
    {
        
    }

    
    void Update()
    {
        Xrotation += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        Yrotation += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;

        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, Yrotation, 0), cameraAcceleration * Time.deltaTime);
        myCamera.localRotation = Quaternion.Lerp(myCamera.localRotation, Quaternion.Euler(-Xrotation, 0, 0), cameraAcceleration * Time.deltaTime);
    }
}
