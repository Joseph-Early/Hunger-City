using UnityEngine;

public class FPS_BaseBullet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Check collision with target
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Target"))
        {
            Destroy(obj.gameObject);
            Destroy(gameObject);
        }
    }
}
