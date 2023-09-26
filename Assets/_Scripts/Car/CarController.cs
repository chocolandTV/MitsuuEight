using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody m_RigidBody;

    [SerializeField]
    private CarSuspension m_Suspension;

    [SerializeField]
    private List<TrailRenderer> m_SkidmarkRenderers;

    [Header("Parameter")]
    [Header("Normal Acc/Turn")]
    [SerializeField]
    private float m_Acc;

    [SerializeField]
    private float m_MaxSpeed;

    [SerializeField]
    private float m_TurnForce;

    [SerializeField]
    private float m_MaxAngularVelocity;

    [SerializeField]
    private float m_Grip;

    [Header("Drift Acc/Turn")]
    [SerializeField]
    private float m_DriftAcc;

    [SerializeField]
    private float m_MaxDriftSpeed;

    [SerializeField]
    private float m_DriftTurnForce;

    [SerializeField]
    private float m_MaxDriftAngularVelocity;

    [SerializeField]
    private float m_DriftGrip;

    [Header("Other")]
    [SerializeField]
    private float m_MinVelForDrift = 0.5f;

    private float m_AccInput;

    private float m_ReverseInput;

    private Vector2 m_SteeringInput;
    private bool m_IsDrivingForward;

    private bool m_IsDrivingEnabled;

    private bool m_SkidmarksActive;

    public Vector2 steeringInput => m_SteeringInput;

    public Vector2 velocity { get; private set; }

    public float driftVal { get; private set; }

    public float speedVal { get; private set; }

    public bool isDrifting { get; private set; }
    public void DisableDriving()
    {
        m_IsDrivingEnabled = false;
    }
    public void EnableDriving()
    {
        m_IsDrivingEnabled = true;
        // Sound engine start;
    }
    public void Move(Vector2 _moveHorizontalInput, float _AccelerateInput, float _brakeInput, float _handBrakeInput)
    {
        if (!m_IsDrivingEnabled)
        {
            return;
        }
        if (m_Suspension.isGrounded)
        {
            Vector3 vector = base.transform.forward;
            if (Physics.Raycast(base.transform.position, -base.transform.up, out var hitInfo, 100f))
            {
                Debug.DrawLine(base.transform.position, hitInfo.point, Color.cyan);
                vector = Quaternion.AngleAxis(Vector3.SignedAngle(base.transform.up, hitInfo.normal, base.transform.right), base.transform.right) * base.transform.forward;
                Debug.DrawLine(base.transform.position, base.transform.position + vector * 50f, Color.green);
            }
            if (_AccelerateInput > 0f)
            {
                float num = (isDrifting ? m_DriftAcc : m_Acc);
                m_RigidBody.AddForce(_AccelerateInput * num * vector, ForceMode.Acceleration);
            }
            if (_brakeInput > 0f)
            {
                float num2 = (isDrifting ? m_DriftAcc : m_Acc);
                m_RigidBody.AddForce(_brakeInput * num2 * -vector, ForceMode.Acceleration);
            }
            m_IsDrivingForward = Vector3.Angle(m_RigidBody.velocity, vector) < Vector3.Angle(m_RigidBody.velocity, -vector);
            float num3 = (isDrifting ? m_MaxDriftSpeed : m_MaxSpeed);
            m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, num3);
            speedVal = Mathf.InverseLerp(0f, num3, m_RigidBody.velocity.magnitude);

            // SUS IS GROUNDED
            _moveHorizontalInput *= speedVal;
            if (!m_IsDrivingForward)
            {
                _moveHorizontalInput = -_moveHorizontalInput;
            }
            float num4 = (isDrifting ? m_DriftTurnForce : m_TurnForce);
            m_RigidBody.AddTorque(base.transform.up * _moveHorizontalInput.x * num4, ForceMode.Acceleration);
            float num5 = (isDrifting ? m_MaxDriftAngularVelocity : m_MaxAngularVelocity);
            m_RigidBody.angularVelocity = new Vector3(m_RigidBody.angularVelocity.x, num5 * _moveHorizontalInput.x, m_RigidBody.angularVelocity.z);
            // END SUS
            float value = Vector3.SignedAngle(m_RigidBody.velocity, base.transform.forward, Vector3.up);
            driftVal = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-80f, 80f, value));
            if (m_RigidBody.velocity.magnitude > m_MinVelForDrift)
            {
                m_RigidBody.AddForce(base.transform.right * driftVal * (isDrifting ? m_DriftGrip : m_Grip), ForceMode.Acceleration);

            }
            if (!m_SkidmarksActive && isDrifting)
            {
                m_SkidmarksActive = true;
                foreach (TrailRenderer skidmarkRenderer in m_SkidmarkRenderers)
                {
                    skidmarkRenderer.emitting = true;
                }
            }
        }
        else if (m_SkidmarksActive && isDrifting)
        {
            m_SkidmarksActive = false;
            foreach (TrailRenderer skidmarkRenderer2 in m_SkidmarkRenderers)
            {
                skidmarkRenderer2.emitting = false;
            }
        }
        velocity = m_RigidBody.velocity;
        //update Enginesound speedval
        // update Enginesound Pitch speedval

    }
    private void StartDrift()
    {
        isDrifting = true;
        foreach (TrailRenderer skidmarkRenderer in m_SkidmarkRenderers)
        {
            skidmarkRenderer.emitting = true;
        }
        // Drifting Sound
    }
    private void EndDrift()
    {
        isDrifting = false;
        foreach (TrailRenderer skidmarkRenderer in m_SkidmarkRenderers)
        {
            skidmarkRenderer.emitting = false;
        }
        // sound Stop drifting
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(m_RigidBody.centerOfMass + base.transform.position, 0.3f);
        Gizmos.color = (m_IsDrivingForward ? Color.green : Color.red);
        Gizmos.DrawSphere(m_RigidBody.position + m_RigidBody.transform.up * 2.5f, 0.3f);
    }
}