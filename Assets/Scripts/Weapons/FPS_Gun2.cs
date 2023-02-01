using UnityEngine;

public class FPS_Gun2 : MonoBehaviour
{
    [SerializeField] private float gunDamage, gunRange, gunFireRate;
    [SerializeField] private GameObject gunFirePoint;

    private LineRenderer lineRenderer;

    private float nextFire;

    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        ShootGun();


    }

    // Shoot the gun
    private void ShootGun()
    {
        RaycastHit hit;

        // Time until next fire
        nextFire = Time.time + gunFireRate;

        // Fire the gun
        if (Physics.Raycast(gunFirePoint.transform.position, gunFirePoint.transform.forward, out hit, gunRange))
        {
            print(hit.transform.name);
            FPS_Target target = hit.transform.GetComponent<FPS_Target>();

            if (target != null) target.TakeDamage(gunDamage);
        }

        // // Line renderer update position
        // lineRenderer.SetPosition(0, transform.position);
        // lineRenderer.SetPosition(1, hit.transform.position);
    }
}
