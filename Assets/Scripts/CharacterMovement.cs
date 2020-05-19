using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode {Walking, Running, Crouching, Proning, Swimming, Sprinting};

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public Transform t_mesh;
    public float maxSpeed = 0.1f;
    public float walkSpeed = 2f;
    public float runSpeed = 6.7f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 3.3f;
    public float proneSpeed = 1f;
    public float swimSpeed = 1f;
    public float jump_force = 1000;
    private float smoothSpeed;
    private float rotationSpeed = 10;
    private Rigidbody rigidbody;
    private MovementMode movementMode;
    private Vector3 velocity;
    private Vector3 actualVelocity;
    private Vector3 charPos;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();  // intialize rigidbody reference
        SetMovementMode(MovementMode.Walking);
        charPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(velocity.normalized*maxSpeed);

        actualVelocity = Vector3.Lerp(actualVelocity, (transform.position - charPos) / Time.deltaTime, Time.deltaTime *10);
        charPos = transform.position;

        if(velocity.magnitude > 0)
        {
            rigidbody.velocity = new Vector3 (velocity.normalized.x * smoothSpeed, rigidbody.velocity.y, velocity.normalized.z * smoothSpeed);
            smoothSpeed = Mathf.Lerp(smoothSpeed, maxSpeed, Time.deltaTime);
            //t_mesh.rotation = Quaternion.LookRotation(velocity);
            t_mesh.rotation = Quaternion.Lerp(t_mesh.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);
        }
        else
        {
            smoothSpeed = Mathf.Lerp(smoothSpeed, 0, Time.deltaTime*4);
        }
    }

    internal void Jump()
    {
        rigidbody.AddForce(Vector3.up * jump_force);
    }

    public Vector3 Velocity {get => actualVelocity; set => velocity = value;}
    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;
        switch(mode)
        {
            case MovementMode.Walking:
            {
                maxSpeed = walkSpeed;
                break;
            }
            case MovementMode.Running:
            {
                maxSpeed = runSpeed;
                break;
            }
            case MovementMode.Crouching:
            {
                maxSpeed = crouchSpeed;
                break;
            }
            case MovementMode.Proning:
            {
                maxSpeed = proneSpeed;
                break;
            }
            case MovementMode.Swimming:
            {
                maxSpeed = swimSpeed;
                break;
            }
            case MovementMode.Sprinting:
            {
                maxSpeed = sprintSpeed;
                break;
            }
        }
    }

    public MovementMode GetMovementMode()
    {
        return movementMode;
    }

}
