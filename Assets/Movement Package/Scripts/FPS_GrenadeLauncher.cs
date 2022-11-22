using UnityEngine;

public class FPS_GrenadeLauncher : FPS_Reload
{
    [SerializeField] GameObject grenade;
    [SerializeField] Transform firePoint;

    private void Update() {
        // TODO: Check is nested
        if (Input.GetButtonDown("Fire1"))
            Instantiate(grenade, firePoint.position, firePoint.rotation);
    }
}
