using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingTool : MonoBehaviour
{


    public void PlayerUseTool(Transform playerHoldPoint)
    {
        transform.parent = playerHoldPoint;
        transform.localPosition = Vector3.zero;
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y = 180f;
        transform.localEulerAngles = newRotation;

    }
}
