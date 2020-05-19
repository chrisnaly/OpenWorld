using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator;
    private Character character;
    private float speed;
    
    
    void Start()
    {
        character = GetComponent<Character>();

        //Time.timeScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator == null)
        {
            Debug.LogWarning("No valid animator!");
            return;
        }

        speed = Mathf.SmoothStep(speed, character.getVelocity(), Time.deltaTime * 20);

        animator.SetFloat("Velocity", speed);
        // Debug.Log(character.getVelocity());
    }

    public void SetMovementMode(MovementMode mode)
    {
        switch(mode)
        {
            case MovementMode.Walking:
            {
                animator.SetInteger("movement state", 0);
                break;
            }
            case MovementMode.Running:
            {
                animator.SetInteger("movement state", 0);
                break;
            }
            case MovementMode.Crouching:
            {
                animator.SetInteger("movement state", 1);
                break;
            }
            case MovementMode.Proning:
            {
                animator.SetInteger("movement state", 2);
                break;
            }
            case MovementMode.Swimming:
            {
                animator.SetInteger("movement state", 3);
                break;
            }
            case MovementMode.Sprinting:
            {
                animator.SetInteger("movement state", 0);
                break;
            }
        }
    }

    internal void Jump()
    {
        animator.SetTrigger("Jump");
    }
}
