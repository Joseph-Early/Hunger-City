using UnityEngine;

public class FPS_BaseBullet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private int damageAmount;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Check collision with target
    private void OnTriggerEnter(Collider obj)
    {
        // Insta kill target
        if (obj.CompareTag("Target"))
        {
            Destroy(obj.gameObject);
            Destroy(gameObject);
        }

        // Damage object with FPS_Health component
        var health = obj.GetComponent<FPS_Health>();
        if (health != null) {
            health.health -= damageAmount;
            print(health.health);
        }
    }
}
