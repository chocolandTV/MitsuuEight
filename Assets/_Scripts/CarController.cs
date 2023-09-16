using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
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
    private float steeringAngle = 30f;
    private Camera _mainCamera;
    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _lookInput;
    private float _cameraZoom = 30f;
    // WHEELS 
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private bool braked = false;
    // finds the corresponding visual wheel



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
        _mainCamera = Camera.main;
    }
    private void ApplyLocalPosition(WheelCollider collider , GameObject visualWheel)
    {
        
     
        // Transform visualWheel = collider.transform.GetChild(0);
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        rotation.z = rotation.x;
        rotation.x = 0;
        rotation.y = visualWheel.transform.rotation.y;
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    private void FixedUpdate()
    {
        float motor = maxMotorTorque * _moveInput.y;
        float steering = maxSteeringAngle * _moveInput.x;

        foreach (AxleInfo x in axleInfos)
        {
            if(x.steering){
                x.LeftWheel.steerAngle = steering; // lerpNeed
                x.RightWheel.steerAngle = steering;
            }
            if(x.motor){
                x.LeftWheel.motorTorque = motor;
                x.RightWheel.motorTorque = motor;
            }
            ApplyLocalPosition(x.LeftWheel, x.LeftWheelVisual);
            ApplyLocalPosition(x.RightWheel,x.RightWheelVisual);
        }
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
        if (context.started)
        {
            braked = true;
        }
        if (context.canceled)
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
