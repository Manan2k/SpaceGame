using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowTorch : MonoBehaviour
{
    public GameObject laser; // Assign this in the inspector with your laser GameObject

    private bool isWeaponEquipped = false;

    // Reference to the Renderer component(s)
    private Renderer[] weaponRenderers;

    private void Start()
    {
        // Get all renderer components attached to the weapon and its children (if any)
        weaponRenderers = GetComponentsInChildren<Renderer>();

        // Initially make the weapon invisible
        SetWeaponVisibility(false);

        // Initially deactivate the laser
        laser.SetActive(false);
    }

    void Update()
    {
        // Equip/Unequip the weapon with a press of the '3' key
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isWeaponEquipped = !isWeaponEquipped; // Toggle the state

            // Toggle weapon visibility based on the equipped state
            SetWeaponVisibility(isWeaponEquipped);

            // If equipping the weapon, ensure the laser starts off by deactivating it
            if (isWeaponEquipped)
            {
                laser.SetActive(false); // Make sure the laser is not active
            }
        }

        // Check if the weapon is equipped and the "Fire1" button is being held down
        if (isWeaponEquipped && Input.GetButton("Fire1"))
        {
            // While the "Fire1" button is held down, keep the laser active
            laser.SetActive(true);
        }
        else
        {
            // If the "Fire1" button is not being held down, deactivate the laser
            laser.SetActive(false);
        }
    }

    // Helper method to set weapon visibility
    private void SetWeaponVisibility(bool isVisible)
    {
        foreach (var renderer in weaponRenderers)
        {
            renderer.enabled = isVisible;
        }
    }
}
