using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


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

    [SerializeField]
    private ParticleSystem[] m_BoostParticleObjects = new ParticleSystem[2];
    [Header("Normal Acceleration /Turn")]
    [SerializeField]
    private float m_Acceleration;

    public float m_MaxSpeed;

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
    [SerializeField]
    private float m_BoostPower;
    private bool m_IsDrivingForward;
    private Vector3 m_StartPos;
    private bool m_IsDrivingEnabled;

    private bool m_SkidmarksActive;
    public Vector2 Velocity { get; private set; }

    public float DriftVal { get; private set; }

    public float SpeedVal { get; private set; }
    public float BoostCapacity { get; private set; }
    private bool isDrifting = false;
    public void DisableDriving()
    {
        m_IsDrivingEnabled = false;
    }
    public void EnableDriving()
    {
        m_IsDrivingEnabled = true;
        // Sound engine start;
    }
    private void Start()
    {
        m_StartPos = gameObject.transform.position;
    }
    public void ResetPosition()
    {
        gameObject.transform.position = m_StartPos;
        // EFFECT
    }
    public void Move(Vector2 _moveHorizontalInput, float _AccelerateInput, float _brakeInput, float _handBrakeInput, bool _isBoosting)
    {

        if (!m_IsDrivingEnabled)
        {
            return;
        }
        if (m_Suspension.isGrounded)
        {
            Vector3 vector = gameObject.transform.forward;
            if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out var hitInfo, 100f))
            {
                Debug.DrawLine(gameObject.transform.position, hitInfo.point, Color.cyan);
                vector = Quaternion.AngleAxis(Vector3.SignedAngle(gameObject.transform.up, hitInfo.normal, gameObject.transform.right), gameObject.transform.right) * gameObject.transform.forward;
                Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + vector * 50f, Color.green);
            }
            if (_AccelerateInput > 0f)
            {
                float num = (isDrifting ? m_DriftAcc : m_Acceleration);
                float num_Boost = 1;

                if (_isBoosting && BoostCapacity > 20 * Time.fixedDeltaTime)
                {
                    Debug.Log("Boosting - Capacity: " + BoostCapacity);
                    num_Boost = m_BoostPower;
                    AddBoostCapacity(-(20 * Time.fixedDeltaTime));
                    BoostEffect();
                }
                m_RigidBody.AddForce(_AccelerateInput * num_Boost * num * vector, ForceMode.Acceleration);
            }
            if (_brakeInput > 0f)
            {
                float num2 = (isDrifting ? m_DriftAcc : m_Acceleration);

                m_RigidBody.AddForce(_brakeInput * num2 * -vector, ForceMode.Acceleration);
            }
            m_IsDrivingForward = Vector3.Angle(m_RigidBody.velocity, vector) < Vector3.Angle(m_RigidBody.velocity, -vector);
            float num3 = (isDrifting ? m_MaxDriftSpeed : m_MaxSpeed);
            m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, num3);
            SpeedVal = Mathf.InverseLerp(0f, num3, m_RigidBody.velocity.magnitude);
            if (m_Suspension.isGrounded)
            {
                // SUS IS GROUNDED
                _moveHorizontalInput *= SpeedVal;

                if (!m_IsDrivingForward)
                {
                    _moveHorizontalInput = -_moveHorizontalInput;
                }
                float num4 = (isDrifting ? m_DriftTurnForce : m_TurnForce);
                m_RigidBody.AddTorque(gameObject.transform.up * _moveHorizontalInput.x * num4, ForceMode.Acceleration);
                float num5 = (isDrifting ? m_MaxDriftAngularVelocity : m_MaxAngularVelocity);
                m_RigidBody.angularVelocity = new Vector3(m_RigidBody.angularVelocity.x, num5 * _moveHorizontalInput.x, m_RigidBody.angularVelocity.z);
                // END SUS
            }
            float value = Vector3.SignedAngle(m_RigidBody.velocity, gameObject.transform.forward, Vector3.up);
            DriftVal = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-80f, 80f, value));
            if (m_RigidBody.velocity.magnitude > m_MinVelForDrift)
            {
                m_RigidBody.AddForce(gameObject.transform.right * DriftVal * (isDrifting ? m_DriftGrip : m_Grip), ForceMode.Acceleration);

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
        Velocity = m_RigidBody.velocity;
        //update Enginesound speedval
        // update Enginesound Pitch speedval

    }
    public void StartDrift()
    {
        isDrifting = true;
        foreach (TrailRenderer skidmarkRenderer in m_SkidmarkRenderers)
        {
            skidmarkRenderer.emitting = true;
        }
        // Drifting Sound
    }
    public void EndDrift()
    {
        isDrifting = false;
        foreach (TrailRenderer skidmarkRenderer in m_SkidmarkRenderers)
        {
            skidmarkRenderer.emitting = false;
        }
        // sound Stop drifting
    }
    public void AddBoostCapacity(float amount)
    {
        BoostCapacity += amount;
        if (BoostCapacity > 100.0f)
        {
            BoostCapacity = 100f;
        }
        if (BoostCapacity < 0f)
        {
            BoostCapacity = 0f;
        }
    }
    public void SetBoostCapacity(float amount)
    {
        BoostCapacity = amount;
    }
    public void ResetBoost()
    {
        BoostCapacity = 0f;
    }
    public void BoostEffect()
    {
        m_BoostParticleObjects[0].Emit((int)m_BoostPower * 5);
        m_BoostParticleObjects[1].Emit((int)m_BoostPower * 5);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(m_RigidBody.centerOfMass + gameObject.transform.position, 0.3f);
        Gizmos.color = (m_IsDrivingForward ? Color.green : Color.red);
        Gizmos.DrawSphere(m_RigidBody.position + m_RigidBody.transform.up * 2.5f, 0.3f);
    }
}