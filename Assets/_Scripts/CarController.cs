using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public static CarController Instance { get; private set; }
    private Vector2 MousePosInput;
    private float steeringAngle;
    private Camera _mainCamera;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _cameraZoom = 30f;
    // WHEELS 
    
    
    [SerializeField] private WheelCollider frontWheelLeft,frontWheelRight, rearWheelLeft,rearWheelRight;
    [SerializeField]private Transform frontObjectLeft,frontObjectRight, rearObjectLeft,rearObjectRight;
    public Vector3 eulerTest;
    float maxFwdSpeed  = -3000;
    float maxBwdSpeed = 1000f;
    float gravity  = 9.8f;
    private bool braked = false;
    private float  maxBrakeTorque =500; 
    private Rigidbody rb;
    public Transform centreofmass;
    private float maxTorque = 1000;
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
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass  = centreofmass.transform.localPosition;
    }
    // Update is called once per frame
    private void brakeSteering()
    {
        if(!braked)
        {
            frontWheelLeft.brakeTorque  = 0;
            frontWheelRight.brakeTorque  = 0;
            rearWheelLeft.brakeTorque = 0;
            rearWheelRight.brakeTorque  = 0;
        }
        // Speed of car, car will move as you will provide the input to it.
        rearWheelRight.motorTorque = maxTorque * _moveInput.y;
        rearWheelLeft.motorTorque = maxTorque  * _moveInput.y;

        // Changing car direction
        frontWheelLeft.steerAngle = 30 * ( _moveInput.x);
        frontWheelRight.steerAngle = 30 * _moveInput.x;
    }
    private void HandBrake()
    {
        if(braked)
        {
            rearWheelLeft.brakeTorque = maxBrakeTorque *20;
            rearWheelRight.brakeTorque  = maxBrakeTorque * 20;

            rearWheelLeft.motorTorque = 0;
            rearWheelRight.motorTorque = 0;
        }
    }
    private void RealTireUpdate()
    {
        HandBrake();
        frontObjectLeft.Rotate(frontWheelLeft.rpm/60*360*Time.deltaTime , 0 ,0);
        frontObjectRight.Rotate(frontWheelRight.rpm/60*360*Time.deltaTime , 0 ,0);
        rearObjectLeft.Rotate(rearWheelLeft.rpm/60*360*Time.deltaTime , 0 ,0);
        rearObjectRight.Rotate(rearWheelRight.rpm/60*360*Time.deltaTime , 0 ,0);
        // STEERING UPDATE
        Vector3 temp = frontObjectLeft.localEulerAngles;
        Vector3 temp1 =  frontObjectRight.localEulerAngles;

        temp.y = frontWheelLeft.steerAngle - ( frontObjectLeft.localEulerAngles.z);
        frontObjectLeft.localEulerAngles = temp;

        temp1.y =  frontWheelRight.steerAngle - (frontObjectRight.localEulerAngles.z);
        frontObjectRight.localEulerAngles = temp1;

        eulerTest = frontObjectLeft.localEulerAngles;
    }
    void FixedUpdate()
    {
        brakeSteering();

    }
    private void Update() {
        RealTireUpdate();
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
            Debug.Log(context.ReadValue<float>());
            _cameraZoom = context.ReadValue<float>();
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
        if(context.started)
        {
            braked = true;
        }
        if(context.canceled)
        {
            braked = false;
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
