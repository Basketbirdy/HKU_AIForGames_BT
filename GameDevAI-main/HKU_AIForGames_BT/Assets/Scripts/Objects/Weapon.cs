using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // weapon is set to true inside behaviour tree of enemy
        // so all that has te be done here is remove this object
        Destroy(gameObject);
    }
}
