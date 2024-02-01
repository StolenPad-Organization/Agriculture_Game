using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{

    private FarmingAreaSatus farmingAreaSatus;


    private const string DIRT = "Dirt";

    private bool isPlanting;



    private void Update()
    {
        if(farmingAreaSatus != null)
        {
            if(farmingAreaSatus.IsPreparedLandCompleted())
            {
                isPlanting = true;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("planting");
        if (isPlanting && collision.gameObject.CompareTag(DIRT))
        {
            Debug.Log("inside");
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null && meshRenderer.enabled)
            {
                Transform plantTransform = collision.transform.GetChild(0);
                plantTransform.gameObject.SetActive(true);
            }

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
