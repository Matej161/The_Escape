using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //private float rotationY = 0;
    public float lookSpeed = 2f;
    public float lookYLimit;
    public Camera playerCamera;

    Light myLight;

    private void Start()
    {
       myLight = GetComponent<Light>();
       myLight.enabled = false;
    }

    void Update()
    {
        
        /*rotationY += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -lookYLimit, lookYLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse Y") * lookSpeed, 0);
        */
        if (Input.GetKeyDown(KeyCode.F)) {
            myLight.enabled = !myLight.enabled;
        }
    }
}