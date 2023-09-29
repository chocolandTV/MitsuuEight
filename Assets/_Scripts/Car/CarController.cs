using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody m_RigidBody;
    [SerializeField] private List<ParticleSystem> m_DriftParticles;
    private ParticleSystem.MainModule _DriftPsMain01, _DriftPsMain02;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;
    [SerializeField] private ParticleSystem[] m_BoostParticleObjects = new ParticleSystem[2];
    [Header("Car Settings")]
    public float CurrentSpeed, maxSteerAngle;
    [SerializeField] private float maxDriftAngle = 20f;
    [SerializeField] private Color drift1, drift2, drift3;
    [SerializeField] public float m_MaxSpeed, m_MaxBoostSpeed;
    [SerializeField] private float m_BoostPower, m_driftMinSpeed;
    private Vector3 m_StartPos;
    private bool m_IsDrivingEnabled, touchingGround;
    private float realSpeed;
    public float Velocity { get { return m_RigidBody.velocity.magnitude * 3.6f; } }
    public float CarEnergyCapacity { get; private set; }
    private bool isDriftingLeft = false;
    private bool isDriftingRight = false;
    private bool isSliding = false;
    private float outwardsDirftForce = 50000;

    private float _currentSteerAngle, _driftTime, car_BoostTime, car_BoostPower;

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
        _DriftPsMain01 = m_DriftParticles[0].main;
        _DriftPsMain02 = m_DriftParticles[1].main;
        car_BoostTime = 0f;
        CurrentSpeed = 0f;
        car_BoostPower = 1f;

    }
    public void ResetPosition()
    {
        gameObject.transform.position = m_StartPos;
        // EFFECT
    }
    public void Move(float _moveHorizontalInput, float _AccelerateInput, float _brakeInput, bool _boostInput, bool _isJumping)
    {
        isSliding = _isJumping;
        if (!m_IsDrivingEnabled)
        {
            return;
        }
        // ACCELERATE
        realSpeed = transform.InverseTransformDirection(m_RigidBody.velocity).z;
        Debug.Log(realSpeed);
        if (_AccelerateInput > 0)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, m_MaxSpeed*_AccelerateInput, Time.fixedDeltaTime * 0.5f);

        }
        else if (_brakeInput > 0)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, -m_MaxSpeed / 1.75f, 1f * Time.fixedDeltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.fixedDeltaTime * 1.5f);
        }
        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = m_RigidBody.velocity.y;
        m_RigidBody.velocity = vel;
        // STEERING
        _currentSteerAngle = maxSteerAngle * _moveHorizontalInput;
        frontLeftWheelTransform.localEulerAngles = new Vector3(0, 180 + _currentSteerAngle, 0);
        frontRightWheelTransform.localEulerAngles = new Vector3(0, 180 + _currentSteerAngle, 0);
        // Visuals
        UpdateSingleWheel(frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelTransform);

        // RealSteering
        float _steerDirection = _moveHorizontalInput;
        Vector3 _steerDirVector;
        float _steerAmount;

        if (isDriftingLeft && !isDriftingRight)
        {
            _steerDirection = _moveHorizontalInput < 0 ? -1.5f : -0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, -maxDriftAngle, 0), 8f * Time.fixedDeltaTime);

            if (isSliding && touchingGround)
                m_RigidBody.AddForce(transform.right * outwardsDirftForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        if (isDriftingRight && !isDriftingLeft)
        {
            _steerDirection = _moveHorizontalInput > 0 ? 1.5f : 0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, maxDriftAngle, 0), 8f * Time.fixedDeltaTime);

            if (isSliding && touchingGround)
                m_RigidBody.AddForce(transform.right * -outwardsDirftForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        }
        else
        {
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0f, 0f, 0f), 8f * Time.fixedDeltaTime);
        }

        _steerAmount = realSpeed > 30 ? realSpeed / 4 * _steerDirection : _steerAmount = realSpeed / 1.5f * _steerDirection;

        _steerDirVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + _steerAmount, transform.eulerAngles.z);
        // This is steering 
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _steerDirVector, 3 * Time.fixedDeltaTime);
        // GroundnormalRotation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.75f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.fixedDeltaTime);
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
        // DRIFT 

        if (_moveHorizontalInput != 0 && touchingGround && CurrentSpeed > m_driftMinSpeed && _isJumping)
        {
            _driftTime += Time.fixedDeltaTime;

            if (_driftTime >= 1.5f && _driftTime < 4)
            {
                _DriftPsMain01.startColor = drift1;
                _DriftPsMain02.startColor = drift1;

            }
            if (_driftTime >= 4f && _driftTime < 7)
            {
                _DriftPsMain01.startColor = drift2;
                _DriftPsMain02.startColor = drift2;

            }
            if (_driftTime > 7)
            {
                _DriftPsMain01.startColor = drift3;
                _DriftPsMain02.startColor = drift3;

            }
        }
        // RESET

        if (!_isJumping || realSpeed < m_driftMinSpeed)
        {
            isDriftingLeft = false; isDriftingRight = false; isSliding = false;
            // BoostIncrease
            car_BoostTime = Mathf.Clamp(_driftTime / 3, 1, 2.5f);
            _driftTime = 0f;
            DriftParticle(0, false, Color.yellow);
            DriftParticle(1, false, Color.yellow);

        }
        // BOOST
        car_BoostTime -= Time.fixedDeltaTime;
        if (car_BoostTime > 0)
        {
            BoostEffect();
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, m_MaxSpeed * m_BoostPower, 1 * Time.fixedDeltaTime);
        }
        if (_boostInput && CarEnergyCapacity > 20 * Time.fixedDeltaTime)
        {
            Debug.Log("Boosting - Capacity: " + CarEnergyCapacity);
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, m_MaxSpeed * m_BoostPower, 1 * Time.fixedDeltaTime);
            AddBoostCapacity(-(20 * Time.fixedDeltaTime));
            BoostEffect();
        }

    }
    private void UpdateSingleWheel(Transform wheelTransform)
    {
        wheelTransform.Rotate(0, 0, -90 * Time.fixedDeltaTime * realSpeed * 0.5f);
    }
    public void StartDrift(float _moveHorizontalInput)
    {
        if (touchingGround)
        {
            // transform.GetChild(0).GetComponent<Animator>.SetTrigger("Hop");
            if (_moveHorizontalInput > 0)
            {
                isDriftingRight = true;
                DriftParticle(1, true, Color.yellow);
                isDriftingLeft = false;
            }
            if (_moveHorizontalInput < 0)
            {
                isDriftingLeft = true;
                DriftParticle(0, true, Color.yellow);
                isDriftingRight = false;
            }
        }
    }
    public void DriftParticle(int side, bool value, Color color)
    {
        _DriftPsMain01.startColor = color;
        _DriftPsMain02.startColor = color;
        if (value)
            m_DriftParticles[side].Play();
        if (!value)
            m_DriftParticles[side].Stop();

    }

    public void AddBoostCapacity(float amount)
    {
        CarEnergyCapacity += amount;
        if (CarEnergyCapacity > 100.0f)
        {
            CarEnergyCapacity = 100f;
        }
        if (CarEnergyCapacity < 0f)
        {
            CarEnergyCapacity = 0f;
        }
    }
    public void SetBoostCapacity(float amount)
    {
        CarEnergyCapacity = amount;
    }
    public void ResetBoost()
    {
        CarEnergyCapacity = 0f;
    }
    public void BoostEffect()
    {
        m_BoostParticleObjects[0].Emit((int)m_BoostPower * 5);
        m_BoostParticleObjects[1].Emit((int)m_BoostPower * 5);
    }

}