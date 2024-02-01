using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPoint : MonoBehaviour
{
    private enum State
    {
        collectGrass,
        PrepareLand,
        plantPlants,
        WaterPlants,
        CollectPlants,
    }

    private State state;

    [SerializeField] private FarmingAreaSatus farmingAreaSatus;
    [SerializeField] private Transform collectablesHoldPoint;
    [SerializeField] private Transform readyToPlantPrefab;
    [SerializeField] private Transform readyToPlantParent;

    private List<Transform> collectabledChunksList = new List<Transform>();
    private int maxChunks = 100;
    private int chunkCount = 0;
    private const string GRASS = "Grass";
    private const string DIRT = "Dirt";
    private bool meshActive;



    private void Start()
    {
        Selling.Instance.OnReset += Instance_OnReset;
        state = State.collectGrass;
    }


    private void Update()
    {
        if (farmingAreaSatus.IsGrassCompleted())
        {
            state = State.PrepareLand;
        }
        if(farmingAreaSatus.IsPreparedLandCompleted())
        {
            state = State.plantPlants;
        }
        Debug.Log(state);
        Debug.Log(farmingAreaSatus.IsPreparedLandCompleted());
        
    }

    private void Instance_OnReset(object sender, System.EventArgs e)
    {
        chunkCount--;
        collectabledChunksList.RemoveAt(chunkCount);
        
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        
        if (chunkCount < maxChunks && collision.gameObject.CompareTag(GRASS) && state == State.collectGrass)
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            collectabledChunksList.Add(collision.transform);
            StackCollectedChunks();
            chunkCount++;
        }
        if(state == State.PrepareLand && collision.gameObject.CompareTag(DIRT) && !PlayerHasItems())
        {
            
            if(collision.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = true;
            }
        }
        if(state == State.plantPlants && collision.gameObject.CompareTag(DIRT))
        {
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            if(meshRenderer != null && meshRenderer.enabled)
            {
                Transform plantTransform = collision.transform.GetChild(0);
                plantTransform.gameObject.SetActive(true);
            }
            
        }
    }
    private void StackCollectedChunks()
    {
        float yOffset = 0f;
        foreach (Transform chunk in collectabledChunksList)
        {
            chunk.transform.position = collectablesHoldPoint.position + Vector3.up * yOffset;
            yOffset += chunk.transform.localScale.y;
            chunk.transform.parent = collectablesHoldPoint;
        }

    }
    

    private bool PlayerHasItems()
    {
        return (collectablesHoldPoint.childCount != 0);
    }
}
