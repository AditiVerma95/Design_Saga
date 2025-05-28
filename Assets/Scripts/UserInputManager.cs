using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class UserInputManager : MonoBehaviour {
    public UserInputActionAsset userInputActionAsset;

    public static UserInputManager Instance;
    
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool isSprinting;  
   
    public event EventHandler removeEvent;
    public event EventHandler spawnEvent;
    
    public event EventHandler enableDisabled;
    public event EventHandler OpenCloseEvent;
    
    public event EventHandler ApplyMaterialToObject;
    
    private void Awake() {
        Instance = this;
        userInputActionAsset = new UserInputActionAsset();    
        
    }

    private void Start() {
        
    }

    private void OnEnable() {
        userInputActionAsset.Ghost.Enable();

        userInputActionAsset.Ghost.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        userInputActionAsset.Ghost.Move.canceled += context => moveInput = Vector2.zero;

        userInputActionAsset.Ghost.Look.performed += context => lookInput = context.ReadValue<Vector2>();
        userInputActionAsset.Ghost.Look.canceled += context => lookInput = Vector2.zero;

        userInputActionAsset.Ghost.LeftClick.performed += LeftClickOnperformed;
        userInputActionAsset.Ghost.RightClick.performed += context => removeEvent?.Invoke(this, EventArgs.Empty);
        
        userInputActionAsset.Ghost.OpenClose.performed += context => OpenCloseEvent?.Invoke(this, EventArgs.Empty);
        
        userInputActionAsset.Ghost.Sprint.performed += OnSprint;
        userInputActionAsset.Ghost.Sprint.canceled += OnSprint;

        userInputActionAsset.UI.Enable();
        userInputActionAsset.UI.EnableDisable.performed += context => enableDisabled?.Invoke(this, EventArgs.Empty);
       
    }

    private void LeftClickOnperformed(InputAction.CallbackContext obj) {
        spawnEvent?.Invoke(this, EventArgs.Empty);
        ApplyMaterialToObject?.Invoke(this, EventArgs.Empty);
    }


    private void OnDisable() {
        userInputActionAsset.Ghost.Disable();
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            isSprinting = true;
        else if (context.canceled)
            isSprinting = false;
    }
}
