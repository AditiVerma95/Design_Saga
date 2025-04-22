using System;
using UnityEngine;


public class DoorWorking : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                animator.SetBool("isOpen", false);
                isOpen = false;
            }
            else
            {
                animator.SetBool("isOpen", true);
                isOpen = true;
            }
        }
    }
}