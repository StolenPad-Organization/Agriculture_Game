using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FarmingAreaSatus : MonoBehaviour
{
    [SerializeField] private int farmingAreaChunksNumber = 512;
    private int grassChunksNumber = 0;
    private int activeChunkCount = 0;
    private int seedsNumber = 0;
    private int plantsNumber = 0;
    [SerializeField] private Transform grassParent;
    [SerializeField] private Transform readyToPlantParent;




    private void Update()
    {
    }



    public bool IsCleaning()
    {
        grassChunksNumber = grassParent.childCount;
        if (grassChunksNumber <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
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
                if (meshRenderer.enabled)
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
        seedsNumber = 0;
        foreach (Transform child in readyToPlantParent)
        {
            if (child.GetChild(0).TryGetComponent(out Transform transform))
            {
                if (transform.gameObject.activeSelf)
                {
                    seedsNumber++;
                }
            }
        }
        if (seedsNumber >= farmingAreaChunksNumber)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool IsWateringComplete()
    {
        plantsNumber = 0;
        foreach (Transform child in readyToPlantParent)
        {
            if (child.childCount > 1)
            {
                if (child.GetChild(1).gameObject.activeSelf)
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


    private bool IsAnimationCompleted(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }

    public Transform GetFarmingArea()
    {
        return readyToPlantParent;
    }

}
