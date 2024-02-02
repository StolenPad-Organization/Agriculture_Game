using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FarmingAreaSatus : MonoBehaviour
{
    [SerializeField] private int farmingAreaChunksNumber = 512;
    private int grassChunksNumber = 0;
    private int activeChunkCount = 0;
    private int plantsNumber = 0;
    [SerializeField] private Transform grassParent;
    [SerializeField] private Transform readyToPlantParent;




    private void Update()
    {
        //IsGrassCompleted();
        //IsPreparedLandCompleted();
    }


    public bool IsCleaningCompleted()
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

    public bool IsLoosingCompleted()
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
        

        if (activeChunkCount >= farmingAreaChunksNumber)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool IsPlantingComplete()
    {
        plantsNumber = 0;
        foreach(Transform child in readyToPlantParent)
        {
            if(child.GetChild(0).TryGetComponent(out Transform transform))
            {
                if(transform.gameObject.activeSelf)
                {
                    plantsNumber++;
                }
            }
        }
        if (plantsNumber >= farmingAreaChunksNumber)
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
