using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerZone : MonoBehaviour
{
    public KeyType requiredKey = KeyType.Bronze;
    public GameObject doorObject; // Assign the Door child in Inspector

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

            if (inventory != null && inventory.HasKey(requiredKey))
            {
                doorObject.GetComponent<DoorOpener>().Open();
                Debug.Log("Door Opened");
            }
            else
            {
                Debug.Log("You need the " + requiredKey + " key!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
