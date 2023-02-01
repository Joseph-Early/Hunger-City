using UnityEngine;

public class FPS_GunBaseClass : FPS_Reload
{
    [SerializeField] private GameObject bullet, firePoint, grenade;

    [SerializeField] ParticleSystem muzzleFlash;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) FireGun();
    }

    // Fire the gun
    private void FireGun() {
        muzzleFlash.Play();
        var bulletFired = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }
}
