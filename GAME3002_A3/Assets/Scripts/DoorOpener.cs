using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private bool isOpen = false;

    private HingeJoint hinge;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        if (hinge != null)
        {
            JointMotor motor = hinge.motor;
            motor.targetVelocity = 90f; // adjust speed of swing
            motor.force = 100f;         // how strong the motor is
            hinge.motor = motor;
            hinge.useMotor = true;
        }
    }
}
