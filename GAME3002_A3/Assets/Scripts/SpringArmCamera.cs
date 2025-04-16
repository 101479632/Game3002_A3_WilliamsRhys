using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArmCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(4f, 2f, -6f); // Position offset relative to player
    public float smoothSpeed = 0.125f; // Lag Speed

    private Vector3 currentVelocity;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned.");
        }
    }


    private void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player with the smooth lag
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothSpeed);
        }
    }
}

