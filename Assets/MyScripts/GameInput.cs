using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    Vector2 inputVector = new Vector2(0, 0);

    public Vector2 GetMovementVector()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;

        inputVector = new Vector2 (horizontal, vertical);
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
