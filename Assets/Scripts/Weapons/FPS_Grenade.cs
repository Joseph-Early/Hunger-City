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
    void OnCollisionEnter(Collision obj) {
        if (obj.gameObject.CompareTag("Target")) {
            // Damage the target - instant kill
            var target = obj.gameObject.GetComponent<FPS_Target>();

            if (target != null) target.InstantKill();
        }
    }
}
