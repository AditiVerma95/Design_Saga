using System;
using UnityEngine;


public class DoorWorking : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool playerInRange = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        UserInputManager.Instance.OpenCloseEvent += TriggerDoor;
    }

    private void Update()
    {
        
    }

    private void TriggerDoor(object sender, EventArgs e) {
        if (!playerInRange) {
            return;
        }

        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
    
}