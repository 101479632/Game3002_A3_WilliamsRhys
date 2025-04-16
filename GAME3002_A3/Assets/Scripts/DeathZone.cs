using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public PlayerRespawn playerRespawnScript;

    void OnTriggerEnter(Collider other)
    {
        // Only trigger respawn if the player falls into the death zone
        if (other.CompareTag("Player"))
        {
            playerRespawnScript.Respawn();
        }
    }
}
