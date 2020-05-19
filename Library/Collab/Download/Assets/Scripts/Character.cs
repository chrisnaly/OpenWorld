using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private float forwardInput;
    private float rightInput;
    private Vector3 velocity;
    public CameraController cameraController;
    public CharacterMovement characterMovement;
    public CharacterAnimationController characterAnimation;
    public Interactable focus;
    Camera cam;

    void Start()
    {
        cam= Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StaminaController.Instance.UseStamina(5);

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                RemoveFocus();
            }  
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }    
        }
    }
    void SetFocus (Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus ()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }

    public void AddMovementInput(float forward, float right)
    {
        forwardInput = forward;
        rightInput = right;
        
        Vector3 camFwd = cameraController.transform.forward;
        Vector3 camRight = cameraController.transform.right;

        Vector3 translation = forward*cameraController.transform.forward;
        translation += right*cameraController.transform.right;
        translation.y = 0;

        if(translation.magnitude > 0)
        {
            velocity = translation;
        }
        else
        {
            velocity = Vector3.zero;
        }
        characterMovement.Velocity = translation;
    }

    public float getVelocity()
    {
        return characterMovement.Velocity.magnitude;
    }

    public void ToggleRun()
    {
        if(characterMovement.GetMovementMode() != MovementMode.Running)
        {
            characterMovement.SetMovementMode(MovementMode.Running);
            characterAnimation.SetMovementMode(MovementMode.Running);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            characterAnimation.SetMovementMode(MovementMode.Walking);
        }
    }

    public void ToggleCrouching()
    {
        if(characterMovement.GetMovementMode() != MovementMode.Crouching)
        {
            characterMovement.SetMovementMode(MovementMode.Crouching);
            characterAnimation.SetMovementMode(MovementMode.Crouching);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            characterAnimation.SetMovementMode(MovementMode.Walking);
        }
    }

}
