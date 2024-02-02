using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillUpZone : MonoBehaviour
{

    public event EventHandler<OnEquipToolEvetnArgs> OnFill;

    public class OnEquipToolEvetnArgs : EventArgs
    {
        public float progressNormalized;
    }

    public enum FilledObject
    {
        seeds,
        water
    }

    public  FilledObject filledObject;

    private int plantsNumber;
    private int waterAmount;
    private int plantFillupSpeed;
    private int waterFillUpSpeed;
    private int plantsNumberMax = 260;
    private int waterAmountMax = 260;




    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Planting planting) && filledObject == FilledObject.seeds)
        {
            plantsNumber = planting.GetPlantsNumber();
            if (plantsNumber < plantsNumberMax)
            {
                plantsNumber++;
                planting.plantsNumber = plantsNumber;
                OnFill?.Invoke(this, new OnEquipToolEvetnArgs
                {
                    progressNormalized = (float)plantsNumber / plantsNumberMax
                });
            }
        }
        if (other.gameObject.TryGetComponent(out Watering watering) && filledObject == FilledObject.water)
        {
            waterAmount = watering.GetWaterAmount();
            if (waterAmount < waterAmountMax)
            {
                waterAmount++;
                watering.waterAmount = waterAmount;
                OnFill?.Invoke(this, new OnEquipToolEvetnArgs
                {
                    progressNormalized = (float)waterAmount / waterAmountMax
                });
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        OnFill?.Invoke(this, new OnEquipToolEvetnArgs
        {
            progressNormalized = 0
        });
    }

    

}
