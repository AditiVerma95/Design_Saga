using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputManager : MonoBehaviour {
    private UserInputActionAsset userInputActionAsset;

    public Vector2 moveInput;
    public Vector2 lookInput;

    public static UserInputManager Instance;

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
    }

    

    private void OnDisable() {
        userInputActionAsset.Ghost.Disable();
        
    }

    private void Update() {
        Debug.Log(lookInput);
        
    }
}
