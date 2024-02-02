using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAndLoosing : MonoBehaviour
{
    private enum State
    {
        cleaning,
        loosing,

    }

    private State state;

    private FarmingAreaSatus farmingAreaSatus;
    private Transform collectablesHoldPoint;
    [SerializeField] private Transform readyToPlantPrefab;

    private List<Transform> collectabledChunksList = new List<Transform>();
    private int maxChunks = 260;
    private int chunkCount = 0;
    






    private void Start()
    {
        collectablesHoldPoint = Player.Instance.GetCollectablesHoldPoint();
        Selling.Instance.OnReset += Instance_OnReset;
        state = State.cleaning;
    }


    private void Update()
    {
        if (farmingAreaSatus != null)
        {
            if (farmingAreaSatus.IsCleaningCompleted())
            {
                state = State.loosing;
            }
            if(farmingAreaSatus.IsLoosingCompleted())
            {
                state = State.cleaning;
            }
        }
        //Debug.Log(state);

    }

    private void Instance_OnReset(object sender, System.EventArgs e)
    {
        chunkCount--;
        collectabledChunksList.RemoveAt(chunkCount);

    }



    private void OnCollisionEnter(Collision collision)
    {

        if (chunkCount < maxChunks && collision.gameObject.CompareTag(Constants.GRASS) && state == State.cleaning)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            collectabledChunksList.Add(collision.transform);
            StackCollectedChunks();
            chunkCount++;
        }
        if (state == State.loosing && collision.gameObject.CompareTag(Constants.DIRT) && !Player.Instance.PlayerHasCollectables())
        {

            if (collision.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = true;
            }
        }
        //if(state == State.plantPlants && collision.gameObject.CompareTag(DIRT))
        //{
        //    MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
        //    if(meshRenderer != null && meshRenderer.enabled)
        //    {
        //        Transform plantTransform = collision.transform.GetChild(0);
        //        plantTransform.gameObject.SetActive(true);
        //    }

        //}
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingAreaSatus farmingAreaSatus))
        {
            this.farmingAreaSatus = farmingAreaSatus;
        }
    }






}
