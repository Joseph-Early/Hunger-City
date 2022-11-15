using UnityEngine;

public class FPS_Health : MonoBehaviour
{
    [SerializeField] private float InitialHealth;
    [HideInInspector] public float health;

    // Set the health on awake
    private void Awake() {
        health = InitialHealth;
    }

    // If the actor is at zero health, kill them
    void Update()
    {
        if (health < 1)
            Kill();
    }

    internal virtual void Kill() => Destroy(gameObject);
}
