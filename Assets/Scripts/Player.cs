using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 10f;



    private bool isWalking;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        Vector2 inputVector = gameInput.GetMovementVector();
        if (inputVector == Vector2.zero)
        {
            if (isWalking)
            {
                playerAnimator.SetAnimatorSate(false);
                isWalking = false;
            }
            return;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        if (!isWalking)
        {
            playerAnimator.SetAnimatorSate(true);
            isWalking = true;
        }

    }


    public bool IsWalking()
    {
        return isWalking;
    }


}
