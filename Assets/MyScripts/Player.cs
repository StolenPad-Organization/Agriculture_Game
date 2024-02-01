using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] Transform playerHoldPoint;


    private enum State
    {
        Idle,
        Walking,
        Pushing,
        Driving,
    }


    private State state;

    private bool isWalking;
    private bool isPushing;


    void Start()
    {
        state = State.Idle;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Walking:
                HandleMovement();
                break;
            case State.Pushing:
                HandlePushing();
                break;
        }

    }
    private void HandleIdle()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector != Vector2.zero)
        {
            state = State.Walking;
            isWalking = true;
            playerAnimator.SetWalkingState(true);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector == Vector2.zero)
        {
            //player is not moving 
            if (isWalking)
            {
                //player was moving and then stop
                playerAnimator.SetWalkingState(false);
                isWalking = false;
                state = State.Idle;
            }
            return;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        if (!isWalking)
        {

            //player was not moving and then started to move
            playerAnimator.SetWalkingState(true);
            isWalking = true;
            state = State.Walking;
        }

    }


    private void HandlePushing()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector == Vector2.zero)
        {
            
            if (isPushing)
            {
                
                playerAnimator.SetPushingState(true);
                playerAnimator.SetPushSpeed(0);
                isPushing = false;
            }
            return;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        if (!isPushing)
        {
            playerAnimator.SetWalkingState(true);
            playerAnimator.SetPushSpeed(1);
            isPushing = true;
        }
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.TryGetComponent(out FarmingTool farmingTool))
    //    {
    //        farmingTool.PlayerUseTool(playerHoldPoint);
    //        playerAnimator.SetPushingState(true);
    //        state = State.Pushing;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out FarmingTool farmingTool))
        {
            farmingTool.PlayerUseTool(playerHoldPoint);
            playerAnimator.SetPushingState(true);
            state = State.Pushing;
        }
    }



    public bool IsWalking()
    {
        return isWalking;
    }

  
}
