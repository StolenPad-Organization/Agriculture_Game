using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingTool : MonoBehaviour
{

    private Quaternion initialRotation;
    private Vector3 initialTransform;
    


    private void Start()
    {
        initialRotation = transform.localRotation;
        initialTransform = transform.localPosition;
    }

    public void PlayerUseTool(Transform playerHoldPoint)
    {
        if (!Player.Instance.PlayerHasTool())
        {
            transform.parent = playerHoldPoint;
            transform.localPosition = Vector3.zero;
            transform.localRotation = initialRotation;
        }
    }

    public void PlayerUnuseTool(Transform restPosition)
    {
        if(Player.Instance.PlayerHasTool())
        {
            transform.parent = restPosition;
            transform.localPosition = restPosition.localPosition;
            transform.localRotation = restPosition.localRotation;
        }
    }

    public bool PlayerUsingTool()
    {
        return Player.Instance.PlayerHasTool();
    }



}
