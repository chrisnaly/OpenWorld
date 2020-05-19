using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class CharacterIK : MonoBehaviour
{

    protected Animator animator;
    public Vector3 footIk_offset;
    public CharacterMovement characterMovement;
    private float ik_Weight;
    private float lerp_speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovement.Velocity.magnitude > .1f)
        {
            ik_Weight = Mathf.Lerp(ik_Weight, 0, Time.deltaTime * lerp_speed);
        }
        else
        {
            ik_Weight = Mathf.Lerp(ik_Weight, 1, Time.deltaTime * lerp_speed);
        }
    }

    void OnAnimatorIK()
    {
        Vector3 L_foot = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        Vector3 R_foot = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
        
        L_foot = GetHitPoint(L_foot + Vector3.up, L_foot - Vector3.up * 5) + footIk_offset;
        R_foot = GetHitPoint(R_foot + Vector3.up, R_foot - Vector3.up * 5) + footIk_offset;

        transform.localPosition = new Vector3(0,-Mathf.Abs(L_foot.y - R_foot.y) /2 * ik_Weight, 0);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ik_Weight);
        animator.SetIKPosition(AvatarIKGoal.LeftFoot, L_foot);
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ik_Weight);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, R_foot);
    }

    private Vector3 GetHitPoint(Vector3 start, Vector2 end)
    {
        RaycastHit hit;
        if(Physics.Linecast(start, end, out hit))
        {
            return hit.point;
        }
        return end;
    }
}
