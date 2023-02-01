using UnityEngine;

public class FPS_Reload : MonoBehaviour
{
    public byte usesBeforeReload = 10; // How long until reload
    public float timeInSecsToReload = 1; // Reload duration

    public byte fireCount = 0;

    // Reload the weapon
    public void ReloadWeapon()
    {
        // If Fire count is the same as max, reload
        Invoke("ReloadWeaponNow", timeInSecsToReload);
    }

    public void ReloadWeaponNow() => fireCount = 0;
}
