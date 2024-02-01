using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FarmingAreaSatus : MonoBehaviour
{
    private int farmingAreaChunksNumber = 512;
    private int grassChunksNumber = 0;
    private int activeChunkCount = 0;
    [SerializeField] private Transform grassParent;
    [SerializeField] private Transform readyToPlantParent;


    public bool IsGrassCompleted()
    {
        grassChunksNumber = grassParent.childCount;
        if (grassChunksNumber <= 0)
        {
            readyToPlantParent.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPreparedLandCompleted()
    {
        activeChunkCount = 0;
        foreach (Transform child in readyToPlantParent)
        {
            if (child.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                if(meshRenderer.enabled)
                {
                    activeChunkCount++;
                }
            }
        }
        Debug.Log(activeChunkCount);

        if (activeChunkCount >= farmingAreaChunksNumber)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public Transform GetFarmingArea()
    {
        return readyToPlantParent;
    }

}
