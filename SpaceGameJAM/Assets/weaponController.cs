using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class weaponController : MonoBehaviour
{
    public GameObject blowTorch; // Assign in inspector
    public GameObject rayGun; // Assign in inspector
    public GameObject laser; // Laser of the blowTorch, assign in inspector

    private Renderer[] blowTorchRenderers;
    private Renderer[] rayGunRenderers;

    private enum WeaponType { None, BlowTorch, RayGun }
    private WeaponType equippedWeapon = WeaponType.None;

    void Start()
    {
        // Get all renderer components for both weapons
        blowTorchRenderers = blowTorch.GetComponentsInChildren<Renderer>();
        rayGunRenderers = rayGun.GetComponentsInChildren<Renderer>();

        // Initially make both weapons invisible
        SetWeaponVisibility(blowTorchRenderers, false);
        SetWeaponVisibility(rayGunRenderers, false);

        // Deactivate the laser
        laser.SetActive(false);
    }

    void Update()
    {
        // Equip BlowTorch with the press of the '3' key
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedWeapon = (equippedWeapon == WeaponType.BlowTorch) ? WeaponType.None : WeaponType.BlowTorch;
            UpdateWeaponStates();
        }

        // Equip RayGun with the press of another key, say '4'
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equippedWeapon = (equippedWeapon == WeaponType.RayGun) ? WeaponType.None : WeaponType.RayGun;
            UpdateWeaponStates();
        }

        // Check for "Fire1" action based on equipped weapon
        if (Input.GetButton("Fire1"))
        {
            PerformWeaponAction();
        }
        else if (equippedWeapon == WeaponType.BlowTorch)
        {
            // Ensure laser is deactivated when not holding "Fire1"
            laser.SetActive(false);
        }
    }

    void UpdateWeaponStates()
    {
        // Update visibility based on equipped weapon
        SetWeaponVisibility(blowTorchRenderers, equippedWeapon == WeaponType.BlowTorch);
        SetWeaponVisibility(rayGunRenderers, equippedWeapon == WeaponType.RayGun);

        // Deactivate blow torch's laser if it's not equipped
        if (equippedWeapon != WeaponType.BlowTorch)
        {
            laser.SetActive(false);
        }
    }

    void PerformWeaponAction()
    {
        if (equippedWeapon == WeaponType.BlowTorch)
        {
            // Activate the laser
            laser.SetActive(true);
        }
        else if (equippedWeapon == WeaponType.RayGun)
        {
            // Perform raycast
            RaycastFromRayGun();
        }
    }

    void RaycastFromRayGun()
    {
        RaycastHit hit;
        // Assuming the ray originates from the position of the ray gun and goes forward from it
        if (Physics.Raycast(rayGun.transform.position, rayGun.transform.forward, out hit))
        {
            Debug.Log("RayGun hit: " + hit.collider.name);

            // Check if the hit object has the tag "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy: " + hit.collider.name);
                // Here you can implement what happens when you hit an enemy.
                // For example, apply damage, play an animation, etc.

                // If the enemy has a script attached for taking damage, you might call a method like this:
                // hit.collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            }
        }
    }

    // Helper method to set visibility for a collection of renderers
    private void SetWeaponVisibility(Renderer[] renderers, bool isVisible)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }
}

