using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInput _playerInput;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
        SubscribeToInput();
        DontDestroyOnLoad(gameObject);
    }


    public static event Action<CallbackContext> OnLook;
    public static event Action<CallbackContext> OnMove;
    public static event Action<CallbackContext> OnZoom;
    public static event Action<CallbackContext> OnMousePos;
    public static event Action<CallbackContext> OnMenu;
    public static event Action<CallbackContext> OnJump;
    public static event Action<CallbackContext> OnBrake;
    public static event Action<CallbackContext> OnAccelerate;
    public static event Action<CallbackContext> OnBoost;
    /// <summary>
    ///  INPUT METHODS
    /// </summary>
  
    private void OnLookInput(CallbackContext context)
    {
        OnLook?.Invoke(context);
    }
    private void OnMoveInput(CallbackContext context)
    {
        OnMove?.Invoke(context);
    }
    private void OnZoomInput(CallbackContext context)
    {
        OnZoom?.Invoke(context);
    }
    private void OnMousePosInput(CallbackContext context)
    {
        OnMousePos?.Invoke(context);
    }
    private void OnMenuInput(CallbackContext context)
    {
        OnMenu?.Invoke(context);
    }
    private void OnJumpInput(CallbackContext context)
    {
        OnJump?.Invoke(context);
    }
    public void OnBrakeInput(CallbackContext context)
    {
        OnBrake?.Invoke(context);
    }
    public void OnAccelerateInput(CallbackContext context)
    {
        OnAccelerate?.Invoke(context);
    }
    public void OnBoostInput(CallbackContext context)
    {
        OnBoost?.Invoke(context);
    }
    private void SubscribeToInput()
    {
        _playerInput.actions["Look"].started += OnLookInput;
        _playerInput.actions["Look"].performed += OnLookInput;
        _playerInput.actions["Look"].canceled += OnLookInput;

        _playerInput.actions["Move"].started += OnMoveInput;
        _playerInput.actions["Move"].performed += OnMoveInput;
        _playerInput.actions["Move"].canceled += OnMoveInput;
        
        _playerInput.actions["Zoom"].started += OnZoomInput;
        _playerInput.actions["Zoom"].performed += OnZoomInput;
        _playerInput.actions["Zoom"].canceled += OnZoomInput;

        _playerInput.actions["MousePos"].started += OnMousePosInput;
        _playerInput.actions["MousePos"].performed += OnMousePosInput;
        _playerInput.actions["MousePos"].canceled += OnMousePosInput;
        
        _playerInput.actions["Menu"].started += OnMenuInput;
        _playerInput.actions["Menu"].performed += OnMenuInput;
        _playerInput.actions["Menu"].canceled += OnMenuInput;

        _playerInput.actions["Jump"].started += OnJumpInput;
        _playerInput.actions["Jump"].performed += OnJumpInput;
        _playerInput.actions["Jump"].canceled += OnJumpInput;
        
        _playerInput.actions["Brake"].started += OnBrakeInput;
        _playerInput.actions["Brake"].performed += OnBrakeInput;
        _playerInput.actions["Brake"].canceled += OnBrakeInput;

        _playerInput.actions["Accelerate"].started += OnAccelerateInput;
        _playerInput.actions["Accelerate"].performed += OnAccelerateInput;
        _playerInput.actions["Accelerate"].canceled += OnAccelerateInput;

        _playerInput.actions["Boost"].started += OnBoostInput;
        _playerInput.actions["Boost"].performed += OnBoostInput;
        _playerInput.actions["Boost"].canceled += OnBoostInput;
    }


    private void UnsubscribeFromInput()
    {
        _playerInput.actions["Look"].started -= OnLookInput;
        _playerInput.actions["Look"].performed -= OnLookInput;
        _playerInput.actions["Look"].canceled -= OnLookInput;

        _playerInput.actions["Move"].started -= OnMoveInput;
        _playerInput.actions["Move"].performed -= OnMoveInput;
        _playerInput.actions["Move"].canceled -= OnMoveInput;
        
                
        _playerInput.actions["Zoom"].started -= OnZoomInput;
        _playerInput.actions["Zoom"].performed -= OnZoomInput;
        _playerInput.actions["Zoom"].canceled -= OnZoomInput;

        _playerInput.actions["MousePos"].started -= OnMousePosInput;
        _playerInput.actions["MousePos"].performed -= OnMousePosInput;
        _playerInput.actions["MousePos"].canceled -= OnMousePosInput;
        
        _playerInput.actions["Menu"].started -= OnMenuInput;
        _playerInput.actions["Menu"].performed -= OnMenuInput;
        _playerInput.actions["Menu"].canceled -= OnMenuInput;

        _playerInput.actions["Jump"].started -= OnJumpInput;
        _playerInput.actions["Jump"].performed -= OnJumpInput;
        _playerInput.actions["Jump"].canceled -= OnJumpInput;

        _playerInput.actions["Brake"].started -= OnBrakeInput;
        _playerInput.actions["Brake"].performed -= OnBrakeInput;
        _playerInput.actions["Brake"].canceled -= OnBrakeInput;

        _playerInput.actions["Accelerate"].started -= OnAccelerateInput;
        _playerInput.actions["Accelerate"].performed -= OnAccelerateInput;
        _playerInput.actions["Accelerate"].canceled -= OnAccelerateInput;

        _playerInput.actions["Boost"].started -= OnBoostInput;
        _playerInput.actions["Boost"].performed -= OnBoostInput;
        _playerInput.actions["Boost"].canceled -= OnBoostInput;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            UnsubscribeFromInput();
        }
    }

}
