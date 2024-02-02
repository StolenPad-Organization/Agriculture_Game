using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    private FarmingAreaSatus farmingAreaSatus;


    public int waterAmount = 0;


    private bool isWatering;



    private void Update()
    {
        
        if (farmingAreaSatus != null)
        {
            if (farmingAreaSatus.IsPlantingComplete())
            {
                isWatering = true;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (isWatering && collision.gameObject.CompareTag(Constants.DIRT))
        {
            Debug.Log("inside");
            if ( waterAmount != 0 && collision.transform.GetChild(0).gameObject.activeSelf && !collision.transform.GetChild(1).gameObject.activeSelf)
            {
                
                waterAmount--;
                Transform plantTransform = collision.transform.GetChild(1);
                plantTransform.gameObject.SetActive(true);
                Debug.Log(plantTransform.gameObject);
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

    public int GetWaterAmount()
    {
        return waterAmount;
    }
}
