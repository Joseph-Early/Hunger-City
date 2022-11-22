using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject triggered;

     private void OnTriggerStay(Collider other)
    {
        // other.GetComponent<DoorSwing>().isOpening = true;

        triggered.SetActive(false);
    }
}
