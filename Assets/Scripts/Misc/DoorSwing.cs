using UnityEngine;

public class DoorSwing : MonoBehaviour
{
    // private float doorOpenSpeed = 2f;

    public bool isOpening = true;

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            // transform += new Vector3(0f, doorOpenSpeed * Time.deltaTime, 0f);

            // Check if more than 90 degrees open
            if (transform.rotation.y >= 90) isOpening = false;
        }
    }
}
