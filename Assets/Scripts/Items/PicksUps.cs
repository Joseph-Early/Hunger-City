using UnityEngine;
using Hunger = HungerCity.Actor.Components.Hunger;

public class PicksUps : MonoBehaviour
{
    // Pickup player health pack
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food")) {
            Destroy(other.gameObject);
            GetComponent<Hunger>().hunger = 0;
        }

        // Exit if not health pack
        if (!other.CompareTag("HealthPack")) return;

        // Increase health
        GetComponent<PlayerHealth>().health += 10;

        // Destroy health pack
        Destroy(other.gameObject);

        #if DEBUG
        // Debug log
        print("Picked up health");
        #endif
    }

    // Pick up items
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            // Check weapon is nested under player or not
            var switcher = GetComponent<FPS_WeaponSwitch>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (switcher.IsNested(other.gameObject))
                {
                    // Drop the weapon
                    switcher.RemoveWeapon(other.gameObject); // Remove from weapon list
                    switcher.Nest(other.gameObject, false); // De-nest the weapon (drop it)

                    print("Drop!");
                }
                else
                {
                    // Pickup the weapon
                    switcher.AddWeapon(other.gameObject); // add to weapon list
                    switcher.Nest(other.gameObject); // De-nest the weapon (drop it)

                    print("Pick up!");
                }
            }
        }
    }
}
