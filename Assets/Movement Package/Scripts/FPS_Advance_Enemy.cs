using UnityEngine;

public class FPS_Advance_Enemy : FPS_Enemy
{
    [SerializeField] private GameObject bullet, firePoint;

    [SerializeField] ParticleSystem muzzleFlash;

    float time;
    
    void Start() => time = Time.time + 1.5f;

    // Update is called once per frame
    new void Update()
    {
        // Invoke the enemies update
        base.Update();
        
        // Fire the gun every 1.5 secs
        if (navMeshAgent.isActiveAndEnabled)
        {
            if (time - Time.time <= 0) {
                FireGun();

                // Add 1.5 secs
                time += 1.5f;
            }
        }

    }

    // Fire the gun
    private void FireGun() {
        // muzzleFlash.Play();
        var bulletFired = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }
}
