using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;  // The respawn point (typically at the start)
    public GameObject player;
    public float respawnDelay = 0.5f;  // Optional delay before respawn

    void Start()
    {
        // Ensure the spawn point is set at the start
        if (spawnPoint == null)
        {
            spawnPoint = transform;  // Default to the player's current position if not assigned
        }
    }

    public void Respawn()
    {
        Invoke("RespawnAtStart", respawnDelay);
    }

    void RespawnAtStart()
    {
        // Respawn the player at the spawn point
        player.transform.position = spawnPoint.position;
        Debug.Log("Player respawned at the start point!");
    }
}
