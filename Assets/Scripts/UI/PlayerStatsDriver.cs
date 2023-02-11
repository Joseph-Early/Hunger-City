using UnityEngine;
using TMPro;
using Hunger = HungerCity.Actor.Components.Hunger;

namespace HungerCity.UI
{
    [RequireComponent(typeof(PlayerHealth), typeof(Hunger))]
    public class PlayerStatsDriver : MonoBehaviour
    {
        // Get a reference to the TMP_Text component.
        [SerializeField] TextMeshProUGUI TMP;

        // Reference to the PlayerHealth and Hunger component
        private PlayerHealth health;
        private Hunger hunger;

        // Get the PlayerHealth and Hunger component on awake
        private void Awake()
        {
            // Get the PlayerHealth and Hunger component
            health = GetComponent<PlayerHealth>();
            hunger = GetComponent<Hunger>();
        }

        // In late-update, update the UI with the current health and hunger
        private void LateUpdate()
        {
            // Get hunger and check if the player is starving, if so, set the text to "Starving"
            var hungerPercent = hunger.hunger >= 100 ? "Starving" : $"{Mathf.Round(hunger.hunger)}%";

            // Set the text to the current health and hunger
            TMP.text = $"Health: {Mathf.Round(health.GetHealth())}\nHunger: {hungerPercent}";
        }
    }

}