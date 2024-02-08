using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{


    public event EventHandler OnWaterEmptyIn;
    public event EventHandler OnWaterEmptyOut;
    private FarmingAreaSatus farmingAreaSatus;


    public int waterAmount = 0;


    private bool isWatering;
    private Vector3 plantEndPosition = new Vector3(0, 0.29f, 0);
    private float plantScale = 2f;



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
            if (waterAmount != 0 && collision.transform.GetChild(0).gameObject.activeSelf && !collision.transform.GetChild(1).gameObject.activeSelf)
            {
                waterAmount--;
                Transform plantTransform = collision.transform.GetChild(1);
                Transform seedTransform = collision.transform.GetChild(0);
                plantTransform.gameObject.SetActive(true);
                plantTransform.DOScale(Vector3.one * plantScale, 2.5f).SetEase(Ease.OutBounce);
                plantTransform.DOMove(plantTransform.position + plantEndPosition, 2f)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() =>
                    {
                        seedTransform.gameObject.SetActive(false);
                    });
            }
            if (waterAmount == 0)
            {
                OnWaterEmptyIn?.Invoke(this, new EventArgs());
            }
            Debug.Log("plant grow");


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingAreaSatus farmingAreaSatus))
        {
            this.farmingAreaSatus = farmingAreaSatus;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingAreaSatus farmingAreaSatus))
        {
            OnWaterEmptyOut?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetWaterAmount()
    {
        return waterAmount;
    }
}
