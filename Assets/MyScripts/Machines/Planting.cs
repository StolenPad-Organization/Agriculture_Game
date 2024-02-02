using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{

    private FarmingAreaSatus farmingAreaSatus;


    public int plantsNumber = 0;


    private bool isPlanting;



    private void Update()
    {
        if (farmingAreaSatus != null)
        {
            if (farmingAreaSatus.IsLoosingCompleted())
            {
                isPlanting = true;
            }
            
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (isPlanting && collision.gameObject.CompareTag(Constants.DIRT))
        {
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            

            if (meshRenderer != null && meshRenderer.enabled && plantsNumber != 0 && !collision.transform.GetChild(0).gameObject.activeSelf)
            {
                plantsNumber--;
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

    public int GetPlantsNumber()
    {
        return plantsNumber;
    }

}
