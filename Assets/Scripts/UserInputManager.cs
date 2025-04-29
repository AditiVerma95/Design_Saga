using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class UserInputManager : MonoBehaviour {
    public UserInputActionAsset userInputActionAsset;

    public Vector2 moveInput;
    public Vector2 lookInput;
    public static UserInputManager Instance;

    public event EventHandler spawnEvent;
    public event EventHandler enableDisabled;
   
    
    private void Awake() {
        userInputActionAsset = new UserInputActionAsset();    
    }

    private void Start() {
        Instance = this;
    }

    private void OnEnable() {
        userInputActionAsset.Ghost.Enable();
        userInputActionAsset.Ghost.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        userInputActionAsset.Ghost.Move.canceled += context => moveInput = Vector2.zero;
        userInputActionAsset.Ghost.Look.performed += context => lookInput = context.ReadValue<Vector2>();
        userInputActionAsset.Ghost.Look.canceled += context => lookInput = Vector2.zero;
        userInputActionAsset.Ghost.Spawn.performed += context => spawnEvent?.Invoke(this, EventArgs.Empty);
        
        userInputActionAsset.UI.Enable();
        userInputActionAsset.UI.EnableDisable.performed += context => enableDisabled?.Invoke(this, EventArgs.Empty);
    }

    

    private void OnDisable() {
        userInputActionAsset.Ghost.Disable();
        
    }
    
    private void Update() {
       
        
    }
}
