using DG.Tweening;
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
            transform.DOLocalMove(Vector3.zero, 0.2f); 
            transform.DOLocalRotate(initialRotation.eulerAngles, 0.2f); 
        }
    }

    public void PlayerUnuseTool(Transform restPosition)
    {
        if (Player.Instance.PlayerHasPushingTool() || Player.Instance.PlayerHasHandTool())
        {
            transform.parent = restPosition;
            
            transform.DOLocalMove(restPosition.localPosition, 0.5f); 
            transform.DOLocalRotate(restPosition.localRotation.eulerAngles, 0.5f); 
        }
    }

    public void PlayerHoldToolInHand(Transform HandPosition)
    {
        if (!Player.Instance.PlayerHasNoTool())
        {
            transform.parent = HandPosition;
            transform.DOLocalMove(Vector3.zero, 0.5f); 
            transform.DOLocalRotate(initialRotation.eulerAngles, 0.5f); 
        }
    }

    public bool PlayerUsingTool()
    {
        return Player.Instance.PlayerHasPushingTool();
    }



}
