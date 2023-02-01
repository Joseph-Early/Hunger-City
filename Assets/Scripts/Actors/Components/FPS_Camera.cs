using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Extends Monobheaviour (Unity gameobject class)
public class FPS_Camera : MonoBehaviour
{
    public float maximumX = 60f; // Max x rotation
    public float minimumX = -60f; // Min x rotation
    public float turnSpeed = 5f; //What is this?
    public Camera cam; // Reference to camera
    float rotX = 0f;
    float rotY = 0f; 

    void Update()
    {
        PlayerLook();
        Cursor.lockState = CursorLockMode.Locked; // Hide the mouse
    }

    void PlayerLook()
    {
        // Axes backwards due to converting 2D coordinates into 3D space
        rotX += Input.GetAxis("Mouse Y") * turnSpeed; // Rotate on the X axis
        rotY += Input.GetAxis("Mouse X") * turnSpeed; // Rotate on the Y axis

        rotX = Mathf.Clamp(rotX, minimumX, maximumX); // Prevent X rotation from getting of bounds

        transform.localEulerAngles = new Vector3(0, rotY, 0); // Represents rotation in world space (euler)
        cam.transform.localEulerAngles = new Vector3(-rotX, 0, 0); // vec3 takes three values for each axis?
    }
}
