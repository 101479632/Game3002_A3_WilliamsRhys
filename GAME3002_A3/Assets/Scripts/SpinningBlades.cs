using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpinningBlades : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTorque = new Vector3(0f, 10f, 0f); // Rotate around Y only

    private Rigidbody m_rb = null;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.maxAngularVelocity = 500f;

        // Freeze all position and unwanted rotation axes
        m_rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX |  RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        m_rb.AddTorque(m_vTorque);
    }
}
