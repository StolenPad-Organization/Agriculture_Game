using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingTool : MonoBehaviour
{

    private Quaternion initialRotation;
    private Vector3 initialTransform;
    public enum ToolName
    {
        CleaningAndLoosingFarmTool,
        PlantingTool,
        WateringTool
    }

    public ToolName toolName;

    private void Start()
    {
        initialRotation = transform.localRotation;
        initialTransform = transform.localPosition;
    }

    public void PlayerUseTool(Transform playerHoldPoint)
    {
        if (!Player.Instance.PlayerHasNoTool())
        {
            transform.parent = playerHoldPoint;
            transform.localPosition = Vector3.zero;
            transform.localRotation = initialRotation;
        }
    }

    public void PlayerUnuseTool(Transform restPosition)
    {
        if(Player.Instance.PlayerHasPushingTool() || Player.Instance.PlayerHasHandTool())
        {
            transform.parent = restPosition;
            transform.localPosition = restPosition.localPosition;
            transform.localRotation = restPosition.localRotation;
        }
    }

    public void PlayerHoldToolInHand(Transform HandPosition)
    {
        if (!Player.Instance.PlayerHasNoTool())
        {
            transform.parent = HandPosition;
            transform.localPosition = Vector3.zero;
            transform.localRotation = initialRotation;
        }
    }

    public bool PlayerUsingTool()
    {
        return Player.Instance.PlayerHasPushingTool();
    }



}
