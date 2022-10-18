using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3.1f);
    }

    // Update is called once per frame
    void Explode()
    {
        // if (GetComponent<ParticleSystem.Particle>().remainingLifetime < 0)
        // {
            // Enlarge the object by five
            transform.localScale = new Vector3(20f, 20f, 20f);
        // }
    }

    // Check colliding with target
    void OnCollisionEnter(Collider obj) {
        if (obj.CompareTag("target")) {
            // Damage the target - instant kill
            var target = obj.GetComponent<FPS_Target>();

            if (target != null) target.InstantKill();
        }
    }
}
