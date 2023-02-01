using UnityEngine;

namespace Actor
{
    public class Guard : FPS_Enemy
    {
        [SerializeField] private GameObject[] FoodToGuard;
        private void Start() => activated = false;

        private new void Update() {
            // Call base class's update
            base.Update();

            // If activated, skip
            if (activated) return;

            // Check if any of the food to guard no longer exists
            foreach (var food in FoodToGuard)
            {
                // If any food is null, set activate
                if (food == null) activated = true;
            }
        }
    }
}