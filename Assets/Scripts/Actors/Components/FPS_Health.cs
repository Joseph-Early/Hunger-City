using UnityEngine;
using FPS_Health = HungerCity.Actor.Components.FPS_Health;

namespace HungerCity.Actor.Components
{
    public class FPS_Health : MonoBehaviour
    {
        [SerializeField] private float InitialHealth;
        [HideInInspector] public float health;

        // Set the health on awake
        private void Awake()
        {
            health = InitialHealth;
        }

        // If the actor is at zero health, kill them
        void Update()
        {
            health = Mathf.Clamp(health, 0, 100);

            if (health < 1)
                Kill();
        }

        internal virtual void Kill() => Destroy(gameObject);
    }
}