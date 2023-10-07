using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarUserControl : MonoBehaviour
{
    private Vector2 _moveHorizontalInput, _lookInput;
    private float CameraZoom, _brakeInput, _AccelerateInput;
    private bool  _isBoosting, _isJumping;
    private CarController m_car;
    public static CarUserControl Instance;
    public float MaxSpeed { get { return m_car.m_MaxSpeed; } }
    public float Current_KMH { get { return m_car.Velocity; } }
    public float Current_Life { get { return m_car.CarLife; } }
    public float Current_Nitro { get { return m_car.CarNitro; } }
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
        m_car.Move(_moveHorizontalInput.normalized.x, _AccelerateInput, _brakeInput, _isBoosting, _isJumping);


    }
    #region Controls
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveHorizontalInput = context.ReadValue<Vector2>();

        }
        if(context.canceled)
        {
            _moveHorizontalInput = Vector2.zero;
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
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
           
            _isJumping = true;
            m_car.StartDrift(_moveHorizontalInput.normalized.x);

        }
        if (context.canceled)
        {

            _isJumping = false;
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
        InputManager.OnJump += OnJumpInput;
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
        InputManager.OnJump -= OnJumpInput;
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
