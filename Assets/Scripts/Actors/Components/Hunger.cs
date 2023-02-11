using UnityEngine;

namespace Actor
{
    // Require any object implementing hunger have FPS_Health
    [RequireComponent(typeof(FPS_Health))]
    public class Hunger : MonoBehaviour
    {
        private FPS_Health health;
        [HideInInspector] public float hunger;
        [SerializeField] private float hungerRate, hungerDamage;

        // Get a reference to the FPS_Health component
        private void Awake() => health = gameObject.GetComponent<FPS_Health>();

        private void Update()
        {
            // Increase hunger every frame
            hunger += Time.deltaTime * hungerRate;

            // Clamp hunger
            hunger = Mathf.Clamp(hunger, 0, 100);

            // If hunger is greater than 100, take damage at rate of hungerDamage
            health.health -= hunger >= 100 ? Time.deltaTime * hungerDamage : 0;
        }
    }
}
