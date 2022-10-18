using UnityEngine;

public class FPS_GunBaseClass : MonoBehaviour
{
    [SerializeField] private GameObject bullet, firePoint;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) FireGun();
    }

    // Fire the gun
    private void FireGun() {
        var bulletFired = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }
}
