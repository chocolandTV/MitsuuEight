using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider LeftWheel;
    public WheelCollider RightWheel;
    public GameObject LeftWheelVisual;
    public GameObject RightWheelVisual;
    public bool motor;
    public bool steering;
}
public class CarController : MonoBehaviour
{

    public List<AxleInfo> axleInfos;
    public static CarController Instance { get; private set; }
    private Vector2 MousePosInput;
    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _lookInput;
    private float _brakeInput;
    private bool isBraking;
    private Rigidbody m_Rigidbody;
    private float AccelInput;
   
    [Range(100, 300)][SerializeField] private float m_CarAccelleration; // car different  accelleration
    [Range(0, 1)][SerializeField] private float m_ChocoGrip; // 0 is raw physics , 1 the car will grip in the direction it is facing
    [Range(0, 1)][SerializeField] private float m_TractionControl; // 0 is no traction control, 1 is full interference
    [SerializeField] private float m_Topspeed = 200;

    [SerializeField] private float m_MaxHandbrakeTorque;
    public float HUD_MaxSpeed { get { return m_Topspeed; } }
    public float HUD_current_KMH { get { return m_Rigidbody.velocity.magnitude; } }
    public float HUD_current_RPM { get { return m_CurrentTorque / maxMotorTorque; } }
    private float m_OldRotation;
    private float m_CurrentTorque;
    private float m_CarDriveType = 0;
    // WHEELS 
    public float maxMotorTorque;
    public float maxSteeringAngle;

    // Public Vars:
    public float CameraZoom { get; private set; }
    /*
        PROBLEMS:  DRIFTTIME ++++
                   SLOW ACCELLERATION
                   LOW GRIP
    */
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SubscribeToInput();
    }
    private void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
        m_CurrentTorque = 500;
        isBraking = false;
        foreach (AxleInfo x in axleInfos)
        {

            if (x.motor)
                m_CarDriveType++;
        }
    }
    private void ApplyLocalPosition(WheelCollider collider, GameObject visualWheel)
    {


        // Transform visualWheel = collider.transform.GetChild(0);
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    private void ChocoGrip()
    {
        foreach (AxleInfo m_WheelCollider in axleInfos)
        {

            m_WheelCollider.LeftWheel.GetGroundHit(out WheelHit wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return; // wheels arent on the ground so dont realign the rigidbody velocity
            m_WheelCollider.RightWheel.GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return;

        }
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_ChocoGrip;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
        }
        m_OldRotation = transform.eulerAngles.y;
    }
    private void CarHandbreak()
    {
        axleInfos[0].LeftWheel.brakeTorque = m_MaxHandbrakeTorque;
        axleInfos[0].RightWheel.brakeTorque = m_MaxHandbrakeTorque;

        axleInfos[1].LeftWheel.brakeTorque = m_MaxHandbrakeTorque;
        axleInfos[1].RightWheel.brakeTorque = m_MaxHandbrakeTorque;
    }
    private void AdjustTorque()
    {
        if (AccelInput == 1)
            m_CurrentTorque += m_CarAccelleration;

        if (AccelInput == -1)
            m_CurrentTorque -= m_CarAccelleration;
    }
    public void Move(float car_horizontal, float car_vertical)
    {
        //clamp input values
        car_horizontal = Mathf.Clamp(car_horizontal, -1, 1);
        // BrakeCheck
        _brakeInput =  Mathf.Clamp(car_vertical, -1,0);
        AccelInput = Mathf.Clamp(car_vertical, 0, 1);


        if (_brakeInput == -1 && !isBraking)
        {
            CarBrakeLight.Instance.CarBrakeVisualEffect();
            isBraking = true;
            CarHandbreak();
        }
        if (_brakeInput == 0 && isBraking)
        {
            CarBrakeLight.Instance.CarBrakeVisualEffectOff();
            isBraking = false;
        }

        
        float steering = maxSteeringAngle * car_horizontal;
        // STEERHELPER
        ChocoGrip();
        // accel
        float thrustTorque = AccelInput * (m_CurrentTorque / m_CarDriveType);
        Debug.Log(thrustTorque);
        float speed = m_Rigidbody.velocity.sqrMagnitude;

        foreach (AxleInfo x in axleInfos)
        {
            if (x.steering)
            {
                x.LeftWheel.steerAngle = steering;
                x.RightWheel.steerAngle = steering;
            }
            if (x.motor)
            {

                if (speed < m_Topspeed)
                {
                    x.LeftWheel.motorTorque = thrustTorque;
                    x.RightWheel.motorTorque = thrustTorque;
                }

                //Accelleration 
                AdjustTorque();


            }
            ApplyLocalPosition(x.LeftWheel, x.LeftWheelVisual);
            ApplyLocalPosition(x.RightWheel, x.RightWheelVisual);
        }

    }
    private void FixedUpdate()
    {
        Move(_moveInput.x, _moveInput.y);

    }
    #region Controls
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();

        }

    }
    private void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookInput = context.ReadValue<Vector2>();
        }
    }
    private void OnMousePosInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MousePosInput = context.ReadValue<Vector2>();

        }
    }
    private void OnZoomInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CameraZoom = context.ReadValue<float>();
            Debug.Log(CameraZoom);
        }
    }
    private void OnMenuInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // OPEN / CLOSE  MENU 
        }
    }
    private void OnHandbrakeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _brakeInput = context.ReadValue<float>();
            CarHandbreak();
        }
        if (context.canceled)
        {

            _brakeInput = context.ReadValue<float>();
        }
    }

    private void SubscribeToInput()
    {
        InputManager.OnMousePos += OnMousePosInput;
        InputManager.OnMove += OnMoveInput;
        InputManager.OnLook += OnLookInput;
        InputManager.OnZoom += OnZoomInput;
        InputManager.OnMenu += OnMenuInput;
        InputManager.OnHandbrake += OnHandbrakeInput;
    }

    private void UnsubscribeFromInput()
    {
        InputManager.OnMousePos -= OnMousePosInput;
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnLook -= OnLookInput;
        InputManager.OnZoom -= OnZoomInput;
        InputManager.OnMenu -= OnMenuInput;
        InputManager.OnHandbrake -= OnHandbrakeInput;
    }



    #endregion Controls

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();

    }
}
