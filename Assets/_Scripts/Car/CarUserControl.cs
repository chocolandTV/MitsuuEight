using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarUserControl : MonoBehaviour
{
    private Vector2 _moveHorizontalInput, _lookInput;
    private float CameraZoom, _brakeInput, _AccelerateInput, _handBrakeInput;
    private CarController m_car;
    public static CarUserControl Instance;
    public float MaxSpeed { get { return m_car.m_MaxSpeed; } }
    public float Current_KMH { get { return m_car.Velocity.magnitude * 3.6f; } }
    public float Current_Boost { get { return m_car.BoostCapacity; } }

    private bool _isBoosting;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        m_car = GetComponent<CarController>();

        SubscribeToInput();
        m_car.EnableDriving();
        m_car.SetBoostCapacity(100);
    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        m_car.Move(_moveHorizontalInput, _AccelerateInput, _brakeInput, _handBrakeInput, _isBoosting);


    }
    #region Controls
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveHorizontalInput = context.ReadValue<Vector2>();

        }

    }
    private void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookInput = context.ReadValue<Vector2>();

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
            m_car.StartDrift();


        }
        if (context.canceled)
        {

            m_car.EndDrift();
        }
    }
    private void OnBrakeInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _brakeInput = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            _brakeInput = 0f;
        }
    }
    private void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _AccelerateInput = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            _AccelerateInput = 0f;
        }
    }
    private void OnboostInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isBoosting = true;
        }
        if (context.canceled)
        {
            _isBoosting = false;
        }
    }
    private void SubscribeToInput()
    {

        InputManager.OnMove += OnMoveInput;
        InputManager.OnLook += OnLookInput;
        InputManager.OnZoom += OnZoomInput;
        InputManager.OnMenu += OnMenuInput;
        InputManager.OnHandbrake += OnHandbrakeInput;
        InputManager.OnBrake += OnBrakeInput;
        InputManager.OnAccelerate += OnAccelerateInput;
        InputManager.OnBoost += OnboostInput;
    }

    private void UnsubscribeFromInput()
    {

        InputManager.OnMove -= OnMoveInput;
        InputManager.OnLook -= OnLookInput;
        InputManager.OnZoom -= OnZoomInput;
        InputManager.OnMenu -= OnMenuInput;
        InputManager.OnHandbrake -= OnHandbrakeInput;
        InputManager.OnBrake -= OnBrakeInput;
        InputManager.OnAccelerate -= OnAccelerateInput;
        InputManager.OnBoost -= OnboostInput;
    }



    #endregion Controls

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();

    }
}
