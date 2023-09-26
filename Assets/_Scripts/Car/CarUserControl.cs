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
    public float MaxSpeed;
   
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
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        m_car.Move(_moveHorizontalInput, _AccelerateInput, _brakeInput, _handBrakeInput);

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
            _handBrakeInput = context.ReadValue<float>();

        }
        if (context.canceled)
        {

            _handBrakeInput = context.ReadValue<float>();
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

    private void SubscribeToInput()
    {

        InputManager.OnMove += OnMoveInput;
        InputManager.OnLook += OnLookInput;
        InputManager.OnZoom += OnZoomInput;
        InputManager.OnMenu += OnMenuInput;
        InputManager.OnHandbrake += OnHandbrakeInput;
        InputManager.OnBrake += OnBrakeInput;
        InputManager.OnAccelerate += OnAccelerateInput;
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
    }



    #endregion Controls

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeFromInput();

    }
}
