using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetAnimatorSate(bool isWalking)
    {
        Debug.Log("SetAnimatorSate");
        animator.SetBool(IS_WALKING, isWalking);
    }
}
