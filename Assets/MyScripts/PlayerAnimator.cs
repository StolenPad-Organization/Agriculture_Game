using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";
    private const string IS_PUSHING = "IsPushing";
    private const string PUSH_SPEED = "PushSpeed";

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    


    public void SetWalkingState(bool isWalking)
    {
        animator.SetBool(IS_WALKING, isWalking);
        
    }

    public void SetPushingState(bool isPushing)
    {
        animator.SetBool(IS_PUSHING, isPushing);
    }

    public void SetPushSpeed(int speedAmount)
    {
        animator.SetFloat(PUSH_SPEED, speedAmount);
    }


}
