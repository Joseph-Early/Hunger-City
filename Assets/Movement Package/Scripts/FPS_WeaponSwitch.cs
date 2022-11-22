using System;
using System.Collections.Generic;
using UnityEngine;

/*
    * This script is used to switch between weapons.
    * It is attached to the player.
    * To add a new weapon, add it to the weapons list.
    * To change the weapon, use the scroll wheel or the number keys.
    * Code by Az Foxxo (https://github.com/AzFoxxo/Scripts)
    * Anarchist License, MIT Licence, GNU GPL v3.0 Licence, or any later version.
*/

public class FPS_WeaponSwitch : MonoBehaviour
{
    // List of weapons
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] GameObject weaponStoreLocation;

    // Current weapon index
    byte currentWeaponIndex;

    // Delay between weapon switch
    [SerializeField] float switchDelay = 0.25f;

    // Timer for weapon switch delay
    float switchTimer = 0;

    // Activate the default weapon
    private void Awake() {
        // Activate the first weapon
        weapons[0].SetActive(true);
        currentWeaponIndex = 0;
    }

    // Switch weapons when letter e is held
    private void Update()
    {
        // Switch weapons when mouse wheel is scrolled up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) SwitchWeapon();

        // Decrease switch timer
        switchTimer -= Time.deltaTime;

        // Clamp switch timer
        if (switchTimer < 0) switchTimer = 0;
    }

    // Switch the weapon
    private void SwitchWeapon()
    {
        // If the switch delay timer is not finished, return
        if (switchTimer > 0) return;

        // Set the switch timer
        switchTimer = switchDelay;

        // Deactivate the current weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Switch to the next weapon
        currentWeaponIndex++;

        // Check if the index is out of bounds
        if (currentWeaponIndex >= weapons.Count) currentWeaponIndex = 0;
        if (currentWeaponIndex < 0) currentWeaponIndex = (byte)(weapons.Count - 1);

        // Activate the new weapon
        weapons[currentWeaponIndex].SetActive(true);
    }

    #region Helper utilities
    ///<summary> Add a new weapon to the weapon list </summary>
    ///<param name="weapon"> Weapon to add </param>
    public void AddWeapon(GameObject weapon) => weapons.Add(weapon); 

    ///<summary> Remove a weapon from weapon list </summary>
    ///<param name="weapon"> Weapon to remove </param>
    public void RemoveWeapon(GameObject weapon) => weapons.Remove(weapon);

    ///<summary> Return the current weapon </summary>
    ///<returns> Returns weapon </returns>
    public GameObject GetCurrentWeapon() => weapons[currentWeaponIndex];

    ///<summary> Return the current weapon's index </summary>
    ///<returns> Returns weapon index (byte) </returns>
    public byte GetCurrentWeaponIndex() => currentWeaponIndex;

    ///<summary> Nest the weapon under the player prefab </summary>
    ///<param name="weapon"> Weapon to nest </param>
    public void Nest(GameObject weapon, bool nest = true) {
        if (IsNested(weapon))
        {
            // Move the weapon under the player prefab
            weapon.transform.SetParent(weaponStoreLocation.transform);

            // Set the position to 0, 0, 0
            weapon.transform.position = new Vector3(0f, 0f, 0f);
        }
        else
        {
            // Drop the weapon
            weapon.transform.SetParent(null);
        }
    }

    ///<summary> Check the weapon is nested </summary>
    ///<param name="weapon"> Weapon to check is nested </param>
    ///<returns> Bool if nested </returns>
    public bool IsNested(GameObject weapon) => weapon.transform.parent != null ? true : false;
    

    #endregion
}
